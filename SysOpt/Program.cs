// See https://aka.ms/new-console-template for more information
using CsvHelper.Configuration;
using CsvHelper;
using SysOpt;
using System.Globalization;
using System.Xml.Linq;
using SysOpt.Helpers;

string testCasePath = "test_cases\\inf_10_10_seperation\\seperationCase.csv";

(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(testCasePath);

Console.WriteLine("Number of TT Tasks: " + tasks.ttList.Count);
Console.WriteLine("Number of ET Tasks: " + tasks.etList.Count);


//Establishes polling Server
TimeTriggeredTask pollingServer1 = new TimeTriggeredTask(2000, 500, 0, 1500, "PollingServer1");
TimeTriggeredTask pollingServer2 = new TimeTriggeredTask(2000, 500, 0, 1500, "PollingServer2");
TimeTriggeredTask pollingServer3 = new TimeTriggeredTask(2000, 500, 0, 1500, "PollingServer3");

List<TimeTriggeredTask> pollingServers = new();
pollingServers.Add(pollingServer1);
pollingServers.Add(pollingServer2);
pollingServers.Add(pollingServer3);



//EDFsimulation.PrintResult(EDFsimulation.getSchedule(tasks.ttList));
//Console.WriteLine(EDFsimulation.getSchedule(tasks.ttList));



SimulatedAnnealing sa = new SimulatedAnnealing(pollingServers, 5000, 0.99, tasks);

long milliBefore = DateTimeOffset.Now.ToUnixTimeMilliseconds();
Console.WriteLine(sa.Cost(pollingServers));
long milliAfter = DateTimeOffset.Now.ToUnixTimeMilliseconds();
Console.WriteLine(milliAfter - milliBefore);

//Console.WriteLine(sa.ToString());
//Console.WriteLine(sa.Cost(pollingServers));
//Console.WriteLine(sa.Neighbors()[0].ToString());
//Console.WriteLine(sa.Sim()[0].ToString());







