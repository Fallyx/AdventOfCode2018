using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode2018.Day20
{
    class Tasks
    {
        const string inputPath = @"Day20/Input.txt";
        private static List<Room> rooms;

        public static void Task1and2()
        {
            string direction;
            Stack<Room> parentRoom = new Stack<Room>();
            rooms = new List<Room>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                direction = reader.ReadToEnd();
            }

            Room currentRoom = new Room(0, 0, 0);
            parentRoom.Push(currentRoom);
            rooms.Add(currentRoom);

            for (int i = 0; i < direction.Length; i++)
            {
                char c = direction[i];

                if (c == '(')
                {
                    parentRoom.Push(currentRoom);
                }
                else if(c == '|')
                {
                    currentRoom = parentRoom.Peek();
                }
                else if(c == ')')
                {
                    currentRoom = parentRoom.Pop();
                }
                else if(c == 'N')
                {
                    currentRoom = AddRoom(currentRoom.X, currentRoom.Y - 1, currentRoom);
                }
                else if(c == 'S')
                {
                    currentRoom = AddRoom(currentRoom.X, currentRoom.Y + 1, currentRoom);
                }
                else if(c == 'E')
                {
                    currentRoom = AddRoom(currentRoom.X + 1, currentRoom.Y, currentRoom);
                }
                else if(c == 'W')
                {
                    currentRoom = AddRoom(currentRoom.X - 1, currentRoom.Y, currentRoom);
                }
            }

            Console.WriteLine(rooms.Max(r => r.Distance));
            Console.WriteLine(rooms.Count(r => r.Distance >= 1000) - 2);
        }

        private static Room AddRoom(int x, int y, Room parent)
        {
            if(rooms.Any(r => r.X == x && r.Y == y)) return parent;

            Room newRoom = new Room(x, y, parent.Distance + 1);
            rooms.Add(newRoom);

            parent.RoomsNextDoor.Add(newRoom);
            return newRoom;
        }
    }

    class Room
    {
        private int x;
        private int y;
        private List<Room> roomsNextDoor;
        private int dist;

        public Room(int x, int y, int dist)
        {
            X = x;
            Y = y;
            Distance = dist;
            RoomsNextDoor = new List<Room>();
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Distance { get => dist; set => dist = value; }
        internal List<Room> RoomsNextDoor { get => roomsNextDoor; set => roomsNextDoor = value; }
    }
}
