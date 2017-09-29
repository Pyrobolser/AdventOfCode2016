using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace AdventOfCode2016
{
    // http://adventofcode.com/2016/day/3
    public static class Day3
    {
        // Find the number of possible triangles.
        public static void Resolve()
        {
            string[] input = ReadInput;
            int[,] triangles = GetTriangles(input);
            int[,] columnTriangles = GetColumnTriangles(input);

            int validTriangles = GetValidTriangles(triangles);
            int validColumnTriangles = GetValidTriangles(columnTriangles);

            
            WriteLine($"Among the listed triangles, { validTriangles } are possible(s).");
            WriteLine($"Among the listed triangles in columns, { validColumnTriangles } are possible(s).");
        }

        // Read the Day 3 input.
        private static string[] ReadInput => File.ReadAllLines(@"Inputs\Day3.txt").Select(s => s.TrimStart()).ToArray();

        // Get the inline triangles.
        private static int[,] GetTriangles(string[] input)
        {
            int[,] triangles = new int[input.Length, 3];
            for (int i = 0; i < input.Length; i++)
            {
                var values = input[i].Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                triangles[i, 0] = int.Parse(values[0]);
                triangles[i, 1] = int.Parse(values[1]);
                triangles[i, 2] = int.Parse(values[2]);
            }

            return triangles;
        }

        // Get the triangles in columns.
        private static int[,] GetColumnTriangles(string[] input)
        {
            int[,] triangles = new int[input.Length, 3];
            var row = 0;
            var col = 0;
            foreach(string s in input)
            {
                var values = s.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                triangles[col, row] = int.Parse(values[0]);
                triangles[col + 1, row] = int.Parse(values[1]);
                triangles[col + 2 , row] = int.Parse(values[2]);
                row++;

                if (row == 3)
                {
                    row = 0;
                    col += 3;
                }
            }

            return triangles;
        }

        // Get the number of valid triangles.
        private static int GetValidTriangles(int[,] triangles)
        {
            var isValid = true;
            var validTriangles = 0;
            for (int i = 0; i < triangles.GetLength(0); i++)
            {
                isValid = (triangles[i, 0] + triangles[i, 1] > triangles[i, 2])
                        &&
                           (triangles[i, 2] + triangles[i, 0] > triangles[i, 1])
                        &&
                           (triangles[i, 1] + triangles[i, 2] > triangles[i, 0]);

                if (isValid)
                    validTriangles++;
            }

            return validTriangles;
        }
    }
}
