using System;
using System.Collections.Generic;
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
            List<FabricClaim> fabricClaims = new List<FabricClaim>();

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

                    fabricClaims.Add(new FabricClaim(id, marginLeft, marginTop, height, width));

                    for (int x = marginLeft; x < marginLeft + width; x++)
                    {
                        for (int y = marginTop; y < marginTop + height; y++)
                        {
                            fabric[x, y] += 1;
                        }
                    }
                }
            }

            foreach (FabricClaim fC in fabricClaims)
            {
                bool isUnique = true;

                for (int x = fC.Margin.left; x < fC.Margin.left + fC.Size.width; x++)
                {
                    for (int y = fC.Margin.top; y < fC.Margin.top + fC.Size.height; y++)
                    {
                        if (fabric[x, y] > 1) isUnique = false;

                        if (!isUnique) break;
                    }
                    if (!isUnique) break;
                }

                if (isUnique) uniqueID = fC.Id;
            }

            Console.WriteLine(uniqueID);
        }
    }

    class FabricClaim
    {
        private int id;
        private (int left, int top) margin;
        private (int height, int width) size;

        public FabricClaim(int id, int left, int top, int height, int width)
        {
            Id = id;
            Margin = (left, top);
            Size = (height, width);
        }

        public int Id { get => id; set => id = value; }
        public (int left, int top) Margin { get => margin; set => margin = value; }
        public (int height, int width) Size { get => size; set => size = value; }
    }
}
