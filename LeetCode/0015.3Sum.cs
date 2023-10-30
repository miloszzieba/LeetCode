using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class _3Sum
    {
        public List<List<int>> _3SumBruteForce(int[] nums)
        {
            var result = new List<List<int>>();
            Array.Sort(nums);
            var last = nums[nums.Length - 1];

            for(int i = 0; i < nums.Length - 2; i++)
                for(int j = i + 1; j < nums.Length - 1; j++)
                    for(int k = j + 1; k < nums.Length; k++)
                        if (nums[i] + nums[j] + nums[k] == 0)
                            result.Add(new List<int>(3) { nums[i], nums[j], nums[k] });

            return result;
        }
    }
}
