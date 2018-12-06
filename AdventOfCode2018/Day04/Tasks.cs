using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2018.Day04
{
    class Tasks
    {
        const string inputPath = @"Day04/Input.txt";

        public static void Task1()
        {
            List<string> gSleep = new List<string>();
            Dictionary<int, int[]> guardSleep = new Dictionary<int, int[]>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    gSleep.Add(line);
                }
            }

            gSleep.Sort();

            int id = 0;
            DateTime startSleep = new DateTime();
            Regex guardIdRgx = new Regex(@"#\d+");

            foreach(string s in gSleep)
            {
                if(s.Contains("Guard"))
                {
                    Match m = guardIdRgx.Match(s);

                    if(m.Success)
                    {
                        id = int.Parse(m.Value.Substring(1));
                        if (!guardSleep.ContainsKey(id))
                        {
                            int[] mins = new int[60];
                            guardSleep.Add(id, mins);
                        }
                    }
                }
                else if(s.Contains("falls"))
                {
                    startSleep = DateTime.Parse(s.Substring(1, 16));
                }
                else if(s.Contains("wakes"))
                {
                    int[] tmp = guardSleep[id];

                    for (int i = startSleep.Minute; i <= DateTime.Parse(s.Substring(1, 16)).Minute; i++) tmp[i]++;
                    guardSleep[id] = tmp;
                }
            }

            int sleepiestGuardId = guardSleep.Aggregate((l, r) => l.Value.Sum() > r.Value.Sum() ? l : r).Key;
            int min = guardSleep[sleepiestGuardId].ToList().IndexOf(guardSleep[sleepiestGuardId].Max());
            Console.WriteLine(sleepiestGuardId * min);
        }

        public static void Task2()
        {
            List<string> gSleep = new List<string>();
            Dictionary<int, int[]> guardSleep = new Dictionary<int, int[]>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    gSleep.Add(line);
                }
            }

            gSleep.Sort();

            int id = 0;
            DateTime startSleep = new DateTime();
            Regex guardIdRgx = new Regex(@"#\d+");

            foreach(string s in gSleep)
            {
                if(s.Contains("Guard"))
                {
                    Match m = guardIdRgx.Match(s);

                    if(m.Success)
                    {
                        id = int.Parse(m.Value.Substring(1));
                        if (!guardSleep.ContainsKey(id))
                        {
                            int[] mins = new int[60];
                            guardSleep.Add(id, mins);
                        }
                    }
                }
                else if (s.Contains("falls"))
                {
                    startSleep = DateTime.Parse(s.Substring(1, 16));
                }
                else if (s.Contains("wakes"))
                {
                    int[] tmp = guardSleep[id];

                    for (int i = startSleep.Minute; i <= DateTime.Parse(s.Substring(1, 16)).Minute; i++) tmp[i]++;
                    guardSleep[id] = tmp;
                }
            }

            int guardId = guardSleep.Aggregate((l, r) => l.Value.Max() > r.Value.Max() ? l : r).Key;
            int min = guardSleep[guardId].ToList().IndexOf(guardSleep[guardId].Max());

            Console.WriteLine(guardId * min);
        }
    }
}
