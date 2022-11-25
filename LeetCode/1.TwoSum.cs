using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    //BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19042.2251/20H2/October2020Update)
    //Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
    //.NET SDK= 6.0.300
    //  [Host] : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT AVX2

    //Job = InProcess  Toolchain=InProcessEmitToolchain

    //// * Summary for 10^4 numbers from -10^9 to 10^9 range*
    // Target = data[Random from (0, 4999)] + data[Random from (5000, 9999)]
    //|         Method |       Mean |     Error |    StdDev |
    //|--------------- |-----------:|----------:|----------:|
    //|       TwoLoops | 5,751.3 us | 112.42 us | 120.29 us |
    //| TwoLoopsSorted | 1,561.1 us |  22.19 us |  18.53 us |
    //|     Dictionary |   308.4 us |   0.83 us |   0.65 us |

    //// * Summary for 10^4 numbers from -10^9 to 10^9 range*
    // Target = data[9998] + data[9999]
    // Best-case scenario for Dictionary. Worst-case scenario for TwoLoops. 
    //|         Method |             Mean |          Error |         StdDev |
    //|--------------- |-----------------:|---------------:|---------------:|
    //|       TwoLoops | 17,060,438.12 ns | 108,586.892 ns | 101,572.246 ns |
    //| TwoLoopsSorted |  4,239,401.79 ns |  47,967.158 ns |  42,521.637 ns |
    //|     Dictionary |         56.78 ns |       1.159 ns |       1.380 ns |

    //// * Summary for 10^4 numbers from -10^9 to 10^9 range*
    // Target = data[0] + data[9999]
    // Worst-case scenario for Dictionary. O(n) scenario for TwoLoops
    // We can see overhead cost of sorting and creating (num, index) tuples
    //|         Method |         Mean |      Error |     StdDev |
    //|--------------- |-------------:|-----------:|-----------:|
    //|       TwoLoops |     3.340 us |  0.0501 us |  0.0444 us |
    //| TwoLoopsSorted | 8,913.897 us | 98.6797 us | 92.3050 us |
    //|     Dictionary |   486.641 us |  3.7083 us |  3.0966 us |

    //// * Summary for 10^4 numbers from -10^9 to 10^9 range*
    // Target = data[0] + data[9999]. Already sorted data
    // Worst-case scenario for Dictionary. O(n) scenario for TwoLoops
    // We can see overhead cost of creating (num, index) tuples
    //|         Method |       Mean |      Error |     StdDev |
    //|--------------- |-----------:|-----------:|-----------:|
    //|       TwoLoops |   3.379 us |  0.0659 us |  0.1026 us |
    //| TwoLoopsSorted | 831.472 us | 13.1767 us | 11.0032 us |
    //|     Dictionary | 514.791 us |  9.7334 us | 12.3097 us |

    [InProcess]
    public class TwoSum
    {
        private int[] _data;
        private int _target;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var hashSet = new HashSet<int>();
            while (hashSet.Count < 10_000)
            {
                hashSet.Add(Random.Shared.Next(-1_000_000_000, 1_000_000_000));
            }
            this._data = hashSet.OrderBy(x => Random.Shared.Next()).ToArray();
            //this._data = hashSet.OrderBy(x => x).ToArray();

            this._target = this._data[Random.Shared.Next(0, 4999)] + this._data[Random.Shared.Next(5000, 9999)];
            //this._target = this._data[0] + this._data[9999];
            //this._target = this._data[9998] + this._data[9999];
        }

        [Benchmark]
        public int[] TwoLoops() => TwoLoops(this._data, this._target);
        [Benchmark]
        public int[] TwoLoopsSorted() => TwoLoopsSorted(this._data, this._target);
        [Benchmark]
        public int[] Dictionary() => Dictionary(this._data, this._target);

        public int[] TwoLoops(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                var diff = target - nums[i];
                if (diff > 1000_000_000)
                    continue;
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[j] == diff)
                        return new [] { i, j };
                }
            }

            throw new ArgumentException("Can't find pair with sum " + target);
        }

        public int[] TwoLoopsSorted(int[] nums, int target)
        {
            var sortedArray = nums
                .Select((num, index) => ( index, num))
                .OrderBy(x => x.num).ToArray();

            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                var diff = target - sortedArray[i].num;
                if (diff > 1000_000_000)
                    continue;
                for (int j = i + 1; j < sortedArray.Length; j++)
                {
                    if (sortedArray[j].num == diff)
                        return new [] { Math.Min(sortedArray[i].index, sortedArray[j].index), Math.Max(sortedArray[i].index, sortedArray[j].index) };
                    if (sortedArray[j].num > diff)
                        break;
                }
            }

            throw new ArgumentException("Can't find pair with sum " + target);
        }

        public int[] Dictionary(int[] nums, int target)
        {
            var dictionary = new Dictionary<int, int>();
            for (var i = nums.Length - 1; i >= 0; i--)
            {
                var diff = target - nums[i];

                if (dictionary.ContainsKey(diff))
                {
                    return new[] { i, dictionary[diff] };
                }

                dictionary[nums[i]] = i;
            }
            throw new ArgumentException("Can't find pair with sum " + target);
        }
    }
}
