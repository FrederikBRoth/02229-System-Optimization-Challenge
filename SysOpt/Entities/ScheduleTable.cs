using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Entities
{
    internal class TTScheduleTable
    {
        List<TimeTriggeredTask?> schedule;

        public TTScheduleTable()
        {
            schedule = new List<TimeTriggeredTask?> ();
        }

        public void AddNewTask(TimeTriggeredTask task)
        {
            schedule.Add(task);
        }

        public void AddIdle()
        {
            schedule.Add(null);
        }
    }

}
