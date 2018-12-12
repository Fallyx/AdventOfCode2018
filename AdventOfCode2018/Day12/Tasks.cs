using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

                if(diff == 2)
                {
                    potState.Add(minIdx - 1, ".");
                }
                else if(diff == 1)
                {
                    potState.Add(minIdx - 1, ".");
                    potState.Add(minIdx - 2, ".");
                }
                else if(diff == 0)
                {
                    potState.Add(minIdx - 1, ".");
                    potState.Add(minIdx - 2, ".");
                    potState.Add(minIdx - 3, ".");
                }

                int maxPotIdx = potState.Last(p => p.Value == "#").Key;
                int maxIdx = potState.Last().Key;
                diff = Math.Abs(maxIdx - maxPotIdx);

                if(diff == 2)
                {
                    potState.Add(maxIdx + 1, ".");
                }
                else if (diff == 1)
                {
                    potState.Add(maxIdx + 1, ".");
                    potState.Add(maxIdx + 2, ".");
                }
                else if (diff == 0)
                {
                    potState.Add(maxIdx + 1, ".");
                    potState.Add(maxIdx + 2, ".");
                    potState.Add(maxIdx + 3, ".");
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

        public static void Part1()
        {
            string Input = "";

            using (StreamReader reader = new StreamReader(inputPath))
            {
                Input = reader.ReadToEnd();
            }

            var lines = Input.Split('\n');
            var state = new StringBuilder(lines[0].Substring(15).Trim());
            var instructions = lines.Skip(2).Select(x =>
            {
                var instruction = x.Split('=');
                return (from: instruction[0], to: instruction[1][2]);
            }).ToDictionary(x => x.from, x => x.to);
            var minValue = 0;
            for (long i = 0; i < 20; i++)
            {
                var minPotted = state.ToString().IndexOf('#');
                for (int j = 0; j < 4 - minPotted; j++)
                {
                    state.Insert(0, '.');
                    minValue--;
                }

                var s = state.ToString();
                var maxPotted = state.ToString().LastIndexOf('#');
                for (int j = 0; j < 5 - (s.Length - maxPotted); j++)
                {
                    state.Append('.');
                }

                var currentState = state.ToString();
                for (int j = 0; j < currentState.Length - 5; j++)
                {
                    var sub = currentState.Substring(j, 5);
                    if (instructions.ContainsKey(sub))
                    {
                        state[j + 2] = instructions[sub];
                    }
                }
            }

            var finalState = state.ToString();
            var sum = 0;
            for (int z = 0; z < finalState.Length; z++)
            {
                if (finalState[z] == '#')
                {
                    sum += z + minValue;
                }
            }

            Console.WriteLine(sum);
        }
    }
}
