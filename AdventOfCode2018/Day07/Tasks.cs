using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.Day07
{
    class Tasks
    {
        const string inputPath = @"Day07/Input.txt";

        public static void Task1()
        {
            List<(string letter, string prereq)> stepReq = new List<(string, string)>();
            string buildingOrder = "";

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    stepReq.Add((line.ElementAt(36).ToString(), line.ElementAt(5).ToString()));
                }
            }

            List<string> steps = stepReq.Select(l => l.letter).Concat(stepReq.Select(p => p.prereq)).Distinct().OrderBy(l => l).ToList();

            while(steps.Count != 0)
            {
                string step = steps.Where(l => !stepReq.Any(p => p.letter == l)).First();

                buildingOrder += step;
                steps.Remove(step);
                stepReq.RemoveAll(l => l.prereq == step);
            }

            Console.WriteLine(buildingOrder);
        }

        public static void Task2()
        {
            List<string> letter = new List<string>();
        }
    }
}
