using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

// Based on A-Star-Sharp: https://github.com/davecusatis/A-Star-Sharp  Contributors: davecusatis, Brad Hannah
namespace AStarSharp
{
    public class Node
    {
        // Change this depending on what the desired size is for each element in the grid
        public Node Parent;
        public Vector2 Position;
        public float DistanceToTarget;
        public float Cost;
        public float Weight;
        public float F
        {
            get
            {
                if (DistanceToTarget != -1 && Cost != -1)
                    return DistanceToTarget + Cost;
                else
                    return -1;
            }
        }
        public bool Walkable;

        public Node(Vector2 pos, bool walkable, float weight = 1)
        {
            Parent = null;
            Position = pos;
            DistanceToTarget = -1;
            Cost = 1;
            Weight = weight;
            Walkable = walkable;
        }
    }

    public class Astar
    {
        List<List<Node>> Grid;
        int GridRows
        {
            get
            {
                return Grid[0].Count;
            }
        }
        int GridCols
        {
            get
            {
                return Grid.Count;
            }
        }

        public Astar(List<List<Node>> grid)
        {
            Grid = grid;
        }

        public Stack<Node> FindPath(Vector2 Start, Vector2 End)
        {
            Node start = new Node(new Vector2((int)(Start.X), (int)(Start.Y)), true);
            Node end = new Node(new Vector2((int)(End.X), (int)(End.Y)), true);

            Stack<Node> Path = new Stack<Node>();
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>();
            List<Node> adjacencies;
            Node current = start;

            // add start node to Open List
            OpenList.Add(start);

            while (OpenList.Count != 0 && !ClosedList.Exists(x => x.Position == end.Position))
            {
                current = OpenList[0];
                OpenList.Remove(current);
                ClosedList.Add(current);
                adjacencies = GetAdjacentNodes(current);


                foreach (Node n in adjacencies)
                {
                    if (!ClosedList.Contains(n) && n.Walkable)
                    {
                        if (!OpenList.Contains(n))
                        {
                            n.Parent = current;
                            n.DistanceToTarget = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                            n.Cost = n.Weight*100 + n.Parent.Cost; // HAD TO REINFORCE VALUE OF WEIGHT IN FUNCTION TO GET OPTIMAL PATH
                            OpenList.Add(n);
                            OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                        }
                    }
                }
            }

            // construct path, if end was not closed return null
            if (!ClosedList.Exists(x => x.Position == end.Position))
            {
                return null;
            }

            // if all good, return path
            Node temp = ClosedList[ClosedList.IndexOf(current)];
            if (temp == null) return null;
            do
            {
                Path.Push(temp);
                temp = temp.Parent;
            } while (temp != start && temp != null);
            return Path;
        }

        private List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> temp = new List<Node>();

            int row = (int)n.Position.Y;
            int col = (int)n.Position.X;

            if (row + 1 < GridRows)
            {
                temp.Add(Grid[row + 1][col]);
            }
            if (row - 1 >= 0)
            {
                temp.Add(Grid[row - 1][col]);
            }
            if (col - 1 >= 0)
            {
                temp.Add(Grid[row][col - 1]);
            }
            if (col + 1 < GridCols)
            {
                temp.Add(Grid[row][col + 1]);
            }

            return temp;
        }
    }
}

namespace Star_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World from Star 2!");

            var fileLines = File.ReadLines("../../../../input.txt");
            List<string> input = new List<string>();

            foreach (var item in fileLines)
            {
                input.Add(item);
            }

            // Parsing input data
            List<List<AStarSharp.Node>> map = new List<List<AStarSharp.Node>>();
            //List<List<char>> mapChar = new List<List<char>>();

            int y = 0, x = 0;
            foreach (var line in fileLines)
            {
                map.Add(new List<AStarSharp.Node>());

                x = 0;

                List<int> fileLineInt = line.Select(s => int.Parse(s.ToString())).ToList();
                foreach (var cavepart in fileLineInt)
                {
                    map[map.Count - 1].Add(new AStarSharp.Node(new Vector2(x, y), true, cavepart));

                    ++x;
                }

                //mapChar.Add(line.ToList());

                ++y;
            }

            var tempCount = map[0].Count;
            for (y = 0; y < map.Count; y++)
            {
                for (x = tempCount; x < tempCount * 5; x++)
                {
                    map[y].Add(new AStarSharp.Node(new Vector2(x, y), true, map[y][x - tempCount].Weight > 8 ? 1 : map[y][x - tempCount].Weight + 1));
                }
            }

            tempCount = map.Count;

            for (y = tempCount; y < tempCount * 5; y++)
            {
                var listToAdd = new List<AStarSharp.Node>();
                for (x = 0; x < map[0].Count; x++)
                {
                    listToAdd.Add(new AStarSharp.Node(new Vector2(x, y), true, map[y - tempCount][x].Weight > 8 ? 1 : map[y - tempCount][x].Weight + 1));
                }
                map.Add(listToAdd);
            }
            //

            // Searching for an answer
            AStarSharp.Astar astar = new AStarSharp.Astar(map);
            Stack<AStarSharp.Node> path = astar.FindPath(map[0][0].Position, map[map.Count - 1][map[0].Count - 1].Position);

            long totalRisk = 0;
            foreach (var item in path)
            {
                totalRisk += (long)map[(int)item.Position.Y][(int)item.Position.X].Weight;
            }

            //

            Console.WriteLine("Output: {0}", totalRisk);
        }
    }
}