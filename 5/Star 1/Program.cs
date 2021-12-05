using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Star_1
{
    class Line
    {
        public int x1, y1;
        public int x2, y2;
    }
    class Program
    {
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
            int maxX = 1000, maxY = 1000;
            List<Line> lines = new List<Line>();

            foreach (var item in input)
            {
                var coordinates = item.Split(" -> ");
                lines.Add(new Line()
                {
                    x1 = int.Parse(coordinates[0].Split(',')[0]),
                    y1 = int.Parse(coordinates[0].Split(',')[1]),
                    x2 = int.Parse(coordinates[1].Split(',')[0]),
                    y2 = int.Parse(coordinates[1].Split(',')[1]),
                });
            }

            int[,] map = new int[maxY + 1, maxX + 1];
            //

            // Searching for an answer
            foreach (var item in lines)
            {
                if (item.x1 == item.x2 || item.y1 == item.y2)
                {
                    int distance = (int)Math.Sqrt(Math.Pow(item.x2 - item.x1, 2) + Math.Pow(item.y2 - item.y1, 2));
                    for (int i = 0; i < distance + 1; i++)
                    {
                        int x;
                        int y;

                        if (item.x1 < item.x2)
                        {
                            double a = (item.y2 - item.y1) / (item.x2 - item.x1);
                            double b = item.y1 - a * item.x1;

                            x = item.x1 + i;
                            y = (int)(a * (item.x1 + i) + b);
                        }
                        else if (item.x1 > item.x2)
                        {
                            double a = (item.y2 - item.y1) / (item.x2 - item.x1);
                            double b = item.y1 - a * item.x1;

                            x = item.x2 + i;
                            y = (int)(a * (item.x2 + i) + b);
                        }
                        else
                        {
                            if (item.y1 < item.y2)
                            {
                                x = item.x1;
                                y = item.y1 + i;
                            }
                            else
                            {
                                x = item.x1;
                                y = item.y2 + i;
                            }
                        }

                        ++map[y, x];
                    }
                }
            }

            int foundIntersections = 0;
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (map[y,x] > 1)
                    {
                        ++foundIntersections;
                    }
                }
            }
            //

            Console.WriteLine("Output: {0}", foundIntersections);
        }
    }
}
