using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Entities
{
    internal class TTScheduleTable
    {
        List<(int startTime, TimeTriggeredTask task)> schedule = new List<(int startTime, TimeTriggeredTask task)> ();

        public void addNewTask(int startTime, TimeTriggeredTask task)
        {
            schedule.Add((startTime, task));
        }
    }

}
