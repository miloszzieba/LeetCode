using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class ReverseInteger
    {
        public int Reverse(int x)
        {
            var result = 0;
            while (x != 0)
            {
                if (Math.Abs(result) > Int32.MaxValue / 10)
                    return 0;
                result = result * 10 + x % 10;
                x /= 10;
            }
            return result;
        }
    }
}
