using SysOpt;
using SysOpt.Helpers;
using TestParam;


/* 
 * The goal of these tests are to try and finetune the input parameters
 * for the SA algorithm and determine which values yield a lower WCRT for
 * the individual tasks.
 */



/* ---- Standard Parameters ---- */
// Polling Servers
List<TimeTriggeredTask> pollingServers = new List<TimeTriggeredTask>();
int period = 2000;
int deadline = period;
int budget = 1500;

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

/* ----Control run ---- */

//Summary: Gives a bad WCRT within a reasonable amount of time. Average WCRT: 714-5459

//SimulatedAnnealing SAControl = new SimulatedAnnealing(pollingServers, stdStartTemp, stdCoolingRate, tasks);
//ParamChanger.generateTests(SAControl, 5, "\\Control\\Control");

/* ---- Cooling rate Test ---- */

// Summary: Super fast, decent WCRT with most being around 200-500, Average WCRT: 200-3838.

//double HighCoolingRate = 0.00001;
//SimulatedAnnealing SAHighCoolingRate = new SimulatedAnnealing(pollingServers, stdStartTemp, HighCoolingRate, tasks);
//ParamChanger.generateTests(SAHighCoolingRate, 5, "\\HighCooling\\HighCooling");

// Summary: Very slow, Average WCRT 180-300

//double LowCoolingRate = 0.99;
//SimulatedAnnealing SALowCoolingRate = new SimulatedAnnealing(pollingServers, stdStartTemp, LowCoolingRate, tasks);
//(List<TimeTriggeredTask>, List<double>) SALowCoolingResult = SALowCoolingRate.Sim();
//Console.WriteLine(SALowCoolingResult.Item1[0].ToString());
//Console.WriteLine(SALowCoolingRate.getAverageWCRT());

/* ---- Start Temp Test ---- */

// Summary: Decent time to complete with good average WCRT: 203-3838 most being 200-400

//int HighStartTemp = 20000;
//SimulatedAnnealing SAHighTemp = new SimulatedAnnealing(pollingServers, HighStartTemp, stdCoolingRate, tasks);
//ParamChanger.generateTests(SAHighTemp, 5, "\\HighStartTemp\\HighStartTemp");

// Summary: Decent time to complete, most WCRT 300-600, average WCRT: 328-4823

//int LowStartTemp = 2000;
//SimulatedAnnealing SALowTemp = new SimulatedAnnealing(pollingServers, LowStartTemp, stdCoolingRate, tasks);
//ParamChanger.generateTests(SALowTemp, 5, "\\LowStartTemp\\LowStartTemp");

/* ---- StepCountMax Test ---- */

// Summary: Decent time, most WCRT 200-600 average WCRT: 238-3838

//SimulatedAnnealing SAHighStep = new SimulatedAnnealing(pollingServers, stdStartTemp, stdCoolingRate, tasks);
//SAHighStep.StepCountMax = 200;
//(List<TimeTriggeredTask>, List<double>) SAHighStepResult = SAHighStep.Sim();
//Console.WriteLine(SAHighStepResult.Item1[0].ToString());
//Console.WriteLine(SAHighStep.getAverageWCRT());

// Summary: Decent time, SUPER consistent! most 355 average WCRT: 355, 357, 358, 368, 3838

//SimulatedAnnealing SALowStep = new SimulatedAnnealing(pollingServers, stdStartTemp, stdCoolingRate, tasks);
//SALowStep.StepCountMax = 50;
//(List<TimeTriggeredTask>, List<double>) SALowStepResult = SALowStep.Sim();
//Console.WriteLine(SALowStepResult.Item1[0].ToString());
//Console.WriteLine(SALowStep.getAverageWCRT());

/* ---- ImprovementCountMax Test ---- */

// Summary: Decent time, has very high WCRT 190-5612

//SimulatedAnnealing SAHighIC = new SimulatedAnnealing(pollingServers, stdStartTemp, stdCoolingRate, tasks);
//SAHighIC.ImprovementCountMax = 50;
//(List<TimeTriggeredTask>, List<double>) SAHighICResult = SAHighIC.Sim();
//Console.WriteLine(SAHighICResult.Item1[0].ToString());
//TaskReader.WriteSAOutput(SAHighICResult.Item2, "ImprovementCountHigh.csv");
//Console.WriteLine(SAHighIC.getAverageWCRT());

// Summary: Super fast, does not give a good average WCRT 190-5600 most being high

//SimulatedAnnealing SALowIC = new SimulatedAnnealing(pollingServers, stdStartTemp, stdCoolingRate, tasks);
//SALowIC.ImprovementCountMax = 2;
//(List<TimeTriggeredTask>, List<double>) SALowICResult = SALowIC.Sim();
//Console.WriteLine(SALowICResult.Item1[0].ToString());
//Console.WriteLine(SALowIC.getAverageWCRT());

/* ---- Scale Test ---- */

// Summary: Too high WCRT or wont complete

//SimulatedAnnealing SAHighScale = new SimulatedAnnealing(pollingServers, stdStartTemp, stdCoolingRate, tasks);
//SAHighScale.scale = 5;
//ParamChanger.generateTests(SAHighScale, 5, "\\HighScale\\HighScale");

// Summary: Too low scale, worse than control result

//SimulatedAnnealing SALowScale = new SimulatedAnnealing(pollingServers, stdStartTemp, stdCoolingRate, tasks);
//SALowScale.scale = 1;
//ParamChanger.generateTests(SALowScale, 5, "\\LowScale\\LowScale");

/* ---- High Start Temp, High Cooling ---- */

//int HighStartTemp = 20000;
//double HighCoolingRate = 0.80;
//SimulatedAnnealing SAHighTempHighCooling = new SimulatedAnnealing(pollingServers, HighStartTemp, HighCoolingRate, tasks);
//ParamChanger.generateTests(SAHighTempHighCooling, 5, "\\HighTempHighCooling\\HighTempHighCooling");

/* ---- Low Start Temp, High Cooling ---- */

//int LowStartTemp = 2000;
//double HighCoolingRate = 0.80;
//SimulatedAnnealing SALowTempHighCooling = new SimulatedAnnealing(pollingServers, LowStartTemp, HighCoolingRate, tasks);
//ParamChanger.generateTests(SALowTempHighCooling, 5, "\\LowTempHighCooling\\LowTempHighCooling");

/* ---- Low Start Temp, Low Cooling ---- */

//int LowStartTemp = 2000;
//double LowCoolingRate = 0.99;
//SimulatedAnnealing SALowTempLowCooling = new SimulatedAnnealing(pollingServers, LowStartTemp, LowCoolingRate, tasks);
//ParamChanger.generateTests(SALowTempLowCooling, 5, "\\LowTempLowCooling\\LowTempLowCooling");

/* ---- High Start Temp, Low Cooling ---- */

//int HighStartTemp = 20000;
//double LowCoolingRate = 0.99;
//SimulatedAnnealing SAHighTempLowCooling = new SimulatedAnnealing(pollingServers, HighStartTemp, LowCoolingRate, tasks);
//ParamChanger.generateTests(SAHighTempLowCooling, 5, "\\HighTempLowCooling\\HighTempLowCooling");
