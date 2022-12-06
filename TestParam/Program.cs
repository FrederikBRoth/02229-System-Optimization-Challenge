using SysOpt;
using SysOpt.Helpers;

/* 
 * The goal of these tests are to try and finetune the input parameters
 * for the SA algorithm and determine which values yield a lower WCRT for
 * the individual tasks.
 */



/* ---- Standard Parameters ---- */
// Polling Servers
List<TimeTriggeredTask> pollingServers = new List<TimeTriggeredTask>();
int period = 40;
int deadline = period;
int budget = 10;

TimeTriggeredTask pollingServer1 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer1");
TimeTriggeredTask pollingServer2 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer2");
TimeTriggeredTask pollingServer3 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer3");
pollingServers.Add(pollingServer1);
pollingServers.Add(pollingServer2);
pollingServers.Add(pollingServer3);
// Tasks

string path = "test_cases\\inf_10_10_seperation\\test1.csv";

(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(path);

// Standard values

int stdStartTemp = 5000;
double stdCoolingRate = 0.96;

// Control run

SimulatedAnnealing SAControl = new SimulatedAnnealing(pollingServers, stdStartTemp, stdCoolingRate, tasks);
(List<TimeTriggeredTask>, List<double>) SAControlResults = SAControl.Sim();
Console.WriteLine(SAControlResults.Item1[0].ToString());
/* ---- Cooling rate Test ---- */
double HighCoolingRate = 2;
//SimulatedAnnealing SAHighCoolingRate = new SimulatedAnnealing(pollingServers, stdStartTemp, stdCoolingRate, tasks);