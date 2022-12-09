using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class IntegerToRoman
    {
        private readonly (int, string)[] dict = new (int, string)[]{
            (1000, "M"),
            ( 900, "CM" ),
            ( 500, "D" ),
            ( 400, "CD" ),
            ( 100, "C" ),
            ( 90, "XC" ),
            ( 50, "L" ),
            ( 40, "XL" ),
            ( 10, "X" ),
            ( 9, "IX" ),
            ( 5, "V" ),
            ( 4, "IV" ),
            ( 1, "I" )
        };

        public string IntToRoman(int num)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < dict.Length; i++)
                while (num >= dict[i].Item1)
                {
                    sb.Append(dict[i].Item2);
                    num -= dict[i].Item1;
                }

            return sb.ToString();
        }
    }
}
