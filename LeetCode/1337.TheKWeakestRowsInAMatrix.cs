using BenchmarkDotNet.Attributes;

namespace LeetCode
{
    //// * Summary for 100 rows with 100 values and k:50 *

    // BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19042.1889/20H2/October2020Update)
    // Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
    // .NET SDK= 6.0.300
    // [Host] : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT AVX2

    // Job = InProcess  Toolchain=InProcessEmitToolchain

    //|                       Method |      Mean |     Error |    StdDev |   Gen0 | Allocated |
    //|----------------------------- |----------:|----------:|----------:|-------:|----------:|
    //|                InsertionSort |  3.131 us | 0.0468 us | 0.0415 us | 0.2708 |   2.24 KB |
    //| InsertionSortAndBinarySearch |  2.898 us | 0.0186 us | 0.0165 us | 0.2708 |   2.24 KB |
    //|                         LINQ | 35.269 us | 0.2131 us | 0.1994 us | 0.6104 |   5.38 KB |
    //|       MaxHeapAndBinarySearch |  7.502 us | 0.0231 us | 0.0205 us | 0.6485 |   5.32 KB |

    [InProcess]
    [MemoryDiagnoser(true)]
    public class TheKWeakestRowsInAMatrix
    {
        private int[][] _data;

        [GlobalSetup]
        public int[][] BenchmarkSetup()
        {
            this._data = Enumerable.Range(0, 100).Select(_ =>
            {
                var innerArray = new int[100];
                for (int i = 0; i < Random.Shared.Next(0, 100); i++)
                    innerArray[i] = 1;
                return innerArray;
            }).ToArray();
            return this._data;
        }

        [Benchmark]
        public void InsertionSort() => InsertionSort(this._data, 50);
        [Benchmark]
        public void InsertionSortAndBinarySearch() => InsertionSortAndBinarySearch(this._data, 50);
        [Benchmark]
        public void LINQ() => LINQ(this._data, 50);
        [Benchmark]
        public void MaxHeapAndBinarySearch() => MaxHeapAndBinarySearch(this._data, 50);

        public int[] InsertionSort(int[][] mat, int k)
        {
            var result = new int[k][];
            for (int i = 0; i < k; i++)
            {
                result[i] = new int[2] { i, 0 };
                int strength = GetRowStrength(mat[i]);

                var currentIndex = i;
                while (currentIndex > 0 && result[currentIndex - 1][1] > strength)
                {
                    result[currentIndex][0] = result[currentIndex - 1][0];
                    result[currentIndex][1] = result[currentIndex - 1][1];
                    currentIndex--;
                }
                result[currentIndex][0] = i;
                result[currentIndex][1] = strength;
            }

            for (int i = k; i < mat.Length; i++)
            {
                int strength = GetRowStrength(mat[i]);
                if (result[k - 1][1] <= strength)
                    continue;
                var currentIndex = k - 1;
                while (currentIndex > 0 && result[currentIndex - 1][1] > strength)
                {
                    result[currentIndex][0] = result[currentIndex - 1][0];
                    result[currentIndex][1] = result[currentIndex - 1][1];
                    currentIndex--;
                }
                result[currentIndex][0] = i;
                result[currentIndex][1] = strength;
            }
            return result.Select(x => x[0]).ToArray();
        }

        public int[] InsertionSortAndBinarySearch(int[][] mat, int k)
        {
            var result = new int[k][];
            result[0] = new int[2] { 0, GetRowStrengthBinarySearch(mat[0]) };
            for (int i = 1; i < k; i++)
            {
                result[i] = new int[2] { i, 0 };
                int strength = GetRowStrengthBinarySearch(mat[i]);

                var currentIndex = i;
                while (currentIndex > 0 && result[currentIndex - 1][1] > strength)
                {
                    result[currentIndex][0] = result[currentIndex - 1][0];
                    result[currentIndex][1] = result[currentIndex - 1][1];
                    currentIndex--;
                }
                result[currentIndex][0] = i;
                result[currentIndex][1] = strength;

            }

            for (int i = k; i < mat.Length; i++)
            {
                int strength = GetRowStrengthBinarySearch(mat[i]);
                if (result[k - 1][1] <= strength)
                    continue;
                var currentIndex = k - 1;
                while (currentIndex > 0 && result[currentIndex - 1][1] > strength)
                {
                    result[currentIndex][0] = result[currentIndex - 1][0];
                    result[currentIndex][1] = result[currentIndex - 1][1];
                    currentIndex--;
                }
                result[currentIndex][0] = i;
                result[currentIndex][1] = strength;
            }
            return result.Select(x => x[0]).ToArray();
        }

        private int GetRowStrength(int[] row)
        {
            int strength = 0;
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] == 0)
                    return strength;
                strength++;
            }
            return strength;
        }

        private int GetRowStrengthBinarySearch(int[] row)
        {
            // using binary search to count soldiers
            int start = -1;
            int end = row.Length;
            while (start + 1 < end)
            {
                int mid = start + (end - start) / 2;
                if (row[mid] == 0)
                    end = mid;
                else
                    start = mid;
            }
            return end;
        }

        public int[] LINQ(int[][] mat, int k)
        {
            return Enumerable.Range(0, mat.Length).OrderBy(x => mat[x].Sum()).ThenBy(x => x).Take(k).ToArray();
        }

        public int[] MaxHeapAndBinarySearch(int[][] mat, int k)
        {
            var maxHeap = new PriorityQueue<int, Row>(new MaxHeapRowComparer());

            for (int i = 0; i < mat.Length; i++)
            {
                var row = new Row(mat[i], i);
                maxHeap.Enqueue(i, row);
                if (maxHeap.Count > k)
                {
                    maxHeap.Dequeue();
                }
            }

            var res = new List<int>();
            while (maxHeap.Count > 0)
                res.Add(maxHeap.Dequeue());
            res.Reverse();
            return res.ToArray();
        }

        private class MaxHeapRowComparer : IComparer<Row>
        {
            public int Compare(Row x, Row y)
            {
                if (x.Strength == y.Strength)
                    return y.Index - x.Index;
                return y.Strength - x.Strength;
            }
        }

        private record Row
        {
            public Row(int[] row, int index)
            {
                this.Strength = this.GetRowStrengthBinarySearch(row);
                this.Index = index;
            }

            public int Index { get; }
            public int Strength { get; }

            private int GetRowStrengthBinarySearch(int[] row)
            {
                // using binary search to count soldiers
                int start = -1;
                int end = row.Length;
                while (start + 1 < end)
                {
                    int mid = start + (end - start) / 2;
                    if (row[mid] == 0)
                        end = mid;
                    else
                        start = mid;
                }
                return end;
            }
        }
    }
}
