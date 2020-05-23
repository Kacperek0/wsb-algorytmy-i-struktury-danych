using System;
using System.Collections.Generic;
using System.Threading;

namespace searching_algorythms
{
    class Program
    {
        static int[] startArray = new int[50000];
        static int[] arrayAsc = GenerateAscendingArray(startArray);
        static int[] arrayDesc = GenerateDescendingArray(startArray);
        //static int[] arrayRnd = GenerateRandomArray(startArray);
        static int[] arrayV = GenerateVArray(startArray);

        static void Main(string[] args)
        {
            Thread TesterThread = new Thread(Program.Tester, 8 * 1024 * 1024);
            TesterThread.Start();
            TesterThread.Join();
            Console.WriteLine("Tu drukujemy podsumowanie eksperymentu {0}", TesterThread);

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
        static int[] GenerateVArray(int[] t)
        {
            int middle = (t.Length / 2) - 1, right = middle + 1, left = middle;
            t[middle] = 0;
            for (int i = 0; i < t.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    t[right] = i;
                    right++;
                }
                else
                {
                    t[left] = i;
                    left--;
                }

            }
            return t;
        }
        static void InsertionSort(int[] t)
        {
            //time complexity O(n^2)
            for (uint i = 1; i < t.Length; i++)
            {
                uint j = i;
                int Buf = t[j];

                while ((j > 0) && (t[j - 1] > Buf))
                {
                    t[j] = t[j - 1];
                    j--;
                }
                t[j] = Buf;
            }
        }
        static void SelectionSort(int[] t)
        {
            //time complexity O(n^2)
            uint k;
            for (uint i = 0; i < (t.Length - 1); i++)
            {
                int Buf = t[i];
                k = i;
                for (uint j = i + 1; j < t.Length; j++)
                {
                    if (t[j] < Buf)
                    {
                        k = j;
                        Buf = t[j];
                    }
                }
                t[k] = t[i];
                t[i] = Buf;
            }
        }
        static void HeapSort(int[] t)
        {
            for (int i = 1; i < t.Length; i++)
            {
                int Buf = t[i];
                long Left = 0, Right = i - 1, Middle;
                while (Left <= Right)
                {
                    Middle = (Left + Right) / 2;
                    if (Buf < t[Middle]) Right = Middle - 1;
                    else Left = Middle + 1;
                }
                if (i - Left > 0)
                    Array.Copy(t, Left, t, Left + 1, i - Left);
                t[Left] = Buf;
            }
        }
        static void CoctailSort(int[] t)
        {
            int Left = 1, Right = t.Length - 1, k = t.Length - 1;
            do
            {
                for (int j = Right; j >= Left; j--)
                    if (t[j - 1] > t[j])
                    {
                        int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j;
                    }
                Left = k + 1;
                for (int j = Left; j <= Right; j++)
                    if (t[j - 1] > t[j])
                    {
                        int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j;
                    }
                Right = k - 1;
            } while (Left <= Right);
        }

        static void Tester()
        {
            InsertionSort(arrayAsc);
        }
        

    }
}
