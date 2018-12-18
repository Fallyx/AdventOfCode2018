using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2018.Day05
{
    class Tasks
    {
        const string inputPath = @"Day05/Input.txt";

        public static void Task1and2()
        {
            string input;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                input = reader.ReadToEnd();
            }

            List<char> inputChar = input.ToList();
            Stack<char> stackChar = RemainingPolymer(inputChar);

            Console.WriteLine(stackChar.Count);

            int shortestPolymer = int.MaxValue;

            for (int x = 0; x < 26; x++)
            {
                inputChar = input.ToList();
                inputChar.RemoveAll(c => c == 65 + x || c == 97 + x);
                stackChar = RemainingPolymer(inputChar);

                if (stackChar.Count < shortestPolymer) shortestPolymer = stackChar.Count;
            }

            Console.WriteLine(shortestPolymer);
        }

        public static Stack<char> RemainingPolymer(List<char> inputChar)
        {
            Stack<char> stackChar = new Stack<char>();

            foreach (char c in inputChar)
            {
                if (stackChar.Count == 0) stackChar.Push(c);
                else
                {
                    char cS = stackChar.Peek();
                    if ((c + 32) == cS || (c - 32) == cS) stackChar.Pop();
                    else stackChar.Push(c);
                }
            }

            return stackChar;
        }
    }
}
