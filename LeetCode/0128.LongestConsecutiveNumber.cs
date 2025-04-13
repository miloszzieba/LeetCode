using BenchmarkDotNet.Attributes;
using Iced.Intel;
using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    //// * Summary for 10^4 nums.length *

    //BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.2965/22H2/2022Update)
    //Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
    //.NET SDK 8.0.100
    //  [Host] : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2

    //Job = InProcess  Toolchain=InProcessEmitToolchain

    //| Method           | Mean       | Error     | StdDev    | Allocated |
    //|----------------- |-----------:|----------:|----------:|----------:|
    //| SortedArray      |   109.9 us |   4.97 us |  14.66 us |         - |
    //| SortedDictionary | 6,751.7 us | 267.86 us | 785.59 us |  480411 B |
    //| HashSet          |   915.5 us |  40.96 us | 120.79 us |  673142 B |

    //// * Summary for 10^5 nums.length *

    //BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.2965/22H2/2022Update)
    //Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
    //.NET SDK 8.0.100
    //  [Host] : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2

    //Job = InProcess  Toolchain=InProcessEmitToolchain

    //| Method           | Mean      | Error     | StdDev    | Median     | Allocated |
    //|----------------- |----------:|----------:|----------:|-----------:|----------:|
    //| SortedArray      |  1.016 ms | 0.0388 ms | 0.1074 ms |  0.9771 ms |       1 B |
    //| SortedDictionary | 77.509 ms | 1.4786 ms | 2.0240 ms | 77.3853 ms | 4800450 B |
    //| HashSet          |  9.638 ms | 0.3160 ms | 0.9219 ms |  9.4492 ms | 6038232 B |

    //// * Summary for 10^7 nums.length *

    //BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.2965/22H2/2022Update)
    //Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
    //.NET SDK 8.0.100
    //  [Host]     : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
    //  DefaultJob : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2


    //| Method           | Mean       | Error    | StdDev    | Median     | Allocated   |
    //|------------------|-----------:|---------:|----------:|-----------:|------------:|
    //| SortedArray      |   180.3 ms | 12.59 ms |  36.72 ms |   174.0 ms |       200 B |
    //| SortedDictionary | Benchmark SortedDictionary takes too long to run             |
    //| HashSet          | 2,809.6 ms | 54.53 ms | 129.59 ms | 2,762.8 ms | 471722848 B |

    [MemoryDiagnoser(false)]
    public class LongestConsecutiveNumber
    {
        private int[] _array;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            this._array = Enumerable.Range(0, 1_0_000_000)
                .Select(_ => Random.Shared.Next(-1_000_000_000, 1_000_000_000))
                .ToArray();
        }

        [Benchmark]
        public int SortedArray() => SortedArray(this._array);
        //[Benchmark]
        //public int SortedDictionary() => SortedDictionary(this._array);
        [Benchmark]
        public int HashSet() => HashSet(this._array);

        public int SortedArray(int[] nums)
        {
            if (nums == null) return 0;
            if (nums.Length < 2) return nums.Length;

            Array.Sort(nums);

            int longest = 1, current = 1;
            for(int i = 0; i < nums.Length - 1; ++i)
            {
                if (nums[i] == nums[i + 1]) continue;
                if (nums[i] + 1 == nums[i + 1])
                    current++;
                else
                {
                    longest = Math.Max(longest, current);
                    current = 1;
                }
            }
            return Math.Max(current, longest);
        }

        public int SortedDictionary(int[] nums)
        {
            if (nums == null) return 0;
            if (nums.Length < 2) return nums.Length;

            var dictionary = new SortedDictionary<int, bool>();
            for (int i = 0; i < nums.Length; ++i)
            {
                if (dictionary.ContainsKey(nums[i])) continue;
                if (dictionary.ContainsKey(nums[i]+1))
                    dictionary.Add(nums[i], true);
                else dictionary.Add(nums[i], false);
                if (dictionary.ContainsKey(nums[i]-1))
                    dictionary[nums[i]-1] = true;
            }

            int longest = 1, current = 1;
            foreach(var value in dictionary.Values)
            {
                if (value) current++;
                else
                {
                    longest = Math.Max(longest, current);
                    current = 1;
                }
            }
            return longest;
        }

        public int HashSet(int[] nums)
        {
            if (nums == null) return 0;
            if (nums.Length < 2) return nums.Length;

            var set = new Dictionary<int, bool>();
            for(int i = 0; i < nums.Length; ++i)
                set.TryAdd(nums[i], false);
            var longest = 1;
            for(int i = 0; i < nums.Length; ++i)
            {
                if (set.ContainsKey(nums[i] - 1)) continue;
                int head = nums[i] + 1;
                while (set.ContainsKey(head))
                    head++;
                longest = Math.Max(longest, head - nums[i]);
            }

            return longest;
        }
    }
}
