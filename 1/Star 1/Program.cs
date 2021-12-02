using System;
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
            string[] fileLinesArray = fileLines.ToArray();

            int numOfLargerMeas = 0;

            for (int i = 1; i < fileLinesArray.Length; ++i)
            {
                if (Convert.ToInt32(fileLinesArray[i]) > Convert.ToInt32(fileLinesArray[i - 1]))
                {
                    ++numOfLargerMeas;
                }
            }

            Console.WriteLine("numOfLargerMeas: {0}", numOfLargerMeas);
        }
    }
}
