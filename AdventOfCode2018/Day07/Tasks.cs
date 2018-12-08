using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode2018.Day07
{
    class Tasks
    {
        const string inputPath = @"Day07/Input.txt";

        public static void Task1()
        {
            List<(string letter, string prereq)> stepReq = new List<(string, string)>();
            List<string> letters;
            string buildingOrder = "";

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    stepReq.Add((line.ElementAt(36).ToString(), line.ElementAt(5).ToString()));
                }
            }

            letters = stepReq.Select(l => l.letter).Concat(stepReq.Select(p => p.prereq)).Distinct().OrderBy(l => l).ToList();

            while(letters.Count != 0)
            {
                string step = letters.Where(l => !stepReq.Any(p => p.letter == l)).First();

                buildingOrder += step;
                letters.Remove(step);
                stepReq.RemoveAll(l => l.prereq == step);
            }

            Console.WriteLine(buildingOrder);
        }

        public static void Task2()
        {
            List<(string letter, string prereq)> stepReq = new List<(string, string)>();
            List<string> letters;
            List<string> workingOn = new List<string>();
            (string l, int t)[] workers = new (string, int)[5];
            string buildingOrder = "";
            int defaultWorktime = 60;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    stepReq.Add((line.ElementAt(36).ToString(), line.ElementAt(5).ToString()));
                }
            }

            letters = stepReq.Select(l => l.letter).Concat(stepReq.Select(p => p.prereq)).Distinct().OrderBy(l => l).ToList();

            int ticks = 0;

            while(letters.Count != 0)
            {
                for(int i = 0; i < workers.Length; i++)
                {
                    workers[i].t--;
                    if(workers[i].t <= 0 && !string.IsNullOrEmpty(workers[i].l))
                    {
                        buildingOrder += workers[i].l;
                        letters.Remove(workers[i].l);
                        stepReq.RemoveAll(l => l.prereq == workers[i].l);
                        workers[i].l = "";
                        workers[i].t = 0;
                    }
                    if(workers[i].t <= 0)
                    {
                        if (letters.Where(l => !stepReq.Any(p => p.letter == l) && !workingOn.Any(c => c == l)).Count() == 0) continue;
                        string step = letters.Where(l => !stepReq.Any(p => p.letter == l) && !workingOn.Any(c => c == l)).First();
                        workers[i].t = defaultWorktime + step[0] - 64;
                        workers[i].l = step;
                        workingOn.Add(step);
                    }
                }

                ticks++;
            }

            Console.WriteLine(ticks - 2);
        }
    }
}
