using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt
{
    internal abstract class MyTask
    {
        protected MyTask(int duration, int priority, int deadline, string name)
        {
            Duration = duration;
            Priority = priority;
            Deadline = deadline;
            Name = name;
        }

        public int Duration { get; set; }
        public int Priority { get; set; }
        public int Deadline { get; set; }
        public string Name { get; set; }
    }
}
