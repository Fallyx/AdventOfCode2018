using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018.Day01
{
    class Tasks
    {
        const string inputPath = @"Day01/Input.txt";

        public static void Task1()
        {
            int frequency = 0;
            string line;

            using (StreamReader sr = new StreamReader(inputPath))
            {
                while ((line = sr.ReadLine()) != null) frequency += int.Parse(line);
            }

            Console.WriteLine(frequency);
        }

        public static void Task2()
        {
            bool breakLoop = false;
            string line;
            int frequency = 0;

            HashSet<int> freqs = new HashSet<int>();

            while (!breakLoop)
            {
                using (StreamReader reader = new StreamReader(inputPath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        frequency += int.Parse(line);

                        if (freqs.Contains(frequency))
                        {
                            breakLoop = true;
                            break;
                        }
                        else freqs.Add(frequency);
                    }
                }
            }

            Console.WriteLine(frequency);
        }
    }
}
