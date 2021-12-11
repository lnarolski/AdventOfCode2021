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

            //

            // Searching for an answer

            //

            Console.WriteLine("Output: {0}");
        }
    }
}