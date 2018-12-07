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

        public static string Task1()
        {
            string[] prerequisite = new string[26];
            string buildingOrder = "";

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                Regex capitalLetterRgx = new Regex("[A-Z]");
                while ((line = reader.ReadLine()) != null)
                {
                    MatchCollection m = capitalLetterRgx.Matches(line);

                    if(m.Count == 3)
                    {
                        prerequisite[(m[2].Value[0] - 65)] += m[1].Value;
                    }
                }
            }

            List<string> prereq = prerequisite.ToList();

            while(prereq.FindIndex(s => string.IsNullOrEmpty(s)) != -1)
            {
                int idx = prereq.FindIndex(s => string.IsNullOrEmpty(s));

                string letter = ((char)(65 + idx)).ToString();

                buildingOrder += letter;
                prereq[idx] = "0";

                for(int i = 0; i < prereq.Count; i++)
                {
                    if (string.IsNullOrEmpty(prereq[i])) continue;
                    prereq[i] = prereq[i].Replace(letter, "");
                }
            }

            Console.WriteLine(buildingOrder);
            return buildingOrder;
        }

        public static void Task2(string buildingOrder)
        { 
            bool[] isDone = new bool[26];
            int workers = 5;
            int defaultStepTime = 60;


        }
    }
}
