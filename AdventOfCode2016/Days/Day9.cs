using System.IO;
using System.Text;
using static System.Console;

namespace AdventOfCode2016
{
    // http://adventofcode.com/2016/day/9
    public static class Day9
    {
        // 
        public static void Resolve()
        {
            string input = ReadInput();
            WriteLine($"The decompressed length of the file is {Decompress(input).Length}");
            WriteLine($"The decompressed length of the file is actually {DecompressLengthV2("(27x12)(20x12)(13x14)(7x10)(1x12)A")} using version 2 of the format.");
        }

        // Read the Day 9 input.
        private static string ReadInput()
        {
            return File.ReadAllText(@"Inputs\Day9.txt");
        }

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

        private static int DecompressLengthV2(string text, int num = 0)
        {
            StringBuilder sb = new StringBuilder();
            string[] marker;
            int seqSize, times, index = 0;

            if (text.Contains("("))
            {
                num += text.Substring(index, text.IndexOf("(")).Length;
                index = text.IndexOf("(");

                marker = text.Substring(index + 1, text.IndexOf(')', index) - (index + 1)).Split('x');
                seqSize = int.Parse(marker[0]);
                times = int.Parse(marker[1]);

                for (int j = 0; j < times; j++)
                {
                    sb.Append(text.Substring(text.IndexOf(')', index) + 1, seqSize));
                }

                sb.Append(text.Substring(text.IndexOf(")") + seqSize));

                DecompressLengthV2(sb.ToString(), num);
            }
            else
            {
                num += text.Length;
            }

            return num;
        }

    }
}
