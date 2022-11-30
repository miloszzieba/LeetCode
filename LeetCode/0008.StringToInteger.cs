using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class StringToInteger
    {
        public int MyAtoi(string s)
        {
            if (String.IsNullOrEmpty(s))
                return 0;

            int i = 0;
            for (; i < s.Length && (s[i] == ' '); i++) ;

            int result = 0, sign = 1;
            if (i < s.Length && (s[i] == '-' || s[i] == '+'))
            {
                if (s[i] == '-') sign = -1;
                i++;
            }

            for (; i < s.Length; i++)
            {
                if (s[i] < '0' || s[i] > '9')
                    return sign * result;

                var d = s[i] - '0';
                if (result > Int32.MaxValue / 10 ||
                    (result == Int32.MaxValue / 10 && d > 7 /* Int32.MaxValue % 10 */))
                    return sign > 0 ? Int32.MaxValue : Int32.MinValue;

                result = result * 10 + d;
            }
            return sign * result;
        }
    }
}
