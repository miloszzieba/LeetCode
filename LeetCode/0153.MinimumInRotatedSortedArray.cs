using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class MinimumInRotatedSortedArray
    {
        public int BinarySearch(int[] nums)
        {
            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                // Better than (left + right) / 2 to prevent integer overflow
                var mid = left + (right - left) / 2;
                if (nums[mid] > nums[right])
                    left = mid + 1;
                else
                    right = mid;
            }

            return nums[left];
        }
    }
}
