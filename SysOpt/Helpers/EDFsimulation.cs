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
        public static (TTScheduleTable, List<(string, int)>) getSchedule(List<TimeTriggeredTask> tasks)
        {
            //int lcm = AuxiliaryHelper.GetLCM(tasks.Select(t => t.Period).ToArray());
            //Console.WriteLine(lcm);
            int lcm = 12000;
            int tick = 0;
            List<Job> readyJobs = new List<Job>();
            Job? currentJob;
            TTScheduleTable scheduleTable = new TTScheduleTable();
            int tmpResponseTime;

            while (tick < lcm)
            {
                readyJobs.AddRange(GetReadyJobs(tasks, tick));
                currentJob = SelectEarliestDeadlineJob(readyJobs);

                // Update
                if (currentJob != null)
                {
                    scheduleTable.AddNewTask(currentJob);
                    currentJob.Execute();

                }
                else
                {
                    scheduleTable.AddIdle();
                    tick++;
                    continue;
                }

                // Check if job is finished
                if (currentJob.ComputationTimeLeft == 0)
                {
                    // Update WCRT
                    tmpResponseTime = tick - currentJob.ReleaseTime;

                    if (currentJob.Task.WorstCaseResponseTime < tmpResponseTime)
                        currentJob.Task.WorstCaseResponseTime = tmpResponseTime;

                    readyJobs.Remove(currentJob);

                    // Is there anything more to update?
                }

                tick++;
            }

            return (scheduleTable, tasks.Select(t => (t.Name, t.WorstCaseResponseTime)).ToList());

        }

        static public void PrintResult((TTScheduleTable, List<(string, int)>) input)
        {
            String ret = input.Item1.ToString();
            ret += "\n------------\n WCRTs:\n";
            foreach(var elem in input.Item2)
            {
                ret += elem.Item1 + " " + elem.Item2 + "\n ";
            }
            Console.WriteLine(ret);
        }

        static List<Job> GetReadyJobs(List<TimeTriggeredTask> tasks, int tick)
        {
            return tasks.Where(t => tick % t.Period == 0).Select(t => new Job(t, tick)).ToList();
        }

        static public List<int> GetResponseTimeList((TTScheduleTable, List<(string, int)>) input)
        {
            return input.Item2.Select(t => t.Item2).ToList();
        }

        static Job? SelectEarliestDeadlineJob(List<Job> jobs)
        {
            return jobs.MinBy(j => j.AbsoluteDeadline);
        }

    }
}
