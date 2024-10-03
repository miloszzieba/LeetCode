using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCode
{
    public class StringInputToNumberWithCommas
    {
        public (string Result, int Selector) Execute (string oldInput, string input, int selectorEnd)
        {
            var oldValue = Regex.Replace(oldInput, @"[^\d]", "");
            var newValue = Regex.Replace(input, @"[^\d]", "");
            if (newValue == "") return (newValue, 0);
            var newInput = Int32.Parse(newValue).ToString("#,##0");

            var inputDifference = input.Length - oldInput.Length;
            var inputValueDifference = newInput.Length - oldInput.Length;
            if (oldValue == newValue && inputDifference == -1)
                return (newInput, selectorEnd);
            var selector = selectorEnd - inputDifference + inputValueDifference;

            return (newInput, selector);
        }
    }

    public class StringInputToNumberWithCommasResult
    {
        public string ResultNumber { get; set; }
        public int Selector { get; set; }
    }
}
