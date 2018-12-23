using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2018.Day22
{
    class Tasks
    {
        const int depth = 9465;
        static Vector2 target = new Vector2(13, 704);
        private static CaveSystem[,] cave;
        private enum tools { neither, torch, climbingGear }

        public static void Task1()
        {
            cave = new CaveSystem[(int)target.X + 1, (int)target.Y + 1];
            int risklvl = 0;
            for (int y = 0; y <= target.Y; y++)
            {
                for(int x = 0; x <= target.X; x++)
                {
                    int gi = GeologicIndex(x, y);
                    int el = (gi + depth) % 20183;
                    cave[x, y] = new CaveSystem(gi, el);
                    risklvl += cave[x, y].Type;
                }
            }

            Console.WriteLine(risklvl);
        }

        public static void Task2()
        {
            cave = new CaveSystem[(int)target.X + 11, (int)target.Y + 11];

            for (int y = 0; y <= target.Y + 10; y++)
            {
                for (int x = 0; x <= target.X + 10; x++)
                {
                    int gi = GeologicIndex(x, y);
                    int el = (gi + depth) % 20183;
                    cave[x, y] = new CaveSystem(gi, el, x, y);
                }
            }

            ShortestPath();
        }

        private static void ShortestPath()
        {
            (int x, int y)[] movingDirs = { (-1, 0), (1, 0), (0, -1), (0, 1) };

            Queue<(CaveSystem c, tools t, int ts, int min)> queue = new Queue<(CaveSystem, tools, int, int)>();
            HashSet<(CaveSystem c, tools t)> visited = new HashSet<(CaveSystem c, tools t)>();

            queue.Enqueue((cave[0, 0], tools.torch, 0, 0));
            visited.Add((cave[0, 0], tools.torch));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.ts > 0)
                {
                    if (current.ts > 1 || visited.Add((current.c, current.t)))
                    {
                        queue.Enqueue((current.c, current.t, current.ts - 1, current.min + 1));
                    }

                    continue;
                }

                if (current.c.X == target.X && current.c.Y == target.Y && current.t == tools.torch)
                {
                    Console.WriteLine(current.min);
                    break;
                }

                for(int i = 0; i < movingDirs.Length; i++)
                {
                    int x = movingDirs[i].x + current.c.X;
                    int y = movingDirs[i].y + current.c.Y;

                    if (x < 0 || y < 0 || x > target.X + 10 || y > target.Y + 10) continue;

                    if (ToolAllowed(x, y, current.t) && visited.Add((cave[x, y], current.t)))
                    {
                        queue.Enqueue((cave[x, y], current.t, 0, current.min + 1));
                    }
                }

                if (ToolAllowed(current.c.X, current.c.Y, tools.climbingGear) && !visited.Any(v => v.c.X == current.c.X && v.c.Y == current.c.Y && v.t == tools.climbingGear))
                {
                    queue.Enqueue((current.c, tools.climbingGear, 6, current.min + 1));
                }
                else if(ToolAllowed(current.c.X, current.c.Y, tools.torch) && !visited.Any(v => v.c.X == current.c.X && v.c.Y == current.c.Y && v.t == tools.torch))
                {
                    queue.Enqueue((current.c, tools.torch, 6, current.min + 1));
                }
                else if(ToolAllowed(current.c.X, current.c.Y, tools.neither) && !visited.Any(v => v.c.X == current.c.X && v.c.Y == current.c.Y && v.t == tools.neither))
                {
                    queue.Enqueue((current.c, tools.neither, 6, current.min + 1));
                }
            }
        }

        private static bool ToolAllowed(int x, int y, tools currentTool)
        {
            if(cave[x, y].Type == 0)
            {
                return currentTool == tools.climbingGear || currentTool == tools.torch;
            }
            else if (cave[x, y].Type == 1)
            {
                return currentTool == tools.climbingGear || currentTool == tools.neither;
            }
            else
            {
                return currentTool == tools.neither || currentTool == tools.torch;
            }
        }

        private static int GeologicIndex(int x, int y)
        {
            if((x == 0 && y == 0) || (x == target.X && y == target.Y))
            {
                return 0;
            }
            else if(y == 0)
            {
                return x * 16807;
            }
            else if(x == 0)
            {
                return y * 48271;
            }
            else
            {
                return cave[x - 1, y].ErosionLevel * cave[x, y - 1].ErosionLevel;
            }
        }
    }

    internal class CaveSystem
    {
        private int x;
        private int y;
        private int geoIndex;
        private int erosionLvl;
        private int type;

        public CaveSystem(int geoIndex, int erosionLvl)
        {
            GeologicIndex = geoIndex;
            ErosionLevel = erosionLvl;
            Type = erosionLvl % 3;
        }

        public CaveSystem(int geoIndex, int erosionLvl, int x, int y)
        {
            X = x;
            Y = y;
            GeologicIndex = geoIndex;
            ErosionLevel = erosionLvl;
            Type = erosionLvl % 3;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int GeologicIndex { get => geoIndex; set => geoIndex = value; }
        public int ErosionLevel { get => erosionLvl; set => erosionLvl = value; }
        public int Type { get => type; set => type = value; }
    }
}
