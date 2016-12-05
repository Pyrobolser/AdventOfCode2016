using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace AdventOfCode2016
{
    public static class Day5
    {
        // Find the door hash.
        public static void Resolve()
        {
            var input = ReadInput();
            var firstDoor = GetPassword(input);
            var secondDoor = GetSecondPassword(input);

            Clear();
            WriteLine($"The password of the first door is { firstDoor }.");
            WriteLine($"The password of the second door is { secondDoor }.");
        }

        // Read the Day 5 input.
        private static string ReadInput()
        {
            return File.ReadAllText(@"Inputs\Day5.txt");
        }

        // Get the first door password.
        private static string GetPassword(string input)
        {
            var number = 0;
            StringBuilder pwdBuilder = new StringBuilder();
            var hash = string.Empty;
            using (MD5 md5 = MD5.Create())
            {
                while (pwdBuilder.Length < 8)
                {
                    hash = GetMd5Hash(md5, input + number.ToString());

                    if (hash.StartsWith("00000"))
                        pwdBuilder.Append(hash[5]);

                    number++;
                }
            }
            return pwdBuilder.ToString();
        }

        // Get the second door password.
        private static string GetSecondPassword(string input)
        {
            int value;
            var number = 0;
            var password = Enumerable.Repeat('*', 8).ToArray();
            var hash = string.Empty;
            WriteLine(string.Join("", password));

            using (MD5 md5 = MD5.Create())
            {
                while (password.Contains('*'))
                {
                    hash = GetMd5Hash(md5, input + number.ToString());
                    if (hash.StartsWith("00000") && int.TryParse(hash[5].ToString(), out value) && value < password.Length && password[value] == '*')
                    {
                        password[value] = hash[6];
                        Clear();
                        WriteLine(string.Join("", password));
                    }

                    number++;
                }
            }
            return string.Join("", password);
        }

        // Return the MD5 hash.
        private static string GetMd5Hash(MD5 hash, string input)
        {
            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
