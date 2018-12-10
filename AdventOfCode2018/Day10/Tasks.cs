using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace AdventOfCode2018.Day10
{
    class Tasks
    {
        const string inputPath = @"Day10/Input.txt";

        public static void Task1()
        {
            List<(Vector2 pos, Vector2 vel)> lights = new List<(Vector2 pos, Vector2 vel)>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    Vector2 p = new Vector2(int.Parse(line.Substring(10, 6).Trim()), int.Parse(line.Substring(17, 7).Trim()));
                    Vector2 v = new Vector2(int.Parse(line.Substring(36, 2).Trim()), int.Parse(line.Substring(39, 3).Trim()));

                    lights.Add((p, v));
                }
            }

            bool msgFound = false;

            while(!msgFound)
            {
                var compareCopy = new List<(Vector2 pos, Vector2 vel)>(lights);

                int oldMinX = (int)lights.Min(l => l.pos.X);
                int oldMinY = (int)lights.Min(l => l.pos.Y);
                int oldMaxX = (int)lights.Max(l => l.pos.X);
                int oldMaxY = (int)lights.Max(l => l.pos.Y);

                for (int i = 0; i < lights.Count; i++)
                {
                    var l = lights[i];
                    l.pos += l.vel;
                    lights[i] = l;
                }
                
               
                int newMinX = (int)lights.Min(l => l.pos.X);
                int newMinY = (int)lights.Min(l => l.pos.Y);
                int newMaxX = (int)lights.Max(l => l.pos.X);
                int newMaxY = (int)lights.Max(l => l.pos.Y);
                
                if ((newMaxX - newMinX) > (oldMaxX - oldMinX) || (newMaxY - newMinY) > (oldMaxY - oldMinY))
                {
                    for (int y = oldMinY; y <= oldMaxY; y++)
                    {
                        for (int x = oldMinX; x <= oldMaxX; x++)
                        {
                            if (compareCopy.Any(l => l.pos.X == x && l.pos.Y == y))
                                Console.Write("#");
                            else
                                Console.Write(".");
                        }
                        Console.Write("\n");
                    }

                    msgFound = true;
                }
            }
        }

        public static void Task2()
        {
            List<(Vector2 pos, Vector2 vel)> lights = new List<(Vector2 pos, Vector2 vel)>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    Vector2 p = new Vector2(int.Parse(line.Substring(10, 6).Trim()), int.Parse(line.Substring(17, 7).Trim()));
                    Vector2 v = new Vector2(int.Parse(line.Substring(36, 2).Trim()), int.Parse(line.Substring(39, 3).Trim()));

                    lights.Add((p, v));
                }
            }

            bool msgFound = false;
            int ticks = 0;

            while (!msgFound)
            {
                var compareCopy = new List<(Vector2 pos, Vector2 vel)>(lights);

                int oldMinX = (int)lights.Min(l => l.pos.X);
                int oldMinY = (int)lights.Min(l => l.pos.Y);
                int oldMaxX = (int)lights.Max(l => l.pos.X);
                int oldMaxY = (int)lights.Max(l => l.pos.Y);

                for (int i = 0; i < lights.Count; i++)
                {
                    var l = lights[i];
                    l.pos += l.vel;
                    lights[i] = l;
                }


                int newMinX = (int)lights.Min(l => l.pos.X);
                int newMinY = (int)lights.Min(l => l.pos.Y);
                int newMaxX = (int)lights.Max(l => l.pos.X);
                int newMaxY = (int)lights.Max(l => l.pos.Y);

                if ((newMaxX - newMinX) > (oldMaxX - oldMinX) || (newMaxY - newMinY) > (oldMaxY - oldMinY))
                {
                    msgFound = true;

                    Console.WriteLine(ticks);
                }
                ticks++;
            }
        }
    }
}
