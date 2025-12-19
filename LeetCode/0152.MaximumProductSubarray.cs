using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class MaximumProductSubarray
    {
        public int BruteForce(int[] nums)
        {
            var maximum = int.MinValue;
            for (int i = 0; i < nums.Length; i++)
            {
                int product = 1;
                for (int j = i; j < nums.Length; j++)
                {
                    product *= nums[j];
                    maximum = Math.Max(maximum, product);
                }
            }
            return maximum;
        }

        public int Scan(int[] nums)
        {
            int maximum = nums[0];
            int left = 0, right = 0, total = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    maximum = Math.Max(maximum, 0);
                    left = right = total = 0;
                    continue;
                }

                // We start calculating `right` once we encounter the first negative number, excluding that negative.
                if (total < 0 && right == 0) right = 1;
                if (right != 0) right *= nums[i];

                //We calculate total on every iteration
                total = total == 0 ? nums[i] : total * nums[i];

                //We calculate 'left' only when it's positive and we stop when we encounter uneven negative numbers.
                if (total > 0) left = total;

                if (total > 0 || (left == 0 && right == 0))
                    maximum = Math.Max(maximum, total);
                else
                    maximum = Math.Max(maximum, Math.Max(left, right));
            }

            return maximum;
        }

        public int MinMax(int[] nums)
        {
            int minProd = nums[0];
            int maxProd = nums[0];
            int result = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                int x = nums[i];
                int tempMax = Math.Max(x, Math.Max(x * maxProd, x * minProd));

                minProd = Math.Min(x, Math.Min(x * maxProd, x * minProd));
                maxProd = tempMax;
                result = Math.Max(maxProd, result);
            }
            return result;
        }
    }
}
