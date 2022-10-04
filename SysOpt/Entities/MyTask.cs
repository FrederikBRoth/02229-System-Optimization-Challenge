﻿using System;
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
            ReleaseTime = 0;
            WorstCaseReleaseTime = 0;
            ComputationTimeLeft = 0;
            AbsoluteDeadline = 0;
        }

        protected MyTask(MyTask task)
        {
            ComputationTime = task.ComputationTime;
            Priority = task.Priority;
            RelativeDeadline = task.RelativeDeadline;
            Name = task.Name;
            ReleaseTime = task.ReleaseTime;
            WorstCaseReleaseTime = task.WorstCaseReleaseTime;
            ComputationTimeLeft = task.ComputationTimeLeft;
            AbsoluteDeadline = task.AbsoluteDeadline;
        }

        // Ci
        public int ComputationTime { get; set; }
        // ci
        public int ComputationTimeLeft { get; set; }
        // p
        public int Priority { get; set; }
        // Di
        public int RelativeDeadline { get; set; }
        // di
        public int AbsoluteDeadline { get; set; }
        public string Name { get; set; }
        // ri
        public int ReleaseTime { get; set; }
        // WCRT
        public int WorstCaseReleaseTime { get; set; }
    }
}
