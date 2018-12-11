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

            int[,] powerLevels = new int[301, 301];

            for(int y = 1; y < 301; y++)
            {
                for (int x = 1; x < 301; x++)
                {
                    int rackID = x + 10;
                    int powerLvl = rackID * y;
                    powerLvl += input;
                    powerLvl *= rackID;
                    powerLvl = (powerLvl > 99) ? (powerLvl / 100) % 10 : 0;
                    powerLevels[x, y] = powerLvl - 5;
                }
            }

            for(int y = 1; y < 298; y++)
            {
                for (int x = 1; x < 298; x++)
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
                        topLeft = new Vector2(x, y);
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

            int[,] powerLevels = new int[301, 301];

            for (int y = 1; y < 301; y++)
            {
                for (int x = 1; x < 301; x++)
                {
                    int rackID = x + 10;
                    int powerLvl = rackID * y;
                    powerLvl += input;
                    powerLvl *= rackID;
                    powerLvl = (powerLvl > 99) ? (powerLvl / 100) % 10 : 0;
                    powerLevels[x, y] = powerLvl - 5;
                }
            }

            for (int size = 1; size < 301; size++)
            {
                for (int y = 1; y <= 301 - size; y++)
                {
                    for (int x = 1; x <= 301 - size; x++)
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
                            topLeft = new Vector3(x, y, size);
                            largestTotPower = totPower;
                        }
                    }

                }
            }

            Console.WriteLine(topLeft.X + "," + topLeft.Y + "," + topLeft.Z);
        }
    }
}
