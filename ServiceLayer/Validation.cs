using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer
{
    public class Validation
    {
        public bool IsIdNegative(int input) => (input < 0) ? true : false;
        public bool IsPriceNegative(double input) => (input < 0) ? true : false;

        public bool ContainsCharacters(string input)
        {
            return input.All(x => char.IsLetter(x) || x == ' ');
        }
    }
}
