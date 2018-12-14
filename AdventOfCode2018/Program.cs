using System;
using System.Diagnostics;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch swTot = new Stopwatch();
            Stopwatch swDay = new Stopwatch();

            swTot.Start();

            #region day 1
            swDay.Start();
            Day01.Tasks.Task1();
            Day01.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 01 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 2
            swDay.Restart();
            string[] lines = Day02.Tasks.Task1();
            Day02.Tasks.Task2(lines);
            swDay.Stop();
            Console.WriteLine("Day 02 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 3
            swDay.Restart();
            Day03.Tasks.Task1();
            Day03.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 03 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 4
            swDay.Restart();
            Day04.Tasks.Task1();
            Day04.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 04 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 5
            swDay.Restart();
            Day05.Tasks.Task1();
            Day05.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 05 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 6
            swDay.Restart();
            Day06.Tasks.Task1();
            Day06.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 06 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 7
            swDay.Restart();
            Day07.Tasks.Task1();
            Day07.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 07 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 8
            swDay.Restart();
            Day08.LicenseNode root = Day08.Tasks.Task1();
            Day08.Tasks.Task2(root);
            swDay.Stop();
            Console.WriteLine("Day 08 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 9
            swDay.Restart();
            Day09.Tasks.Task1();
            Day09.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 09 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 10
            swDay.Restart();
            Day10.Tasks.Task1();
            Day10.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 10 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 11
            swDay.Restart();
            Day11.Tasks.Task1();
            Day11.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 11 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 12
            swDay.Restart();
            Day12.Tasks.Task1();
            Day12.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 12 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 13
            swDay.Restart();
            Day13.Tasks.Task1();
            Day13.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 13 elapsed time: {0}", swDay.Elapsed);
            #endregion

            #region day 14
            swDay.Restart();
            Day14.Tasks.Task1();
            Day14.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day 14 elapsed time: {0}", swDay.Elapsed);
            #endregion

            swTot.Stop();
            Console.WriteLine("\nTotal elapsed time: {0}", swTot.Elapsed);
        }
    }
}
