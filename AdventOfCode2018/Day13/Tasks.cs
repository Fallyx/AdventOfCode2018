using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Numerics;

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
        private static Vector2 crashPos = new Vector2(-1, -1);

        public static void Task1()
        {
            int lineCount = File.ReadLines(inputPath).Count();
            int lineLength = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                lineLength = reader.ReadLine().Length;
            }

            string[,] trackMap = new string[lineLength, lineCount];
            List<MineCart> mineCarts = new List<MineCart>();

            Setup(trackMap, mineCarts);

            while (crashPos.X == -1 && crashPos.Y == -1)
            {
                for(int i = 0; i < mineCarts.Count; i++)
                {
                    int x = (int)mineCarts[i].Pos.X;
                    int y = (int)mineCarts[i].Pos.Y;

                    if (mineCarts[i].Cart == "^")
                    {
                        MoveMineCart(mineCarts, i, trackMap, directions[0], x, y - 1, true);
                    }
                    else if (mineCarts[i].Cart == "v")
                    {
                        MoveMineCart(mineCarts, i, trackMap, directions[2], x, y + 1, true);
                    }
                    else if (mineCarts[i].Cart == "<")
                    {
                        MoveMineCart(mineCarts, i, trackMap, directions[1], x - 1, y, true);
                    }
                    else if (mineCarts[i].Cart == ">")
                    {
                        MoveMineCart(mineCarts, i, trackMap, directions[3], x + 1, y, true);
                    }

                    if (crashPos.X != -1 && crashPos.Y != -1) break;
                }

                mineCarts = mineCarts.OrderBy(y => y.Pos.Y).ThenBy(x => x.Pos.X).ToList();
            }

            Console.WriteLine($"{crashPos.X},{crashPos.Y}");
        }

        public static void Task2()
        {
            int lineCount = File.ReadLines(inputPath).Count();
            int lineLength = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                lineLength = reader.ReadLine().Length;
            }

            string[,] trackMap = new string[lineLength, lineCount];
            List<MineCart> mineCarts = new List<MineCart>();

            Setup(trackMap, mineCarts);

            while (mineCarts.Count(c => c.Pos.X != -1 && c.Pos.Y != -1) > 1)
            {
                for (int i = 0; i < mineCarts.Count; i++)
                {
                    if (mineCarts[i].Pos.X == -1 && mineCarts[i].Pos.Y == -1) continue;
                    int x = (int)mineCarts[i].Pos.X;
                    int y = (int)mineCarts[i].Pos.Y;

                    if (mineCarts[i].Cart == "^")
                    {
                        MoveMineCart(mineCarts, i, trackMap, directions[0], x, y - 1);
                    }
                    else if (mineCarts[i].Cart == "v")
                    {
                        MoveMineCart(mineCarts, i, trackMap, directions[2], x, y + 1);
                    }
                    else if (mineCarts[i].Cart == "<")
                    {
                        MoveMineCart(mineCarts, i, trackMap, directions[1], x - 1, y);
                    }
                    else if (mineCarts[i].Cart == ">")
                    {
                        MoveMineCart(mineCarts, i, trackMap, directions[3], x + 1, y);
                    }
                }

                mineCarts = mineCarts.OrderBy(y => y.Pos.Y).ThenBy(x => x.Pos.X).ToList();
            }

            MineCart last = mineCarts.Find(c => c.Pos.X != -1 && c.Pos.Y != -1);
            Console.WriteLine($"{last.Pos.X},{last.Pos.Y}");
        }

        private static void Setup(string[,] trackMap, List<MineCart> mineCarts)
        {
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = line[i];
                        string cart = "";
                        string track = "";

                        if (c == '^' || c == 'v')
                        {
                            cart = c.ToString();
                            track = "|";
                            mineCarts.Add(new MineCart(cart, i, counter));
                        }
                        else if (c == '>' || c == '<')
                        {
                            cart = c.ToString();
                            track = "-";
                            mineCarts.Add(new MineCart(cart, i, counter));
                        }
                        else
                        {
                            track = c.ToString();
                        }

                        trackMap[i, counter] = track;
                    }

                    counter++;
                }
            }
        }

        private static void MoveMineCart(List<MineCart> mineCarts, int i, string[,] trackMap, string direction, int x, int y, bool StopOnCrash = false)
        {
            if (mineCarts.Any(c => c.Pos.X == x && c.Pos.Y == y))
            {
                if(StopOnCrash)
                {
                    crashPos = new Vector2(x, y);
                    return;
                }

                mineCarts[i].Pos = new Vector2(-1, -1);
                int idx = mineCarts.FindIndex(c => c.Pos.X == x && c.Pos.Y == y);
                mineCarts[idx].Pos = new Vector2(-1, -1);
                return;
            }
            else if (trackMap[x, y] == "/")
            {
                mineCarts[i].Cart = GetDirection(direction, "/");
            }
            else if (trackMap[x, y] == "\\")
            {
                mineCarts[i].Cart = GetDirection(direction, "\\");
            }
            else if (trackMap[x, y] == "+")
            {
                mineCarts[i].Cart = GetTurn(mineCarts[i].Cart, mineCarts[i].Direction);
                mineCarts[i].Direction += 1;
            }

            mineCarts[i].Pos = new Vector2(x, y);
        }

        private static string GetDirection(string direction, string turn)
        {
            if (turn == "/" && (direction[1] == '^' || direction[1] == 'v'))
            {
                return direction[2].ToString();
            }
            else if (turn == "\\" && (direction[1] == '^' || direction[1] == 'v'))
            {
                return direction[0].ToString();
            }
            if (turn == "\\" && (direction[1] == '<' || direction[1] == '>'))
            {
                return direction[2].ToString();
            }
            else if (turn == "/" && (direction[1] == '<' || direction[1] == '>'))
            {
                return direction[0].ToString();
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

    class MineCart
    {
        private Vector2 pos;
        private string cart;
        private int direction;

        public MineCart(string c,  int x, int y)
        {
            Pos = new Vector2(x, y);
            Cart = c;
            Direction = 0;
        }

        public string Cart { get => cart; set => cart = value; }
        public int Direction { get => direction; set => direction = value; }
        public Vector2 Pos { get => pos; set => pos = value; }
    }
}
