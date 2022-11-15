using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    internal static class PDC
    {
        public static bool IsSchedulableByPDC(int t1, int t2, List<TimeTriggeredTask> tasks)
        {
            int CompTimeSum = 0;
            foreach (TimeTriggeredTask task in tasks){
                CompTimeSum += GetPDCompTime(t1, t2, task);
            }
            return false;
        }

        //To be run on all tasks to calculate their process demand between t1 and t2
        public static int GetPDCompTime(int t1, int t2, TimeTriggeredTask task)
        {
            // Sort out half complete periods
            double Start = t1 / task.Period;
            int RoundedStart = (int) Math.Ceiling(Start);
            double End = t2 / task.RelativeDeadline;
            int RoundedEnd = (int) Math.Floor(End);

            // multiplies all the Computation Time between t1 and t2
            int PDCompTime = ((t2 + task.Period - task.RelativeDeadline) / task.Period) * task.ComputationTime;
            // true if t1 and t2 contain no period
            if(PDCompTime < 0)
                return 0;

            return PDCompTime;
        }
    }
}
