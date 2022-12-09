using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    public static class PDC
    {
        public static bool IsSchedulableByPDC(int t1, int t2, List<TimeTriggeredTask> tasks)
        {
            int CompTimeSum = 0;
            foreach (TimeTriggeredTask task in tasks){
                CompTimeSum += GetPDCompTime(t1, t2, task);
            }
            if(CompTimeSum <= t2)
                return true;
            return false;
        }

        //To be run on all tasks to calculate their process demand between t1 and t2
        // It is expected that [0,L] is used for the time interval, L being LCM of tasks
        public static int GetPDCompTime(int t1, int t2, TimeTriggeredTask task)
        {
            // adds all the Computation Time between t1=0 and t2
            int PDCompTime = ((t2) / task.Period) * task.ComputationTime;

            if(PDCompTime < 0)
                return 0;

            return PDCompTime;
        }
    }
}
