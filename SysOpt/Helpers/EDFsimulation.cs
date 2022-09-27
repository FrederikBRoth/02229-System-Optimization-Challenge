using SysOpt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    static internal class EDFsimulation
    {
        static public TTScheduleTable? getSchedule(List<TimeTriggeredTask> tasks)
        {
            TTScheduleTable scheduleTable = new TTScheduleTable();
            int lcm = getLCM(tasks.Select(t => t.Period).ToArray());
            int t = 0;

            while (t < lcm)
            {
                foreach (var task in tasks)
                {
                    if (task.ComputationTime > 0 && task.Deadline <= t)
                        return null;

                    if (task.ComputationTime == 0 && task.Deadline >= t)
                        if (t - task.ReleaseTime >= task.WorstCaseReleaseTime)
                            task.WorstCaseReleaseTime = t - task.ReleaseTime;
                    
                    if (t%task.Period == 0)
                    {
                        task.ReleaseTime = t;
                        //task.ComputationTime = 
                        task.Deadline += t;
                    }
                }

                foreach (var task in tasks.Where(task => task.ComputationTime == 0))
                {

                }
            }


        }

        static public int getLCM(int[] times)
        {
            int currentLCM = 1;

            foreach(int time in times)
                currentLCM = lcm(currentLCM, time);

            return currentLCM;
        }

        static int gcf(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static int lcm(int a, int b)
        {
            return (a / gcf(a, b)) * b;
        }
    }
}
