using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Star_1
{
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
            long maximumY = long.MinValue;
            for (int xVelocity = 1; xVelocity < 1000; xVelocity++)
            {
                for (int yVelocity = 1; yVelocity < 1000; yVelocity++)
                {
                    Pair position = new Pair(0, 0); // start position
                    Pair currentVelocity = new Pair(xVelocity, yVelocity);

                    long tempMaximumY = 0;

                    bool stop = false;
                    while (!stop)
                    {
                        if (position.y > tempMaximumY)
                            tempMaximumY = position.y;

                        if (position.x > areaEnd.x || position.y < areaEnd.y)
                        {
                            stop = true;
                        }

                        if (position.x >= areaStart.x && position.x <= areaEnd.x && position.y <= areaEnd.y && position.y >= areaEnd.y)
                        {
                            if (tempMaximumY > maximumY)
                                maximumY = tempMaximumY;

                            stop = true;
                        }

                        position.x += currentVelocity.x;
                        position.y += currentVelocity.y;

                        if (currentVelocity.x > 0)
                        {
                            --currentVelocity.x;
                        }
                        else if (currentVelocity.x < 0)
                        {
                            ++currentVelocity.x;
                        }

                        --currentVelocity.y;
                    }
                }
            }
            //

            Console.WriteLine("Output: {0}", maximumY);
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