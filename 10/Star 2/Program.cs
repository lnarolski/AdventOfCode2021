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
            Dictionary<char, long> points = new Dictionary<char, long>()
            {
                {')', 1}, {']', 2}, {'}', 3}, {'>', 4}
            };
            Dictionary<char, char> chunks = new Dictionary<char, char>()
            {
                {'(', ')'}, {'[', ']'}, {'{', '}'}, {'<', '>'}
            };
            //

            // Searching for an answer
            List<long> lineScores = new List<long>();
            foreach (var line in fileLines)
            {
                long lineScore = 0;
                Stack<char> lastChunk = new Stack<char>();
                bool stop = false;
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
                            stop = true;
                            break;
                        }
                        else
                        {
                            lastChunk.Pop();
                        }
                    }
                }
                if (!stop && lastChunk.Count != 0)
                {
                    while (lastChunk.Count > 0)
                    {
                        lineScore *= 5;
                        lineScore += points[chunks[lastChunk.Pop()]];
                    }

                    lineScores.Add(lineScore);
                }
            }
            lineScores.Sort();
            long middleScore = lineScores[lineScores.Count / 2];
            //

            Console.WriteLine("Output: {0}", middleScore);
        }
    }
}