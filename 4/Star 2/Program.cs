using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Star_2
{
    class BingoNumber
    {
        public int bingoNumber;
        public bool marked = false;

        public BingoNumber(int bingoNumber)
        {
            this.bingoNumber = bingoNumber;
        }
    }
    class BingoLine
    {
        public List<BingoNumber> bingoLine;
        public int numOfFoundNumbersRow = 0;
        public List<int> numOfFoundNumbersColumn = new List<int> {0,0,0,0,0};

        public BingoLine(int[] bingoLine)
        {
            this.bingoLine = new List<BingoNumber>();
            foreach (var item in bingoLine)
            {
                this.bingoLine.Add(new BingoNumber(item));
            }
        }
    }
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
            int[] generatedNumbers = input[0].Split(',').Select(s => int.Parse(s)).ToArray();
            List<List<BingoLine>> boards = new List<List<BingoLine>>();

            boards.Add(new List<BingoLine>());
            int inputLine = 2;
            while (inputLine < input.Count)
            {
                if (input[inputLine] == "")
                {
                    boards.Add(new List<BingoLine>());
                }
                else
                {
                    boards[boards.Count - 1].Add(new BingoLine(input[inputLine].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray()));
                }

                ++inputLine;
            }
            //

            // Searching for an answer
            bool found = false;
            int sumOfUnmarkedNumbers = 0, numberThatWasJustCalled = -1, winningBoard = -1, wonBoards = 0;
            List<int> ignoreBoards = new List<int>();

            for (int l = 0; wonBoards != boards.Count && l < generatedNumbers.Length; l++)
            {
                for (int i = 0; i < boards.Count; i++)
                {
                    for (int j = 0; !ignoreBoards.Contains(i) && j < boards[i].Count; j++) // rows
                    {
                        for (int k = 0; k < boards[i][j].bingoLine.Count; k++)
                        {
                            if (generatedNumbers[l] == boards[i][j].bingoLine[k].bingoNumber)
                            {
                                boards[i][j].bingoLine[k].marked = true;
                                ++boards[i][j].numOfFoundNumbersRow;
                                ++boards[i][0].numOfFoundNumbersColumn[k];

                                break;
                            }
                        }

                        if (boards[i][j].numOfFoundNumbersRow == boards[i][j].bingoLine.Count || boards[i][0].numOfFoundNumbersColumn.Contains(boards[i].Count))
                        {
                            found = true;
                        }
                    }

                    if (found)
                    {
                        winningBoard = i;
                        ignoreBoards.Add(i);

                        numberThatWasJustCalled = generatedNumbers[l];
                        ++wonBoards;

                        found = false;
                    }
                }
            }

            for (int i = 0; i < boards[winningBoard].Count; ++i)
            {
                for (int j = 0; j < boards[winningBoard][i].bingoLine.Count; ++j)
                {
                    if (boards[winningBoard][i].bingoLine[j].marked == false)
                        sumOfUnmarkedNumbers += boards[winningBoard][i].bingoLine[j].bingoNumber;
                }
            }
            //

            Console.WriteLine("Output: {0}", sumOfUnmarkedNumbers * numberThatWasJustCalled);
        }
    }
}
