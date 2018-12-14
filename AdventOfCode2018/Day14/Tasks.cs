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
    }
}
