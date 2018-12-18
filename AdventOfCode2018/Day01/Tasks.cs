using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018.Day01
{
    class Tasks
    {
        const string inputPath = @"Day01/Input.txt";

        public static void Task1and2()
        {
            bool breakLoop = false;
            int frequency = 0;

            List<int> freqsInFile = new List<int>();
            HashSet<int> freqs = new HashSet<int>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    freqsInFile.Add(int.Parse(line));
                    frequency += int.Parse(line);
                    freqs.Add(frequency);
                }

                Console.WriteLine(frequency);
            }

            while (!breakLoop)
            {
                for(int i = 0; i < freqsInFile.Count; i++)
                {
                    frequency += freqsInFile[i];

                    if(freqs.Contains(frequency))
                    {
                        Console.WriteLine(frequency);
                        return;
                    }

                    freqs.Add(frequency);
                }
            }
        }
    }
}
