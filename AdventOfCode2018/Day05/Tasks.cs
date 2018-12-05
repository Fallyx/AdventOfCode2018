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
            string input = "";
            
            using (StreamReader reader = new StreamReader(inputPath))
            {
                input = reader.ReadToEnd();
            }
            
            List<char> inputChar = input.ToList();

            for(int i = 0; i < inputChar.Count - 1; i++)
            {
                if((inputChar[i] + 32) == inputChar[i+1] || (inputChar[i] - 32) == inputChar[i+1])
                {
                    inputChar.RemoveAt(i);
                    inputChar.RemoveAt(i);
                    i = -1;
                }
            }

            Console.WriteLine(inputChar.Count);
        }
    }
}
