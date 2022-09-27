using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt
{
    internal class Task
    {
        public int Duration { get; set; }
        public int Priority { get; set; }
        public int Deadline { get; set; }
        public string Name { get; set; }
    }
}
