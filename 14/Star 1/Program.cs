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
            string polymerTemplate = "";
            Dictionary<string, char> pairInsertionRules = new Dictionary<string, char>();
            bool rules = false;
            foreach (var line in fileLines)
            {
                if (line == "")
                {
                    rules = true;

                    continue;
                }

                if (!rules)
                {
                    polymerTemplate = line;
                }
                else
                {
                    string[] temp = line.Split(" -> ");

                    pairInsertionRules.Add(temp[0], temp[1][0]);
                }
            }
            //

            // Searching for an answer
            bool stop;
            int maxSteps = 10;
            for (int i = 0; i < maxSteps; i++)
            {
                do
                {
                    stop = true;

                    for (int j = 0; j < polymerTemplate.Length - 1; j++)
                    {
                        string polymerTemplatePart = polymerTemplate.Substring(j, 2);
                        if (pairInsertionRules.ContainsKey(polymerTemplatePart))
                        {
                            polymerTemplate = polymerTemplate.Insert(j + 1, pairInsertionRules[polymerTemplatePart].ToString());

                            j += 2;
                            stop = false;
                        }
                    }
                } while (!stop);
            }

            long minCount = long.MaxValue, maxCount = long.MinValue;
            HashSet<char> characters = new HashSet<char>();
            for (int i = 0; i < polymerTemplate.Length; i++)
            {
                if (!characters.Contains(polymerTemplate[i]))
                {
                    int temp = polymerTemplate.Count(c => c == polymerTemplate[i]);
                    if (temp < minCount)
                        minCount = temp;
                    if (temp > maxCount)
                        maxCount = temp;
                }
            }
            //

            Console.WriteLine("Output: {0}", maxSteps - minCount);
        }
    }
}