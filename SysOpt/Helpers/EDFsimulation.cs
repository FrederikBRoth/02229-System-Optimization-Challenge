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
        public static (TTScheduleTable?, List<int>?) getSchedule(List<TimeTriggeredTask> tasks)
        {
            TTScheduleTable scheduleTable = new TTScheduleTable();
            int lcm = getLCM(tasks.Select(t => t.Period).ToArray());
            int t = 0;
            bool isIdle;
            while (t < lcm)
            {
                isIdle = true;
                foreach (var task in tasks)
                {
                    if (task.ComputationTimeLeft > 0)
                    {
                        // Setting the flag for setting idle slots
                        isIdle = false;

                        // Deadline missed :(
                        if (task.AbsoluteDeadline <= t)
                            return (null, null);
                    }

                    // The task has finished within the deadline
                    if (task.ComputationTimeLeft == 0 && task.AbsoluteDeadline >= t)
                        // Update WCRT if the observed time was worse (longer)
                        if (t - task.ReleaseTime >= task.WorstCaseReleaseTime)
                            task.WorstCaseReleaseTime = t - task.ReleaseTime;

                    // Schedule new job according to the period.
                    if (t % task.Period == 0)
                    {
                        task.ReleaseTime = t;
                        task.ComputationTimeLeft = task.ComputationTime;
                        task.AbsoluteDeadline = t + task.RelativeDeadline;
                    }

                }

                if (isIdle)
                    scheduleTable.AddIdle();
                else
                {
                    // Schedule the task with the earliest deadline
                    TimeTriggeredTask? earliestDeadlineTask = tasks.MinBy(t => t.AbsoluteDeadline);
                    if(earliestDeadlineTask != null)
                    {
                        earliestDeadlineTask.ComputationTimeLeft -= 1;
                        scheduleTable.AddNewTask(new TimeTriggeredTask(earliestDeadlineTask));
                    }
                }
                t += 1;
            }

            // Infeasable scheduling
            if(tasks.Any(task => task.ComputationTimeLeft > 0))
                return (null, null);

            // ????? what's the WCRT?
            return (scheduleTable, tasks.Select(t => t.WorstCaseReleaseTime).ToList());
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
