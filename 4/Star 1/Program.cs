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

            int[] generatedNumbers = input[0].Split(',').Select(s => int.Parse(s)).ToArray();
            List<List<int>> boards = new List<List<int>>();




            Console.WriteLine("Output: {0}");
        }
    }
}
