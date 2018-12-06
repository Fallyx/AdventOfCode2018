using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day02
{
    class Tasks
    {
        const string inputPath = @"Day02/Input.txt";

        public static String[] Task1()
        {
            int twoTimes = 0;
            int threeTimes = 0;
            string line;
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    
                    int[] letters = new int[26];
                    bool added = false;

                    for(int i = 0; i < line.Length; i++)
                    {
                        letters[line[i] - 97]++;
                    }

                    List<int> listLetters = letters.ToList();
                    if (listLetters.Exists(l => l == 2))
                    {
                        twoTimes++;
                        if (!added)
                        {
                            lines.Add(line);
                            added = true;
                        }
                    }
                    if (listLetters.Exists(l => l == 3))
                    {
                        threeTimes++;
                        if(!added)
                        {
                            lines.Add(line);
                            added = true;
                        }
                    }    
                }    
            }

            Console.WriteLine(twoTimes * threeTimes);

            return lines.ToArray();
        }

        public static void Task2(string[] lines)
        {
            int lowestCharDiff = byte.MaxValue;
            string[] sameLines = new string[2];

            for(int i = 0; i < lines.Length - 1; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    int lineDiff = 0;

                    lineDiff = lines[i].Zip<char, char, int>(lines[j], (a, b) => (a != b) ? lineDiff++ : lineDiff + 0).Max();

                    if (lineDiff < lowestCharDiff)
                    {
                        lowestCharDiff = lineDiff;
                        sameLines[0] = lines[i];
                        sameLines[1] = lines[j];
                    }
                }
            }

            string commonLetters = "";

            var tmp = sameLines[0].Zip<char, char, char>(sameLines[1], (a, b) => (a == b) ? a : ' ');

            commonLetters = new string(tmp.Where(c => !Char.IsWhiteSpace(c)).ToArray());

            Console.WriteLine(commonLetters);
        }
    }
}
