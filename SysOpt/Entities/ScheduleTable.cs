using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Entities
{
    internal class TTScheduleTable
    {
        List<Job?> schedule;

        public TTScheduleTable()
        {
            schedule = new List<Job?> ();
        }

        public void AddNewTask(Job jobs)
        {
            schedule.Add(jobs);
        }

        public void AddIdle()
        {
            schedule.Add(null);
        }

        public override string ToString()
        {
            String ret = "";
            String s;
            int i = 0;
            foreach (Job job in schedule)
            {
                if (job != null)
                    s = ". (name = " + job.Task.Name + ", releaseTime = " + job.ReleaseTime + ", deadline = " + job.AbsoluteDeadline + ")\n";
                else
                    s = ". Idle job \n";
                ret += (i++ + s);

                if (i > 8000)
                    break;
            }


            return ret;
        }
    }

}
