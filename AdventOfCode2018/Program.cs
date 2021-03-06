﻿using System;
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

            /*
            #region day 1
            swDay.Start();
            Day01.Tasks.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 01 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 2
            swDay.Restart();
            string[] lines = Day02.Tasks.Task1();
            Day02.Tasks.Task2(lines);
            swDay.Stop();
            Console.WriteLine($"Day 02 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 3
            swDay.Restart();
            Day03.Tasks.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 03 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 4
            swDay.Restart();
            Day04.Tasks.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 04 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 5
            swDay.Restart();
            Day05.Tasks.Task1();
            Day05.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 05 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 6
            swDay.Restart();
            Day06.Tasks.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 06 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 7
            swDay.Restart();
            Day07.Tasks.Task1();
            Day07.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 07 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 8
            swDay.Restart();
            Day08.LicenseNode root = Day08.Tasks.Task1();
            Day08.Tasks.Task2(root);
            swDay.Stop();
            Console.WriteLine($"Day 08 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 9
            swDay.Restart();
            Day09.Tasks.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 09 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 10
            swDay.Restart();
            Day10.Tasks.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 10 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 11
            swDay.Restart();
            Day11.Tasks.Task1();
            Day11.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 11 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 12
            swDay.Restart();
            Day12.Tasks.Task1();
            Day12.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 12 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 13
            swDay.Restart();
            Day13.Tasks.Task1();
            Day13.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 13 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 14
            swDay.Restart();
            Day14.Tasks.Task1();
            Day14.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 14 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 15
            swDay.Restart();
            Day15.Tasks.Task1();
            //Day15.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 15 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 16
            swDay.Restart();
            Day16.Tasks.Task1();
            Day16.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 16 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 17
            swDay.Restart();
            //Day17.Tasks.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 17 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 18
            swDay.Restart();
            Day18.Tasks.Task1();
            Day18.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 18 elapsed time: {swDay.Elapsed}");
            #endregion      

            #region day 19
            swDay.Restart();
            Day19.Tasks.Task1();
            Day19.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 19 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 20
            swDay.Restart();
            Day20.Tasks.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 20 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 21
            swDay.Restart();
            Day21.Tasks.Task1();
            Day21.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 21 elapsed time: {swDay.Elapsed}");
            #endregion

            #region day 22
            swDay.Restart();
            Day22.Tasks.Task1();
            Day22.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 22 elapsed time: {swDay.Elapsed}");
            #endregion
            */

            #region day 23
            swDay.Restart();
            Day23.Tasks.Task1();
            //Day23.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine($"Day 23 elapsed time: {swDay.Elapsed}");
            #endregion

            swTot.Stop();
            Console.WriteLine($"\nTotal elapsed time: {swTot.Elapsed}");
        }
    }
}
