using BenchmarkDotNet.Attributes;
using Iced.Intel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    //BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2364/21H1/May2021Update)
    //Intel Core i9-9900K CPU 3.60GHz(Coffee Lake), 1 CPU, 16 logical and 8 physical cores
    //.NET SDK= 7.0.401
    //  [Host] : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2

    //Job = InProcess  Toolchain=InProcessEmitToolchain

    //// * Summary for 5*10^2 numbers in -10^3 to 10^3 range*
    //|      Method |          Mean |       Error |      StdDev | Allocated |
    //|------------ |--------------:|------------:|------------:|----------:|
    //|  BruteForce | 35,796.210 us | 246.2854 us | 218.3256 us |      51 B |
    //| TwoPointers |      1.396 us |   0.0073 us |   0.0064 us |         - |

    [InProcess]
    [MemoryDiagnoser(false)]
    public class ThreeSumClosest
    {
        private int[] _data;
        private int _target;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var hashSet = new HashSet<int>();
            while (hashSet.Count < 500)
            {
                hashSet.Add(Random.Shared.Next(-1_000, 1_000));
            }
            this._data = hashSet.OrderBy(x => Random.Shared.Next()).ToArray();
            this._target = Random.Shared.Next(-10_000, 10_000);
        }

        [Benchmark]
        public int BruteForce() => BruteForce(this._data, this._target);
        [Benchmark]
        public int TwoPointers() => TwoPointers(this._data, this._target);

        public int BruteForce(int[] nums, int target)
        {
            var result = 0;
            var closest = int.MaxValue;

            for (int i = 0; i < nums.Length - 2; i++)
                for (int j = i + 1; j < nums.Length - 1; j++)
                    for (int k = j + 1; k < nums.Length; k++)
                    {
                        var sum = nums[i] + nums[j] + nums[k];
                        var difference = Math.Abs(sum - target);
                        if (closest > difference)
                        {
                            closest = difference;
                            result = sum;
                        }
                    }

            return result;
        }

        public int TwoPointers(int[] nums, int target)
        {
            if (nums.Length < 3) return int.MaxValue;

            Array.Sort(nums);

            var sum = nums[nums.Length - 3] + nums[nums.Length - 2] + nums[nums.Length - 1];
            if (sum <= target) return sum;

            sum = nums[0] + nums[1] + nums[2];
            if (sum >= target) return sum;

            for (int i = 0; i < nums.Length - 2; i++)
            {
                int j = i + 1;
                int k = nums.Length - 1;
                while (j < k)
                {
                    int temp = nums[i] + nums[j] + nums[k];
                    if (Math.Abs(temp - target) < Math.Abs(sum - target)) 
                        sum = temp;
                    if (temp > target)
                        k--;
                    else if (temp < target)
                        j++;
                    else return target;
                }
            }
            return sum;
        }
    }
}