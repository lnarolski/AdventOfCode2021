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
            List<List<Octopuse>> map = new List<List<Octopuse>>();
            foreach (var line in fileLines)
            {
                List<long> temp = line.ToCharArray().Select(s => long.Parse(s.ToString())).ToList();
                map.Add(new List<Octopuse>());
                for (int i = 0; i < temp.Count; i++)
                {
                    map[map.Count - 1].Add(new Octopuse() { energyValue = temp[i], flashed = false });
                }
            }
            //

            // Searching for an answer
            long numOfFlashes = 0;
            int step = 1;
            bool stop = false;
            while (!stop)
            {
                for (int y = 0; y < map.Count; y++)
                {
                    for (int x = 0; x < map[0].Count; x++)
                    {
                        ++map[y][x].energyValue;
                    }
                }

                bool allFlashed;
                do
                {
                    allFlashed = true;
                    for (int y = 0; y < map.Count; y++)
                    {
                        for (int x = 0; x < map[0].Count; x++)
                        {
                            if (map[y][x].energyValue > 9 && !map[y][x].flashed)
                            {
                                Flash(x, y, map);
                                allFlashed = false;
                            }
                        }
                    }
                } while (!allFlashed);

                for (int y = 0; y < map.Count; y++)
                {
                    for (int x = 0; x < map[0].Count; x++)
                    {
                        if (map[y][x].flashed)
                        {
                            ++numOfFlashes;
                            map[y][x].energyValue = 0;
                            map[y][x].flashed = false;
                        }
                    }
                }

                bool allOctopusesFlush = true;
                for (int y = 0; allOctopusesFlush && y < map.Count; y++)
                {
                    for (int x = 0; allOctopusesFlush && x < map[0].Count; x++)
                    {
                        if (map[y][x].energyValue != 0)
                        {
                            allOctopusesFlush = false;
                        }
                    }
                }

                if (!allOctopusesFlush)
                    ++step;
                else
                    stop = true;
            }

            PrintMap(map);
            //

            Console.WriteLine("Output: {0}", step);
        }

        private static void PrintMap(List<List<Octopuse>> map)
        {
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    Console.Write(map[y][x].energyValue);
                }
                Console.WriteLine();
            }
        }
    }
}