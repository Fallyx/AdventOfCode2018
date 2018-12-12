using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode2018.Day12
{
    class Tasks
    {
        const string inputPath = @"Day12/Input.txt";

        public static void Task1()
        {
            SortedDictionary<int, string> potState = new SortedDictionary<int, string>();
            Dictionary<string, string> generation = new Dictionary<string, string>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("initial"))
                    {
                        string initState = line.Substring(15);
                        for(int i = 0; i < initState.Length; i++)
                        {
                            potState.Add(i, initState[i].ToString());
                        }
                    }
                    else if (string.IsNullOrEmpty(line)) continue;
                    else
                    {
                        generation.Add(line.Substring(0, 5), line.Substring(9));
                    }
                }
            }

            for (int i = 0; i < 20; i++)
            {
                int minPotIdx = potState.First(p => p.Value == "#").Key;
                int minIdx = potState.First().Key;
                int diff = Math.Abs(minIdx - minPotIdx);

                for(int j = 0; j + diff < 3; j++)
                {
                    potState.Add(minIdx - j - 1, ".");
                }

                int maxPotIdx = potState.Last(p => p.Value == "#").Key;
                int maxIdx = potState.Last().Key;
                diff = Math.Abs(maxIdx - maxPotIdx);

                for (int j = 0; j + diff < 3; j++)
                {
                    potState.Add(maxIdx + j + 1, ".");
                }

                var copy = new SortedDictionary<int, string>(potState);

                for (int x = 2; x < potState.Count - 2; x++)
                {
                    string pattern = copy.ElementAt(x - 2).Value + copy.ElementAt(x - 1).Value + copy.ElementAt(x).Value + copy.ElementAt(x + 1).Value + copy.ElementAt(x + 2).Value;

                    if(generation.ContainsKey(pattern))
                    {
                        potState[potState.ElementAt(x).Key] = generation[pattern];
                    }
                    else
                    {
                        potState[potState.ElementAt(x).Key] = ".";
                    }
                }
            }

            Console.WriteLine(potState.Sum(p => p.Value == "#" ? p.Key : 0));
        }

        public static void Task2()
        {
            SortedDictionary<int, string> potState = new SortedDictionary<int, string>();
            Dictionary<string, string> generation = new Dictionary<string, string>();
            var potStateCopy = new SortedDictionary<int, string>(potState);

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("initial"))
                    {
                        string initState = line.Substring(15);
                        for (int i = 0; i < initState.Length; i++)
                        {
                            potState.Add(i, initState[i].ToString());
                        }
                    }
                    else if (string.IsNullOrEmpty(line)) continue;
                    else
                    {
                        generation.Add(line.Substring(0, 5), line.Substring(9));
                    }
                }
            }

            (string pattern, int counter, bool patternFound, int sum, int gen) pat =  ("", 0, false, 0, 0);
            int sumDiff = 0;
            while(!pat.patternFound)
            {
                int minPotIdx = potState.First(p => p.Value == "#").Key;
                int minIdx = potState.First().Key;
                int diff = Math.Abs(minIdx - minPotIdx);

                for (int j = 0; j + diff < 3; j++)
                {
                    potState.Add(minIdx - j - 1, ".");
                }

                int maxPotIdx = potState.Last(p => p.Value == "#").Key;
                int maxIdx = potState.Last().Key;
                diff = Math.Abs(maxIdx - maxPotIdx);

                for (int j = 0; j + diff < 3; j++)
                {
                    potState.Add(maxIdx + j + 1, ".");
                }

                var copy = new SortedDictionary<int, string>(potState);

                for (int x = 2; x < potState.Count - 2; x++)
                {
                    string pattern = copy.ElementAt(x - 2).Value + copy.ElementAt(x - 1).Value + copy.ElementAt(x).Value + copy.ElementAt(x + 1).Value + copy.ElementAt(x + 2).Value;

                    if (generation.ContainsKey(pattern))
                    {
                        potState[potState.ElementAt(x).Key] = generation[pattern];
                    }
                    else
                    {
                        potState[potState.ElementAt(x).Key] = ".";
                    }
                }

                string state = "";
                for(int i = potState.First(p => p.Value == "#").Key; i <= potState.Last(p => p.Value == "#").Key; i++)
                {
                    state += potState[i];
                }
                
                if(state == pat.pattern)
                {
                    pat.counter++;
                    sumDiff = potState.Sum(p => p.Value == "#" ? p.Key : 0) - pat.sum;
                    pat.sum = potState.Sum(p => p.Value == "#" ? p.Key : 0);

                    if (pat.counter == 5)
                    {
                        pat.patternFound = true;
                    }
                }
                else
                {
                    pat.pattern = state;
                    pat.counter = 0;
                    sumDiff = 0;
                }

                pat.gen++;
            }

            Console.WriteLine((50000000000 - pat.gen) * sumDiff + pat.sum);
        }
    }
}
