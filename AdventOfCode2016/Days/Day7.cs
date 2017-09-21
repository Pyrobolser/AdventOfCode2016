using System.IO;
using System.Text.RegularExpressions;
using static System.Console;

namespace AdventOfCode2016
{
    // http://adventofcode.com/2016/day/7
    public static class Day7
    {
        // Find how much IPs support TLS/SSL.
        public static void Resolve()
        {
            string[] input = ReadInput();
            int supportTLS = 0, supportSSL = 0;
            foreach (var line in input)
            {
                if (IPSupportTLS(line))
                    supportTLS++;

                if (IPSupportSSL(line))
                    supportSSL++;
            }

            WriteLine($"{supportTLS} IPs support TLS.");
            WriteLine($"{supportSSL} IPs support SSL.");
        }

        // Read the Day 7 input.
        private static string[] ReadInput()
        {
            return File.ReadAllLines(@"Inputs\Day7.txt");
        }

        // Find if an IP supports TLS.
        private static bool IPSupportTLS(string ip)
        {
            return !Regex.IsMatch(ip, @"\[[a-z]*([a-z])(?!\1)([a-z])\2\1[a-z]*\]") && Regex.IsMatch(ip, @"([a-z])(?!\1)([a-z])\2\1");
        }

        // Find if an IP supports SSL.
        private static bool IPSupportSSL(string ip)
        {
            foreach (var outside in Regex.Split(ip, @"\[.*?\]"))
            {
                foreach (Match aba in Regex.Matches(outside, @"(?=((.)([^2])\2))"))
                {
                    if (IsBABAvailable(ip, string.Concat(aba.Groups[1].Value[1], aba.Groups[1].Value[0], aba.Groups[1].Value[1])))
                        return true;
                }
            }
            return false;
        }

        // Get the matching BAB if it exists.
        private static bool IsBABAvailable(string ip, string bab)
        {
            foreach (Match inside in Regex.Matches(ip, @"\[.*?\]"))
            {
                if (inside.Value.Contains(bab))
                    return true;
            }
            return false;
        }
    }
}
