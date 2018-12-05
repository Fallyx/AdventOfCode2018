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
            SortedDictionary<DateTime, string> inputDict = new SortedDictionary<DateTime, string>();
            Dictionary<int, int[]> guardSleep = new Dictionary<int, int[]>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] tmp = line.Split(']');
                    inputDict.Add(DateTime.Parse(tmp[0].Substring(1)), tmp[1].Substring(1));
                }
            }

            int id = 0;
            DateTime startSleep = new DateTime();
            Regex guardIdRgx = new Regex(@"#\d+");

            foreach (KeyValuePair<DateTime, string> action in inputDict)
            {
                if (action.Value.StartsWith("Guard"))
                {
                    Match m = guardIdRgx.Match(action.Value);

                    if (m.Success)
                    {
                        id = int.Parse(m.Value.Substring(1));
                        if (!guardSleep.ContainsKey(id))
                        {
                            int[] mins = new int[60];
                            guardSleep.Add(id, mins);
                        }
                    }
                }
                else if (action.Value.StartsWith("wakes"))
                {
                    int[] tmp = guardSleep[id];

                    for(int i = startSleep.Minute; i <= action.Key.Minute; i++) tmp[i]++; 

                    guardSleep[id] = tmp;
                }
                else if (action.Value.StartsWith("falls"))
                {
                    startSleep = action.Key;
                }
            }

            int sleepiestGuardId = guardSleep.Aggregate((l, r) => l.Value.Sum() > r.Value.Sum() ? l : r).Key;
            int min = guardSleep[sleepiestGuardId].ToList().IndexOf(guardSleep[sleepiestGuardId].Max());
            Console.WriteLine(sleepiestGuardId * min);
        }

        public static void Task2()
        {
            SortedDictionary<DateTime, string> inputDict = new SortedDictionary<DateTime, string>();
            Dictionary<int, int[]> guardSleep = new Dictionary<int, int[]>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] tmp = line.Split(']');
                    inputDict.Add(DateTime.Parse(tmp[0].Substring(1)), tmp[1].Substring(1));
                }
            }

            int id = 0;
            DateTime startSleep = new DateTime();
            Regex guardIdRgx = new Regex(@"#\d+");

            foreach(KeyValuePair<DateTime, string> action in inputDict)
            {
                if (action.Value.StartsWith("Guard"))
                {
                    Match m = guardIdRgx.Match(action.Value);

                    if (m.Success)
                    {
                        id = int.Parse(m.Value.Substring(1));
                        if (!guardSleep.ContainsKey(id))
                        {
                            int[] mins = new int[60];
                            guardSleep.Add(id, mins);
                        }
                    }
                }
                else if (action.Value.StartsWith("wakes"))
                {
                    int[] tmp = guardSleep[id];

                    for (int i = startSleep.Minute; i <= action.Key.Minute; i++) tmp[i]++;

                    guardSleep[id] = tmp;
                }
                else if (action.Value.StartsWith("falls"))
                {
                    startSleep = action.Key;
                }
            }

            int guardId = guardSleep.Aggregate((l, r) => l.Value.Max() > r.Value.Max() ? l : r).Key;
            int min = guardSleep[guardId].ToList().IndexOf(guardSleep[guardId].Max());

            Console.WriteLine("asdf " + guardId * min);
        }
    }

    class SleepTime
    {
        private DateTime startSleep;
        private DateTime endSleep;
        private int minOfSleep;

        public SleepTime(DateTime startSleep, DateTime endSleep)
        {
            StartSleep = startSleep;
            EndSleep = endSleep;
            MinOfSleep = endSleep.Minute - startSleep.Minute;
        }

        public DateTime StartSleep { get => startSleep; set => startSleep = value; }
        public DateTime EndSleep { get => endSleep; set => endSleep = value; }
        public int MinOfSleep { get => minOfSleep; set => minOfSleep = value; }
    }
}
