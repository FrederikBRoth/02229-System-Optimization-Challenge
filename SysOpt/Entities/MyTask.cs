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
            ComputationTime = duration;
            Priority = priority;
            Deadline = deadline;
            Name = name;
            ReleaseTime = 0;
            WorstCaseReleaseTime = 0;
        }

        public int ComputationTime { get; set; }
        public int Priority { get; set; }
        public int Deadline { get; set; }
        public string Name { get; set; }
        public int ReleaseTime { get; set; }
        public int WorstCaseReleaseTime { get; set; }
    }
}
