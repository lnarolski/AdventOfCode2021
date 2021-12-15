using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

// A-Star-Sharp: https://github.com/davecusatis/A-Star-Sharp  Contributors: davecusatis, Brad Hannah
namespace AStarSharp
{
    public class Node
    {
        // Change this depending on what the desired size is for each element in the grid
        public static int NODE_SIZE = 1;
        public Node Parent;
        public Vector2 Position;
        public Vector2 Center
        {
            get
            {
                return new Vector2(Position.X + NODE_SIZE / 2, Position.Y + NODE_SIZE / 2);
            }
        }
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

        public Node(Vector2 pos, bool walkable, float weight = 1, float cost = 1)
        {
            Parent = null;
            Position = pos;
            DistanceToTarget = -1;
            Cost = cost;
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
            Node start = new Node(new Vector2((int)(Start.X / Node.NODE_SIZE), (int)(Start.Y / Node.NODE_SIZE)), true);
            Node end = new Node(new Vector2((int)(End.X / Node.NODE_SIZE), (int)(End.Y / Node.NODE_SIZE)), true);

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
                            n.Cost = n.Weight + n.Parent.Cost;
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
                temp.Add(Grid[col][row + 1]);
            }
            if (row - 1 >= 0)
            {
                temp.Add(Grid[col][row - 1]);
            }
            if (col - 1 >= 0)
            {
                temp.Add(Grid[col - 1][row]);
            }
            if (col + 1 < GridCols)
            {
                temp.Add(Grid[col + 1][row]);
            }

            return temp;
        }
    }
}

namespace Star_1
{
    class Program
    {
        class Cavepart
        {
            public int riskLevel;
            public bool _checked = false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World from Star 1!");

            var fileLines = File.ReadLines("../../../../input.txt");
            List<string> input = new List<string>();

            foreach (var item in fileLines)
            {
                input.Add(item);
            }

            // Parsing input data
            List<List<AStarSharp.Node>> map = new List<List<AStarSharp.Node>>();
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
                
                ++y;
            }
            //

            // Searching for an answer
            //Tuple<int, int> start = new Tuple<int, int>(0, 0);
            //Tuple<int, int> end = new Tuple<int, int>(map.Count - 1, map[0].Count -1);
            //Tuple<int, int> currentPostition = start;

            //bool found;
            //do
            //{
            //    found = true;

            //    if (currentPostition.Item1 != 0 && !map[currentPostition.Item1 - 1][currentPostition.Item2]._checked) //up
            //    {
                    
            //    }
            //    if (currentPostition.Item1 != map.Count - 1 && !map[currentPostition.Item1 + 1][currentPostition.Item2]._checked) //down
            //    {

            //    }
            //    if (currentPostition.Item2 != 0 && !map[currentPostition.Item1][currentPostition.Item2 - 1]._checked) //left
            //    {

            //    }
            //    if (currentPostition.Item2 != map[0].Count - 1 && !map[currentPostition.Item1][currentPostition.Item2 + 1]._checked) //right
            //    {

            //    }


            //} while (!found);

            AStarSharp.Astar astar = new AStarSharp.Astar(map);
            Stack<AStarSharp.Node> path = astar.FindPath(map[0][0].Position, map[map.Count - 1][map[0].Count - 1].Position);

            long totalRisk = 0;
            totalRisk = (long) path.Peek().Cost;

            Console.WriteLine("Path: ");
            foreach (var item in path)
            {
                Console.WriteLine("{0}: {1}", item.Position, item.Weight);
            }
            //

            Console.WriteLine("Output: {0}", totalRisk);
        }
    }
}