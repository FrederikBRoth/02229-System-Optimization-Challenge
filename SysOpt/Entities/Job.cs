using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Entities
{
    public class Job
    {
        public Job(MyTask task, int tick)
        {
            ComputationTimeLeft = task.ComputationTime;
            AbsoluteDeadline = task.RelativeDeadline + tick;
            ReleaseTime = tick;
            Task = task;
        }

        public Job(Job job)
        {
            Task = job.Task;
            ComputationTimeLeft = job.ComputationTimeLeft;
            AbsoluteDeadline = job.AbsoluteDeadline;
            ReleaseTime = job.ReleaseTime;  
        }

        public void Execute()
        {
            ComputationTimeLeft--;
        }

        
        public MyTask Task { get; set; }
        public int ComputationTimeLeft { get; set; }
        public int AbsoluteDeadline { get; set; }
        public int ReleaseTime { get; set; }
    }
}
