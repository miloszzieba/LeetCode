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


    //BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2251/21H1/May2021Update)
    //Intel Core i9-9900K CPU 3.60GHz(Coffee Lake), 1 CPU, 16 logical and 8 physical cores
    //.NET SDK= 6.0.402
    //  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2
    //  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2

    // * Summary for 10^4 nums.length *
    // -10^9 < nums1[i] < 10^9 and -10^9 < nums2[i] < 10^9
    //|        Method |          Mean |       Error |      StdDev | Allocated |
    //|-------------- |--------------:|------------:|------------:|----------:|
    //| FromTheMiddle |      8.768 ns |   0.0582 ns |   0.0516 ns |         - |
    //|  BinarySearch |     41.591 ns |   0.3006 ns |   0.2812 ns |         - |
    //|     MergeSort | 74,774.802 ns | 353.3740 ns | 313.2569 ns |   80024 B |

    // * Summary for 10^4 nums.length *
    // -10^9 < nums1[i] < 5*10^8 and 0 < nums2[i] < 10^9
    //|        Method |         Mean |      Error |     StdDev | Allocated |
    //|-------------- |-------------:|-----------:|-----------:|----------:|
    //| FromTheMiddle |  2,242.25 ns |   6.146 ns |   5.449 ns |         - |
    //|  BinarySearch |     38.45 ns |   0.069 ns |   0.057 ns |         - |
    //|     MergeSort | 44,431.76 ns | 692.713 ns | 647.964 ns |   80024 B |

    // * Summary for 10^6 nums.length *
    // -10^9 < nums1[i] < 5*10^8 and 0 < nums2[i] < 10^9
    //|        Method |            Mean |         Error |        StdDev | Allocated |
    //|-------------- |----------------:|--------------:|--------------:|----------:|
    //| FromTheMiddle |     3,291.10 ns |     11.411 ns |     10.116 ns |         - |
    //|  BinarySearch |        43.77 ns |      0.097 ns |      0.086 ns |         - |
    //|     MergeSort | 1,731,174.50 ns | 31,775.600 ns | 28,168.242 ns | 4040232 B |

    [InProcess]
    [MemoryDiagnoser(false)]
    public class MedianOfTwoSortedArrays
    {
        private int[] _nums1;
        private int[] _nums2;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            this._nums1 = Enumerable.Range(0, 1_0_000)
                .Select(_ => Random.Shared.Next(-1_000_000_000, 1_000_000_000))
                .OrderBy(x => x)
                .ToArray();
            this._nums2 = Enumerable.Range(0, 10_000)
                .Select(_ => Random.Shared.Next(-1_000_000_000, 1_000_000_000))
                .OrderBy(x => x)
                .ToArray();
        }

        [Benchmark]
        public double FromTheMiddle() => FromTheMiddle(this._nums1, this._nums2);
        [Benchmark]
        public double BinarySearch() => BinarySearch(this._nums1, this._nums2);
        [Benchmark]
        public double MergeSort() => MergeSort(this._nums1, this._nums2);

        public double FromTheMiddle(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
                return FromTheMiddle(nums2, nums1);

            if (nums1.Length == 0)
            {
                if (nums2.Length % 2 == 1)
                    return nums2[nums2.Length / 2];
                return (nums2[nums2.Length / 2 - 1] + nums2[nums2.Length / 2]) / 2d;
            }

            if (nums2.Length == 0)
            {
                if (nums1.Length % 2 == 1)
                    return nums1[nums1.Length / 2];
                return (nums1[nums1.Length / 2 - 1] + nums1[nums1.Length / 2]) / 2d;
            }

            if (nums1.Length == 1 && nums2.Length == 1)
            {
                return (nums1[0] + nums2[0]) / 2d;
            }

            var totalLength = nums1.Length + nums2.Length;
            var medianIndex = (totalLength - 1) / 2;

            var i = (nums1.Length - 1) / 2;
            var j = medianIndex - i - 1;

            if (nums1[i] > nums2[j])
            {
                while (j + 1 < nums2.Length && i >= 0 && nums1[i] > nums2[j + 1])
                {
                    i--;
                    j++;
                }
            }
            else if (nums1[i] < nums2[j])
            {
                while (i + 1 < nums1.Length && j >= 0 && nums2[j] > nums1[i + 1])
                {
                    i++;
                    j--;
                }
            }

            int result1;
            if (i < 0)
                result1 = nums2[j];
            else if (j < 0)
                result1 = nums1[i];
            else
                result1 = Math.Max(nums1[i], nums2[j]);

            if (totalLength % 2 == 1)
                return result1;

            int result2;
            if (i == nums1.Length - 1)
                result2 = nums2[j + 1];
            else if (j == nums2.Length - 1)
                result2 = nums1[i + 1];
            else
                result2 = Math.Min(nums1[i + 1], nums2[j + 1]);

            return (result1 + result2) / 2d;
        }

        public double BinarySearch(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
                return BinarySearch(nums2, nums1);

            var totalLength = nums1.Length + nums2.Length;
            int start = 0, end = nums1.Length;
            while (start <= end)
            {
                int i = (start + end) / 2;
                int j = (totalLength + 1) / 2 - i;

                int aLeft = (i == 0) ? Int32.MinValue : nums1[i - 1];
                int aRight = (i == nums1.Length) ? Int32.MaxValue : nums1[i];
                int bLeft = (j == 0) ? Int32.MinValue : nums2[j - 1];
                int bRight = (j == nums2.Length) ? Int32.MaxValue : nums2[j];

                if (aLeft <= bRight && bLeft <= aRight)
                {
                    if (totalLength % 2 == 0)
                        return (Math.Max(aLeft, bLeft) + Math.Min(aRight, bRight)) / 2d;
                    return Math.Max(aLeft, bLeft);
                }
                else if (aLeft > bRight) end = i - 1;
                else start = i + 1;

            }
            return 0d;
        }

        //It's here to make my first solution look good
        public double MergeSort(int[] nums1, int[] nums2)
        {
            int[] result = new int[nums1.Length + nums2.Length];

            int x = 0;
            int y = 0;
            int i = 0;
            while (x < nums1.Length || y < nums2.Length)
            {
                if (x < nums1.Length && (y == nums2.Length || nums1[x] < nums2[y]))
                {
                    result[i] = nums1[x];
                    x++;
                }
                else
                {
                    result[i] = nums2[y];
                    y++;
                }
                i++;
            }

            if (result.Length % 2 == 1)
                return result[result.Length / 2];
            return (result[result.Length / 2] + result[(result.Length / 2) - 1]) / 2d;
        }
    }
}
