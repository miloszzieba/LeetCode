using BenchmarkDotNet.Attributes;

namespace LeetCode
{
    //BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19042.2251/20H2/October2020Update)
    //Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
    //.NET SDK= 6.0.300
    //  [Host] : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT AVX2

    //Job = InProcess  Toolchain=InProcessEmitToolchain

    //// * Summary for 10^4 numbers from -10^9 to 10^9 range*
    // Target = data[Random from (0, 4999)] + data[Random from (5000, 9999)]
    //|         Method |       Mean |    Error |   StdDev | Allocated |
    //|--------------- |-----------:|---------:|---------:|----------:|
    //|       TwoLoops | 2,548.2 us | 41.05 us | 38.40 us |      35 B |
    //| TwoLoopsSorted | 2,869.8 us | 26.59 us | 23.57 us |  422843 B |
    //|     Dictionary |   225.5 us |  2.33 us |  2.18 us |  322690 B |

    //// * Summary for 10^4 numbers from -10^9 to 10^9 range*
    // Target = data[9998] + data[9999]
    // Best-case scenario for Dictionary. Worst-case scenario for TwoLoops. 
    //|         Method |             Mean |         Error |        StdDev | Allocated |
    //|--------------- |-----------------:|--------------:|--------------:|----------:|
    //|       TwoLoops | 14,285,584.25 ns | 79,351.620 ns | 66,262.196 ns |      44 B |
    //| TwoLoopsSorted |  8,261,266.72 ns | 66,517.940 ns | 62,220.922 ns |  422845 B |
    //|     Dictionary |         41.23 ns |      0.409 ns |      0.363 ns |     224 B |

    //// * Summary for 10^4 numbers from -10^9 to 10^9 range*
    // Target = data[0] + data[9999]
    // Worst-case scenario for Dictionary. O(n) scenario for TwoLoops
    // We can see overhead cost of sorting and creating (num, index) tuples
    //|         Method |         Mean |      Error |     StdDev | Allocated |
    //|--------------- |-------------:|-----------:|-----------:|----------:|
    //|       TwoLoops |     2.864 us |  0.0104 us |  0.0097 us |      32 B |
    //| TwoLoopsSorted | 3,405.745 us | 23.5914 us | 22.0674 us |  422843 B |
    //|     Dictionary |   396.379 us |  1.7353 us |  1.4490 us |  673174 B |

    //// * Summary for 10^4 numbers from -10^9 to 10^9 range*
    // Target = data[0] + data[9999]. Already sorted data
    // Worst-case scenario for Dictionary. O(n) scenario for TwoLoops
    // We can see overhead cost of creating (num, index) tuples
    //|         Method |       Mean |     Error |    StdDev | Allocated |
    //|--------------- |-----------:|----------:|----------:|----------:|
    //|       TwoLoops |   2.867 us | 0.0111 us | 0.0104 us |      32 B |
    //| TwoLoopsSorted | 667.324 us | 2.9088 us | 2.7209 us |  422842 B |
    //|     Dictionary | 395.224 us | 0.9380 us | 0.8315 us |  673174 B |

    [InProcess]
    [MemoryDiagnoser(false)]
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
            //this._data = hashSet.OrderBy(x => Random.Shared.Next()).ToArray();
            this._data = hashSet.OrderBy(x => x).ToArray();

            //this._target = this._data[Random.Shared.Next(0, 4999)] + this._data[Random.Shared.Next(5000, 9999)];
            this._target = this._data[0] + this._data[9999];
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

        //Assumption is that each input has exactly one solution.
        //This solution is not correct, if numbers are duplicated in nums array
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
