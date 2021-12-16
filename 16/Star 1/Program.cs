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

            //

            // Searching for an answer
            long sumOfVersionNumbers = 0;
            foreach (var line in fileLines)
            {
                string binaryLine = hex2binary(line);

                sumOfVersionNumbers += GetVersionNumbers(binaryLine);
            }
            //

            Console.WriteLine("Output: {0}", sumOfVersionNumbers);
        }

        private static long GetVersionNumbers(string binaryLine, bool lastPacket = false)
        {
            long sumOfVersionNumbers = 0;

            long packetVersion = Convert.ToInt64(binaryLine.Substring(0, 3), 2);
            long typeID = Convert.ToInt64(binaryLine.Substring(3, 3), 2);

            sumOfVersionNumbers += packetVersion;

            if (typeID != 4)
            {
                if (binaryLine[6] == '0') // -> total length in bits of the sub-packets
                {
                    long lengthInBits = Convert.ToInt64(binaryLine.Substring(7, 15), 2);

                    int subPacketStart = 22;
                    while (subPacketStart < binaryLine.Length - lengthInBits)
                    {
                        packetVersion = Convert.ToInt64(binaryLine.Substring(subPacketStart, 3), 2);
                        typeID = Convert.ToInt64(binaryLine.Substring(subPacketStart + 3, 3), 2);

                        sumOfVersionNumbers += packetVersion;


                    }
                }
                else // length type ID = 1 -> number of sub-packets immediately contained
                {
                    long numOfSubpackets = Convert.ToInt64(binaryLine.Substring(7, 11), 2);
                }
            }
            else
            {
                string literalValue = "";
                bool stop = false;
                int i = 7;
                while (!stop)
                {
                    if (binaryLine[i] == '1')
                    {
                        literalValue += binaryLine.Substring(i + 1, 4);
                        i += 5;

                    }
                    else // last group packet
                    {
                        literalValue += binaryLine.Substring(i + 1, 4);
                    }
                }
            }

            return sumOfVersionNumbers;
        }

        static private string hex2binary(string hexvalue)
        {
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt32(hexvalue, 16), 2);
            return binaryval;
        }


    }
}