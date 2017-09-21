using System.IO;
using System.Text;
using static System.Console;

namespace AdventOfCode2016
{
    // http://adventofcode.com/2016/day/8
    public static class Day8
    { 

        public static void Resolve()
        {
            string[] input = ReadInput();
            var a = new Screen();
            a.LitRectangle(3, 2);
            WriteLine(a.ToString());
        }

        // Read the Day 8 input.
        private static string[] ReadInput()
        {
            return File.ReadAllLines(@"Inputs\Day8.txt");
        }
    }

    public class Screen
    {
        private const int Width = 50;
        private const int Height = 6;
        public string[,] PixelGrid { get; set; } = new string[Width, Height];

        public void LitRectangle(int x, int y)
        {
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    PixelGrid[i, j] = "#";
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    sb.Append(PixelGrid[x, y]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}