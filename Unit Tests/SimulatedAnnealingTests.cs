using SysOpt;
using SysOpt.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests
{
    [TestClass]
    public class SimulatedAnnealingTests
    {

        string testCasePath = "test_cases\\inf_10_10_seperation\\test1.csv";

        

        [TestMethod]
        public void TestTest()
        {
            Assert.IsTrue(true);
        }

        // Not needed, ChangeAllParams is the same
        [TestMethod]
        public void NeighborTest()
        {

        }

        [TestMethod]
        public void ChangeAllParamsTest()
        {
            (List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(testCasePath);
            List<int> periods = AuxiliaryHelper.GetRefinedList(12000);
            TimeTriggeredTask ps = new TimeTriggeredTask(100, 5, 7, 100, "tttest");
            int period = 40;
            int deadline = period;
            int budget = 10;


            //Establishes polling Server
            TimeTriggeredTask pollingServer1 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer1");
            TimeTriggeredTask pollingServer2 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer2");
            TimeTriggeredTask pollingServer3 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer3");



            List<TimeTriggeredTask> pollingServers = new()
            {
                pollingServer1,
                pollingServer2,
                pollingServer3
            };
            SimulatedAnnealing sa = new SimulatedAnnealing(pollingServers, 5000, 0.95, tasks);

            TimeTriggeredTask result =  sa.ChangeAllParameters(ps, periods);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void CostTest()
        {

        }
    }
}
