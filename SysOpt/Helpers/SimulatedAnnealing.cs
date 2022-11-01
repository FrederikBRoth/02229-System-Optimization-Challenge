﻿using System;
using System.Collections;
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
            return responseET.Average();
        }

        public List<TimeTriggeredTask> Neighbors()
        {
            TimeTriggeredTask temp = new TimeTriggeredTask(pollingServers[0].Period, pollingServers[0].ComputationTime, pollingServers[0].Priority, pollingServers[0].RelativeDeadline, pollingServers[0].Name);
            TimeTriggeredTask ps = ChangeAllParameters(temp);
            List<TimeTriggeredTask> altPollingServers = new();
            foreach(TimeTriggeredTask t in pollingServers)
            {
                altPollingServers.Add(ps);
            }
            return altPollingServers;
        }


        public List<TimeTriggeredTask> Sim()
        {
            bool running = true;
            double temp = startTemp;
            int improvementCount = 0;
            int stepCount = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (running)
            {
                Debug.WriteLine("Before Neighbor " + AuxiliaryHelper.GetCurrentRuntime(stopwatch.Elapsed));
                List<TimeTriggeredTask> neighbors = new(Neighbors());
                Debug.WriteLine("After Neighbor " + AuxiliaryHelper.GetCurrentRuntime(stopwatch.Elapsed));
                double nbCost = Cost(neighbors);
                Debug.WriteLine("Neighbor Cost " + AuxiliaryHelper.GetCurrentRuntime(stopwatch.Elapsed));

                double currentCost = Cost(pollingServers);
                Debug.WriteLine("Current Cost " + AuxiliaryHelper.GetCurrentRuntime(stopwatch.Elapsed));
                Debug.WriteLine("------------------------------------------------");

                double difference = nbCost - currentCost;
                if(difference < 0.0 || Anneal(difference, temp))
                {
                    pollingServers = new(neighbors);
                    if(difference < 0.0)
                    {
                        improvementCount++;
                        if(improvementCount == ImprovementCountMax)
                        {
                            temp *= coolingRate;
                            improvementCount = 0;
                            stepCount = -1;
                        }
                    }

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
            ps.Period += AuxiliaryHelper.RandomChange(50);
            ps.ComputationTime += AuxiliaryHelper.RandomChange(50);
            ps.RelativeDeadline += AuxiliaryHelper.RandomChange(50);
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
                ps.RelativeDeadline = 0;
            }
            if (ps.Period < 0)
            {
                ps.Period = 0;
            }
            if (ps.ComputationTime < 0)
            {
                ps.ComputationTime = 0;
            }
            if(ps.ComputationTime > ps.Period)
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


    }


}
