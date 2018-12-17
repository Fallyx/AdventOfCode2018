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

        public static void Task1()
        {
            List<Vector2> clay = new List<Vector2>();

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
                            clay.Add(new Vector2(single, i));
                        }
                    }
                    else
                    {
                        for (int i = minMulti; i <= maxMulti; i++)
                        {
                            clay.Add(new Vector2(i, single));
                        }
                    }
                }
            }

            int miy = (int)clay.Min(i => i.Y);
            int may = (int)clay.Max(i => i.Y);

            int mix = (int)clay.Min(i => i.X);
            int max = (int)clay.Max(i => i.X);

            // + 1 to include minBound + 2 for space left/right
            // + 1 to include minBound + 1 to include water spring
            int width = max - mix + 3;
            int height = may - miy + 2;

            string[,] reservoir = new string[width, height]; 

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(clay.Any(c => c.X - mix + 1 == x && c.Y + 1 - miy == y))
                    {
                        reservoir[x, y] = "#";
                    }
                    else
                    {
                        reservoir[x, y] = ".";
                    }
                }
            }

            reservoir[500 - mix + 1, 0] = "+";

            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    Console.Write(reservoir[x, y]);
                }

                Console.WriteLine();
            }


            Console.Write("");
        }
    }
}
