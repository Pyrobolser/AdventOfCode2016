using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Console;

namespace AdventOfCode2016
{
    // http://adventofcode.com/2016/day/4
    public static class Day4
    {
        // Find the real rooms.
        public static void Resolve()
        {
            string[] input = ReadInput();
            int result = GetSectorIdSum(input);

            WriteLine($"The sum of the sector IDs of the real rooms is { result }.");
        }

        // Read the Day 4 input.
        private static string[] ReadInput()
        {
            return File.ReadAllLines(@"Inputs\Day4.txt");
        }

        // Get the sum of the sector id of the valid rooms.
        private static int GetSectorIdSum(string[] input)
        {
            var sum = 0;
            string[] details;
            foreach (string room in input)
            {
                details = GetRoomDetails(room);

                if (CheckRoomValidity(details[0].Replace("-", string.Empty), details[2]))
                {
                    sum += int.Parse(details[1]);
                    var roomName = DecypherRoomName(details[0].Replace("-", " "), int.Parse(details[1]));
                    if(roomName.Contains("northpole"))
                        WriteLine($"The room we are looking for is \"{ roomName }\" it is located in Sector ID {details[1]}.");
                }
            }
            return sum;
        }

        // Get the name, the sector id and the checksum of the room.
        private static string[] GetRoomDetails(string room)
        {
            string[] details = new string[3];
            var pattern = @"^(.+)-([0-9]+)\[([a-z]+)\]$";
            MatchCollection matches = Regex.Matches(room, pattern);

            details[0] = matches[0].Groups[1].Value;
            details[1] = matches[0].Groups[2].Value;
            details[2] = matches[0].Groups[3].Value;

            return details;
        }

        // Compare the given checksum with the checksum given by following the rules.
        private static bool CheckRoomValidity(string name, string checksum)
        {
            var rulesChecksum = string.Join("", name.GroupBy(c => c).OrderByDescending(c => c.Count()).ThenBy(c => c.Key).Take(5).Select(c => c.Key).ToList());
            return rulesChecksum == checksum;
        }

        // Decypher the roomname by shifting the letters.
        private static string DecypherRoomName(string name, int shift)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var builder = new StringBuilder(name);
            for(int i = 0; i < name.Length; i++)
            {
                if (builder[i] == ' ')
                    continue;
                builder[i] = alphabet[(alphabet.IndexOf(builder[i]) + shift) % 26];
            }
            
            return builder.ToString();
        }
    }
}
