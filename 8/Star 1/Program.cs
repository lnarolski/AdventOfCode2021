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
            List<List<string>> outputs = new List<List<string>>();
            foreach (var item in input)
            {
                outputs.Add(new List<string>());
                outputs[outputs.Count - 1] = item.Split(" | ")[1].Split(' ').ToList();
            }
            //

            // Searching for an answer
            long countOfNumbers = 0;
            foreach (var item in outputs)
            {
                foreach (var number in item)
                {
                    if (number.Length == 2 || number.Length == 4 || number.Length == 3 || number.Length == 7)
                        ++countOfNumbers;
                }
            }
            //

            Console.WriteLine("Output: {0}", countOfNumbers);
        }
    }
}
