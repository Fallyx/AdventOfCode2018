using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode2018.Day11
{
    class Tasks
    {
        const int input = 7989;

        public static void Task1()
        {
            Vector2 topLeft = new Vector2();
            int largestTotPower = 0;

            int[,] powerLevels = new int[300, 300];

            for(int y = 0; y < 300; y++)
            {
                for (int x = 0; x < 300; x++)
                {
                    int rackID = x + 1 + 10;
                    int powerLvl = rackID * (y+1);
                    powerLvl += input;
                    powerLvl *= rackID;
                    powerLvl = (powerLvl > 99) ? (powerLvl / 100) % 10 : 0;
                    powerLevels[x, y] = powerLvl - 5;
                }
            }

            for(int y = 0; y < 297; y++)
            {
                for (int x = 0; x < 297; x++)
                {
                    int totPower = 0;
                    for(int i = 0; i < 3; i++)
                    {
                        for(int j = 0; j < 3; j++)
                        {
                            totPower += powerLevels[x + j, y + i];
                        }
                    }

                    if(totPower > largestTotPower)
                    {
                        topLeft = new Vector2(x + 1, y + 1);
                        largestTotPower = totPower;
                    }
                }
            }

            Console.WriteLine(topLeft.X + "," + topLeft.Y);
        }

        public static void Task2()
        {
            Vector3 topLeft = new Vector3();
            int largestTotPower = 0;

            int[,] powerLevels = new int[300, 300];

            for (int y = 0; y < 300; y++)
            {
                for (int x = 0; x < 300; x++)
                {
                    int rackID = x + 1 + 10;
                    int powerLvl = rackID * (y + 1);
                    powerLvl += input;
                    powerLvl *= rackID;
                    powerLvl = (powerLvl > 99) ? (powerLvl / 100) % 10 : 0;
                    powerLevels[x, y] = powerLvl - 5;
                }
            }

            for (int size = 1; size <= 300; size++)
            {
                for (int y = 0; y < 300 - size; y++)
                {
                    for (int x = 0; x < 300 - size; x++)
                    {
                        int totPower = 0;
                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                totPower += powerLevels[x + j, y + i];
                            }
                        }

                        if (totPower > largestTotPower)
                        {
                            topLeft = new Vector3(x + 1, y + 1, size);
                            largestTotPower = totPower;
                        }
                    }

                }
            }

            Console.WriteLine(topLeft.X + "," + topLeft.Y + "," + topLeft.Z);
        }
    }
}
