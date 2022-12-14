using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    public class SimulatedAnnealing
    {
        public int ImprovementCountMax = 5;
        public int StepCountMax = 100;
        public int scale = 3;
        private double EDFWeight = 0.7;
        private double ETWeight = 0.3;
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

        public SimulatedAnnealing DeepCopy()
        {
            SimulatedAnnealing dc = new SimulatedAnnealing(this.pollingServers, this.startTemp, this.coolingRate, this.tasks);
            return dc;
        }

        public double Cost(List<TimeTriggeredTask> ps)
        {
            tasks.ttList.AddRange(ps);
            foreach(var tasks in tasks.ttList)
            {
                tasks.WorstCaseResponseTime = 0;
            }
            List<int> responseEDF = EDFsimulation.GetResponseTimeList(EDFsimulation.getSchedule(tasks.ttList));
            List<int> responseET = ETSchedulability.GetResponseTimeList(ETSchedulability.Schedulability(ps, tasks.etList));
            double result = (responseEDF.Sum() * EDFWeight) + (responseET.Sum() * ETWeight);
            tasks.ttList.RemoveAll(t => ps.Any(ps => ps.Name == t.Name));
            //Do sum instead of average
            return result;
        }

        public List<TimeTriggeredTask> Neighbors(List<int> periods)
        {
            TimeTriggeredTask temp = new TimeTriggeredTask(pollingServers[0].Period, pollingServers[0].ComputationTime, pollingServers[0].Priority, pollingServers[0].RelativeDeadline, pollingServers[0].Name);
            TimeTriggeredTask ps = ChangeAllParameters(temp, periods);

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
         * RESULT
         * 
         * Small period and duration isn't necesarrily a bad thing because its easier to fit into a Schedule using EDF. Also the important factor
         * for PollingServers is the ratio between Duration and Budget. The specific number is fairly irrelevant. 
         */

        public (List<TimeTriggeredTask>, List<double>) Sim()
        {
            List<int> periods = AuxiliaryHelper.GetRefinedList(12000);
            List<double> costsThroughSearch = new();
            bool running = true;
            double temp = startTemp;
            int stepCount = 0;
            bool updateCurrentCost = true;
            double currentCost = 0.0;
            while (running)
            {
                List<TimeTriggeredTask> neighbors = new(Neighbors(periods));
                double nbCost = Cost(neighbors);
                if (updateCurrentCost)
                {
                    currentCost = Cost(pollingServers);
                }

                double difference = nbCost - currentCost;
                if (difference < 0.0 || Anneal(difference, temp))
                {
                    updateCurrentCost = true;
                    pollingServers = new(neighbors);
                    if (difference < 0.0)
                    {
                        //Console.WriteLine("Improvement" + currentCost);
                        //improvementCount++;
                        //if(improvementCount == ImprovementCountMax)
                        //{
                        //improvementCount = 0;
                        stepCount = -1;
                        //}
                    }
                    temp *= coolingRate;

                }
                else
                {
                    updateCurrentCost = false;
                }
                //Console.Write(pollingServers[0].ToString());
                costsThroughSearch.Add(currentCost);

                stepCount++;
                if (stepCount > StepCountMax)
                {
                    running = false;
                }

            }
            return (pollingServers, costsThroughSearch);

        }

        public bool Anneal(double difference, double temperature)
        {
            Random random = new();
            return random.NextDouble() < Math.Exp(-difference / temperature);
        }

        //First of potential many attempts in finding a good neighbor function. Might be garbanzo.
        public TimeTriggeredTask ChangeAllParameters(TimeTriggeredTask ps, List<int> periods)
        {
            int randomIndex = periods.IndexOf(ps.Period) + AuxiliaryHelper.RandomChange(scale);
            while (randomIndex < 0 || periods.Count <= randomIndex)
            {
                randomIndex = periods.IndexOf(ps.Period) + AuxiliaryHelper.RandomChange(scale);
            }
            ps.Period = periods[randomIndex];
            ps.RelativeDeadline = periods[randomIndex];
            ps.ComputationTime = new Random().Next(1, ps.Period/2);
            return WellformedPollingServer(ps);

        }

        //Making sure that the Polling server parameters isn't in the negative to prevent the algorithms working
        static public TimeTriggeredTask WellformedPollingServer(TimeTriggeredTask ps)
        {
            if (ps.ComputationTime <= 0)
            {
                ps.ComputationTime = 1;
            }
            if (ps.ComputationTime > ps.Period)
            {
                ps.ComputationTime = ps.Period;
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

        public (int ttwcrt, int etwcrt ) getAverageWCRT()
        {
            int WCRTSum = 0;
            foreach (TimeTriggeredTask t in tasks.ttList)
            {
                WCRTSum += t.WorstCaseResponseTime;

                Console.WriteLine(t.Name + " " + t.WorstCaseResponseTime);
            }
                

            return (WCRTSum / tasks.ttList.Count, (int)ETSchedulability.GetResponseTimeList(ETSchedulability.Schedulability(pollingServers, tasks.etList)).Average());
        }


    }


}
