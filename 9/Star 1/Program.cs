using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Star_1
{
    class Program
    {
        static private bool IsLowpoint(int x, int y, List<List<long>> map)
        {
            if (x != 0 && map[y][x] >= map[y][x - 1]) // left
                return false;
            if (x != map[0].Count - 1 && map[y][x] >= map[y][x + 1]) //right
                return false;
            if (y != 0 && map[y][x] >= map[y - 1][x]) //up
                return false;
            if (y != map.Count - 1 && map[y][x] >= map[y + 1][x]) //down
                return false;

            return true;
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
            List<List<long>> map = new List<List<long>>();
            foreach (var line in fileLines)
            {
                map.Add(line.ToCharArray().Select(s => long.Parse(s.ToString())).ToList());
            }
            //

            // Searching for an answer
            long risk = 0;
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    if (IsLowpoint(x, y, map))
                    {
                        risk += map[y][x] + 1;
                    }
                }
            }
            //

            Console.WriteLine("Output: {0}", risk);
        }
    }
}