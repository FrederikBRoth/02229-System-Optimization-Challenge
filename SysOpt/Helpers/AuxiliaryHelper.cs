using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    public class AuxiliaryHelper
    {

        static public int RandomSeperation()
        {
            Random random = new();
            int randomInt = random.Next(0, 2);
            if(randomInt == 0)
            {
                Console.WriteLine("Test");
                return 0;
            }else
            {
                Console.WriteLine(randomInt);
                return random.Next(1, 4);
            }
              
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
    }
}
