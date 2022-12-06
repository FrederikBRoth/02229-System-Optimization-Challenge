using SysOpt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    internal class ETSchedulability
    {
        public static List<(EventTriggeredTask, int)> Schedulability(TimeTriggeredTask pollingServer, List<EventTriggeredTask> tasks)
        {
            //Computes Delta and Alpha
            int delta = pollingServer.Period + pollingServer.RelativeDeadline - (2 * pollingServer.ComputationTime);
            double alpha = (double)pollingServer.ComputationTime / (double)pollingServer.Period;
            int lcm = AuxiliaryHelper.GetLCM(tasks.Select(t => t.MinimalInterArrival).ToArray());
            int responseTime = 0;
            List<(EventTriggeredTask, int)> responseTimes = new();
            foreach (EventTriggeredTask task in tasks)
            {
                int t = 0;
                responseTime = task.RelativeDeadline + 1;

                while (t <= lcm)
                {
                    double supply = alpha * (t - delta);
                    double demand = 0;
                    foreach (EventTriggeredTask task2 in tasks.Where(t => t.Priority >= task.Priority))
                    {
                        demand += Math.Ceiling((t / (double)task2.MinimalInterArrival)) * (double)task2.ComputationTime;
                    }
                    if (supply >= demand)
                    {
                        responseTime = t;
                        responseTimes.Add((task, responseTime));
                        break;
                    }
                    t++;
                }
                if (responseTime > task.RelativeDeadline)
                {

                    responseTimes.Add((task, responseTime + AuxiliaryHelper.GetPenaltyValue()));
                    return responseTimes;
                }
            }

            return responseTimes;

        }

        public static List<(EventTriggeredTask, int)> Schedulability(List<TimeTriggeredTask> pollingServers, List<EventTriggeredTask> tasks)
        {
            int maxSeperation = 0;
            //Index each ET to an indice to make sure we get the right lists down. ET with seperation 0 goes in all lists cause they can be in everything
            Dictionary<int, List<EventTriggeredTask>> etmap = new();
            List<(EventTriggeredTask, int)> responseTimes = new(); 
            foreach (EventTriggeredTask ettask in tasks) {
               if(ettask.Seperation != 0)
                {
                    if (maxSeperation < ettask.Seperation) maxSeperation = ettask.Seperation;
                    if (!etmap.ContainsKey(ettask.Seperation))
                    {
                        etmap.Add(ettask.Seperation, new List<EventTriggeredTask>());
                    }
                    etmap[ettask.Seperation].Add(ettask);
                }
            }
            foreach(EventTriggeredTask ettask in tasks) {
                if(ettask.Seperation == 0)
                {
                    for (int i = 1; i < etmap.Keys.Count; i++)
                    {
                        etmap[i].Add(ettask);
                    }
                }
            }

            if (etmap.Keys.Count == pollingServers.Count)
            {
                for (int i = 1; i <= maxSeperation; i++)
                {
                    responseTimes.AddRange(Schedulability(pollingServers[i - 1], etmap[i]));
                }
                return responseTimes;

            } else
            {
                return responseTimes;
            }
        }

        public static void PrintETSchedulability(List<(EventTriggeredTask, int)> responseTimes)
        {

            foreach((EventTriggeredTask, int) e in responseTimes)
            {
                Console.WriteLine(e.ToString() + " Response Times = " + e.Item2);
            }

        }

        public static List<int> GetResponseTimeList(List<(EventTriggeredTask, int)> responseTimes)
        {
            return responseTimes.Select(t => t.Item2).ToList();
        }
    }
}
