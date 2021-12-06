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
            Dictionary<long, long> fishList = new Dictionary<long, long>() { { -1, 0 }, { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 } };
            List<long> tempList = input[0].Split(',').Select(s => long.Parse(s)).ToList();
            foreach (var item in tempList)
            {
                ++fishList[item];
            }
            //

            // Searching for an answer
            long numOfDays = 256;
            for (int i = 0; i < numOfDays; i++)
            {
                long fishToAdd = 0;
                for (int j = 0; j < fishList.Count - 1; ++j)
                {
                    fishList[j - 1] = fishList[j];
                    if (j - 1 == -1)
                    {
                        fishToAdd = fishList[-1];
                        fishList[-1] = 0;
                    }
                }
                fishList[8] = fishToAdd;
                fishList[6] += fishToAdd;
            }
            long sumOfFish = 0;
            foreach (var item in fishList)
            {
                sumOfFish += item.Value;
            }
            //

            Console.WriteLine("Output: {0}", sumOfFish);
        }
    }
}
