using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            //PrintPaths(paths);
            //

            Console.WriteLine("Output: {0}", paths.Count);
        }

        private static void PrintPaths(List<List<string>> paths)
        {
            Console.WriteLine();
            foreach (var path in paths)
            {
                foreach (var vert in path)
                {
                    Console.Write(vert + ",");
                }
                Console.WriteLine();
            }
        }

        private static void FindPath(string currentVert, Dictionary<string, List<string>> connections, List<List<string>> paths, List<string> currentPath, bool smallCaveVisitedTwice = false)
        {
            foreach (var connection in connections[currentVert])
            {
                if (connection == "start")
                    continue;

                bool smallCaveVisitedTwiceCopy = smallCaveVisitedTwice;
                List<string> temp = new List<string>(currentPath);

                if (connection.Any(c => char.IsLower(c)))
                {
                    int counter = temp.FindAll(delegate (string s) { return s == connection; }).Count;
                    if (counter >= 2)
                    {
                        continue;
                    }
                    if (smallCaveVisitedTwice && temp.Contains(connection))
                        continue;
                    if (counter == 1)
                        smallCaveVisitedTwiceCopy = true;
                }

                temp.Add(connection);
                if (connection == "end")
                {
                    paths.Add(temp);
                    continue;
                }

                FindPath(connection, connections, paths, temp, smallCaveVisitedTwiceCopy);
            }
        }
    }
}