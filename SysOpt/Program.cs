﻿// See https://aka.ms/new-console-template for more information
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

EDFsimulation.PrintResult(EDFsimulation.getSchedule(tasks.ttList));

TimeTriggeredTask pollingServer = new TimeTriggeredTask(200, 50, 0, 200, "PollingServer1");
(bool schedulable, double averageResponseTime) result = ETSchedulability.Schedulability(pollingServer, tasks.etList);
Console.WriteLine(result.schedulable);
Console.WriteLine(result.averageResponseTime);


