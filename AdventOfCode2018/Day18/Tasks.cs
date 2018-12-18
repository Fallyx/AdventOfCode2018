using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;

namespace AdventOfCode2018.Day18
{
    class Tasks
    {
        const string inputPath = @"Day18/Input.txt";
        static readonly int size = 50;
        
        static string[] outskirt = new string[size * size];

        public static void Task1()
        {
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    for(int i = 0; i < size; i++)
                    {
                        outskirt[i + counter * size] = line[i].ToString();
                    }
                    counter++;
                }
            }

            string[] copy = new string[size * size];

            Array.Copy(outskirt, 0, copy, 0, outskirt.Length);

            for (int i = 0; i < 10; i++)
            {
                for(int y = 0; y < size; y++)
                {
                    for(int x = 0; x < size; x++)
                    {
                        string acre = outskirt[x + y * size];
                        if (acre == ".")
                        {
                            if (OpenToTree(x, y))
                                copy[x + y * size] = "|";
                        }
                        else if(acre == "|")
                        {
                            if (TreeToLumber(x, y))
                                copy[x + y * size] = "#";
                        }
                        else if(acre == "#")
                        {
                            if (!LumberToLumber(x, y))
                                copy[x + y * size] = ".";
                        }
                    }
                }

                Array.Copy(copy, 0, outskirt, 0, outskirt.Length);
            }

            int woodAcre = 0;
            int lumberAcre = 0;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (outskirt[x + y * size] == "|") woodAcre++;
                    else if (outskirt[x + y * size] == "#") lumberAcre++;
                }
            }

            Console.WriteLine(woodAcre * lumberAcre);
        }

        public static void Task2()
        {
            List<int> stepsBlock = new List<int>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < size; i++)
                    {
                        outskirt[i + counter * size] = line[i].ToString();
                    }
                    counter++;
                }
            }

            int mins;

            for (mins = 0; mins < 1201; mins++)
            {
                string[] copy = new string[size * size];
                Array.Copy(outskirt, 0, copy, 0, outskirt.Length);

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        string acre = outskirt[x + y * size];
                        if (acre == ".")
                        {
                            if (OpenToTree(x, y))
                                copy[x + y * size] = "|";
                        }
                        else if (acre == "|")
                        {
                            if (TreeToLumber(x, y))
                                copy[x + y * size] = "#";
                        }
                        else if (acre == "#")
                        {
                            if (!LumberToLumber(x, y))
                                copy[x + y * size] = ".";
                        }
                    }
                }

                Array.Copy(copy, 0, outskirt, 0, outskirt.Length);

                if (mins >= 1000)
                { 
                    var bs = CountBlocks();
                    stepsBlock.Add(bs.l * bs.t);
                }
            }

            int first = stepsBlock[0];
            int j;
            for (j = 1; j < stepsBlock.Count; j++)
            {
                if(stepsBlock[j] == first)
                {
                    break;
                }
            }

            int equivalent = (1000000000 - 1000 - 1) % j;

            int lt = stepsBlock.ElementAt(equivalent);

            Console.WriteLine(lt);
        }

        private static (int g, int t, int l) CountBlocks()
        {
            int g = 0;
            int t = 0;
            int l = 0;
            for(int y = 0; y < size; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    if (outskirt[x + y * size] == ".") g++;
                    else if (outskirt[x + y * size] == "|") t++;
                    else if (outskirt[x + y * size] == "#") l++;
                }
            }

            return (g, t, l);
        }

        private static bool OpenToTree(int x, int y)
        {
            int countTree = 0;

            if (x - 1 >= 0 && outskirt[x - 1 + y * size] == "|") countTree++;
            if (x + 1 < size && outskirt[x + 1 + y * size] == "|") countTree++;
            if (x - 1 >= 0 && y - 1 >= 0 && outskirt[x - 1 + (y - 1) * size ] == "|") countTree++;
            if (x - 1 >= 0 && y + 1 < size && outskirt[x - 1 + (y + 1) * size] == "|") countTree++;
            if (y - 1 >= 0 && outskirt[x + (y - 1) * size] == "|") countTree++;
            if (y + 1 < size && outskirt[x + (y + 1) * size] == "|") countTree++;
            if (x + 1 < size && y + 1 < size && outskirt[x + 1 + (y + 1) * size] == "|") countTree++;
            if (x + 1 < size && y - 1 >= 0 && outskirt[x + 1 + (y - 1) * size] == "|") countTree++;

            return countTree >= 3;
        }

        private static bool TreeToLumber(int x, int y)
        {
            int countLumber = 0;

            if (x - 1 >= 0 && outskirt[x - 1 + y * size] == "#") countLumber++;
            if (x + 1 < size && outskirt[x + 1 + y * size] == "#") countLumber++;
            if (x - 1 >= 0 && y - 1 >= 0 && outskirt[x - 1 + (y - 1) * size] == "#") countLumber++;
            if (x - 1 >= 0 && y + 1 < size && outskirt[x - 1 + (y + 1) * size] == "#") countLumber++;
            if (y - 1 >= 0 && outskirt[x + (y - 1) * size] == "#") countLumber++;
            if (y + 1 < size && outskirt[x + (y + 1) * size] == "#") countLumber++;
            if (x + 1 < size && y + 1 < size && outskirt[x + 1 + (y + 1) * size] == "#") countLumber++;
            if (x + 1 < size && y - 1 >= 0 && outskirt[x + 1 + (y - 1) * size] == "#") countLumber++;

            return countLumber >= 3;
        }

        private static bool LumberToLumber(int x, int y)
        {
            int countLumber = 0;
            int countTree = 0;

            if (x - 1 >= 0 && outskirt[x - 1 + y * size] == "#") countLumber++;
            else if (x - 1 >= 0 && outskirt[x - 1 + y * size] == "|") countTree++;

            if (x + 1 < size && outskirt[x + 1 + y * size] == "#") countLumber++;
            else if (x + 1 < size && outskirt[x + 1 + y * size] == "|") countTree++;

            if (x - 1 >= 0 && y - 1 >= 0 && outskirt[x - 1 + (y - 1) * size] == "#") countLumber++;
            else if (x - 1 >= 0 && y - 1 >= 0 && outskirt[x - 1 + (y - 1) * size] == "|") countTree++;

            if (x - 1 >= 0 && y + 1 < size && outskirt[x - 1 + (y + 1) * size] == "#") countLumber++;
            else if (x - 1 >= 0 && y + 1 < size && outskirt[x - 1 + (y + 1) * size] == "|") countTree++;

            if (y - 1 >= 0 && outskirt[x + (y - 1) * size] == "#") countLumber++;
            else if (y - 1 >= 0 && outskirt[x + (y - 1) * size] == "|") countTree++;

            if (y + 1 < size && outskirt[x + (y + 1) * size] == "#") countLumber++;
            else if (y + 1 < size && outskirt[x + (y + 1) * size] == "|") countTree++;

            if (x + 1 < size && y + 1 < size && outskirt[x + 1 + (y + 1) * size] == "#") countLumber++;
            else if (x + 1 < size && y + 1 < size && outskirt[x + 1 + (y + 1) * size] == "|") countTree++;

            if (x + 1 < size && y - 1 >= 0 && outskirt[x + 1 + (y - 1) * size] == "#") countLumber++;
            else if (x + 1 < size && y - 1 >= 0 && outskirt[x + 1 + (y - 1) * size] == "|") countTree++;

            return countLumber >= 1 && countTree >= 1;
        }

        private static int StepHashCode(string[] array)
        {
            return ((IStructuralEquatable)array).GetHashCode(EqualityComparer<string>.Default);
        }
    }
}
