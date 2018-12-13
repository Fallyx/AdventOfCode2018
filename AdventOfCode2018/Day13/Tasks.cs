using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2018.Day13
{
    class Tasks
    {
        const string inputPath = @"Day13/Input.txt";
        private static string[] directions = new string[4]
        {
            "<^>",
            "v<^",
            ">v<",
            "^>v"
        };

        public static void Task1()
        {
            string line;
            int lineCount = File.ReadLines(inputPath).Count();
            int lineLength = 0;
            

            using (StreamReader reader = new StreamReader(inputPath))
            {
                lineLength = reader.ReadLine().Length;
            }

            (string track, string cart, int direction, bool moved)[,] mineMap = new (string, string, int, bool)[lineLength, lineCount];

            using (StreamReader reader = new StreamReader(inputPath))
            {
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = line[i];
                        string track = "";
                        string cart = "";
                        if (c == '^' || c == 'v' || c == '>' || c == '<')
                        {
                            cart = c.ToString();
                        }
                        else
                        {
                            track = c.ToString();
                        }
                        mineMap[i, counter] = (track, cart, 0, false);
                    }

                    counter++;
                }
            }

            for (int y = 0; y < lineCount; y++)
            {
                for (int x = 0; x < lineLength; x++)
                {
                    if(mineMap[x, y].track == "")
                    {
                        mineMap[x,y].track = generateTrack(mineMap, x, y);

                        if(mineMap[x,y].track == "")
                        {
                            Console.Write("");
                        }
                    }
                }
            }

            bool crash = false;
            int crashX = 0;
            int crashY = 0;

            while (!crash)
            {
                for (int y = 0; y < lineCount; y++)
                {
                    for(int x = 0; x < lineLength; x++)
                    {
                        if (mineMap[x, y].moved) continue;
                        if(mineMap[x,y].cart == "^")
                        {
                            if (mineMap[x, y - 1].cart.Any(m => m == '^' || m == '>' || m == '<' || m == 'v'))
                            {
                                crash = true;
                                crashX = x;
                                crashY = y - 1;
                                break;
                            }
                            else if (mineMap[x, y - 1].track == "/")
                            {
                                mineMap[x, y - 1].cart = ">";
                            }
                            else if (mineMap[x, y - 1].track == "\\")
                            {
                                mineMap[x, y - 1].cart = "<";
                            }
                            else if (mineMap[x, y - 1].track == "+")
                            {
                                mineMap[x, y - 1].cart = GetTurn(mineMap[x, y].cart, mineMap[x, y].direction);
                                mineMap[x, y].direction += 1;
                            }
                            else if(mineMap[x, y - 1].track == "|")
                            {
                                mineMap[x, y - 1].cart = "^";
                            }

                            mineMap[x, y - 1].direction = mineMap[x, y].direction;
                            mineMap[x, y - 1].moved = true;
                            mineMap[x, y].cart = "";
                            mineMap[x, y].direction = 0;
                            mineMap[x, y].moved = false;
                        }
                        else if (mineMap[x, y].cart == "v")
                        {
                            if (mineMap[x, y + 1].cart.Any(m => m == '^' || m == '>' || m == '<' || m == 'v'))
                            {
                                crash = true;
                                crashX = x;
                                crashY = y + 1;
                                break;
                            }
                            else if (mineMap[x, y + 1].track == "/")
                            {
                                mineMap[x, y + 1].cart = "<";
                            }
                            else if (mineMap[x, y + 1].track == "\\")
                            {
                                mineMap[x, y + 1].cart = ">";
                            }
                            else if (mineMap[x, y + 1].track == "+")
                            {
                                mineMap[x, y + 1].cart = GetTurn(mineMap[x, y].cart, mineMap[x, y].direction);
                                mineMap[x, y].direction += 1;
                            }
                            else if (mineMap[x, y + 1].track == "|")
                            {
                                mineMap[x, y + 1].cart = "v";
                            }

                            mineMap[x, y + 1].direction = mineMap[x, y].direction;
                            mineMap[x, y + 1].moved = true;
                            mineMap[x, y].cart = "";
                            mineMap[x, y].direction = 0;
                            mineMap[x, y].moved = false;
                        }
                        else if (mineMap[x, y].cart == "<")
                        {
                            if (mineMap[x - 1, y].cart.Any(m => m == '^' || m == '>' || m == '<' || m == 'v'))
                            {
                                crash = true;
                                crashX = x - 1;
                                crashY = y;
                                break;
                            }
                            else if (mineMap[x - 1, y].track == "/")
                            {
                                mineMap[x - 1, y].cart = "v";
                            }
                            else if (mineMap[x - 1, y].track == "\\")
                            {
                                mineMap[x - 1, y].cart = "^";
                            }
                            else if (mineMap[x - 1, y].track == "+")
                            {
                                mineMap[x - 1, y].cart = GetTurn(mineMap[x, y].cart, mineMap[x, y].direction);
                                mineMap[x, y].direction += 1;
                            }
                            else if (mineMap[x - 1, y].track == "-")
                            {
                                mineMap[x - 1, y].cart = "<";
                            }

                            mineMap[x - 1, y].direction = mineMap[x, y].direction;
                            mineMap[x - 1, y].moved = true;
                            mineMap[x, y].cart = "";
                            mineMap[x, y].direction = 0;
                            mineMap[x - 1, y].moved = false;
                        }
                        else if (mineMap[x, y].cart == ">")
                        {
                            if (mineMap[x + 1, y].cart.Any(m => m == '^' || m == '>' || m == '<' || m == 'v'))
                            {
                                crash = true;
                                crashX = x + 1;
                                crashY = y;
                                break;
                            }
                            else if (mineMap[x + 1, y].track == "/")
                            {
                                mineMap[x + 1, y].cart = "^";
                            }
                            else if (mineMap[x + 1, y].track == "\\")
                            {
                                mineMap[x + 1, y].cart = "v";
                            }
                            else if (mineMap[x + 1, y].track == "+")
                            {
                                mineMap[x + 1, y].cart = GetTurn(mineMap[x, y].cart, mineMap[x, y].direction);
                                mineMap[x, y].direction += 1;
                            }
                            else if (mineMap[x + 1, y].track == "-")
                            {
                                mineMap[x + 1, y].cart = ">";
                            }

                            mineMap[x + 1, y].direction = mineMap[x, y].direction;
                            mineMap[x + 1, y].moved = true;
                            mineMap[x, y].cart = "";
                            mineMap[x, y].direction = 0;
                            mineMap[x, y].moved = false;
                        }
                    }
                    if (crash) break;
                }

                for(int y = 0; y < lineCount; y++)
                {
                    for(int x = 0; x < lineLength; x++)
                    {
                        mineMap[x, y].moved = false;
                    }
                }
            }

            Console.WriteLine($"{crashX},{crashY}");
        }

        private static string generateTrack((string track, string cart, int direction, bool moved)[,] mineMap, int x, int y)
        {
            // 0: x, y - 1
            // 1: x, y + 1
            // 2: x - 1, y
            // 3: x + 1, y
            string[] adjacentChars = new string[4];
            adjacentChars[0] = (y == 0) ? " " : mineMap[x, y - 1].track;
            adjacentChars[1] = (y == 149) ? " " : mineMap[x, y + 1].track;
            adjacentChars[2] = (x == 0) ? " " : mineMap[x - 1, y].track;
            adjacentChars[3] = (x == 149) ? " " : mineMap[x + 1, y].track;

            if(adjacentChars[0] != " " && adjacentChars[1] != " " && adjacentChars[2] != " " && adjacentChars[3] != " ")
            {
                return "+";
            }
            else if(adjacentChars[0] == " " || adjacentChars[0] == "-" && adjacentChars[1] == " " || adjacentChars[1] == "-" && adjacentChars[2] != " " && adjacentChars[3] != " ")
            {
                return "-";
            }
            else if(adjacentChars[0] != " " && adjacentChars[1] != " " && adjacentChars[2] == " " || adjacentChars[2] == "|" && adjacentChars[3] == " " || adjacentChars[3] == "|")
            {
                return "|";
            }
            else if(adjacentChars[0] != " " && adjacentChars[1] == " " && adjacentChars[2] != " " && adjacentChars[3] == " ")
            {
                return "/";
            }
            else if(adjacentChars[0] != " " && adjacentChars[1] == " " && adjacentChars[2] == " " && adjacentChars[3] != " ")
            {
                return "\\";
            }
            else if(adjacentChars[0] == " " && adjacentChars[1] != " " && adjacentChars[2] != " " && adjacentChars[3] == " ")
            {
                return "\\";
            }
            else if(adjacentChars[0] == " " && adjacentChars[1] != " " && adjacentChars[2] == " " && adjacentChars[3] != " ")
            {
                return "/";
            }

            return "";
        }

        private static string GetTurn(string cart, int dir)
        {
            // 0: up
            // 1: left
            // 2: down
            // 3: right
            int turn = dir % 3;

            if(cart == "^")
            {
                return directions[0][turn].ToString();
            }
            else if(cart == "<")
            {
                return directions[1][turn].ToString();
            }
            else if(cart == "v")
            {
                return directions[2][turn].ToString();
            }
            else if(cart == ">")
            {
                return directions[3][turn].ToString();
            }
            return "";
        }
    }
}
