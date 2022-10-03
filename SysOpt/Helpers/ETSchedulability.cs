using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    internal class ETSchedulability
    {
        public static (bool, int) Schedulability(TimeTriggeredTask pollingServer, List<EventTriggeredTask> tasks)
        {
            //Computes Delta and Alpha
            int delta = pollingServer.Period + pollingServer.Deadline - (2 * pollingServer.ComputationTime);
            double alpha = (double)pollingServer.ComputationTime / (double)pollingServer.Period;
            int lcm = EDFsimulation.getLCM(tasks.Select(t => t.MinimalInterArrival).ToArray());
            int responseTime = 0;
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
                        break;
                    }
                    t++;
                }
                if (responseTime > task.Deadline)
                {
                    return (false, responseTime);
                }
            }
            return (true, responseTime);

        }
    }
}
