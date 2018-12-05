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
            Regex guardId = new Regex(@"#\d+");

            foreach (KeyValuePair<DateTime, string> action in inputDict)
            {
                if (action.Value.StartsWith("Guard"))
                {
                    Match m = guardId.Match(action.Value);

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

                    for(int i = startSleep.Minute; i <= action.Key.Minute; i++)
                    {
                        tmp[i] += 1;
                    }
                    guardSleep[id] = tmp;
                }
                else if (action.Value.StartsWith("falls"))
                {
                    startSleep = action.Key;
                }
            }

            int sleepiestGuardId = guardSleep.Aggregate((l, r) => l.Value.Sum() > r.Value.Sum() ? l : r).Key;
            int day = guardSleep[sleepiestGuardId].ToList().IndexOf(guardSleep[sleepiestGuardId].Max());
            Console.WriteLine(sleepiestGuardId * day);
        }

        public static void Task2()
        {
            SortedDictionary<DateTime, string> inputDict = new SortedDictionary<DateTime, string>();
            Dictionary<int, List<SleepTime>> guardAction = new Dictionary<int, List<SleepTime>>();

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

            Regex guardId = new Regex(@"#\d+");
            foreach (KeyValuePair<DateTime, string> action in inputDict)
            {
                if (action.Value.StartsWith("Guard"))
                {
                    Match m = guardId.Match(action.Value);

                    if (m.Success)
                    {
                        id = int.Parse(m.Value.Substring(1));
                        if (!guardAction.ContainsKey(id))
                        {
                            List<SleepTime> list = new List<SleepTime>();
                            guardAction.Add(id, list);
                        }
                    }
                }
                else if (action.Value.StartsWith("wakes"))
                {
                    List<SleepTime> list = guardAction[id];
                    SleepTime tmp = new SleepTime(startSleep, action.Key);
                    list.Add(tmp);
                    guardAction[id] = list;
                }
                else if (action.Value.StartsWith("falls"))
                {
                    startSleep = action.Key;
                }
            }

            int guardID = 0;
            int frequentMin = 0;
            int frequentSleep = 0;

            foreach(KeyValuePair<int, List<SleepTime>> guard in guardAction)
            {
                int[] minutes = new int[60];
                for (int i = 0; i < guard.Value.Count; i++)
                {
                    for (int x = guard.Value[i].StartSleep.Minute; x <= guard.Value[i].EndSleep.Minute; x++)
                    {
                        minutes[x] += 1;
                    }
                }

                if(frequentSleep < minutes.Max())
                {
                    guardID = guard.Key;
                    frequentSleep = minutes.Max();
                    frequentMin = minutes.ToList().IndexOf(minutes.Max());
                }
            }

            Console.WriteLine(guardID * frequentMin);
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
