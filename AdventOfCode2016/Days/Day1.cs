using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace AdventOfCode2016
{
    // http://adventofcode.com/2016/day/1
    public static class Day1
    {
        private enum Direction
        {
            North,
            East,
            South,
            West
        }

        private struct Coord
        {
            public int x, y;

            public Coord(int p1, int p2)
            {
                x = p1;
                y = p2;
            }
        }

        // Initialize the character to face North at coordinates (0, 0)
        // Prep the list to find the duplicate location for part 2.
        private static Direction myDirection = Direction.North;
        private static Coord myPosition = new Coord(0, 0);
        private static List<Coord> locations = new List<Coord>();
        
        // Start the movement analysis.
        public static void Resolve()
        {
            var input = ReadInput();
            var isDoubleLocationFound = false;
            foreach (string s in input)
            {
                myDirection = GetNewDirection(s[0]);
                for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                {
                    myPosition = GetNewPosition();

                    if (!isDoubleLocationFound && locations.Contains(myPosition))
                    {
                        WriteLine($"The Easter Bunny HQ real location is {Math.Abs(myPosition.x) + Math.Abs(myPosition.y)} block(s) away.");
                        isDoubleLocationFound = true;
                    }
                    else
                    {
                        locations.Add(myPosition);
                    }
                }
            }

            WriteLine($"The Easter Bunny HQ is {Math.Abs(myPosition.x) + Math.Abs(myPosition.y)} block(s) away.");
        }

        // Read the Day 1 input.
        private static string[] ReadInput()
        {
            return File.ReadAllText(@"Inputs\day1.txt").Split(',').Select(s => s.Trim()).ToArray();
        }

        // Get the character new position one block at a time for part 2.
        private static Coord GetNewPosition()
        {
            Coord newPosition;
            switch (myDirection)
            {
                case Direction.North:
                    newPosition = new Coord(myPosition.x, myPosition.y + 1 );
                    break;
                case Direction.East:
                    newPosition = new Coord(myPosition.x + 1, myPosition.y );
                    break;
                case Direction.South:
                    newPosition = new Coord(myPosition.x, myPosition.y - 1 );
                    break;
                default:
                    newPosition = new Coord(myPosition.x - 1, myPosition.y );
                    break;
            }
            return newPosition;

        }

        // Get the character new direction depending on previous direction.
        private static Direction GetNewDirection(char c)
        {
            Direction newDirection;
            if (c == 'R')
            {
                switch (myDirection)
                {
                    case Direction.North:
                        newDirection = Direction.East;
                        break;
                    case Direction.East:
                        newDirection = Direction.South;
                        break;
                    case Direction.South:
                        newDirection = Direction.West;
                        break;
                    default:
                        newDirection = Direction.North;
                        break;
                }
            }
            else
            {
                switch (myDirection)
                {
                    case Direction.North:
                        newDirection = Direction.West;
                        break;
                    case Direction.East:
                        newDirection = Direction.North;
                        break;
                    case Direction.South:
                        newDirection = Direction.East;
                        break;
                    default:
                        newDirection = Direction.South;
                        break;
                }
            }
            return newDirection;
        }

        
    }
}