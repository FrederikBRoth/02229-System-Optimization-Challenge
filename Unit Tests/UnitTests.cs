using SysOpt.Helpers;
using SysOpt;
using System.Formats.Asn1;
using SysOpt.Entities;

namespace Unit_Tests
{
    [TestClass]
    public class UnitTests
    {

        /* Auxiliary Helper Methods */ 

        [TestMethod]
        public void GetLCM_Test()
        {
            // Arrange
            int[] input = { 1, 2, 3 };
            int expected = 6;

            // Assert
            Assert.AreEqual(expected, AuxiliaryHelper.GetLCM(input));
        }

        [TestMethod]
        public void Gcf_Test()
        {
            // Arrange
            int a = 5;
            int b = 10;
            int expected = 5;

            // Assert
            Assert.AreEqual(expected, AuxiliaryHelper.Gcf(a, b));
        }

        [TestMethod]
        public void Lcm_Test()
        {
            // Arrange
            int a = 1;
            int b = 2;
            int expected = 2;

            // Assert
            Assert.AreEqual(expected, AuxiliaryHelper.Lcm(a, b));
        }

        /* EDF Simulation */

        [TestMethod]
        public void EDFsimulation_Test()
        {
            // Arrange
            string testCasePath = "test_cases\\inf_10_10\\taskset__1643188013-a_0.1-b_0.1-n_30-m_20-d_unif-p_2000-q_4000-g_1000-t_5__0__tsk.csv";
            string TTTestPath = "TestingCSV\\TTTest.csv";
            (List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(TTTestPath);
            (TTScheduleTable, List<(string, int)>) schedule;
            double idleTime = 0.0;

            // Act
            schedule = EDFsimulation.getSchedule(tasks.ttList);
            EDFsimulation.PrintResult(schedule);
            idleTime =(double) schedule.Item1.schedule.FindAll(j => j == null).Count / (double) schedule.Item1.schedule.Count;
            Console.WriteLine(idleTime);

            // Assert
            Assert.IsTrue(tasks.ttList.Count != 0);
            Assert.IsTrue(tasks.etList.Count == 0);
            Assert.IsTrue(idleTime == 0.125);

            Assert.AreEqual(9, schedule.Item2.Find(x => x.Item1 == "tTT0").Item2);
            Assert.AreEqual(0, schedule.Item2.Find(x => x.Item1 == "tTT1").Item2);
            Assert.AreEqual(19, schedule.Item2.Find(x => x.Item1 == "tTT2").Item2);
        }
    }
}