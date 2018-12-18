using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode2018.Day06
{
    class Tasks
    {
        const string inputPath = @"Day06/Input.txt";
        private static List<(int x, int y)> lines = new List<(int, int)>();
        private static int maxX;
        private static int maxY;

        public static void Task1and2()
        {
            using (StreamReader sr = new StreamReader(inputPath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] coord = line.Split(',');

                    lines.Add((int.Parse(coord[0].Trim()), int.Parse(coord[1].Trim())));
                }
            }

            maxX = lines.Max(i => i.x) + 1;
            maxY = lines.Max(i => i.y) + 1;

            (int id, int distanceFromCenter)[] location = new (int, int)[maxX * maxY];
            int[] totDistance = new int[maxX * maxY];

            for (int i = 0; i < location.Length; i++)
            {
                location[i].distanceFromCenter = int.MaxValue;
            }

            CalculateDistance(location, totDistance);

            MarkInfinite(location);

            Dictionary<int, int> finiteLocations = new Dictionary<int, int>();

            foreach ((int i, int d) in location)
            {
                if (i == -1) continue;
                if (finiteLocations.ContainsKey(i))
                {
                    finiteLocations[i]++;
                }
                else
                {
                    finiteLocations.Add(i, 1);
                }
            }

            Console.WriteLine(finiteLocations.Max(l => l.Value));
            Console.WriteLine(totDistance.Count(c => c < 10000));
        }

        private static void CalculateDistance((int id, int dist)[] loc, int[] tot)
        {
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    int idx = y * maxX + x;
                    int totDist = 0;

                    for (int i = 0; i < lines.Count; i++)
                    {
                        int distance = Math.Abs(lines[i].x - x) + Math.Abs(lines[i].y - y);
                        totDist += Math.Abs(lines[i].x - x) + Math.Abs(lines[i].y - y);
                        tot[x + y * maxX] = totDist;

                        if (loc[idx].dist == distance)
                        {
                            loc[idx].id = -1;
                        }
                        else if (loc[idx].dist > distance)
                        {
                            loc[idx].id = i;
                            loc[idx].dist = distance;
                        }
                    }
                }
            }
        }

        private static void MarkInfinite((int id, int dist)[] loc)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (y == 0 || y == maxY - 1)
                {
                    for (int x = 0; x < maxX; x++)
                    {
                        int idx = loc[y * maxX + x].id;
                        for (int i = 0; i < loc.Length; i++)
                        {
                            if (loc[i].id == idx) loc[i].id = -1;
                        }
                    }
                }
                if (loc[y * maxX].id != -1)
                {
                    int idx = loc[y * maxX].id;
                    for (int i = 0; i < loc.Length; i++)
                    {
                        if (loc[i].id == idx) loc[i].id = -1;
                    }
                }
                if (y != 0 && loc[y * maxX - 1].id != -1)
                {
                    int idx = loc[y * maxX - 1].id;
                    for (int i = 0; i < loc.Length; i++)
                    {
                        if (loc[i].id == idx) loc[i].id = -1;
                    }
                }
            }
        }
    }
}
