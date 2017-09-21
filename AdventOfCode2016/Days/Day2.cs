using System.IO;
using static System.Console;

namespace AdventOfCode2016
{
    // http://adventofcode.com/2016/day/2
    public static class Day2
    {
        private static int[,] keypad = { { 7, 8, 9 }, { 4, 5, 6 }, { 1, 2, 3 } };
        private static string[,] realKeypad = { { "", "", "D", "", "" }, { "", "A", "B", "C", "" }, { "5", "6", "7", "8", "9" }, { "", "2", "3", "4", "" }, { "", "", "1", "", "" } };
        private static int[] coord = { 1, 1 };
        private static int[] realCoord = { 2, 0 };
        private static string code = string.Empty;
        private static string realCode = string.Empty;

        // Crack the bathroom code.
        public static void Resolve()
        {
            string[] input = ReadInput();
            foreach (string line in input)
            {
                foreach (char c in line)
                {
                    UpdateCoordinates(c);
                    UpdateRealCoordinates(c);
                }
                code += keypad[coord[0], coord[1]];
                realCode += realKeypad[realCoord[0], realCoord[1]];
            }

            WriteLine($"The bathroom code is { code }. ");
            WriteLine($"The correct bathroom code for the actual keypad layout is { realCode }. ");
        }

        // Read the Day 2 input.
        private static string[] ReadInput()
        {
            return File.ReadAllLines(@"Inputs\Day2.txt");
        }

        // Update the coordinates according to instructions.
        private static void UpdateCoordinates(char c)
        {
            switch(c)
            {
                case 'U':
                    coord[0] = coord[0] + 1 <= 2 ? coord[0] + 1 : coord[0];
                    break;
                case 'R':
                    coord[1] = coord[1] + 1 <= 2 ? coord[1] + 1 : coord[1];
                    break;
                case 'D':
                    coord[0] = coord[0] - 1 >= 0 ? coord[0] - 1 : coord[0];
                    break;
                default:
                    coord[1] = coord[1] - 1 >= 0 ? coord[1] - 1 : coord[1];
                    break;
            }
        }

        // Update the coordinates with respect of the actual keypad layout.
        private static void UpdateRealCoordinates(char c)
        {
            switch (c)
            {
                case 'U':
                    realCoord[0] = realCoord[0] + 1 <= 4 && realKeypad[realCoord[0] + 1, realCoord[1]] != string.Empty ? realCoord[0] + 1 : realCoord[0];
                    break;
                case 'R':
                    realCoord[1] = realCoord[1] + 1 <= 4 && realKeypad[realCoord[0], realCoord[1] + 1] != string.Empty ? realCoord[1] + 1 : realCoord[1];
                    break;
                case 'D':
                    realCoord[0] = realCoord[0] - 1 >= 0 && realKeypad[realCoord[0] - 1, realCoord[1]] != string.Empty ? realCoord[0] - 1 : realCoord[0];
                    break;
                default:
                    realCoord[1] = realCoord[1] - 1 >= 0 && realKeypad[realCoord[0], realCoord[1] - 1] != string.Empty ? realCoord[1] - 1 : realCoord[1];
                    break;
            }
        }
    }
}
