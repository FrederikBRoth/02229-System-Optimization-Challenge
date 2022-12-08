// See https://aka.ms/new-console-template for more information
using CsvHelper.Configuration;
using CsvHelper;
using SysOpt;
using System.Globalization;
using System.Xml.Linq;
using SysOpt.Helpers;

string testCasePath = "test_cases\\inf_10_10_seperation\\test1.csv";

(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(testCasePath);

Console.WriteLine("Number of TT Tasks: " + tasks.ttList.Count);
Console.WriteLine("Number of ET Tasks: " + tasks.etList.Count);

int period = 40;
int deadline = period;
int budget = 10;


//Establishes polling Server
TimeTriggeredTask pollingServer1 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer1");
TimeTriggeredTask pollingServer2 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer2");
TimeTriggeredTask pollingServer3 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer3");



List<TimeTriggeredTask> pollingServers = new();
pollingServers.Add(pollingServer1);
pollingServers.Add(pollingServer2);
pollingServers.Add(pollingServer3);

List<TimeTriggeredTask> withPS = new();
withPS.AddRange(pollingServers);
withPS.AddRange(tasks.ttList);


//EDFsimulation.PrintResult(EDFsimulation.getSchedule(tasks.ttList));

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

(List<TimeTriggeredTask>, List<double>) SAResults = sa.Sim();
Console.WriteLine(SAResults.Item1[0].ToString());
//TaskReader.WriteSAOutput(SAResults.Item2);








