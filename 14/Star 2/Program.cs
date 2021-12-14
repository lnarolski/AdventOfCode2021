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
            LinkedList<char> polymerTemplate = new LinkedList<char>();
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
                    polymerTemplate = new LinkedList<char>(line);
                }
                else
                {
                    string[] temp = line.Split(" -> ");

                    pairInsertionRules.Add(temp[0], temp[1][0]);
                }
            }
            //

            // Searching for an answer
            DateTime dateTime = DateTime.Now;

            bool stop;
            int maxSteps = 40;
            for (int i = 0; i < maxSteps; i++)
            {
                LinkedListNode<char> linkedListNode = polymerTemplate.First;

                for (int j = 0; j < polymerTemplate.Count - 1; j++)
                {
                    string polymerTemplatePart = "";

                    polymerTemplatePart += linkedListNode.Value;
                    linkedListNode = linkedListNode.Next;
                    polymerTemplatePart += linkedListNode.Value;

                    if (pairInsertionRules.ContainsKey(polymerTemplatePart))
                    {
                        polymerTemplate.AddBefore(linkedListNode, pairInsertionRules[polymerTemplatePart]);

                        j += 1;
                    }
                }

                Console.WriteLine(i);
                Console.WriteLine("Execution time: {0}", (DateTime.Now - dateTime).TotalSeconds);
                dateTime = DateTime.Now;
            }

            long minCount = long.MaxValue, maxCount = long.MinValue;
            char[] polymerTemplateChar = polymerTemplate.ToArray();
            HashSet<char> characters = new HashSet<char>();
            for (int i = 0; i < polymerTemplate.Count; i++)
            {
                if (!characters.Contains(polymerTemplateChar[i]))
                {
                    int temp = polymerTemplate.Count(c => c == polymerTemplateChar[i]);
                    if (temp < minCount)
                        minCount = temp;
                    if (temp > maxCount)
                        maxCount = temp;

                    characters.Add(polymerTemplateChar[i]);
                }
            }
            //

            Console.WriteLine("Output: {0}", maxCount - minCount);
        }
    }
}