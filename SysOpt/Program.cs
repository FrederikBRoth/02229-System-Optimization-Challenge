// See https://aka.ms/new-console-template for more information
using CsvHelper.Configuration;
using CsvHelper;
using SysOpt;
using System.Globalization;
using System.Xml.Linq;
using SysOpt.Helpers;

string testCasePath = "test_cases\\inf_10_10\\taskset__1643188013-a_0.1-b_0.1-n_30-m_20-d_unif-p_2000-q_4000-g_1000-t_5__0__tsk.csv";

(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(testCasePath);

Console.WriteLine("Number of TT Tasks: " + tasks.ttList.Count);
Console.WriteLine("Number of ET Tasks: " + tasks.etList.Count);


//Establishes polling Server
TimeTriggeredTask pollingServer1 = new TimeTriggeredTask(200, 50, 0, 200, "PollingServer1");
TimeTriggeredTask pollingServer2 = new TimeTriggeredTask(200, 50, 0, 200, "PollingServer2");
TimeTriggeredTask pollingServer3 = new TimeTriggeredTask(200, 50, 0, 200, "PollingServer2");
List<TimeTriggeredTask> pollingServers = new();
pollingServers.Add(pollingServer1);
pollingServers.Add(pollingServer2);
pollingServers.Add(pollingServer3);


tasks.ttList.Add(pollingServer1);
//EDFsimulation.PrintResult(EDFsimulation.getSchedule(tasks.ttList));
//Console.WriteLine(EDFsimulation.getSchedule(tasks.ttList));
ETSchedulability.PrintETSchedulability(ETSchedulability.Schedulability(pollingServers, tasks.etList));

List<int> responseEDF = EDFsimulation.GetResponseTimeList(EDFsimulation.getSchedule(tasks.ttList));
List<int> responseET = ETSchedulability.GetResponseTimeList(ETSchedulability.Schedulability(pollingServers, tasks.etList));
Console.WriteLine(SimulatedAnnealing.Cost(responseET));
responseET.AddRange(responseEDF);
Console.WriteLine(SimulatedAnnealing.Cost(responseET));
Console.WriteLine(SimulatedAnnealing.Cost(responseEDF));








