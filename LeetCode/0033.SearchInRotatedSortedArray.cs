using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class SearchInRotatedSortedArray
    {
        public int BinarySearch(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;
            if(nums.Length == 1)
                return nums[0] == target ? 0 : -1;

            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                // Better than (left + right) / 2 to prevent integer overflow
                var mid = left + (right - left) / 2;
                if (nums[mid] == target)
                    return mid;

                if (nums[mid] < nums[right])
                    if (nums[mid] < target && target <= nums[right])
                        left = mid + 1;
                    else
                        right = mid;
                else if (nums[left] <= target && target < nums[mid])
                    right = mid;
                else
                    left = mid + 1;
            }

            return nums[left] == target ? left : -1;
        }
    }
}
