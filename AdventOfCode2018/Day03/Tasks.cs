using System;
using System.IO;

namespace AdventOfCode2018.Day03
{
    class Tasks
    {
        const string inputPath = @"Day03/Input.txt";

        public static void Task1()
        {
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                int[,] fabric = new int[1000,1000];
                int twoOrMoreFabricClaims = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] claim = line.Split(' ');

                    string[] margins = claim[2].Split(',');
                    int marginLeft = int.Parse(margins[0]);
                    int marginTop = int.Parse(margins[1].Substring(0, margins[1].Length - 1));

                    string[] dimension = claim[3].Split('x');
                    int width = int.Parse(dimension[0]);
                    int height = int.Parse(dimension[1]);

                    for(int x = marginLeft; x < marginLeft + width; x++)
                    {
                        for (int y = marginTop; y < marginTop + height; y++)
                        {
                            fabric[x,y] += 1;
                        }
                    }
                }

                for(int i = 0; i < fabric.GetLength(0); i++)
                {
                    for(int j = 0; j < fabric.GetLength(1); j++)
                    {
                        if(fabric[i,j] > 1) twoOrMoreFabricClaims++;
                    }
                }

                Console.WriteLine(twoOrMoreFabricClaims);
            }
        }

        public static void Task2()
        {
            int[,] fabric = new int[1000, 1000];
            int uniqueID = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] claim = line.Split(' ');

                    string[] margins = claim[2].Split(',');
                    int marginLeft = int.Parse(margins[0]);
                    int marginTop = int.Parse(margins[1].Substring(0, margins[1].Length - 1));

                    string[] dimension = claim[3].Split('x');
                    int width = int.Parse(dimension[0]);
                    int height = int.Parse(dimension[1]);

                    for (int x = marginLeft; x < marginLeft + width; x++)
                    {
                        for (int y = marginTop; y < marginTop + height; y++)
                        {
                            fabric[x, y] += 1;
                        }
                    }
                }
            }

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] claim = line.Split(' ');

                    int id = int.Parse(claim[0].Substring(1));

                    string[] margins = claim[2].Split(',');
                    int marginLeft = int.Parse(margins[0]);
                    int marginTop = int.Parse(margins[1].Substring(0, margins[1].Length - 1));

                    string[] dimension = claim[3].Split('x');
                    int width = int.Parse(dimension[0]);
                    int height = int.Parse(dimension[1]);

                    bool isUnique = true;

                    for (int x = marginLeft; x < marginLeft + width; x++)
                    {
                        for (int y = marginTop; y < marginTop + height; y++)
                        {
                            if (fabric[x, y] > 1) isUnique = false;

                            if (!isUnique) break;
                        }
                        if (!isUnique) break;
                    }

                    if (isUnique) uniqueID = id;
                }

                Console.WriteLine(uniqueID);
            }
        }
    }
}
