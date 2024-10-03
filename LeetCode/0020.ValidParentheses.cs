using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class ValidParentheses
    {
        private static readonly char[] closingCharacters = [')', '}', ']'];
        private static readonly Dictionary<char, char> dict = new Dictionary<char, char>()
        {
            {')','('},
            {'}','{'},
            {']','['},
        };

        public bool IsValid(string s)
        {
            if (s == null || s.Length == 0) return true;
            if (s.Length % 2 == 1) return false;

            var stack = new Stack<char>();
            stack.Push(s[0]);
            for (int i = 1; i < s.Length; i++)
            {
                if (closingCharacters.Contains(s[i]))
                    if (stack.Any() && stack.Peek() == dict[s[i]]) stack.Pop();
                    else return false;
                else stack.Push(s[i]);
            }
            return stack.Count == 0;
        }
    }
}
