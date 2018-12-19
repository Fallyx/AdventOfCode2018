using System;
using System.Collections.Generic;

namespace AdventOfCode2018.Day14
{
    class Tasks
    {
        const int input = 503761;

        
        public static void Task1()
        {
            List<int> recipeScores = new List<int> { 3, 7 };

            int idx1 = 0;
            int idx2 = 1;

            while (recipeScores.Count < input + 10)
            {
                var i = CalculateRecipScore(recipeScores, idx1, idx2);
                idx1 = i.Item1;
                idx2 = i.Item2;
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Write(recipeScores[input + i]);
            }

            Console.WriteLine("");
        }
        

        public static void Task2()
        {
            int[] inputArr = new int[] { 5, 0, 3, 7, 6, 1 };
            int idx = 0;
            int inputIdx = 0;
            bool found = false;

            List<int> recipeScores = new List<int> { 3, 7 };

            int idx1 = 0;
            int idx2 = 1;

            while (!found)
            {
                var i = CalculateRecipScore(recipeScores, idx1, idx2);
                idx1 = i.Item1;
                idx2 = i.Item2;

                while (idx + inputIdx < recipeScores.Count)
                {
                    if (inputArr[inputIdx] == recipeScores[idx + inputIdx])
                    {
                        if (inputIdx == inputArr.Length - 1)
                        {
                            found = true;
                            break;
                        }
                        inputIdx++;
                    }
                    else
                    {
                        inputIdx = 0;
                        idx++;
                    }
                }
            }

            Console.WriteLine(idx);
        }        

        private static (int, int) CalculateRecipScore(List<int> recipeScores, int idx1, int idx2)
        {
            int r1 = recipeScores[idx1];
            int r2 = recipeScores[idx2];

            int rScore = r1 + r2;

            if (rScore > 9)
            {
                recipeScores.Add(rScore / 10);
                recipeScores.Add(rScore % 10);
            }
            else
            {
                recipeScores.Add(rScore);
            }

            idx1 = (idx1 + r1 + 1) % recipeScores.Count;
            idx2 = (idx2 + r2 + 1) % recipeScores.Count;

            return (idx1, idx2);
        }
    }
}
