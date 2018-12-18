using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace AdventOfCode2018.Day17
{
    class Tasks
    {
        const string inputPath = @"Day17/Input.txt";

        Vector2 waterSpring = new Vector2(500, 0);
        static int width = 1000;
        static int height = 2000;
        static char[,] reservoir;

        static int minY = int.MaxValue;
        static int maxY = 0;

        public static void Task1and2()
        {
            reservoir = new char[width, height];

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                bool xSingle = true;

                while((line = reader.ReadLine()) != null)
                {
                    string[] coords = line.Split(' ');

                    if(coords[0][0] == 'x')
                    {
                        xSingle = true;
                    }
                    else
                    {
                        xSingle = false;
                    }
                    string singleStr = coords[0].Substring(2);
                    string[] minMax = coords[1].Split('.');

                    int single = int.Parse(singleStr.Substring(0, singleStr.Length - 1));
                    int minMulti = int.Parse(minMax[0].Substring(2));
                    int maxMulti = int.Parse(minMax[2]);

                    if(xSingle)
                    {
                        for(int i = minMulti; i <= maxMulti; i++)
                        {
                            reservoir[single, i] = '#';
                        }

                        if (minMulti < minY)
                        {
                            minY = minMulti;
                        }
                        if(maxMulti > maxY)
                        {
                            maxY = maxMulti;
                        }
                    }
                    else
                    {
                        for (int i = minMulti; i <= maxMulti; i++)
                        {
                            reservoir[i, single] = '#';
                        }

                        if(single < minY)
                        {
                            minY = single;
                        }
                        if(single > maxY)
                        {
                            maxY = single;
                        }
                    }
                }
            }

            reservoir[500, 0] = '+';

            WaterDown(500, 1);

            int counterStill = 0;
            int counterFall = 0;

            for(int y = minY; y <= maxY; y++)
            {
                for(int x = 0; x < reservoir.GetLength(0); x++)
                {
                    if(reservoir[x, y] == '|')
                    {
                        counterFall++;
                    }
                    else if(reservoir[x, y] == '~')
                    {
                        counterStill++;
                    }
                }
            }

            Console.WriteLine($"{counterStill} + {counterFall} = {counterStill + counterFall}");
        }

        private static bool BlockBlocked(int x, int y)
        {
            return reservoir[x, y] == '#' || reservoir[x, y] == '~';
        }

        private static void WaterDown(int x, int y)
        {
            reservoir[x, y] = '|';

            while (reservoir[x, y + 1] != '#' && reservoir[x, y + 1] != '~')
            {
                y++;
                if (y > maxY)
                {
                    return;
                }
                reservoir[x, y] = '|';
            } 

            while (true)
            {
                bool waterDownLeft = false;
                bool waterDownRight = false;
                int leftX;
                int rightX;

                for(leftX = x; leftX >= 0;  leftX--)
                {
                    if(BlockBlocked(leftX, y + 1) == false)
                    {
                        waterDownLeft = true;
                        break;
                    }

                    reservoir[leftX, y] = '|';

                    if(BlockBlocked(leftX - 1, y))
                    {
                        break;
                    }
                }

                for(rightX = x; rightX < reservoir.GetLength(0); rightX++)
                {
                    if (BlockBlocked(rightX, y + 1) == false)
                    {
                        waterDownRight = true;
                        break;
                    }

                    reservoir[rightX, y] = '|';

                    if (BlockBlocked(rightX + 1, y))
                    {
                        break;
                    }
                }

                if(waterDownLeft)
                {
                    WaterDown(leftX, y);
                }

                if(waterDownRight)
                {
                    WaterDown(rightX, y);
                }

                if(waterDownLeft || waterDownRight)
                {
                    return;
                }

                for(int i = leftX; i <= rightX; i++)
                {
                    reservoir[i, y] = '~';
                }

                y--;
            }
        }
    }
}