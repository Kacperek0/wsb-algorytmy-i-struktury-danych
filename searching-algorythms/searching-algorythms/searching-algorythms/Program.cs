using System;
using System.Diagnostics;
using System.Threading;

namespace searching_algorythms
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[10];
            int[] testArray = GenerateVshapeArray(array);

            foreach (var item in testArray)
            {
                Console.Write("{0} ", item);
            }
        }

        static int[] GenerateAscendingArray(int[] t)
        {
            for (int i = 0; i < t.Length; ++i) t[i] = i;
            return t;
        }

        static int[] GenerateDescendingArray(int[] t)
        {
            for (int i = 0; i < t.Length; ++i) t[i] = t.Length - i - 1;
            return t;
        }

        static int[] GenerateRandomArray(int[] t, Random rnd, int maxValue = int.MaxValue)
        {
            for (int i = 0; i < t.Length; ++i)
                t[i] = rnd.Next(maxValue);
            return t;
        }

        static int[] GenerateConstantArray(int[] t)
        {
            for (int i = 0; i < t.Length; i++) t[i] = 3;
            return t;
        }

        static int[] GenerateVshapeArray(int[] t)
        {
            int counterL = 1;
            int counterR = 1;
            int middle = (t.Length / 2) - 1;
            for (int i = 0; i < t.Length; ++i)
            {
                if (i == 0)
                {
                    t[middle] = i;
                }
                else if (i % 2 == 0)
                {
                    t[middle + counterR] = i;
                    counterR++;
                }
                else
                {
                    t[middle + counterL] = i;
                    counterL++;
                }
                
            }
            return t;
        }
    }
}
