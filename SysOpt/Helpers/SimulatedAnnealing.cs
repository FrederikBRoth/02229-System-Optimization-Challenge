using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    internal class SimulatedAnnealing
    {
        private const int ImprovementCountMax = 5;
        private const int StepCountMax = 100;
        List<TimeTriggeredTask> pollingServers;
        int startTemp;
        double coolingRate;
        (List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks;


        public SimulatedAnnealing(List<TimeTriggeredTask> pollingServers, int startTemp, double coolingRate, (List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks)
        {
            this.pollingServers = pollingServers;
            this.startTemp = startTemp;
            this.coolingRate = coolingRate;
            this.tasks = tasks;
        }


        public double Cost(List<TimeTriggeredTask> ps)
        {
            tasks.ttList.AddRange(ps);
            List<int> responseEDF = EDFsimulation.GetResponseTimeList(EDFsimulation.getSchedule(tasks.ttList));
            List<int> responseET = ETSchedulability.GetResponseTimeList(ETSchedulability.Schedulability(ps, tasks.etList));
            responseET.AddRange(responseEDF);
            tasks.ttList.RemoveAll(t => ps.Any(ps => ps.Name == t.Name));
            //Do sum instead of average
            return responseET.Average();
        }

        public List<TimeTriggeredTask> Neighbors()
        {
            TimeTriggeredTask temp = new TimeTriggeredTask(pollingServers[0].Period, pollingServers[0].ComputationTime, pollingServers[0].Priority, pollingServers[0].RelativeDeadline, pollingServers[0].Name);    
            TimeTriggeredTask ps = ChangeAllParameters(temp);

            List<TimeTriggeredTask> altPollingServers = new();
            foreach (TimeTriggeredTask t in pollingServers)
            {
                altPollingServers.Add(ps);
            }
            return altPollingServers;
        }

        /*  TODO
         * 
         *  Refactor lists to arrays
         *  Remove redundant Cost() function executions if a better neighbor model wasn't found
         *  Look into better Neighbor() function. Ideas:
         *      Generate 'n'-number of PollingServers, get the best one.
         *      Only change 1 parameter instead of all of them
         * 
         */

        public List<TimeTriggeredTask> Sim()
        {
            bool running = true;
            double temp = startTemp;
            int improvementCount = 0;
            int stepCount = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool updateCurrentCost = true;
            double currentCost = 0.0;
            while (running)
            {
                Debug.WriteLine("Before Neighbor " + AuxiliaryHelper.GetCurrentRuntime(stopwatch.Elapsed));
                List<TimeTriggeredTask> neighbors = new(Neighbors());
                Debug.WriteLine("After Neighbor " + AuxiliaryHelper.GetCurrentRuntime(stopwatch.Elapsed));
                double nbCost = Cost(neighbors);
                Debug.WriteLine("Neighbor Cost " + AuxiliaryHelper.GetCurrentRuntime(stopwatch.Elapsed));
                if(updateCurrentCost)
                {
                    currentCost = Cost(pollingServers);

                }
                Debug.WriteLine("Current Cost " + AuxiliaryHelper.GetCurrentRuntime(stopwatch.Elapsed));
                Debug.WriteLine("------------------------------------------------");

                double difference = nbCost - currentCost;
                if(difference < 0.0 || Anneal(difference, temp))
                {
                    updateCurrentCost = true;
                    pollingServers = new(neighbors);
                    if(difference < 0.0)
                    {
                        Console.WriteLine("Improvement" + currentCost);
                        improvementCount++;
                        if(improvementCount == ImprovementCountMax)
                        {
                            temp *= coolingRate;
                            improvementCount = 0;
                            stepCount = -1;
                        }
                    }
                } else
                {
                    updateCurrentCost = false;
                }
                
                stepCount++;
                if(stepCount > StepCountMax)
                {
                    running = false;
                }
                
            }
            return pollingServers;

        }

        public bool Anneal(double difference, double temperature)
        {
            Random random = new();
            return random.Next(0, 2) < Math.Exp(-difference / temperature);
        }

        //First of potential many attempts in finding a good neighbor function. Might be garbanzo.
        static public TimeTriggeredTask ChangeAllParameters(TimeTriggeredTask ps)
        {
            int randomChange = AuxiliaryHelper.RandomChange(10);
            ps.Period += randomChange;
            ps.RelativeDeadline += randomChange;
            ps.ComputationTime = ps.RelativeDeadline / 2;
            Console.WriteLine(ps.ToString());
            return WellformedPollingServer(ps);

        }

        //Making sure that the Polling server parameters isn't in the negative to prevent the algorithms working
        static public TimeTriggeredTask WellformedPollingServer(TimeTriggeredTask ps)
        {
            if (ps.RelativeDeadline > ps.Period)
            {
                ps.RelativeDeadline = ps.Period;
            }
            if (ps.RelativeDeadline < 0)
            {
                ps.RelativeDeadline = 10;
            }
            if (ps.Period < 0)
            {
                ps.Period = 10;
            }
            if (ps.ComputationTime < 0)
            {
                ps.ComputationTime = 0;
            }
            if(ps.ComputationTime > ps.Period)
            {
                ps.ComputationTime = ps.Period;
            }
            if(ps.RelativeDeadline == 0)
            {
                ps.RelativeDeadline = 10;
            }
            if (ps.Period == 0)
            {
                ps.Period = 10;
            }
            return ps;
        }

        public override string ToString()
        {
            string output = "";
            foreach (TimeTriggeredTask t in pollingServers)
            {
                output += t.ToString() + "\n";
            }
            return output;
        }


    }


}
