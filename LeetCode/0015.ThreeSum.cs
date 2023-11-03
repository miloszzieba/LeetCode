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
    //[Host] : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2

    //Job = InProcess  Toolchain=InProcessEmitToolchain

    //// * Summary for 5*10^2 numbers in -10^5 to 10^5 range*
    //|            Method |        Mean |     Error |    StdDev |   Gen0 | Allocated |
    //|------------------ |------------:|----------:|----------:|-------:|----------:|
    //|        BruteForce | 38,623.9 us | 231.38 us | 205.11 us |      - |   7.33 KB |
    //| BruteForceTrimmed | 22,847.0 us |  80.64 us |  75.43 us |      - |    6.8 KB |
    //|       TwoPointers |    277.9 us |   4.03 us |   3.77 us | 0.4883 |   7.84 KB |
    //|        Dictionary |  1,773.3 us |   8.08 us |   7.17 us | 3.9063 |  41.24 KB |

    //// * Summary for 10^3 numbers in -10^5 to 10^5 range*
    //|            Method |       Mean |     Error |    StdDev |    Gen0 | Allocated |
    //|------------------ |-----------:|----------:|----------:|--------:|----------:|
    //|        BruteForce | 302.123 ms | 1.7626 ms | 1.4718 ms |       - |   61.3 KB |
    //| BruteForceTrimmed | 187.727 ms | 0.4693 ms | 0.4390 ms |       - |  59.66 KB |
    //|       TwoPointers |   1.143 ms | 0.0065 ms | 0.0058 ms |  5.8594 |  60.02 KB |
    //|        Dictionary |   6.690 ms | 0.0495 ms | 0.0413 ms | 15.6250 | 131.05 KB |

    [InProcess]
    [MemoryDiagnoser(true)]
    public class ThreeSum
    {
        private int[] _data;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var hashSet = new HashSet<int>();
            while (hashSet.Count < 1000)
            {
                hashSet.Add(Random.Shared.Next(-100_000, 100_000));
            }
            this._data = hashSet.OrderBy(x => Random.Shared.Next()).ToArray();
        }

        [Benchmark]
        public IList<IList<int>> BruteForce() => BruteForce(this._data);
        [Benchmark]
        public IList<IList<int>> BruteForceTrimmed() => BruteForceTrimmed(this._data);
        [Benchmark]
        public IList<IList<int>> TwoPointers() => TwoPointers(this._data);
        [Benchmark]
        public IList<IList<int>> Dictionary() => Dictionary(this._data);

        public IList<IList<int>> BruteForce(int[] nums)
        {
            var result = new List<IList<int>>();
            Array.Sort(nums);
            var last = nums[nums.Length - 1];

            for (int i = 0; i < nums.Length - 2; i++)
                if (i == 0 || nums[i] > nums[i - 1])
                    for (int j = i + 1; j < nums.Length - 1; j++)
                        if (j == i + 1 || nums[j] > nums[j - 1])
                            for (int k = j + 1; k < nums.Length; k++)
                                if (k == j + 1 || nums[k] > nums[k - 1])
                                    if (nums[i] + nums[j] + nums[k] == 0)
                                        result.Add(new List<int>(3) { nums[i], nums[j], nums[k] });

            return result;
        }

        public IList<IList<int>> BruteForceTrimmed(int[] nums)
        {
            var result = new List<IList<int>>();
            Array.Sort(nums);
            var last = nums[nums.Length - 1];

            for (int i = 0; i < nums.Length - 2 && nums[i] <= 0; i++)
                if (i == 0 || nums[i] > nums[i - 1])
                    for (int j = i + 1; j < nums.Length - 1 && nums[i] + nums[j] <= 0; j++)
                        if (nums[i] + nums[j] + nums[j + 1] <= 0
                            && (j == i + 1 || nums[j] > nums[j - 1]))
                            for (int k = j + 1; k < nums.Length && nums[i] + nums[j] + nums[k] <= 0; k++)
                                if (k == j + 1 || nums[k] > nums[k - 1])
                                    if (nums[i] + nums[j] + nums[k] == 0)
                                        result.Add(new List<int>(3) { nums[i], nums[j], nums[k] });

            return result;
        }

        public IList<IList<int>> TwoPointers(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums.Length < 3) return result;
            Array.Sort(nums);
            if (nums[0] > 0) return result;

            for (int i = 0; i < nums.Length - 2;)
            {
                if (nums[i] > 0) break;

                int target = -nums[i], left = i + 1, right = nums.Length - 1;
                while (left < right)
                {
                    int sum = nums[left] + nums[right];
                    if (sum < target) while (left < right && nums[++left] == nums[left - 1]) ;
                    else if (sum > target) while (left < right && nums[--right] == nums[right + 1]) ;
                    else
                    {
                        result.Add(new List<int>(3) { nums[i], nums[left], nums[right] });
                        while (left < right && nums[++left] == nums[left - 1]) ;
                        while (left < right && nums[--right] == nums[right + 1]) ;
                    }
                }

                while (++i < nums.Length && nums[i] == nums[i - 1]) ;
            }
            return result;
        }

        public IList<IList<int>> Dictionary(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums.Length < 3) return result;
            Array.Sort(nums);
            if (nums[0] > 0) return result;

            var dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
                dict[nums[i]] = i;

            int targetIndex = 0;
            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (nums[i] > 0) break;

                for (int j = i + 1; j < nums.Length - 1; j++)
                {
                    int target = -1 * (nums[i] + nums[j]);
                    if (dict.TryGetValue(target, out targetIndex) && targetIndex > j)
                        result.Add(new List<int>(3) { nums[i], nums[j], target });
                    j = dict[nums[j]];
                }
                i = dict[nums[i]];
            }
            return result;
        }
    }
}