using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    internal class AuxiliaryHelper
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
            return (random.Next(0, 2)*2-1)*scale; 
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

        static int Gcf(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static int Lcm(int a, int b)
        {
            return (a / Gcf(a, b)) * b;
        }

        static List<int> GetRefinedList(int maxLCM)
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
    }
}
