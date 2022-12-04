using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class PalindromeNumber
    {
        public bool IsPalindrome(int x)
        {
            if (x == 0)
                return true;

            if (x < 0 || x % 10 == 0)
                return false;

            var reverse = 0;
            while (x > reverse)
            {
                reverse = reverse * 10 + x % 10;
                x /= 10;
            }
            return reverse == x || x == reverse / 10;
        }
    }
}
