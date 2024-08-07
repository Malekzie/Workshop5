using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.Utils
{
    public class BookingUtil
    {
        public static string GenerateBookingNumber()
        {
            Random random = new Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";

            // Generate 3 random letters
            char[] randomLetters = Enumerable.Range(0, 3)
                .Select(_ => letters[random.Next(letters.Length)])
                .ToArray();

            // Generate 3 random numbers
            char[] randomNumbers = Enumerable.Range(0, 3)
                .Select(_ => digits[random.Next(digits.Length)])
                .ToArray();

            // Combine letters and numbers
            char[] combined = randomLetters.Concat(randomNumbers).ToArray();

            // Shuffle the combined array
            return new string(combined.OrderBy(_ => random.Next()).ToArray());
        }
    }
}
