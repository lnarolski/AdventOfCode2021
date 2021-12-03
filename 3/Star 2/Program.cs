using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Star_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World from Star 2!");

            var fileLines = File.ReadLines("../../../../input.txt");
            List<string> diagnosticReport = new List<string>();

            foreach (var item in fileLines)
            {
                diagnosticReport.Add(item);
            }

            int oxygenGeneratorRating, CO2ScrubberRating;

            List<string> diagnosticReportCopy = new List<string>(diagnosticReport);
            for (int i = 0; i < diagnosticReportCopy[0].Length; ++i)
            {
                int numOf0s = 0, numOf1s = 0;

                for (int j = 0; j < diagnosticReportCopy.Count; ++j)
                {
                    if (diagnosticReportCopy[j][i] == '1')
                    {
                        ++numOf1s;
                    }
                    else
                    {
                        ++numOf0s;
                    }
                }

                if (numOf1s >= numOf0s)
                {
                    for (int k = diagnosticReportCopy.Count - 1; diagnosticReportCopy.Count > 1 && k >= 0; --k)
                    {
                        if (diagnosticReportCopy[k][i] != '1')
                        {
                            diagnosticReportCopy.RemoveAt(k);
                        }
                    }
                }
                else
                {
                    for (int k = diagnosticReportCopy.Count - 1; diagnosticReportCopy.Count > 1 && k >= 0; --k)
                    {
                        if (diagnosticReportCopy[k][i] != '0')
                        {
                            diagnosticReportCopy.RemoveAt(k);
                        }
                    }
                }
            }
            oxygenGeneratorRating = Convert.ToInt32(diagnosticReportCopy[0], 2);

            diagnosticReportCopy = new List<string>(diagnosticReport);
            for (int i = 0; i < diagnosticReportCopy[0].Length; ++i)
            {
                int numOf0s = 0, numOf1s = 0;

                for (int j = 0; j < diagnosticReportCopy.Count; ++j)
                {
                    if (diagnosticReportCopy[j][i] == '1')
                    {
                        ++numOf1s;
                    }
                    else
                    {
                        ++numOf0s;
                    }
                }

                if (numOf1s >= numOf0s)
                {
                    for (int k = diagnosticReportCopy.Count - 1; diagnosticReportCopy.Count > 1 && k >= 0; --k)
                    {
                        if (diagnosticReportCopy[k][i] != '0')
                        {
                            diagnosticReportCopy.RemoveAt(k);
                        }
                    }
                }
                else
                {
                    for (int k = diagnosticReportCopy.Count - 1; diagnosticReportCopy.Count > 1 && k >= 0; --k)
                    {
                        if (diagnosticReportCopy[k][i] != '1')
                        {
                            diagnosticReportCopy.RemoveAt(k);
                        }
                    }
                }
            }
            CO2ScrubberRating = Convert.ToInt32(diagnosticReportCopy[0], 2);

            Console.WriteLine("oxygenGeneratorRating: {1} CO2ScrubberRating: {2} Output: {0}", oxygenGeneratorRating * CO2ScrubberRating, oxygenGeneratorRating, CO2ScrubberRating);
        }
    }
}
