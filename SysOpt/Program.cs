// See https://aka.ms/new-console-template for more information
using CsvHelper.Configuration;
using CsvHelper;
using SysOpt;
using System.Globalization;
using System.Xml.Linq;
using SysOpt.Helper;

string testCasePath = "test_cases\\inf_10_10\\taskset__1643188013-a_0.1-b_0.1-n_30-m_20-d_unif-p_2000-q_4000-g_1000-t_5__0__tsk.csv";

(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(testCasePath);

Console.WriteLine("Number of TT Tasks: " + tasks.ttList.Count);
Console.WriteLine("Number of ET Tasks: " + tasks.etList.Count);

    }
}

Console.WriteLine("Hello, World!");
Console.WriteLine(EDFsimulation.getLCM(new int[] { 2, 3, 5 }));


