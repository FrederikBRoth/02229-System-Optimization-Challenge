using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt
{
    internal abstract class MyTask
    {
        protected MyTask(int duration, int priority, int relativeDeadline, string name)
        {
            ComputationTime = duration;
            Priority = priority;
            RelativeDeadline = relativeDeadline;
            Name = name;
            WorstCaseResponseTime = 0;
        }

        protected MyTask(MyTask task)
        {
            ComputationTime = task.ComputationTime;
            Priority = task.Priority;
            RelativeDeadline = task.RelativeDeadline;
            Name = task.Name;
            WorstCaseResponseTime = task.WorstCaseResponseTime;
        }

        // Ci
        public int ComputationTime { get; set; }
        // p
        public int Priority { get; set; }
        // Di
        public int RelativeDeadline { get; set; }
        // WCRTi
        public int WorstCaseResponseTime { get; set; }

        public string Name { get; set; }
    }
}
