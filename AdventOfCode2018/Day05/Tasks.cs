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

        public static void Task1()
        {
            string input;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                input = reader.ReadToEnd();
            }           
            
            List<char> inputChar = input.ToList();
            Stack<Char> stackChar = new Stack<char>();

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

            Console.WriteLine(stackChar.Count);
        }

        public static void Task2()
        {
            string input;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                input = reader.ReadToEnd();
            }

            int shortestPolymer = int.MaxValue;

            for(int x = 0; x < 26; x++)
            {
                List<char> inputChar = input.ToList();
                inputChar.RemoveAll(c => c == 65 + x || c == 97 + x);
                Stack<Char> stackChar = new Stack<char>();

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

                if (stackChar.Count < shortestPolymer) shortestPolymer = stackChar.Count;
            }

            Console.WriteLine(shortestPolymer);
        }
    }
}
