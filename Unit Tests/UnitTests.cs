using SysOpt.Helpers;
using SysOpt;
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
            List<TimeTriggeredTask> tasks;
        }
    }
}