using SysOpt.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt
{
    
    internal class EventTriggeredTask : MyTask
    {
        public EventTriggeredTask(int minimalInterArrival, int duration, int priority, int deadline, string name, int seperation) : base(duration, priority, deadline, name)
        {
            MinimalInterArrival = minimalInterArrival;
            Seperation = seperation;
        }
        public EventTriggeredTask(EventTriggeredTask task) : base(task)
        {
            MinimalInterArrival = task.MinimalInterArrival;
        }
        
        // Ti
        public int MinimalInterArrival { get; set; }
        public int Seperation { get; set; }

        public override string ToString()
        {
            return base.ToString() + " min arrival = " + MinimalInterArrival;
        }
    }
}
