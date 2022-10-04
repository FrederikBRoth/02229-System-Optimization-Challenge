using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    internal class SchedulabilitySeperation
    {
        //Struct containing delta and alpha
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
        public static (bool, double) Schedulability(List<TimeTriggeredTask> pollingServer, List<EventTriggeredTask> tasks)
        {

            List<PollingServer> servers = new();

            //Computes Delta and Alpha and adds polling server to list

            foreach (TimeTriggeredTask ps in pollingServer) {
                servers.Add(new PollingServer(ps.Period + ps.Deadline - (2 * ps.ComputationTime), (double)ps.ComputationTime / (double)ps.Period));
            }

            int lcm = EDFsimulation.getLCM(tasks.Select(t => t.MinimalInterArrival).ToArray());
            int responseTime = 0;
            List<int> responseTimes = new();
            foreach(EventTriggeredTask task in tasks)
            {
                int t = 0;
                responseTime = task.Deadline + 1;

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
                        responseTimes.Add(responseTime);
                        break;
                    }
                    t++;
                }
                if (responseTime > task.Deadline)
                {
                    return (false, responseTime);
                }
            }

            return (true, responseTimes.Average());

        }
    }
}
