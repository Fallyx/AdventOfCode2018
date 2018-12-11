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

            for(int y = 1; y < 298; y++)
            {
                for (int x = 1; x < 298; x++)
                {
                    int totPower = 0;
                    for(int i = 0; i < 3; i++)
                    {
                        for(int j = 0; j < 3; j++)
                        {
                            int rackID = x + j + 10;
                            int powerLvl = rackID * (y + i);
                            powerLvl += input;
                            powerLvl *= rackID;
                            powerLvl = (powerLvl > 99) ? (powerLvl / 100) % 10 : 0;
                            totPower += powerLvl - 5;
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

            for (int size = 1; size < 300; size++)
            {
                for (int y = 1; y <= 300 - size; y++)
                {
                    for (int x = 1; x <= 300 - size; x++)
                    {
                        int totPower = 0;
                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                int rackID = x + j + 10;
                                int powerLvl = rackID * (y + i);
                                powerLvl += input;
                                powerLvl *= rackID;
                                powerLvl = (powerLvl > 99) ? (powerLvl / 100) % 10 : 0;
                                totPower += powerLvl - 5;
                            }
                        }

                        if (totPower > largestTotPower)
                        {
                            topLeft = new Vector3(x, y, size);
                            largestTotPower = totPower;
                        }
                    }

                }

                Console.WriteLine(size);
            }

            Console.WriteLine(topLeft.X + "," + topLeft.Y + "," + topLeft.Z);
        }
    }
}
