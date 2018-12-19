using System;
using System.Numerics;

namespace AdventOfCode2018.Day11
{
    class Tasks
    {
        const int input = 7989;
        const int gridSize = 300;

        public static void Task1_()
        {
            int[,] powerLevels = GeneratePowerLevels();

            var tot = LargestTotalPower(powerLevels, 3);
            Console.WriteLine(tot.tl.X + "," + tot.tl.Y);
        }

        public static void Task2_()
        {
            int[,] powerLevels = GeneratePowerLevels();
            Vector3 topLeft = new Vector3();
            int largestTotPower = 0;

            for(int size = 1; size <= gridSize; size++)
            {
                var tot = LargestTotalPower(powerLevels, size, largestTotPower);
                if (tot.ltp > largestTotPower)
                {
                    topLeft = tot.tl;
                    largestTotPower = tot.ltp;
                }
            }
            
            Console.WriteLine($"{topLeft.X},{topLeft.Y},{topLeft.Z}");
        }

        private static int[,] GeneratePowerLevels()
        {
            int[,] powerLevels = new int[gridSize + 1, gridSize + 1];
            for(int y = 1; y <= gridSize; y++)
            {
                for(int x = 1; x <= gridSize; x++)
                {
                    int rackID = x + 10;
                    int powerLvl = rackID * y;
                    powerLvl += input;
                    powerLvl *= rackID;
                    powerLvl = (powerLvl > 99) ? (powerLvl / 100) % 10 : 0;
                    powerLevels[x, y] = powerLvl - 5;
                    powerLevels[x, y] += powerLevels[x - 1, y] + powerLevels[x, y - 1] - powerLevels[x - 1, y - 1];
                }
            }

            return powerLevels;
        }

        private static (int ltp, Vector3 tl) LargestTotalPower(int[,] powerLevels, int squareSize, int largestTotPower = 0)
        {
            Vector3 topLeft = new Vector3();

            for (int y = 1; y <= 300 - squareSize; y++)
            {
                for (int x = 1; x <= 300 - squareSize; x++)
                {
                    int totPower = 0;

                    totPower += powerLevels[x, y] - powerLevels[x + squareSize, y] - powerLevels[x, y + squareSize] + powerLevels[x + squareSize, y + squareSize];

                    if (totPower > largestTotPower)
                    {
                        topLeft = new Vector3(x + 1, y + 1, squareSize);
                        largestTotPower = totPower;
                    }
                }
            }

            return (largestTotPower, topLeft);
        }
    }
}
