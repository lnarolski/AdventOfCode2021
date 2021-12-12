using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Star_1
{
    class Program
    {
        class Octopuse
        {
            public long energyValue;
            public bool flashed;
        }

        static void Flash(int x, int y, List<List<Octopuse>> map)
        {
            if (map[y][x].energyValue <= 9)
                return;

            map[y][x].flashed = true;
            if (x != 0) // left
            {
                ++map[y][x - 1].energyValue;
            }
            if (x != map[0].Count - 1) //right
            {
                ++map[y][x + 1].energyValue;
            }
            if (y != 0) //up
            {
                ++map[y - 1][x].energyValue;
            }
            if (y != map.Count - 1) //down
            {
                ++map[y + 1][x].energyValue;
            }

            if (x != 0 && y != 0) // left-up
            {
                ++map[y - 1][x - 1].energyValue;
            }
            if (x != map[0].Count - 1 && y != 0) //right-up
            {
                ++map[y - 1][x + 1].energyValue;
            }
            if (y != map.Count - 1 && x != 0) //left-down
            {
                ++map[y + 1][x - 1].energyValue;
            }
            if (y != map.Count - 1 && x != map[0].Count - 1) //right-down
            {
                ++map[y + 1][x + 1].energyValue;
            }
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
            Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
            foreach (var line in fileLines)
            {
                string[] temp = line.Split('-');
                if (!connections.ContainsKey(temp[0]))
                {
                    connections.Add(temp[0], new List<string>() { temp[1] });
                }
                else
                {
                    connections[temp[0]].Add(temp[1]);
                }

                if (!connections.ContainsKey(temp[1]))
                {
                    connections.Add(temp[1], new List<string>() { temp[0] });
                }
                else
                {
                    connections[temp[1]].Add(temp[0]);
                }
            }
            //

            // Searching for an answer
            List<List<string>> paths = new List<List<string>>();
            bool stop = false;
            string currentVert = "start";
            List<string> currentPath = new List<string>() { "start" };
            FindPath(currentVert, connections, paths, currentPath);
            //

            Console.WriteLine("Output: {0}", paths.Count);
        }

        private static void FindPath(string currentVert,  Dictionary<string, List<string>> connections, List<List<string>> paths, List<string> currentPath)
        {
            foreach (var connection in connections[currentVert])
            {
                List<string> temp = new List<string>(currentPath);

                if (connection.Any(c => char.IsLower(c)))
                {
                    if (temp.Contains(connection))
                    {
                        continue;
                    }
                }

                temp.Add(connection);
                if (connection == "end")
                {
                    paths.Add(temp);
                    continue;
                }

                FindPath(connection, connections, paths, temp);
            }
        }
    }
}