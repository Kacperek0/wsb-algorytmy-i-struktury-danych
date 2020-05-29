using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace searching_algorythms
{
    class Program
    {
        static Random rnd = new Random(Guid.NewGuid().GetHashCode());

        static void Main(string[] args)
        {
            //SortingPart1();

            Thread TesterThread = new Thread(Program.Tester, 8 * 1024 * 1024);
            TesterThread.Start();
            TesterThread.Join();
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
            //time complexity O(n log n) - average case
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
            //time complexity O(n^2) - average case
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
        static void QuicksortRecursive(int[] t, int l, int p)
        {
            int i, j, x;
            i = l;
            j = p;
            x = t[(l + p) / 2];
            do
            {
                while (t[i] < x) i++;
                while (x < t[j]) j--;
                if (i <= j)
                {
                    int buf = t[i]; t[i] = t[j]; t[j] = buf;
                    i++; j--;
                }
            } while (i <= j);
            if (l < j) QuicksortRecursive(t, l, j);
            if (i < p) QuicksortRecursive(t, i, p);
        }
        static void QuicksortIterative(int[] t)
        {
            int i, j, l, p, sp;
            int[] stos_l = new int[t.Length],
                  stos_p = new int[t.Length];
            sp = 0; stos_l[sp] = 0; stos_p[sp] = t.Length - 1;
            do
            {
                l = stos_l[sp]; p = stos_p[sp]; sp--;
                do
                {
                    int x;
                    i = l; j = p; x = t[(l + p) / 2];
                    do
                    {
                        while (t[i] < x) i++;
                        while (x < t[j]) j--;
                        if (i <= j)
                        {
                            int buf = t[i]; t[i] = t[j]; t[j] = buf;
                            i++; j--;
                        }
                    } while (i <= j);
                    if (i < p) { sp++; stos_l[sp] = i; stos_p[sp] = p; }
                    p = j;
                } while (l < p);
            } while (sp >= 0);
        }
        static void QuicksortRecursiveLeft(int[] t, int l, int p)
        {
            int i, j, x;
            i = l;
            j = p;
            x = t[0];
            do
            {
                while (t[i] < x) i++;
                while (x < t[j]) j--;
                if (i <= j)
                {
                    int buf = t[i]; t[i] = t[j]; t[j] = buf;
                    i++; j--;
                }
            } while (i <= j);
            if (l < j) QuicksortRecursiveLeft(t, l, j);
            if (i < p) QuicksortRecursiveLeft(t, i, p);
        }
        static void QuicksortIterativeLeft(int[] t)
        {
            int i, j, l, p, sp;
            int[] stos_l = new int[t.Length],
                  stos_p = new int[t.Length];
            sp = 0; stos_l[sp] = 0; stos_p[sp] = t.Length - 1;
            do
            {
                l = stos_l[sp]; p = stos_p[sp]; sp--;
                do
                {
                    int x;
                    i = l; j = p; x = t[(l + p) / 2];
                    do
                    {
                        while (t[i] < x) i++;
                        while (x < t[j]) j--;
                        if (i <= j)
                        {
                            int buf = t[i]; t[i] = t[j]; t[j] = buf;
                            i++; j--;
                        }
                    } while (i <= j);
                    if (i < p) { sp++; stos_l[sp] = i; stos_p[sp] = p; }
                    p = j;
                } while (l < p);
            } while (sp >= 0);
        }
        static void QuicksortRecursiveRight(int[] t, int l, int p)
        {
            int i, j, x;
            i = l;
            j = p;
            x = t[^1];
            do
            {
                while (t[i] < x) i++;
                while (x < t[j]) j--;
                if (i <= j)
                {
                    int buf = t[i]; t[i] = t[j]; t[j] = buf;
                    i++; j--;
                }
            } while (i <= j);
            if (l < j) QuicksortRecursiveRight(t, l, j);
            if (i < p) QuicksortRecursiveRight(t, i, p);
        }
        static void QuicksortIterativeRight(int[] t)
        {
            int i, j, l, p, sp;
            int[] stos_l = new int[t.Length],
                  stos_p = new int[t.Length];
            sp = 0; stos_l[sp] = 0; stos_p[sp] = t.Length - 1;
            do
            {
                l = stos_l[sp]; p = stos_p[sp]; sp--;
                do
                {
                    int x;
                    i = l; j = p; x = t[^1];
                    do
                    {
                        while (t[i] < x) i++;
                        while (x < t[j]) j--;
                        if (i <= j)
                        {
                            int buf = t[i]; t[i] = t[j]; t[j] = buf;
                            i++; j--;
                        }
                    } while (i <= j);
                    if (i < p) { sp++; stos_l[sp] = i; stos_p[sp] = p; }
                    p = j;
                } while (l < p);
            } while (sp >= 0);
        }
        static void QuicksortRecursiveRnd(int[] t, int l, int p)
        {
            Random r = new Random();
            int i, j, x;
            i = l;
            j = p;
            x = t[r.Next(0, t.Length - 1)];
            do
            {
                while (t[i] < x) i++;
                while (x < t[j]) j--;
                if (i <= j)
                {
                    int buf = t[i]; t[i] = t[j]; t[j] = buf;
                    i++; j--;
                }
            } while (i <= j);
            if (l < j) QuicksortRecursiveRnd(t, l, j);
            if (i < p) QuicksortRecursive(t, i, p);
        }
        static void QuicksortIterativeRnd(int[] t)
        {
            Random r = new Random();

            int i, j, l, p, sp;
            int[] stos_l = new int[t.Length],
                  stos_p = new int[t.Length];
            sp = 0; stos_l[sp] = 0; stos_p[sp] = t.Length - 1;
            do
            {
                l = stos_l[sp]; p = stos_p[sp]; sp--;
                do
                {
                    int x;
                    i = l; j = p; x = t[r.Next(0, t.Length - 1)];
                    do
                    {
                        while (t[i] < x) i++;
                        while (x < t[j]) j--;
                        if (i <= j)
                        {
                            int buf = t[i]; t[i] = t[j]; t[j] = buf;
                            i++; j--;
                        }
                    } while (i <= j);
                    if (i < p) { sp++; stos_l[sp] = i; stos_p[sp] = p; }
                    p = j;
                } while (l < p);
            } while (sp >= 0);
        }
        static void Sorting(int[] t)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            InsertionSort(t);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            SelectionSort(t);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            HeapSort(t);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            CoctailSort(t);
            stopwatch.Stop();
            Console.Write("{0}\n", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
        }
        static void QuickSorting(int[] t)
        {
            Stopwatch stopwatch = new Stopwatch();

            int left = 0;
            int right = t.Length - 1;

            stopwatch.Start();
            QuicksortRecursive(t, left, right);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            QuicksortIterative(t);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            QuicksortRecursiveLeft(t, left, right);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            QuicksortIterativeLeft(t);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            QuicksortRecursiveRight(t, left, right);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            QuicksortIterativeRight(t);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            QuicksortRecursiveRnd(t, left, right);
            stopwatch.Stop();
            Console.Write("{0};", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            QuicksortIterativeRnd(t);
            stopwatch.Stop();
            Console.Write("{0}\n", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
        }
        static void SortingPart1()
        {
            for (int arraySize = 50000; arraySize <= 200000; arraySize += 50000)
            {

                Console.WriteLine("{0}\narray_type;insertion;selection;heap;coctail", arraySize);
                int[] startArray = new int[arraySize];
                Console.Write("ascending_array;");
                Sorting(GenerateAscendingArray(startArray));
                Console.Write("descending_array;");
                Sorting(GenerateDescendingArray(startArray));
                Console.Write("random_array;");
                Sorting(GenerateRandomArray(startArray, rnd));
                Console.Write("v_array;");
                Sorting(GenerateVArray(startArray));
            }

        }
        static void Tester()
        {
            for (int arraySize = 50000; arraySize <= 200000; arraySize += 50000)
            {
                Console.WriteLine("{0}\narray_type;recursive;iterative;rec_left;iter_left;rec_right;iter_right", arraySize);
                int[] startArray = new int[arraySize];
                Console.Write("ascending_array;");
                QuickSorting(GenerateAscendingArray(startArray));
                Console.Write("descending_array;");
                QuickSorting(GenerateDescendingArray(startArray));
                Console.Write("random_array;");
                QuickSorting(GenerateRandomArray(startArray, rnd));
                Console.Write("v_array;");
                QuickSorting(GenerateVArray(startArray));
            }
        }
    }
}
