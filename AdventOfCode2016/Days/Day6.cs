using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

namespace AdventOfCode2016
{
    public static class Day6
    {
        // Decode the messages.
        public static void Resolve()
        {
            var input = ReadInput();
            var encoded = ReverseInput(input);
            var messagePartOne = GetMessage(encoded, true);
            var messagePartTwo = GetMessage(encoded, false);

            WriteLine($"The message being sent is { messagePartOne }.");
            WriteLine($"The message actually being sent by Santa is { messagePartTwo }.");
        }

        // Read the Day 6 input.
        private static string[] ReadInput()
        {
            return File.ReadAllLines(@"Inputs\Day6.txt");
        }

        // Put the columns in line for easy processing.
        private static char[][] ReverseInput(string[] input)
        {
            char[][] encoded = new char[input[0].Length][];

            for (int i = 0; i < encoded.GetLength(0); i++)
                encoded[i] = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                encoded[0][i] = input[i][0];
                encoded[1][i] = input[i][1];
                encoded[2][i] = input[i][2];
                encoded[3][i] = input[i][3];
                encoded[4][i] = input[i][4];
                encoded[5][i] = input[i][5];
                encoded[6][i] = input[i][6];
                encoded[7][i] = input[i][7];
            }

            return encoded;
        }

        // Find the letter for every column.
        private static string GetMessage(char[][] encoded, bool isMostFrequent)
        {
            var builder = new StringBuilder();
            if (isMostFrequent)
            {
                for (int i = 0; i < encoded.GetLength(0); i++)
                {
                    builder.Append(encoded[i].GroupBy(c => c).OrderByDescending(c => c.Count()).Select(c => c.Key).First());
                }
            }
            else
            {
                for (int i = 0; i < encoded.GetLength(0); i++)
                {
                    builder.Append(encoded[i].GroupBy(c => c).OrderBy(c => c.Count()).Select(c => c.Key).First());
                }
            }

            return builder.ToString();
        }

        
    }
}
