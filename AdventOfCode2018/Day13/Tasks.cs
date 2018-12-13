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

            MineCart[,] mineMap = new MineCart[lineLength, lineCount];

            using (StreamReader reader = new StreamReader(inputPath))
            {
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = line[i];
                        string cart = "";
                        string track = "";

                        if (c == '^' || c == 'v' || c == '>' || c == '<')
                        {
                            cart = c.ToString();

                            if (cart == "^" || cart == "v")
                            {
                                track = "|";
                            }
                            else if (cart == "<" || cart == ">")
                            {
                                track = "-";
                            }
                        }
                        else
                        {
                            track = c.ToString();
                        }

                        mineMap[i, counter] = new MineCart(track, cart, 0, false);
                    }

                    counter++;
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
                        if (mineMap[x, y].Moved) continue;
                        if(mineMap[x,y].Cart == "^")
                        {
                            if (mineMap[x, y - 1].Cart.Any(m => m == '^' || m == '>' || m == '<' || m == 'v'))
                            {
                                crash = true;
                                crashX = x;
                                crashY = y - 1;
                                break;
                            }
                            else if (mineMap[x, y - 1].Track == "|")
                            {
                                mineMap[x, y - 1].Cart = "^";
                            }
                            else if (mineMap[x, y - 1].Track == "/")
                            {
                                mineMap[x, y - 1].Cart = ">";
                            }
                            else if (mineMap[x, y - 1].Track == "\\")
                            {
                                mineMap[x, y - 1].Cart = "<";
                            }
                            else if (mineMap[x, y - 1].Track == "+")
                            {
                                mineMap[x, y - 1].Cart = GetTurn(mineMap[x, y].Cart, mineMap[x, y].Direction);
                                mineMap[x, y].Direction += 1;
                            }

                            mineMap[x, y - 1].Direction = mineMap[x, y].Direction;
                            mineMap[x, y - 1].Moved = true;
                            mineMap[x, y].Cart = "";
                            mineMap[x, y].Direction = 0;
                            mineMap[x, y].Moved = false;
                        }
                        else if (mineMap[x, y].Cart == "v")
                        {
                            if (mineMap[x, y + 1].Cart.Any(m => m == '^' || m == '>' || m == '<' || m == 'v'))
                            {
                                crash = true;
                                crashX = x;
                                crashY = y + 1;
                                break;
                            }
                            else if (mineMap[x, y + 1].Track == "/")
                            {
                                mineMap[x, y + 1].Cart = "<";
                            }
                            else if (mineMap[x, y + 1].Track == "\\")
                            {
                                mineMap[x, y + 1].Cart = ">";
                            }
                            else if (mineMap[x, y + 1].Track == "+")
                            {
                                mineMap[x, y + 1].Cart = GetTurn(mineMap[x, y].Cart, mineMap[x, y].Direction);
                                mineMap[x, y].Direction += 1;
                            }
                            else if (mineMap[x, y + 1].Track == "|")
                            {
                                mineMap[x, y + 1].Cart = "v";
                            }

                            mineMap[x, y + 1].Direction = mineMap[x, y].Direction;
                            mineMap[x, y + 1].Moved = true;
                            mineMap[x, y].Cart = "";
                            mineMap[x, y].Direction = 0;
                            mineMap[x, y].Moved = false;
                        }
                        else if (mineMap[x, y].Cart == "<")
                        {
                            if (mineMap[x - 1, y].Cart.Any(m => m == '^' || m == '>' || m == '<' || m == 'v'))
                            {
                                crash = true;
                                crashX = x - 1;
                                crashY = y;
                                break;
                            }
                            else if (mineMap[x - 1, y].Track == "/")
                            {
                                mineMap[x - 1, y].Cart = "v";
                            }
                            else if (mineMap[x - 1, y].Track == "\\")
                            {
                                mineMap[x - 1, y].Cart = "^";
                            }
                            else if (mineMap[x - 1, y].Track == "+")
                            {
                                mineMap[x - 1, y].Cart = GetTurn(mineMap[x, y].Cart, mineMap[x, y].Direction);
                                mineMap[x, y].Direction += 1;
                            }
                            else if (mineMap[x - 1, y].Track == "-")
                            {
                                mineMap[x - 1, y].Cart = "<";
                            }

                            mineMap[x - 1, y].Direction = mineMap[x, y].Direction;
                            mineMap[x - 1, y].Moved = true;
                            mineMap[x, y].Cart = "";
                            mineMap[x, y].Direction = 0;
                            mineMap[x, y].Moved = false;
                        }
                        else if (mineMap[x, y].Cart == ">")
                        {
                            if (mineMap[x + 1, y].Cart.Any(m => m == '^' || m == '>' || m == '<' || m == 'v'))
                            {
                                crash = true;
                                crashX = x + 1;
                                crashY = y;
                                break;
                            }
                            else if (mineMap[x + 1, y].Track == "/")
                            {
                                mineMap[x + 1, y].Cart = "^";
                            }
                            else if (mineMap[x + 1, y].Track == "\\")
                            {
                                mineMap[x + 1, y].Cart = "v";
                            }
                            else if (mineMap[x + 1, y].Track == "+")
                            {
                                mineMap[x + 1, y].Cart = GetTurn(mineMap[x, y].Cart, mineMap[x, y].Direction);
                                mineMap[x, y].Direction += 1;
                            }
                            else if (mineMap[x + 1, y].Track == "-")
                            {
                                mineMap[x + 1, y].Cart = ">";
                            }

                            mineMap[x + 1, y].Direction = mineMap[x, y].Direction;
                            mineMap[x + 1, y].Moved = true;
                            mineMap[x, y].Cart = "";
                            mineMap[x, y].Direction = 0;
                            mineMap[x, y].Moved = false;
                        }
                    }
                    if (crash) break;
                }

                for(int y = 0; y < lineCount; y++)
                {
                    for(int x = 0; x < lineLength; x++)
                    {
                        mineMap[x, y].Moved = false;
                    }
                }
            }

            Console.WriteLine($"{crashX},{crashY}");
        }

        public static void Task2()
        {
            string line;
            int lineCount = File.ReadLines(inputPath).Count();
            int lineLength = 0;
            int cartCounter = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                lineLength = reader.ReadLine().Length;
            }

            MineCart[,] mineMap = new MineCart[lineLength, lineCount];

            using (StreamReader reader = new StreamReader(inputPath))
            {
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = line[i];
                        string cart = "";
                        string track = "";

                        if (c == '^' || c == 'v' || c == '>' || c == '<')
                        {
                            cart = c.ToString();
                            cartCounter++;

                            if (cart == "^" || cart == "v")
                            {
                                track = "|";
                            }
                            else if (cart == "<" || cart == ">")
                            {
                                track = "-";
                            }
                        }
                        else
                        {
                            track = c.ToString();
                        }

                        mineMap[i, counter] = new MineCart(track, cart, 0, false);
                    }

                    counter++;
                }
            }

            while (cartCounter > 1)
            {
                for (int y = 0; y < lineCount; y++)
                {
                    for (int x = 0; x < lineLength; x++)
                    {
                        if (mineMap[x, y].Moved) continue;
                        if (mineMap[x, y].Cart == "^")
                        {
                            if (mineMap[x, y - 1].Cart != "")
                            {
                                mineMap[x, y - 1].Cart = "";
                                mineMap[x, y - 1].Moved = false;
                                mineMap[x, y - 1].Direction = 0;
                                mineMap[x, y].Cart = "";
                                mineMap[x, y].Moved = false;
                                mineMap[x, y].Direction = 0;
                                cartCounter -= 2;
                                continue;
                            }
                            else if (mineMap[x, y - 1].Track == "|")
                            {
                                mineMap[x, y - 1].Cart = "^";
                            }
                            else if (mineMap[x, y - 1].Track == "/")
                            {
                                mineMap[x, y - 1].Cart = ">";
                            }
                            else if (mineMap[x, y - 1].Track == "\\")
                            {
                                mineMap[x, y - 1].Cart = "<";
                            }
                            else if (mineMap[x, y - 1].Track == "+")
                            {
                                mineMap[x, y - 1].Cart = GetTurn(mineMap[x, y].Cart, mineMap[x, y].Direction);
                                mineMap[x, y].Direction += 1;
                            }

                            mineMap[x, y - 1].Direction = mineMap[x, y].Direction;
                            mineMap[x, y - 1].Moved = true;
                            mineMap[x, y].Cart = "";
                            mineMap[x, y].Direction = 0;
                            mineMap[x, y].Moved = false;
                        }
                        else if (mineMap[x, y].Cart == "v")
                        {
                            if (mineMap[x, y + 1].Cart != "")
                            {
                                mineMap[x, y + 1].Cart = "";
                                mineMap[x, y + 1].Moved = false;
                                mineMap[x, y + 1].Direction = 0;
                                mineMap[x, y].Cart = "";
                                mineMap[x, y].Moved = false;
                                mineMap[x, y].Direction = 0;
                                cartCounter -= 2;
                                continue;
                            }
                            else if (mineMap[x, y + 1].Track == "/")
                            {
                                mineMap[x, y + 1].Cart = "<";
                            }
                            else if (mineMap[x, y + 1].Track == "\\")
                            {
                                mineMap[x, y + 1].Cart = ">";
                            }
                            else if (mineMap[x, y + 1].Track == "+")
                            {
                                mineMap[x, y + 1].Cart = GetTurn(mineMap[x, y].Cart, mineMap[x, y].Direction);
                                mineMap[x, y].Direction += 1;
                            }
                            else if (mineMap[x, y + 1].Track == "|")
                            {
                                mineMap[x, y + 1].Cart = "v";
                            }

                            mineMap[x, y + 1].Direction = mineMap[x, y].Direction;
                            mineMap[x, y + 1].Moved = true;
                            mineMap[x, y].Cart = "";
                            mineMap[x, y].Direction = 0;
                            mineMap[x, y].Moved = false;
                        }
                        else if (mineMap[x, y].Cart == "<")
                        {
                            if (mineMap[x - 1, y].Cart != "")
                            {
                                mineMap[x - 1, y].Cart = "";
                                mineMap[x - 1, y].Moved = false;
                                mineMap[x - 1, y].Direction = 0;
                                mineMap[x, y].Cart = "";
                                mineMap[x, y].Moved = false;
                                mineMap[x, y].Direction = 0;
                                cartCounter -= 2;
                                continue;
                            }
                            else if (mineMap[x - 1, y].Track == "/")
                            {
                                mineMap[x - 1, y].Cart = "v";
                            }
                            else if (mineMap[x - 1, y].Track == "\\")
                            {
                                mineMap[x - 1, y].Cart = "^";
                            }
                            else if (mineMap[x - 1, y].Track == "+")
                            {
                                mineMap[x - 1, y].Cart = GetTurn(mineMap[x, y].Cart, mineMap[x, y].Direction);
                                mineMap[x, y].Direction += 1;
                            }
                            else if (mineMap[x - 1, y].Track == "-")
                            {
                                mineMap[x - 1, y].Cart = "<";
                            }

                            mineMap[x - 1, y].Direction = mineMap[x, y].Direction;
                            mineMap[x - 1, y].Moved = true;
                            mineMap[x, y].Cart = "";
                            mineMap[x, y].Direction = 0;
                            mineMap[x, y].Moved = false;
                        }
                        else if (mineMap[x, y].Cart == ">")
                        {
                            if (mineMap[x + 1, y].Cart != "")
                            {
                                mineMap[x + 1, y].Cart = "";
                                mineMap[x + 1, y].Moved = false;
                                mineMap[x + 1, y].Direction = 0;
                                mineMap[x, y].Cart = "";
                                mineMap[x, y].Moved = false;
                                mineMap[x, y].Direction = 0;
                                cartCounter -= 2;
                                continue;
                            }
                            else if (mineMap[x + 1, y].Track == "/")
                            {
                                mineMap[x + 1, y].Cart = "^";
                            }
                            else if (mineMap[x + 1, y].Track == "\\")
                            {
                                mineMap[x + 1, y].Cart = "v";
                            }
                            else if (mineMap[x + 1, y].Track == "+")
                            {
                                mineMap[x + 1, y].Cart = GetTurn(mineMap[x, y].Cart, mineMap[x, y].Direction);
                                mineMap[x, y].Direction += 1;
                            }
                            else if (mineMap[x + 1, y].Track == "-")
                            {
                                mineMap[x + 1, y].Cart = ">";
                            }

                            mineMap[x + 1, y].Direction = mineMap[x, y].Direction;
                            mineMap[x + 1, y].Moved = true;
                            mineMap[x, y].Cart = "";
                            mineMap[x, y].Direction = 0;
                            mineMap[x, y].Moved = false;
                        }
                    }
                }

                for (int y = 0; y < lineCount; y++)
                {
                    for (int x = 0; x < lineLength; x++)
                    {
                        mineMap[x, y].Moved = false;
                    }
                }
            }

            for (int y = 0; y < lineCount; y++)
            {
                for (int x = 0; x < lineLength; x++)
                {
                    if (mineMap[x, y].Cart != "")
                    {
                        Console.WriteLine($"{x},{y}");
                    }
                }
            }  
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

    class MineCart
    {
        private string track;
        private string cart;
        private int direction;
        private bool moved;

        public MineCart(string t, string c, int d, bool m)
        {
            Track = t;
            Cart = c;
            Direction = d;
            Moved = m;
        }

        public string Track { get => track; set => track = value; }
        public string Cart { get => cart; set => cart = value; }
        public int Direction { get => direction; set => direction = value; }
        public bool Moved { get => moved; set => moved = value; }
    }
}
