using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day14
{
    class Tasks
    {
        const int input = 503761;

        public static void Task1()
        {
            List<int> recipeScores = new List<int>
            {
                3,
                7
            };

            int idx1 = 0;
            int idx2 = 1;

            while (recipeScores.Count < input + 10)
            {
                int r1 = recipeScores[idx1];
                int r2 = recipeScores[idx2];

                int rScore = r1 + r2;

                if(rScore > 9)
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
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Write(recipeScores[input + i]);
            }

            Console.WriteLine("");
        }

        public static void Task2()
        {
            int match = 10442;
            List<int> recipeScores = new List<int>
            {
                3,
                7
            };

            int idx1 = 0;
            int idx2 = 1;
            bool found = false;
            int foundIdx = 0;

            while (!found)
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

                int matchCounter = 0;

                if(recipeScores.Count > 6)
                {
                    for(int i = recipeScores.Count - 6; i < recipeScores.Count; i++)
                    {
                        int a = match / (int)(Math.Pow(10, 5 - matchCounter)) % 10;
                        if (recipeScores[i] == a)
                        {
                            matchCounter++;
                            if(matchCounter == 1)
                            {
                                foundIdx = i;
                            }
                            else if(matchCounter == 6)
                            {
                                found = true;
                            }
                        }
                        else
                        {
                            matchCounter = 0;
                        }
                    }
                }
            }

            Console.WriteLine(foundIdx);
        }
    }
}
