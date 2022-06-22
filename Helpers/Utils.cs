using System;
using System.Collections.Generic;
using System.Text;

namespace BookAPI.Helpers
{
    class Utils
    {
        public static string RandomString(int length)
        {
            string characters = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            var random = new Random();
            for (int i = 0; i < length; i++)
            {
                _ = result.Append(characters[random.Next(characters.Length)]);
            }

            return result.ToString();
        }
    }
}
