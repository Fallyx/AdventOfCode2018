using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace AdventOfCode2018.Day23
{
    class Tasks
    {
        const string inputPath = @"Day23/Input.txt";

        public static void Task1()
        {
            Dictionary<(int x, int y, int z), int> nanobots = Setup();

            int lR = nanobots.Max(n => n.Value);
            Dictionary<(int x, int y, int z), int> largestR = new Dictionary<(int, int, int), int>();

            foreach (var kv in nanobots)
            {
                if (kv.Value == lR)
                {
                    largestR.Add(kv.Key, kv.Value);
                }
            }

            int amountOfNanobotsInSignal = 0;

            foreach(var kvR in largestR)
            {
                int nInSig = 0;
                foreach(var kvN in nanobots)
                {
                    int distance = Math.Abs(kvR.Key.x - kvN.Key.x) + Math.Abs(kvR.Key.y - kvN.Key.y) + Math.Abs(kvR.Key.z - kvN.Key.z);

                    if(kvR.Value >= distance)
                    {
                        nInSig++;
                    }
                }

                if(amountOfNanobotsInSignal < nInSig)
                {
                    amountOfNanobotsInSignal = nInSig;
                }
            }

            Console.WriteLine(amountOfNanobotsInSignal);
        }

        public static void Task2()
        {

        }

        private static Dictionary<(int x, int y, int z), int> Setup()
        {
            Dictionary<(int x, int y, int z), int> nanobots = new Dictionary<(int, int, int), int>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] info = line.Split(',');

                    int x = int.Parse(info[0].Substring(5));
                    int y = int.Parse(info[1]);
                    int z = int.Parse(info[2].Substring(0, info[2].Length - 1));
                    int r = int.Parse(info[3].Substring(3));

                    nanobots.Add((x, y, z), r);
                }
            }

            return nanobots;
        }
    }
}
