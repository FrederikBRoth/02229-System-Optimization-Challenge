using SysOpt.Helpers;
using SysOpt;
using System.Formats.Asn1;
using SysOpt.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit_Tests
{
    [TestClass]
    public class UnitTests
    {
        string TTTestPath = "TestingCSV\\TTTest.csv";
        string TTTest100 = "TestingCSV\\TTTest100Booked.csv";
        string TTTestOB = "TestingCSV\\TTTestOverBooked.csv";


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
            (List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(TTTestPath);
            (TTScheduleTable, List<(string, int)>) schedule;
            double idleTime;

            // Act
            schedule = EDFsimulation.getSchedule(tasks.ttList);
            EDFsimulation.PrintResult(schedule);
            idleTime =(double) schedule.Item1.schedule.FindAll(j => j == null).Count / (double) schedule.Item1.schedule.Count;

            // Assert
            Assert.IsTrue(tasks.ttList.Count != 0);
            Assert.IsTrue(tasks.etList.Count == 0);
            Assert.IsTrue(idleTime == 0.125);

            Assert.AreEqual(9, schedule.Item2.Find(x => x.Item1 == "tTT0").Item2);
            Assert.AreEqual(0, schedule.Item2.Find(x => x.Item1 == "tTT1").Item2);
            Assert.AreEqual(19, schedule.Item2.Find(x => x.Item1 == "tTT2").Item2);
        }

        [TestMethod]
        public void GetPDCompTime_Test()
        {
            // Arrange
            TimeTriggeredTask task = new TimeTriggeredTask(5, 2, 7, 5, "TTTestTask");
            int t1 = 0;
            int t2 = 15;

            // Assert
            Assert.AreEqual(6, PDC.GetPDCompTime(t1, t2, task));
        }

        [TestMethod]
        public void IsSchedulableByPDC_Test()
        {
            // Arrange
            (List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(TTTestPath);
            (List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks100 = TaskReader.LoadTasks(TTTest100);
            (List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasksOB = TaskReader.LoadTasks(TTTestOB);

            int taskslcm = AuxiliaryHelper.GetLCM(tasks.ttList.Select(t => t.Period).ToArray());
            int taskslcm100 = AuxiliaryHelper.GetLCM(tasks100.ttList.Select(t => t.Period).ToArray());
            int taskslcmOB = AuxiliaryHelper.GetLCM(tasksOB.ttList.Select(t => t.Period).ToArray());

            // Assert
            Assert.IsTrue(PDC.IsSchedulableByPDC(0, taskslcm, tasks.ttList));
            Assert.IsTrue(PDC.IsSchedulableByPDC(0, taskslcm100, tasks100.ttList));
            Assert.IsFalse(PDC.IsSchedulableByPDC(0, taskslcmOB, tasksOB.ttList));
        }
    }
}