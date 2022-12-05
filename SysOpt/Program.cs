// See https://aka.ms/new-console-template for more information
using CsvHelper.Configuration;
using CsvHelper;
using SysOpt;
using System.Globalization;
using System.Xml.Linq;
using SysOpt.Helpers;

string tc1Path = "test_cases\\inf_10_10_seperation\\TC1.csv";
string tc2Path = "test_cases\\inf_10_10_seperation\\taskset__1643188013-a_0.1-b_0.1-n_30-m_20-d_unif-p_2000-q_4000-g_1000-t_5__0__tsk.csv";
string tc3Path = "test_cases\\inf_10_10_seperation\\taskset__1643188175-a_0.2-b_0.3-n_30-m_20-d_unif-p_2000-q_4000-g_1000-t_5__0__tsk.csv";
string tc4Path = "test_cases\\inf_10_10_seperation\\taskset__1643188539-a_0.6-b_0.1-n_30-m_20-d_unif-p_2000-q_4000-g_1000-t_5__0__tsk.csv";


(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tc1 = TaskReader.LoadTasks(tc1Path);
(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tc2 = TaskReader.LoadTasks(tc2Path);
(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tc3 = TaskReader.LoadTasks(tc3Path);
(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tc4 = TaskReader.LoadTasks(tc4Path);


(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = tc1;


List<TimeTriggeredTask> pollingServers = AuxiliaryHelper.GetRandomPollingServers();
List<TimeTriggeredTask> withPS = new();
withPS.AddRange(pollingServers);
withPS.AddRange(tasks.ttList);


//EDFsimulation.PrintResult(EDFsimulation.getSchedule(tasks.ttList));
EDFsimulation.PrintResult(EDFsimulation.getSchedule(withPS));

//Console.WriteLine(EDFsimulation.getSchedule(withPS));

ETSchedulability.PrintETSchedulability(ETSchedulability.Schedulability(pollingServers, tasks.etList));
//List<int> test = AuxiliaryHelper.GetRefinedList(12000);



//foreach (int i in test)
//{
//    Console.WriteLine(i);
//}


SimulatedAnnealing sa = new SimulatedAnnealing(pollingServers, 5000, 0.95, tasks);

long milliBefore = DateTimeOffset.Now.ToUnixTimeMilliseconds();
//long milliAfter = DateTimeOffset.Now.ToUnixTimeMilliseconds();
//Console.WriteLine(milliAfter - milliBefore);

////Console.WriteLine(sa.ToString());
////Console.WriteLine(sa.Cost(pollingServers));
////Console.WriteLine(sa.Neighbors()[0].ToString());
Console.WriteLine(sa.Sim()[0].ToString());







