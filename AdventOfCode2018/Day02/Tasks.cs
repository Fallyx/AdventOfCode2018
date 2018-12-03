using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018.Day02
{
    class Tasks
    {
        public static String[] Task1()
        {
            int twoTimes = 0;
            int threeTimes = 0;
            string line;
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(@"Day02/Input.txt"))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    bool hasTwo = false;
                    bool hasThree = false;
                    
                    Dictionary<char, int> letters = new Dictionary<char, int>();
                    for(int i = 0; i < line.Length; i++)
                    {
                        if(letters.ContainsKey(line[i]))
                        {
                            letters[line[i]] += 1;
                        }
                        else
                        {
                            letters.Add(line[i], 1);
                        }
                    }

                    foreach (var pair in letters)
                    {
                        if(pair.Value == 2 && !hasTwo)
                        {
                            twoTimes++;
                            hasTwo = true;
                        }
                        else if(pair.Value == 3 && !hasThree)
                        {
                            threeTimes++;
                            hasThree = true;
                        }
                    }

                    if(hasTwo || hasThree)
                    {
                        lines.Add(line);
                    }
                }    
            }

            Console.WriteLine(twoTimes * threeTimes);

            return lines.ToArray();
        }

        public static void Task2(string[] lines)
        {
            byte lowestCharDiff = byte.MaxValue;
            string[] sameLines = new string[2];

            for(int i = 0; i < lines.Length - 1; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    byte lineDiff = 0;

                    for (int x = 0; x < lines[i].Length; x++)
                    {
                        if (lines[i][x] != lines[j][x])
                        {
                            lineDiff++;
                        }
                    }

                    if (lineDiff < lowestCharDiff)
                    {
                        lowestCharDiff = lineDiff;
                        sameLines[0] = lines[i];
                        sameLines[1] = lines[j];
                    }
                }
            }

            string commonLetters = "";

            for(int i = 0; i < sameLines[0].Length; i++)
            {
                if(sameLines[0][i] == sameLines[1][i])
                {
                    commonLetters += sameLines[0][i];
                }
            }

            Console.WriteLine(commonLetters);
        }
    }
}
