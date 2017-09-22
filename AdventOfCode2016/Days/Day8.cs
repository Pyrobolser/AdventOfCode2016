using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

namespace AdventOfCode2016
{
    // http://adventofcode.com/2016/day/8
    public static class Day8
    {
        // Display the password on the screen.
        public static void Resolve()
        {
            Screen screen = new Screen();
            string[] input = ReadInput();
            foreach (var line in input)
            {
                DrawInstruction(screen, line);
            }

            WriteLine($"{screen.PixelGrid.SelectMany(x => x).Count(x => x == "#")} pixels are lit on the screen.");
        }

        // Read the Day 8 input.
        private static string[] ReadInput()
        {
            return File.ReadAllLines(@"Inputs\Day8.txt");
        }

        // Draw every line of instruction.
        private static void DrawInstruction(Screen screen, string line)
        {
            var chunks = line.Split(' ');
            switch (chunks[0])
            {
                case "rect":
                    var size = chunks[1].Split('x');
                    screen.LitRectangle(int.Parse(size[0]), int.Parse(size[1]));
                    break;
                case "rotate":
                    var coord = int.Parse(chunks[2].Split('=')[1]);
                    var value = int.Parse(chunks[4]);

                    if(chunks[1] == "row")
                    {
                        screen.RotateRow(coord, value);
                    }
                    else
                    {
                        screen.RotateColumn(coord, value);
                    }
                    break;
            }
            SetCursorPosition(0, 0);
            WriteLine(screen.ToString());
        }
    }

    // Ad-hoc screen class.
    public class Screen
    {
        private const int Width = 50;
        private const int Height = 6;
        public string[][] PixelGrid { get; set; }

        public Screen()
        {
            PixelGrid = new string[Width][];
            for (int x = 0; x < Width; x++)
            {
                PixelGrid[x] = new string[Height];
                for (int y = 0; y < Height; y++)
                {
                
                    PixelGrid[x][y] = ".";
                }
            }
        }

        // Lit a x * y rectangle of pixels.
        public void LitRectangle(int x, int y)
        {
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    PixelGrid[i][j] = "#";
                }
            }
        }

        // Rotate the x column by val pixels.
        public void RotateColumn(int x, int val)
        {
            var column = new string[Height];
            for (int i = 0; i < Height; i++)
            {
                column[(i + val) % Height] = PixelGrid[x][i]; 
            }

            PixelGrid[x] = column;
        }

        // Rotate the y row by val pixels.
        public void RotateRow(int y, int val)
        {
            var row = new string[Width];
            for (int i = 0; i < Width; i++)
            {
                row[(i + val) % Width] = PixelGrid[i][y];
            }

            for (int i = 0; i < Width; i++)
            {
                PixelGrid[i][y] = row[i];
            }
        }

        // Display the current screen pixels.
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    sb.Append(PixelGrid[x][y]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}