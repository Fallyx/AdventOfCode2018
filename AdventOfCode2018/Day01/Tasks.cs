using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018.Day01
{
    class Tasks
    {
        public static void Task1()
        {
            int frequency = 0;
            string line;

            StreamReader sr = new StreamReader(@"Day01/Input.txt");

            while ((line = sr.ReadLine()) != null)
            {
                if (line[0] == '+')
                {
                    frequency += int.Parse(line.Substring(1));
                }
                else
                {
                    frequency -= int.Parse(line.Substring(1));
                }
            }

            Console.WriteLine(frequency);
        }

        public static void Task2()
        {
            Dictionary<int, bool> freqs = new Dictionary<int, bool>();
            bool breakLoop = false;
            string line;
            int frequency = 0;

            while (!breakLoop)
            {
                using (StreamReader reader = new StreamReader(@"Day01/Input.txt"))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line[0] == '+')
                        {
                            frequency += int.Parse(line.Substring(1));
                        }
                        else
                        {
                            frequency -= int.Parse(line.Substring(1));
                        }

                        if (freqs.ContainsKey(frequency))
                        {
                            breakLoop = true;
                            break;
                        }
                        else
                        {
                            freqs.Add(frequency, true);
                        }
                    }
                }
            }

            Console.WriteLine(frequency);
        }
    }
}
