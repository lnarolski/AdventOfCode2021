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
            string[] temp = input[0].Split("target area: ")[1].Split(", ");

            int x1 = int.Parse(temp[0].Split("x=")[1].Split("..")[0]);
            int x2 = int.Parse(temp[0].Split("x=")[1].Split("..")[1]);
            int y1 = int.Parse(temp[1].Split("y=")[1].Split("..")[0]);
            int y2 = int.Parse(temp[1].Split("y=")[1].Split("..")[1]);

            Pair areaStart; // x, y
            Pair areaEnd; // x, y

            if (x1 < x2)
            {
                if (y1 > y2)
                {
                    areaStart = new Pair(x1, y1);
                    areaEnd = new Pair(x2, y2);
                }
                else
                {
                    areaStart = new Pair(x1, y2);
                    areaEnd = new Pair(x2, y1);
                }
            }
            else
            {
                if (y1 > y2)
                {
                    areaStart = new Pair(x2, y1);
                    areaEnd = new Pair(x1, y2);
                }
                else
                {
                    areaStart = new Pair(x2, y2);
                    areaEnd = new Pair(x1, y1);
                }
            }
            //

            // Searching for an answer
            int maxX = 100000, maxY = 100000;

            long numOfVelocityValues = 0;
            for (int xVelocity = -1000; xVelocity <= 1000; xVelocity++)
            {
                for (int yVelocity = -1000; yVelocity <= 1000; yVelocity++)
                {
                    Pair position = new Pair(0, 0); // start position
                    Pair currentVelocity = new Pair(xVelocity, yVelocity);

                    position.x += currentVelocity.x;
                    position.y += currentVelocity.y;

                    while (true)
                    {
                        if (position.x > maxX || position.x < -maxX || position.y < -maxY || position.y > maxY)
                        {
                            break;
                        }

                        if (position.x >= areaStart.x && position.x <= areaEnd.x && position.y <= areaStart.y && position.y >= areaEnd.y)
                        {
                            ++numOfVelocityValues;

                            //Console.WriteLine("Initial velocity: {0},{1}", xVelocity, yVelocity);

                            break;
                        }

                        if (currentVelocity.x > 0)
                        {
                            --currentVelocity.x;
                        }
                        else if (currentVelocity.x < 0)
                        {
                            ++currentVelocity.x;
                        }

                        --currentVelocity.y;

                        position.x += currentVelocity.x;
                        position.y += currentVelocity.y;
                    }
                }
            }
            //

            Console.WriteLine("Output: {0}", numOfVelocityValues);
        }

        class Pair
        {
            public long x;
            public long y;

            public Pair(long x, long y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}