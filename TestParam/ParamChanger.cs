using SysOpt;
using SysOpt.Helpers;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParam
{

    public class ParamChanger

    {
        List<TimeTriggeredTask> pollingServers = new List<TimeTriggeredTask>();
        //const int period = 2000;
        //const int deadline = period;
        //const int budget = 1500;

        //const int stdStartTemp = 5000;
        //const double stdCoolingRate = 0.96;

        //string test1Path = "test_cases\\inf_10_10_seperation\\test1.csv";


        public ParamChanger()
        {
            //(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = TaskReader.LoadTasks(test1Path);
            //TimeTriggeredTask pollingServer1 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer1");
            //TimeTriggeredTask pollingServer2 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer2");
            //TimeTriggeredTask pollingServer3 = new TimeTriggeredTask(period, budget, 7, deadline, "PollingServer3");
            //pollingServers.Add(pollingServer1);
            //pollingServers.Add(pollingServer2);
            //pollingServers.Add(pollingServer3);

        }

        public static void generateTests(SimulatedAnnealing SA, int testNumber, string path)
        {
            for (int i = 1; i <= testNumber; i++)
            {
                SimulatedAnnealing tempSA = SA.DeepCopy();
                (List<TimeTriggeredTask>, List<double>) SAResults = tempSA.Sim();
                Console.WriteLine(SAResults.Item1[0].ToString());
                Console.WriteLine(tempSA.getAverageWCRT());
                TaskReader.WriteSAOutput(SAResults.Item2, path + i + ".csv");
            }


        }

        public static void speedTest(SimulatedAnnealing sa)
        {
            long milliBefore = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            sa.Sim();
            long milliAfter = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            int speed = (int)(milliAfter - milliBefore);

            
            Console.WriteLine("Average WCRT: " + sa.getAverageWCRT());
            Console.WriteLine("Speed: " + speed + "ms");
        }
    }

}
