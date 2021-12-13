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
            int maxX = int.MinValue, maxY = int.MinValue;
            List<List<char>> map = new List<List<char>>();
            List<List<int>> dots = new List<List<int>>();
            bool folds = false;
            foreach (var line in fileLines)
            {
                if (line == "")
                {
                    folds = true;

                    for (int y = 0; y < maxY + 1; y++)
                    {
                        map.Add(new List<char>());
                        for (int x = 0; x < maxX + 1; x++)
                        {
                            map[map.Count - 1].Add('.');
                        }
                    }

                    foreach (var dot in dots)
                    {
                        map[dot[1]][dot[0]] = '#';
                    }

                    continue;
                }

                if (!folds)
                {
                    int[] temp = line.Split(',').Select(s => int.Parse(s)).ToArray();
                    if (temp[0] > maxX)
                        maxX = temp[0];
                    if (temp[1] > maxY)
                        maxY = temp[1];

                    dots.Add(temp.ToList());
                }
                else
                {
                    string[] temp = line.Split("fold along ")[1].Split('=');
                    int value = int.Parse(temp[1]);

                    if (temp[0] == "x")
                    {
                        for (int y = 0; y < map.Count; y++)
                        {
                            for (int x = value + 1; x < map[0].Count; x++)
                            {
                                if (map[y][x] == '#')
                                {
                                    map[y][value - (x - value)] = '#';
                                    map[y][x] = '.';
                                }
                            }
                        }

                        for (int y = 0; y < map.Count; y++)
                        {
                            map[y][value] = '.';
                        }

                        maxX = value;
                    }
                    else // then y
                    {
                        for (int y = value + 1; y < map.Count; y++)
                        {
                            for (int x = 0; x < map[0].Count; x++)
                            {
                                if (map[y][x] == '#')
                                {
                                    map[value - (y - value)][x] = '#';
                                    map[y][x] = '.';
                                }
                            }
                        }

                        for (int x = 0; x < map[value].Count; x++)
                        {
                            map[value][x] = '.';
                        }

                        maxY = value;
                    }
                }
            }
            //

            // Searching for an answer
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    Console.Write(map[y][x]);
                }
                Console.WriteLine();
            }
            //

            //Console.WriteLine("Output: {0}", visibleDots);
        }
    }
}