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
            List<long> fileLinesInt;
            fileLinesInt = input[0].Split(',').Select(s => long.Parse(s)).ToList();
            Dictionary<long, long> crabPositions = new Dictionary<long, long>();
            long minValue = long.MaxValue, maxValue = long.MinValue;
            foreach (var item in fileLinesInt)
            {
                if (crabPositions.ContainsKey(item))
                {
                    ++crabPositions[item];
                }
                else
                {
                    crabPositions.Add(item, 1);
                }

                if (item < minValue)
                    minValue = item;
                if (item > maxValue)
                    maxValue = item;
            }
            for (long i = minValue; i <= maxValue; i++)
            {
                if (!crabPositions.ContainsKey(i))
                    crabPositions.Add(i, 0);
            }
            //

            // Searching for an answer
            long minimumFuel = long.MaxValue;
            long foundPosition = -1;
            foreach (var positionToCheck in crabPositions)
            {
                long sumOfFuel = 0;
                foreach (var position in crabPositions)
                {
                    if (positionToCheck.Key == position.Key || position.Value == 0)
                        continue;

                    if (position.Key < positionToCheck.Key)
                    {
                        long n = positionToCheck.Key - position.Key;
                        sumOfFuel += ((n * (n + 1)) / 2) * position.Value;
                    }
                    else
                    {
                        long n = position.Key - positionToCheck.Key;
                        sumOfFuel += ((n * (n + 1)) / 2) * position.Value;
                    }
                }

                if (sumOfFuel < minimumFuel)
                {
                    minimumFuel = sumOfFuel;
                    foundPosition = positionToCheck.Key;
                }
            }
            //

            Console.WriteLine("Output: {0} foundPosition: {1}", minimumFuel, foundPosition);
        }
    }
}
