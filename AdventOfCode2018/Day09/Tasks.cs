using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode2018.Day09
{
    class Tasks
    {
        const string inputPath = @"Day09/Input.txt";
        private static LinkedList<int> marbles;
        private static uint[] playerScores;
        private static int  lastMarble;

        public static void Task1and2()
        {
            string input;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                input = reader.ReadToEnd();
            }

            string[] splittedInput = input.Split(' ');

            marbles = new LinkedList<int>();
            playerScores = new uint[int.Parse(splittedInput[0])];
            lastMarble = int.Parse(splittedInput[6]);

            var current = SetupMarbles();

            CalculatePlayerScore(current);
            Console.WriteLine(playerScores.Max());

            marbles = new LinkedList<int>();
            playerScores = new uint[int.Parse(splittedInput[0])];
            lastMarble = int.Parse(splittedInput[6]) * 100;
            
            current = SetupMarbles();

            CalculatePlayerScore(current);
            Console.WriteLine(playerScores.Max());
        }

        private static LinkedListNode<int> SetupMarbles()
        {
            marbles.AddFirst(0);
            LinkedListNode<int> current = marbles.Find(0);
            marbles.AddAfter(current, 1);
            marbles.AddAfter(current, 2);
            current = current.Next;

            return current;
        }

        private static void CalculatePlayerScore(LinkedListNode<int> current)
        {
            for (int i = 3; i < lastMarble + 1; i++)
            {
                if (i % 23 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        current = current.Previous ?? marbles.Last;
                    }

                    playerScores[(i - 1) % playerScores.Length] += (uint)(i + current.Value);

                    var remove = current;
                    current = current.Next ?? marbles.First;

                    marbles.Remove(remove);
                }
                else
                {
                    current = current.Next ?? marbles.First;
                    marbles.AddAfter(current, i);
                    current = current.Next;
                }
            }
        }
    }
}
