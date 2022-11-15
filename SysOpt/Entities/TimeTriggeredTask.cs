using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt
{
    public class TimeTriggeredTask : MyTask
    {
        public TimeTriggeredTask(int period, int duration, int priority, int deadline, string name ) : base(duration, priority, deadline, name)
        {
            Period = period;
        }

        public TimeTriggeredTask(TimeTriggeredTask task) : base(task)
        {
            Period = task.Period;
        }

        //Ti
        public int Period { get; set; }

        public override string ToString()
        {
            return base.ToString() + " period = " + Period;
        }
    }
}
