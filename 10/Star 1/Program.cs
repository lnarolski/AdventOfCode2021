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
            Dictionary<char, long> points = new Dictionary<char, long>()
            {
                {')', 3}, {']', 57}, {'}', 1197}, {'>', 25137}
            };
            Dictionary<char, char> chunks = new Dictionary<char, char>()
            {
                {'(', ')'}, {'[', ']'}, {'{', '}'}, {'<', '>'}
            };
            //

            // Searching for an answer
            long syntaxErrorScore = 0;
            foreach (var line in fileLines)
            {
                Stack<char> lastChunk = new Stack<char>();
                foreach (var character in line)
                {
                    if (chunks.ContainsKey(character))
                    {
                        lastChunk.Push(character);
                    }
                    else
                    {
                        if (character != chunks[lastChunk.Peek()])
                        {
                            syntaxErrorScore += points[character];
                            break;
                        }
                        else
                        {
                            lastChunk.Pop();
                        }
                    }
                }
            }
            //

            Console.WriteLine("Output: {0}", syntaxErrorScore);
        }
    }
}