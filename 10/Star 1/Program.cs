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

            //

            // Searching for an answer

            //

            Console.WriteLine("Output: {0}");
        }
    }
}