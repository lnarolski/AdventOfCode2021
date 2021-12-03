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
            List<string> diagnosticReport = new List<string>();

            foreach (var item in fileLines)
            {
                diagnosticReport.Add(item);
            }

            string epsilonRateString = string.Empty, gammaRateString = string.Empty;
            int epsilonRate, gammaRate;

            for (int i = 0; i < diagnosticReport[0].Length; ++i)
            {
                int numOf0s = 0, numOf1s = 0;

                for (int j = 0; j < diagnosticReport.Count; ++j)
                {
                    if (diagnosticReport[j][i] == '1')
                    {
                        ++numOf1s;
                    }
                    else
                    {
                        ++numOf0s;
                    }
                }

                if (numOf1s > numOf0s)
                {
                    epsilonRateString += '1';
                    gammaRateString += '0';
                }
                else
                {
                    epsilonRateString += '0';
                    gammaRateString += '1';
                }

            }

            epsilonRate = Convert.ToInt32(epsilonRateString, 2);
            gammaRate = Convert.ToInt32(gammaRateString, 2);

            Console.WriteLine("Output: {0}", epsilonRate * gammaRate);
        }
    }
}
