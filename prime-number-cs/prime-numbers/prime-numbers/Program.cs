using System;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;

namespace prime_numbers
{
    public static class Program
    {
        static void Main(string[] args)
        {
            BigInteger[] array = { 100913, 1009139, 10091401, 100914061, 1009140611, 10091406133, 100914061337, 1009140613399 };
            //BigInteger[] array = {101, 1009, 100913, 1009139, 10091401, 100914061, 1009140611 };
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("number;is_prime;time_in_ms;count");
            Console.WriteLine("\n-----Example algorithm-----");
            for (int i = 0; i < array.Length; i++)
            {
                stopwatch.Start();
                bool chk = IsPrime(array[i]);
                stopwatch.Stop();
                Console.WriteLine("{0};{1};{2};{3}", array[i], chk, stopwatch.ElapsedMilliseconds, counter);
                stopwatch.Reset();
            }
            Console.WriteLine("\n-----Optimized algorithm-----");
            for (int i = 0; i < array.Length; i++)
            {
                stopwatch.Start();
                bool chk = IsPrimeBetter(array[i]);
                stopwatch.Stop();
                Console.WriteLine("{0};{1};{2};{3}", array[i], chk, stopwatch.ElapsedMilliseconds, counter);
                stopwatch.Reset();
            }
            Console.WriteLine("\n-----Sieve Of Eratosthenes-----");
            for (int i = 0; i < array.Length; i++)
            {
                int x = Convert.ToInt32((long)array[array.Length - 1]);
                List<BigInteger> list = Generate(x);
                stopwatch.Start();
                bool chk = IsPrimeOptimal(array[i], list);
                stopwatch.Stop();
                Console.WriteLine("{0};{1};{2};{3}", array[i], chk, stopwatch.ElapsedMilliseconds, counter);
                stopwatch.Reset();
            }
            Console.WriteLine("\n-----Sieve Of Eratosthenes (optimal) -----");
            for (int i = 0; i < array.Length; i++)
            {
                int x = (Convert.ToInt32(Math.Sqrt((long)(array[array.Length - 1]))));
                List<BigInteger> list = Generate(x);
                stopwatch.Start();
                bool chk = IsPrimeOptimal(array[i], list);
                stopwatch.Stop();
                Console.WriteLine("{0};{1};{2};{3}", array[i], chk, stopwatch.ElapsedMilliseconds, counter);
                stopwatch.Reset();
            }


        }

        static ulong counter = 0;

        static BigInteger Sqrt(this BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }

        static bool IsPrime(BigInteger Num)
        {
            counter = 1;
            if (Num < 2)
            {
                return false;
            }
            else if (Num < 4)
            {
                return true;
            }
            else if (Num % 2 == 0)
            {
                return false;
            }
            else
            {
                for (BigInteger u = 3; u < Num / 2; u += 2)
                {
                    counter++;
                    if (Num % u == 0) return false;
                }
            }
            return true;
        }

        static bool IsPrimeBetter(BigInteger Num)
        {
            counter = 1;
            if (Num < 2)
            {
                return false;
            }
            else if (Num < 4)
            {
                return true;
            }
            else if (Num % 2 == 0)
            {
                return false;
            }
            else
            {
                for (BigInteger u = 3; u < Sqrt(Num); u += 2)
                {
                    counter++;
                    if (Num % u == 0) return false;
                }
           }
            return true;

        }

        static List<BigInteger> Generate(int Num)
        {
            List<bool> is_prime = new List<bool>();
            for (int i = 0; i <= Num + 1; i++)
            {
                is_prime.Add(true);
            }
            is_prime[0] = false; is_prime[1] = false;

            for (int i = 2; i <= Num; i++)
            {
                if (is_prime[i] == true)
                {
                    for (int j = i + i; j <= Num - 1; j = j + i)
                    {
                        is_prime[j] = false;
                    }
                }
            }
            is_prime[2] = false;
            List<BigInteger> list = new List<BigInteger>();
            for (int i = 0; i <= Num; i++)
            {
                if (is_prime[i] == true)
                {
                    list.Add(i);
                }
            }
            return list;
        }

        static bool IsPrimeOptimal(BigInteger Num, List<BigInteger> list)
        {
            if (Num < 2) return false;
            else if (Num < 4) return true;
            else if (Num % 2 == 0) return false;
            else
            {
                counter = 1;
                for (int k = 0; list[k] * list[k] <= Num; k++)
                {
                    counter++;
                    if (Num % list[k] == 0) return false;
                }
            }
            return true;
        }

    }
}
