using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            for (int y = 0; y <= target.Y; y++)
            {
                for (int x = 0; x <= target.X; x++)
                {
                    int gi = GeologicIndex(x, y);
                    int el = (gi + depth) % 20183;
                    cave[x, y] = new CaveSystem(gi, el);
                }
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

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int GeologicIndex { get => geoIndex; set => geoIndex = value; }
        public int ErosionLevel { get => erosionLvl; set => erosionLvl = value; }
        public int Type { get => type; set => type = value; }
    }
}
