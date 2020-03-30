using System;

namespace rekurencja_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(factorial(990));
        }

        static int Fibbonacci(int a)
        {
            if (a == 1 || a == 2)
            {
                return 1;
            }
            else
            {
                return Fibbonacci(a - 1) + Fibbonacci(a - 2);
            }
        }

        static long factorial(int n)
        {
            if (n == 1)
            {
                return 1;
            }
            else
            {
                return n * factorial(n - 1);
            }
        }
    }
}
