using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt
{
    
    internal class EventTriggeredTask : MyTask
    {
        public EventTriggeredTask(int minimalInterArrival, int duration, int priority, int deadline, string name) : base(duration, priority, deadline, name)
        {
            MinimalInterArrival = minimalInterArrival;
            Seperation = 0;
        }
        public int MinimalInterArrival { get; set; }
        public int Seperation { get; set; }

    }
}
