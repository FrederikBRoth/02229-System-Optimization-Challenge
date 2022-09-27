using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt
{
    internal class TimeTriggeredTask : MyTask
    {
        public TimeTriggeredTask(int period, int duration, int priority, int deadline, string name ) : base(duration, priority, deadline, name)
        {
            Period = period;
        }

        public int Period { get; set; }
    }
}
