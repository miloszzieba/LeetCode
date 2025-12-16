using BenchmarkDotNet.Attributes;
using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class MissingNumber
    {
        public int Sum(int[] nums)
        {
            var sumTotal = (nums.Length * (nums.Length + 1)) / 2;
            var sumMissing = 0;
            for (int i = 0; i < nums.Length; i++)
                sumMissing += nums[i];

            return sumTotal - sumMissing;
        }
    }
}
