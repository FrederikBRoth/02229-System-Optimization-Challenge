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
        private struct PollingServer
        {
            public PollingServer(int delta, double alpha)
            {
                Delta = delta;
                Alpha = alpha;
            }
            int Delta { get; set; }
            double Alpha { get; set; }
        }

        //public static (bool, double) Schedulability(List<TimeTriggeredTask> pollingServers, List<EventTriggeredTask> tasks)
        //{
        //    List<PollingServer> servers = new();

        //    //Computes Delta and Alpha and adds polling server to list

        //    foreach (TimeTriggeredTask ps in pollingServer)
        //    {
        //        servers.Add(new PollingServer(ps.Period + ps.Deadline - (2 * ps.ComputationTime), (double)ps.ComputationTime / (double)ps.Period));
        //    }

        //    for
        //}
        public static List<(EventTriggeredTask, int)> Schedulability(TimeTriggeredTask pollingServer, List<EventTriggeredTask> tasks)
        {
            //Computes Delta and Alpha
            int delta = pollingServer.Period + pollingServer.RelativeDeadline - (2 * pollingServer.ComputationTime);
            double alpha = (double)pollingServer.ComputationTime / (double)pollingServer.Period;
            int lcm = EDFsimulation.GetLCM(tasks.Select(t => t.MinimalInterArrival).ToArray());
            int responseTime = 0;
            List<(EventTriggeredTask, int)> responseTimes = new();
            foreach(EventTriggeredTask task in tasks)
            {
                int t = 0;
                responseTime = task.RelativeDeadline + 1;

                while(t <= lcm)
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

                    responseTimes.Add((task, responseTime));
                    return responseTimes;
                }
            }

            return responseTimes;

        }

        public static void PrintETSchedulability(List<(EventTriggeredTask, int)> responseTimes)
        {

            foreach((EventTriggeredTask, int) e in responseTimes)
            {
                Console.WriteLine(e.ToString() + " Response Times = " + e.Item2);
            }

        }
    }
}
