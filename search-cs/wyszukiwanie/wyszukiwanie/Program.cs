using System;
using System.Diagnostics;

namespace wyszukiwanie
{
    class Program
    {
        static ulong OpComparisonEQ;
        static int[] TestVector;
        const int NIter = 10;


        static void Main(string[] args)
        {
            Console.WriteLine("Size\tLMaxI\tLMaxT\tBMaxI\tBMaxT");
            for (int ArraySize = 26843545; ArraySize <= 268435450; ArraySize += 26843545)
            {
                Console.Write(ArraySize);
                // tworzymy tablicę
                TestVector = new int[ArraySize];
                // wypełniamy tablicę
                for (int i = 0; i < TestVector.Length; ++i)
                    TestVector[i] = i;
                LinearMaxInstr(); // liniowe max instrumentacja
                LinearMaxTim(); // liniowe max czas
                BinaryMaxInstr(); // binarne max instrumentacja
                BinaryMaxTim(); // binarne max czas
                Console.WriteLine();
            }

            Console.WriteLine("\n" + "Size \tLAvgI \tLAvgT \tBAvgI \tBAvgT");
            for (int ArraySize = 50000; ArraySize <= 500000; ArraySize += 50000)
            {
                Console.Write(ArraySize);
                TestVector = new int[ArraySize];
                for (int i = 0; i < TestVector.Length; i++)
                {
                    TestVector[i] = i;
                }
                LinearAvgInstr();
                LinearAvgTim();
                BinaryAvgInstr();
                BinaryAvgTim();
                Console.WriteLine();
            }

            static bool IsPresent_LinearTim(int[] Vector, int Number)
            {
                for (int i = 0; i < Vector.Length; i++)
                    if (Vector[i] == Number)
                        return true;
                return false;
            }

            static bool IsPresent_LinearInstr(int[] Vector, int Number)
            {
                for (int i = 0; i < Vector.Length; i++)
                {
                    OpComparisonEQ++;
                    if (Vector[i] == Number) return true;
                }
                return false;
            }

            static bool IsPresent_BinaryTim(int[] Vector, int Number)
            {
                int Left = 0, Right = Vector.Length - 1, Middle;
                while (Left <= Right)
                {
                    Middle = (Left + Right) / 2;
                    if (Vector[Middle] == Number) return true;
                    else if (Vector[Middle] > Number) Right = Middle - 1;
                    else Left = Middle + 1;
                }
                return false;
            }

            static bool IsPresent_BinaryInstr(int[] Vector, int Number)
            {
                int Left = 0, Right = Vector.Length - 1, Middle;
                while (Left <= Right)
                {
                    Middle = (Left + Right) / 2;
                    OpComparisonEQ++;
                    if (Vector[Middle] == Number) return true;
                    else
                    {
                        OpComparisonEQ++;
                        if (Vector[Middle] > Number) Right = Middle - 1;
                        else Left = Middle + 1;
                    }
                }
                return false;
            }

            static void LinearMaxInstr()
            {
                OpComparisonEQ = 0;
                bool Present = IsPresent_LinearInstr(TestVector, TestVector.Length - 1);
                Console.Write("\t" + OpComparisonEQ);
            }

            static void LinearMaxTim()
            {
                double ElapsedSeconds;
                long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
                for (int n = 0; n < (NIter + 1 + 1); ++n)
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    bool Present = IsPresent_LinearTim(TestVector, TestVector.Length - 1);
                    long EndingTime = Stopwatch.GetTimestamp();
                    IterationElapsedTime = EndingTime - StartingTime;
                    ElapsedTime += IterationElapsedTime;
                    if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                    if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
                }
                ElapsedTime -= (MinTime + MaxTime);
                ElapsedSeconds = (ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency)))*1000;
                Console.Write("\t" + ElapsedSeconds.ToString("F4") + " ms");
            }

            static void BinaryMaxInstr()
            {
                OpComparisonEQ = 0;
                bool Present = IsPresent_BinaryInstr(TestVector, TestVector.Length);
                Console.Write("\t" + OpComparisonEQ);
            }

            static void BinaryMaxTim()
            {
                double ElapsedSeconds;
                long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
                for (int n = 0; n < (NIter + 1 + 1); ++n)
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    bool Present = IsPresent_BinaryTim(TestVector, TestVector.Length - 1);
                    long EndingTime = Stopwatch.GetTimestamp();
                    IterationElapsedTime = EndingTime - StartingTime;
                    ElapsedTime += IterationElapsedTime;
                    if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                    if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
                }
                ElapsedTime -= (MinTime + MaxTime);
                ElapsedSeconds = (ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency))) * 1000000;
                Console.Write("\t" + ElapsedSeconds.ToString("F4") + " μs");
            }

            static void LinearAvgInstr()
            {
                OpComparisonEQ = 0;
                bool Present;
                for (int i = 0; i < TestVector.Length; ++i)
                    Present = IsPresent_LinearInstr(TestVector, i);
                Console.Write("\t" + ((double)OpComparisonEQ / (double)TestVector.Length).ToString());
            }

            static void LinearAvgTim()
            {
                double ElapsedSeconds, AvgSeconds;
                long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
                for (int n = 0; n < (NIter + 1 + 1); ++n)
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    bool Present;
                    for (int i = 0; i < TestVector.Length; i++)
                    {
                        Present = IsPresent_LinearInstr(TestVector, i);
                    }
                    long EndingTime = Stopwatch.GetTimestamp();
                    IterationElapsedTime = EndingTime - StartingTime;
                    ElapsedTime += IterationElapsedTime;
                    if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                    if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
                }
                ElapsedTime -= (MinTime + MaxTime);
                ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
                AvgSeconds = (ElapsedSeconds / TestVector.Length) * 1000;
                Console.Write("\t" + AvgSeconds.ToString("F4") + " ms");
            }

            static void BinaryAvgInstr()
            {
                OpComparisonEQ = 0;
                bool Present;
                for (int i = 0; i < TestVector.Length; ++i)
                    Present = IsPresent_BinaryInstr(TestVector, i);
                Console.Write("\t" + OpComparisonEQ);
            }

            static void BinaryAvgTim()
            {
                double ElapsedSeconds, AvgSeconds;
                long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
                for (int n = 0; n < (NIter + 1 + 1); ++n)
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    bool Present;
                    for (int i = 0; i < TestVector.Length; ++i)
                        Present = IsPresent_BinaryInstr(TestVector, i);
                    long EndingTime = Stopwatch.GetTimestamp();
                    IterationElapsedTime = EndingTime - StartingTime;
                    ElapsedTime += IterationElapsedTime;
                    if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                    if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
                }
                ElapsedTime -= (MinTime + MaxTime);
                ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
                AvgSeconds = (ElapsedSeconds / TestVector.Length) * 1000000;
                Console.Write("\t" + AvgSeconds.ToString("F4") + " μs");
            }
        }
    }
}
