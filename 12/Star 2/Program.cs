using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Star_2
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
            Console.WriteLine("Hello World from Star 2!");

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

            Console.WriteLine("Output: {0}", step);
        }
    }
}