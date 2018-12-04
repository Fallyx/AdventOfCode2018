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
            Dictionary<int, List<SleepTime>> guardAction = new Dictionary<int, List<SleepTime>>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while((line = reader.ReadLine()) != null)
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

            int sleepiestGuardId = 0;
            int longestTotSleep = 0;

            foreach (KeyValuePair<int, List<SleepTime>> guard in guardAction)
            {
                int totSleep = 0;
                for (int i = 0; i < guard.Value.Count; i++)
                {
                    totSleep += guard.Value[i].MinOfSleep;
                }

                if (totSleep > longestTotSleep)
                {
                    longestTotSleep = totSleep;
                    sleepiestGuardId = guard.Key;
                }
            }

            int[] minutes = new int[60];

            for (int i = 0; i < guardAction[sleepiestGuardId].Count; i++)
            {
                for (int x = guardAction[sleepiestGuardId][i].StartSleep.Minute; x <= guardAction[sleepiestGuardId][i].EndSleep.Minute; x++)
                {
                    minutes[x] += 1;
                }
            }

            int day = 0;
            int maxSleep = 0;
            for (int i = 0; i < minutes.Length; i++)
            {
                if (minutes[i] > maxSleep)
                {
                    maxSleep = minutes[i];
                    day = i;
                }
            }
            Console.WriteLine(sleepiestGuardId * day);
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
