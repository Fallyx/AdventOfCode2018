using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace AdventOfCode2018.Day15
{
    class Tasks
    {
        const string inputPath = @"Day15/Input.txt";

        public static void Task1()
        {
            List<Unit> units = new List<Unit>();
            BattleMap bMap = new BattleMap();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = line[i];

                        if (c == 'E')
                        {
                            units.Add(new Unit(3, new Vector2(i, counter), Unit.UnitType.Elf));
                        }
                        else if (c == 'G')
                        {
                            units.Add(new Unit(3, new Vector2(i, counter), Unit.UnitType.Goblin));
                        }
                        
                        if(c != '#')
                        {
                            bMap.AddNode(new Vector2(i, counter));
                            bMap.AddEdge(new Vector2(i, counter), new Vector2(i, counter - 1));
                            bMap.AddEdge(new Vector2(i, counter), new Vector2(i - 1, counter));
                        }
                    }
                    counter++;
                }
            }

            int rounds = 0;

            while(units.Any(f => f.Type == Unit.UnitType.Elf) && units.Any(f => f.Type == Unit.UnitType.Goblin))
            {
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i].IsDead) continue;

                    int x = (int)units[i].Pos.X;
                    int y = (int)units[i].Pos.Y;

                    Unit.UnitType enemy = units[i].Type == Unit.UnitType.Elf ? Unit.UnitType.Goblin : Unit.UnitType.Elf;

                    List<Unit> enemies = EnemiesAround(units, units[i], enemy);
                    if (enemies.Count == 0)
                    {
                        units[i].Pos = bMap.NextStep(units[i], units);

                        enemies = EnemiesAround(units, units[i], enemy);
                    }

                    if (enemies.Count > 0)
                    {
                        Unit lowestEnemy = enemies.OrderBy(h => h.Hp).ThenBy(py => py.Pos.Y).ThenBy(px => px.Pos.X).First();
                        if (lowestEnemy.Hp - units[i].Dmg > 0) lowestEnemy.Hp -= units[i].Dmg;
                        else lowestEnemy.IsDead = true;
                    }
                }

                units.RemoveAll(u => u.IsDead);

                units = units.OrderBy(y => y.Pos.Y).ThenBy(x => x.Pos.X).ToList();
                rounds++;
            }

            rounds--;
            Console.WriteLine(rounds * units.Sum(u => u.Hp));
        }

        public static void Task2()
        {
            List<Unit> units = new List<Unit>();
            
            int atkDmg = 1;
            int rnds = 0;
            int hpLeft = 0;
            int nrOfElves = 0;

            while (true)
            {
                units.Clear();
                BattleMap bMap = new BattleMap();

                using (StreamReader reader = new StreamReader(inputPath))
                {
                    string line;
                    int counter = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            char c = line[i];

                            if (c == 'E')
                            {
                                units.Add(new Unit(3 + atkDmg, new Vector2(i, counter), Unit.UnitType.Elf));
                            }
                            else if (c == 'G')
                            {
                                units.Add(new Unit(3, new Vector2(i, counter), Unit.UnitType.Goblin));
                            }

                            if (c != '#')
                            {
                                bMap.AddNode(new Vector2(i, counter));
                                bMap.AddEdge(new Vector2(i, counter), new Vector2(i, counter - 1));
                                bMap.AddEdge(new Vector2(i, counter), new Vector2(i - 1, counter));
                            }
                        }
                        counter++;
                    }

                    nrOfElves = units.Count(f => f.Type == Unit.UnitType.Elf);
                }

                int rounds = 0;

                while (units.Any(f => f.Type == Unit.UnitType.Elf) && units.Any(f => f.Type == Unit.UnitType.Goblin))
                {
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (units[i].IsDead) continue;

                        int x = (int)units[i].Pos.X;
                        int y = (int)units[i].Pos.Y;

                        Unit.UnitType enemy = units[i].Type == Unit.UnitType.Elf ? Unit.UnitType.Goblin : Unit.UnitType.Elf;

                        List<Unit> enemies = EnemiesAround(units, units[i], enemy);
                        if (enemies.Count == 0)
                        {
                            units[i].Pos = bMap.NextStep(units[i], units);

                            enemies = EnemiesAround(units, units[i], enemy);
                        }

                        if (enemies.Count > 0)
                        {
                            Unit lowestEnemy = enemies.OrderBy(h => h.Hp).ThenBy(py => py.Pos.Y).ThenBy(px => px.Pos.X).First();
                            if (lowestEnemy.Hp - units[i].Dmg > 0) lowestEnemy.Hp -= units[i].Dmg;
                            else lowestEnemy.IsDead = true;
                        }
                    }

                    units.RemoveAll(u => u.IsDead);

                    units = units.OrderBy(y => y.Pos.Y).ThenBy(x => x.Pos.X).ToList();
                    rounds++;
                }

                rounds--;
                atkDmg++;

                if (units.Count(f => f.Type == Unit.UnitType.Elf) == nrOfElves && !units.Any(f => f.Type == Unit.UnitType.Goblin))
                {
                    rnds = rounds;
                    hpLeft = units.Sum(u => u.Hp);
                    break;
                }
            }

            Console.WriteLine($"{rnds} * {hpLeft} = {rnds * hpLeft}");
        }

        private static List<Unit> EnemiesAround(List<Unit> units, Unit currentUnit, Unit.UnitType enemyType)
        {
            List<Unit> unitsAround = new List<Unit>();

            int x = (int)currentUnit.Pos.X;
            int y = (int)currentUnit.Pos.Y;

            Unit top = units.Find(u => u.Type == enemyType && !u.IsDead && u.Pos.X == x && u.Pos.Y == y - 1);
            if (top != null) unitsAround.Add(top);

            Unit left = units.Find(u => u.Type == enemyType && !u.IsDead && u.Pos.X == x - 1 && u.Pos.Y == y);
            if (left != null) unitsAround.Add(left);

            Unit right = units.Find(u => u.Type == enemyType && !u.IsDead && u.Pos.X == x + 1 && u.Pos.Y == y);
            if (right != null) unitsAround.Add(right);

            Unit bottom = units.Find(u => u.Type == enemyType && !u.IsDead && u.Pos.X == x && u.Pos.Y == y + 1);
            if (bottom != null) unitsAround.Add(bottom);

            return unitsAround;
        }
    }

    class BattleMap
    {
        private Dictionary<Vector2, HashSet<Vector2>> adjacentList;
        
        internal BattleMap() 
        {
            AdjacentList = new Dictionary<Vector2, HashSet<Vector2>>();
        }

        internal Dictionary<Vector2, HashSet<Vector2>> AdjacentList { get => adjacentList; private set => adjacentList = value; }

        internal void AddNode(Vector2 node)
        {
            AdjacentList.Add(node, new HashSet<Vector2>());
        }

        internal void AddEdge(Vector2 node1, Vector2 node2)
        {
            if(AdjacentList.ContainsKey(node1) && AdjacentList.ContainsKey(node2))
            {
                AdjacentList[node1].Add(node2);
                AdjacentList[node2].Add(node1);
            }
        }

        internal Vector2 NextStep(Unit currentUnit, List<Unit> units)
        {
            Vector2 currentNode = currentUnit.Pos;
            List<Vector2> destinationNodes = new List<Vector2>();
            List<Vector2> blockedPos = new List<Vector2>();

            foreach(Unit f in units)
            {
                if (f == currentUnit || f.IsDead) continue;

                blockedPos.Add(f.Pos);

                if (f.Type == currentUnit.Type) continue;

                Vector2 top = new Vector2(f.Pos.X, f.Pos.Y - 1);
                Vector2 left = new Vector2(f.Pos.X - 1, f.Pos.Y);
                Vector2 right = new Vector2(f.Pos.X + 1, f.Pos.Y);
                Vector2 bottom = new Vector2(f.Pos.X, f.Pos.Y + 1);

                if (AdjacentList.ContainsKey(top) && !units.Any(u => !u.IsDead && u.Pos.X == top.X && u.Pos.Y == top.Y)) destinationNodes.Add(top);
                if (AdjacentList.ContainsKey(left) && !units.Any(u => !u.IsDead && u.Pos.X == left.X && u.Pos.Y == left.Y)) destinationNodes.Add(left);
                if (AdjacentList.ContainsKey(right) && !units.Any(u => !u.IsDead && u.Pos.X == right.X && u.Pos.Y == right.Y)) destinationNodes.Add(right);
                if (AdjacentList.ContainsKey(bottom) && !units.Any(u => !u.IsDead && u.Pos.X == bottom.X && u.Pos.Y == bottom.Y)) destinationNodes.Add(bottom);
            }

            if(destinationNodes.Count == 0)
            {
                return currentUnit.Pos;
            }

            int shortestSteps = int.MaxValue;
            Vector2 nextStep = new Vector2();
            for(int i = 0; i < destinationNodes.Count; i++)
            {
                (List<Vector2> path, int steps) = ShortestPath(currentNode, destinationNodes[i], blockedPos);

                if(steps < shortestSteps)
                {
                    shortestSteps = steps;
                    nextStep = path.Last();
                }
            }
            if (nextStep.X == 0 && nextStep.Y == 0) return currentUnit.Pos;
            
            return nextStep;
        }

        private (List<Vector2>, int) ShortestPath(Vector2 current, Vector2 destiantion, List<Vector2> blockedPos)
        {
            var previous = new Dictionary<Vector2, Vector2>();
            Queue<Vector2> queue = new Queue<Vector2>();

            bool found = false;
            queue.Enqueue(current);

            while (!found && queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var neighbor in AdjacentList[node])
                {
                    if (previous.ContainsKey(neighbor)) continue;
                    if (blockedPos.Any(b => b == neighbor)) continue;

                    previous.Add(neighbor, node);
                    if (neighbor == destiantion)
                    {
                        found = true;
                        break;
                    }

                    queue.Enqueue(neighbor);
                }
            }

            List<Vector2> path = new List<Vector2>();
            if (!found) return (path, int.MaxValue);
            
            int steps = 0;

            Vector2 c = previous.Last().Key;
            while(c != current)
            {
                path.Add(c);
                c = previous[c];
                steps++;
            }

            return (path , steps);
        }
    }

    internal class Unit
    {
        internal enum UnitType { Elf, Goblin }

        private int hp;
        private int dmg;
        private Vector2 pos;
        private UnitType type;
        private bool isDead;

        public Unit(int dmg, Vector2 pos, UnitType type)
        {
            Hp = 200;
            Dmg = dmg;
            Pos = pos;
            Type = type;
            IsDead = false;
        }

        public int Hp { get => hp; set => hp = value; }
        public int Dmg { get => dmg; set => dmg = value; }
        internal UnitType Type { get => type; set => type = value; }
        public Vector2 Pos { get => pos; set => pos = value; }
        public bool IsDead { get => isDead; set => isDead = value; }
    }
}
