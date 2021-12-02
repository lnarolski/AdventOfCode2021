using System;
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
            int[] fileLinesArray = fileLines.ToArray().Select(s => int.Parse(s)).ToArray();

            int numOfLargerMeas = 0;

            for (int i = 0; i < fileLinesArray.Length - 3; ++i)
            {
                if (fileLinesArray[i] + fileLinesArray[i + 1] + fileLinesArray[i + 2] < fileLinesArray[i + 1] + fileLinesArray[i + 2] + fileLinesArray[i + 3])
                {
                    ++numOfLargerMeas;
                }
            }

            Console.WriteLine("numOfLargerMeas: {0}", numOfLargerMeas);
        }
    }
}
