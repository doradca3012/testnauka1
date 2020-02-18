using System.Collections.Generic;
using System.Linq;

namespace RecognizePdf
{
    public static class PeselHelper
    {
        public static IEnumerable<string> EnumeratePosiblePesels(string input, int length = 11)
        {
            var inputNumbers = string.Join("", input.ToCharArray().Where(char.IsDigit));

            for (var i = 0; i < inputNumbers.Length - length; i++)
            {
                var str = inputNumbers.Substring(i, length);
                if (input.Contains(str))
                {
                    yield return inputNumbers.Substring(i, length);
                }
            }
        }

        public static bool IsValidPesel(string pesel)
        {
            if (pesel.Length != 11 || pesel.Any(c => !char.IsDigit(c)))
            {
                return false;
            }

            var peselArray = pesel.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();

            var arr = new[] {
                1, 3, 7, 9, 1, 3, 7, 9, 1, 3
            };

            var calculatedSum = arr.Select((v, i) => v * peselArray[i]).Sum();

            var result = (10 - calculatedSum % 10) % 10;

            return result == peselArray.Last();
        }
    }
}
