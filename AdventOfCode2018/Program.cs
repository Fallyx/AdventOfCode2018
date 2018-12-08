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

            swDay.Start();
            Day01.Tasks.Task1();
            Day01.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day01 elapsed time: {0}", swDay.Elapsed);

            swDay.Restart();
            string[] lines = Day02.Tasks.Task1();
            Day02.Tasks.Task2(lines);
            swDay.Stop();
            Console.WriteLine("Day02 elapsed time: {0}", swDay.Elapsed);

            swDay.Restart();
            Day03.Tasks.Task1();
            Day03.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day03 elapsed time: {0}", swDay.Elapsed);

            swDay.Restart();
            Day04.Tasks.Task1();
            Day04.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day04 elapsed time: {0}", swDay.Elapsed);

            swDay.Restart();
            Day05.Tasks.Task1();
            Day05.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day05 elapsed time: {0}", swDay.Elapsed);

            swDay.Restart();
            Day06.Tasks.Task1();
            Day06.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day06 elapsed time: {0}", swDay.Elapsed);

            swDay.Restart();
            Day07.Tasks.Task1();
            Day07.Tasks.Task2();
            swDay.Stop();
            Console.WriteLine("Day07 elapsed time: {0}", swDay.Elapsed);

            Day08.Tasks.Task1();

            swTot.Stop();
            Console.WriteLine("\nTotal elapsed time: {0}", swTot.Elapsed);
        }
    }
}
