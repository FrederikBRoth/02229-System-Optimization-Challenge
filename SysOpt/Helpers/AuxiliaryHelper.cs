using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    public class AuxiliaryHelper
    {
        static public string GetCurrentRuntime(TimeSpan ts)
        {
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            return elapsedTime;
        }
        
        static public int RandomChange(int scale)
        {
            Random random = new();
            return (random.Next(0, 2)*2-1)*random.Next(1, scale+1); 
        }
        static public int GetPenaltyValue()
        {
            return 1000000;
        }
        static public int GetLCM(int[] times)
        {
            int currentLCM = 1;

            foreach (int time in times)
                currentLCM = Lcm(currentLCM, time);

            return currentLCM;
        }

        static public int Gcf(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static public int Lcm(int a, int b)
        {
            return (a / Gcf(a, b)) * b;
        }

        public static List<int> GetRefinedList(int maxLCM)
        {
            List<int> list = new List<int>();
            for(int i = 2; i < maxLCM; i++)
            {
                int[] lcmList = { 2000, 3000, 4000, i };
                if (GetLCM(lcmList) <= maxLCM)
                    list.Add(i);
            }
            return list;
        }

        public static List<TimeTriggeredTask> GetRandomPollingServers()
        {
            Random random = new Random();
            List<int> periods = GetRefinedList(12000);
            int size = periods.Count;
            int period = periods[random.Next(0, size)];
            int budget = random.Next(1, period);
            Debug.WriteLine(period);

            return new List<TimeTriggeredTask>()
            {
                new TimeTriggeredTask(period, budget, 7, period, "PollingServer1"),
                new TimeTriggeredTask(period, budget, 7, period, "PollingServer1"),
                new TimeTriggeredTask(period, budget, 7, period, "PollingServer1")
            };
        }
    }
}
