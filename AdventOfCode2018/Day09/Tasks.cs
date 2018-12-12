using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2018.Day09
{
    class Tasks
    {
        const string inputPath = @"Day09/Input.txt";

        public static void Task1()
        {
            string input;
            LinkedList<int> marbles = new LinkedList<int>();
            using (StreamReader reader = new StreamReader(inputPath))
            {
                input = reader.ReadToEnd();
            }

            string[] splittedInput = input.Split(' ');

            int[] playerScores = new int[int.Parse(splittedInput[0])];
            int lastMarble = int.Parse(splittedInput[6]);

            marbles.AddFirst(0);
            LinkedListNode<int> current = marbles.Find(0);
            marbles.AddAfter(current, 1);
            marbles.AddAfter(current, 2);
            current = marbles.Find(2);

            for(int i = 3; i < lastMarble + 1; i++)
            {
                if (i % 23 == 0)
                {
                    for(int j = 0;j < 7; j++)
                    {
                        current = current.Previous ?? marbles.Last;
                    }

                    playerScores[(i - 1) % playerScores.Length] += i + current.Value;

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

            Console.WriteLine(playerScores.Max());
        }

        public static void Task2()
        {
            string input;
            LinkedList<int> marbles = new LinkedList<int>();
            using (StreamReader reader = new StreamReader(inputPath))
            {
                input = reader.ReadToEnd();
            }

            string[] splittedInput = input.Split(' ');
            uint[] playerScores = new uint[int.Parse(splittedInput[0])];
            int lastMarble = int.Parse(splittedInput[6]) * 100;

            marbles.AddFirst(0);
            LinkedListNode<int> current = marbles.Find(0);
            marbles.AddAfter(current, 1);
            marbles.AddAfter(current, 2);
            current = marbles.Find(2);

            for (int i = 3; i < lastMarble + 1; i++)
            {
                if (i % 23 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        current = current.Previous ?? marbles.Last;
                    }

                    playerScores[(i - 1) % playerScores.Length] +=(uint)(i + current.Value);

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

            Console.WriteLine(playerScores.Max());
        }
    }
}
