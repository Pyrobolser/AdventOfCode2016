using System.IO;
using System.Text;
using static System.Console;

namespace AdventOfCode2016
{
    // http://adventofcode.com/2016/day/9
    public static class Day9
    {
        // Get the decompressed length of the text.
        public static void Resolve()
        {
            string input = ReadInput();
            WriteLine($"The decompressed length of the file is {Decompress(input).Length}");
            WriteLine($"The decompressed length of the file is actually {DecompressLengthV2(input)} using version 2 of the format.");
        }

        // Read the Day 9 input.
        private static string ReadInput() => File.ReadAllText(@"Inputs\Day9.txt");

        // Decompress the text using standard version.
        private static string Decompress(string text)
        {
            StringBuilder sb = new StringBuilder();
            string[] marker;
            int seqSize, times;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    marker = text.Substring(i + 1, text.IndexOf(')', i) - (i+1)).Split('x');
                    seqSize = int.Parse(marker[0]);
                    times = int.Parse(marker[1]);

                    for (int j = 0; j < times; j++)
                    {
                        sb.Append(text.Substring(text.IndexOf(')', i) + 1, seqSize));
                    }

                    i = text.IndexOf(')', i) + seqSize;
                }
                else
                {
                    sb.Append(text[i]);
                }
            }
            
            return sb.ToString();
        }

        // Get the decompressed length of the text using version 2.
        private static long DecompressLengthV2(string text, long num = 1)
        {
            string[] marker;
            int seqSize, times;
            long total = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    marker = text.Substring(i + 1, text.IndexOf(')', i) - (i + 1)).Split('x');
                    seqSize = int.Parse(marker[0]);
                    times = int.Parse(marker[1]);

                    total += DecompressLengthV2(text.Substring(text.IndexOf(')', i) + 1, seqSize), times);
                    i = text.IndexOf(')', i) + seqSize;
                }
                else
                {
                    total++;
                }
            }
            return num * total;
        }

    }
}
