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
            List<int> fishList = new List<int>();
            fishList = input[0].Split(',').Select(s => int.Parse(s)).ToList();
            //

            // Searching for an answer
            int numOfDays = 80;
            for (int i = 0; i < numOfDays; i++)
            {
                List<int> fishToAdd = new List<int>();
                for (int j = 0; j < fishList.Count; j++)
                {
                    --fishList[j];
                    if (fishList[j] < 0)
                    {
                        fishList[j] = 6;
                        fishToAdd.Add(8);
                    }
                }
                foreach (var item in fishToAdd)
                {
                    fishList.Add(item);
                }
            }
            //

            Console.WriteLine("Output: {0}", fishList.Count);
        }
    }
}
