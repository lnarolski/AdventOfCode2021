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
            List<long> fileLinesInt;
            fileLinesInt = input[0].Split(',').Select(s => long.Parse(s)).ToList();
            Dictionary<long, long> crabPositions = new Dictionary<long, long>();
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
            }
            //

            // Searching for an answer
            long minimumFuel = long.MaxValue;
            foreach (var positionToCheck in crabPositions)
            {
                long sumOfFuel = 0;
                foreach (var position in crabPositions)
                {
                    if (positionToCheck.Key == position.Key)
                        continue;

                    if (position.Key < positionToCheck.Key)
                    {
                        sumOfFuel += (positionToCheck.Key - position.Key) * position.Value;
                    }
                    else
                    {
                        sumOfFuel += (position.Key - positionToCheck.Key) * position.Value;
                    }
                }

                if (sumOfFuel < minimumFuel)
                {
                    minimumFuel = sumOfFuel;
                }
            }
            //

            Console.WriteLine("Output: {0}", minimumFuel);
        }
    }
}
