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
            Dictionary<string, long> polymerTemplate = new Dictionary<string, long>();
            Dictionary<string, char> pairInsertionRules = new Dictionary<string, char>();
            char lastCharacter = '0';

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
                    for (int i = 0; i < line.Length - 1; i++)
                    {
                        if (!polymerTemplate.ContainsKey(line.Substring(i, 2)))
                        {
                            polymerTemplate.Add(line.Substring(i, 2), 1);
                        }
                        else
                        {
                            ++polymerTemplate[line.Substring(i, 2)];
                        }
                    }

                    lastCharacter = line[line.Length - 1];
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

            int maxSteps = 40;
            for (int i = 0; i < maxSteps; i++)
            {
                Dictionary<string, long> polymerTemplateCopy = new Dictionary<string, long>();

                foreach (var polymer in polymerTemplate)
                {
                    if (pairInsertionRules.ContainsKey(polymer.Key))
                    {
                        if (!polymerTemplateCopy.ContainsKey(polymer.Key[0].ToString() + pairInsertionRules[polymer.Key]))
                        {
                            polymerTemplateCopy.Add(polymer.Key[0].ToString() + pairInsertionRules[polymer.Key], polymer.Value);
                        }
                        else
                        {
                            polymerTemplateCopy[polymer.Key[0].ToString() + pairInsertionRules[polymer.Key]] += polymer.Value;
                        }

                        if (!polymerTemplateCopy.ContainsKey(pairInsertionRules[polymer.Key].ToString() + polymer.Key[1]))
                        {
                            polymerTemplateCopy.Add(pairInsertionRules[polymer.Key].ToString() + polymer.Key[1], polymer.Value);
                        }
                        else
                        {
                            polymerTemplateCopy[pairInsertionRules[polymer.Key].ToString() + polymer.Key[1]] += polymer.Value;
                        }
                    }
                }

                polymerTemplate = new Dictionary<string, long>(polymerTemplateCopy);
            }

            long minCount = long.MaxValue, maxCount = long.MinValue;
            Dictionary<char, long> characters = new Dictionary<char, long>();
            foreach (var polymer in polymerTemplate)
            {
                if (characters.ContainsKey(polymer.Key[0]))
                {
                    characters[polymer.Key[0]] += polymer.Value;
                }
                else
                {
                    characters.Add(polymer.Key[0], polymer.Value);
                }
            }
            characters[lastCharacter] += 1;

            foreach (var character in characters)
            {
                if (character.Value < minCount)
                {
                    minCount = character.Value;
                }
                if (character.Value > maxCount)
                {
                    maxCount = character.Value;
                }
            }

            Console.WriteLine("Execution time: {0}", (DateTime.Now - dateTime).TotalMilliseconds);

            //

            Console.WriteLine("Output: {0}", maxCount - minCount);
        }
    }
}