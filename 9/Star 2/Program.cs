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
            //  dddd     0000
            // e    a   1    2
            // e    a   1    2
            //  ffff     3333
            // g    b   4    5
            // g    b   4    5
            //  cccc     6666
            //
            // 0 -> 6,++
            // 1 -> 2,
            // 2 -> 5,
            // 3 -> 5,
            // 4 -> 4,
            // 5 -> 5,
            // 6 -> 6,++
            // 7 -> 3,
            // 8 -> 7,
            // 9 -> 6++

            List<long> outputNumbers = new List<long>();
            foreach (var line in fileLines)
            {
                Dictionary<string, long> numbersOnDisplayString = new Dictionary<string, long>();
                Dictionary<long, string> numbersOnDisplayLong = new Dictionary<long, string>();
                var temp = line.Split(" | ");
                List<string> numbersForDeduction = temp[0].Split(' ').ToList();
                List<string> numbersToDecode = temp[1].Split(' ').ToList();

                numbersOnDisplayString.Add(numbersForDeduction.Find(x => x.Length == 2), 1); // 1
                numbersOnDisplayLong.Add(1, numbersForDeduction.Find(x => x.Length == 2)); // 1
                numbersForDeduction.Remove(numbersForDeduction.Find(x => x.Length == 2));
                numbersOnDisplayString.Add(numbersForDeduction.Find(x => x.Length == 4), 4); // 4
                numbersOnDisplayLong.Add(4, numbersForDeduction.Find(x => x.Length == 4)); // 4
                numbersForDeduction.Remove(numbersForDeduction.Find(x => x.Length == 4));
                numbersOnDisplayString.Add(numbersForDeduction.Find(x => x.Length == 3), 7); // 7
                numbersOnDisplayLong.Add(7, numbersForDeduction.Find(x => x.Length == 3)); // 7
                numbersForDeduction.Remove(numbersForDeduction.Find(x => x.Length == 3));
                numbersOnDisplayString.Add(numbersForDeduction.Find(x => x.Length == 7), 8); // 8
                numbersOnDisplayLong.Add(8, numbersForDeduction.Find(x => x.Length == 7)); // 8
                numbersForDeduction.Remove(numbersForDeduction.Find(x => x.Length == 7));

                // Find 2
                foreach (var item in numbersForDeduction)
                {
                    if (item.Length == 5)
                    {
                        int charactersFound = 0;
                        int charactersNotFound = 0;
                        foreach (var character in numbersOnDisplayLong[4])
                        {
                            if (item.Contains(character))
                                ++charactersFound;
                            else
                                ++charactersNotFound;
                        }

                        if (charactersNotFound == 2 && charactersFound == 2)
                        {
                            numbersOnDisplayString.Add(item, 2);
                            numbersOnDisplayLong.Add(2, item);
                            numbersForDeduction.Remove(item);
                            break;
                        }
                    }
                }

                // Find 0
                foreach (var item in numbersForDeduction)
                {
                    if (item.Length == 6)
                    {
                        int charactersFound = 0;
                        int charactersNotFound = 0;
                        foreach (var character in numbersOnDisplayLong[8])
                        {
                            if (item.Contains(character))
                                ++charactersFound;
                            else
                                ++charactersNotFound;
                        }
                        foreach (var character in numbersOnDisplayLong[4])
                        {
                            if (item.Contains(character))
                                ++charactersFound;
                            else
                                ++charactersNotFound;
                        }
                        foreach (var character in numbersOnDisplayLong[7])
                        {
                            if (item.Contains(character))
                                ++charactersFound;
                            else
                                ++charactersNotFound;
                        }

                        if (charactersNotFound == 2 && charactersFound == 12)
                        {
                            numbersOnDisplayString.Add(item, 0);
                            numbersOnDisplayLong.Add(0, item);
                            numbersForDeduction.Remove(item);
                            break;
                        }
                    }
                }

                // Find 6
                foreach (var item in numbersForDeduction)
                {
                    if (item.Length == 6)
                    {
                        int charactersFound = 0;
                        int charactersNotFound = 0;
                        foreach (var character in numbersOnDisplayLong[0])
                        {
                            if (item.Contains(character))
                                ++charactersFound;
                            else
                                ++charactersNotFound;
                        }
                        foreach (var character in numbersOnDisplayLong[1])
                        {
                            if (item.Contains(character))
                                ++charactersFound;
                            else
                                ++charactersNotFound;
                        }

                        if (charactersNotFound == 2 && charactersFound == 6)
                        {
                            numbersOnDisplayString.Add(item, 6);
                            numbersOnDisplayLong.Add(6, item);
                            numbersForDeduction.Remove(item);
                            break;
                        }
                    }
                }

                // Find 3
                foreach (var item in numbersForDeduction)
                {
                    if (item.Length == 5)
                    {
                        int charactersFound = 0;
                        int charactersNotFound = 0;
                        foreach (var character in numbersOnDisplayLong[6])
                        {
                            if (item.Contains(character))
                                ++charactersFound;
                            else
                                ++charactersNotFound;
                        }
                        foreach (var character in numbersOnDisplayLong[1])
                        {
                            if (item.Contains(character))
                                ++charactersFound;
                            else
                                ++charactersNotFound;
                        }
                        foreach (var character in numbersOnDisplayLong[2])
                        {
                            if (item.Contains(character))
                                ++charactersFound;
                            else
                                ++charactersNotFound;
                        }

                        if (charactersNotFound == 3 && charactersFound == 10)
                        {
                            numbersOnDisplayString.Add(item, 3);
                            numbersOnDisplayLong.Add(3, item);
                            numbersForDeduction.Remove(item);
                            break;
                        }
                    }
                }

                // Find 5
                foreach (var item in numbersForDeduction)
                {
                    if (item.Length == 5)
                    {
                        numbersOnDisplayString.Add(item, 5);
                        numbersOnDisplayLong.Add(5, item);
                        numbersForDeduction.Remove(item);
                        break;
                    }
                }

                // Find 9
                numbersOnDisplayString.Add(numbersForDeduction[0], 9);
                numbersOnDisplayLong.Add(9 ,numbersForDeduction[0]);

                // Decode output number
                long outputNumberToAdd = 0;
                for (int i = 0; i < numbersToDecode.Count; i++)
                {
                    foreach (var item in numbersOnDisplayString)
                    {
                        if (item.Key.Length == numbersToDecode[i].Length)
                        {
                            bool foundKey = true;

                            foreach (var character in item.Key)
                            {
                                if (!numbersToDecode[i].Contains(character))
                                {
                                    foundKey = false;
                                    break;
                                }
                            }

                            if (foundKey)
                            {
                                outputNumberToAdd += item.Value * (long)(Math.Pow(10, 3 - i));
                                break;
                            }
                        }
                    }
                }
                outputNumbers.Add(outputNumberToAdd);

                // 1, 2, 4, 7, 8, 0, 6, 3, 5, 9 found
            }

            // Searching for an answer
            long sumOfNumbers = 0;
            foreach (var item in outputNumbers)
            {
                sumOfNumbers += item;
            }
            //

            Console.WriteLine("Output: {0}", sumOfNumbers);
        }
    }
}
