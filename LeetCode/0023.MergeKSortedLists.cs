using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using Iced.Intel;
using LeetCode.Extensions;
using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    // * Summary *

    //    BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
    //Intel Core i9-9900K CPU 3.60GHz(Coffee Lake), 1 CPU, 16 logical and 8 physical cores
    //.NET SDK 10.0.101
    //  [Host] : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3

    //Job = InProcess  Toolchain=InProcessEmitToolchain InvocationCount = 1
    //UnrollFactor=1

    //| Method           | Mean      | Error    | StdDev   | Allocated   |
    //|----------------- |----------:|---------:|---------:|------------:|
    //| PriorityQueue    | 184.27 ms | 1.906 ms | 1.592 ms | 32769.95 KB |
    //| DivideAndConquer |  26.90 ms | 0.331 ms | 0.277 ms |     2.66 KB |

    [InProcess]
    [MemoryDiagnoser(false)]
    public class MergeKSortedLists
    {
        private ListNode[] _lists;

        //Unfortunately my solutions change the input lists, so I have to recreate them before each iteration.
        [IterationSetup] 
        public void MergeKSortedListsSetup()
        {
            var random = new Random(0);
            var arrayLength = 1000;

            this._lists = new ListNode[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                this._lists[i] = Enumerable.Range(0, random.Next(1, 1000)).Select(x => random.Next(-10_000, 10_000)).ToLinkedList();
            }
        }

        [Benchmark]
        public ListNode PriorityQueue() => PriorityQueue(this._lists);
        [Benchmark]
        public ListNode DivideAndConquer() => DivideAndConquer(this._lists);

        public ListNode PriorityQueue(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0)
                return null;

            var minHeap = new PriorityQueue<ListNode, int>();

            for (int i = 0; i < lists.Length; i++)
            {
                var listNode = lists[i];
                while (listNode != null)
                {
                    minHeap.Enqueue(listNode, listNode.Value);
                    listNode = listNode.Next;
                }
            }

            var dummyHead = new ListNode(0);
            var current = dummyHead;
            while (minHeap.Count > 0)
            {
                var node = minHeap.Dequeue();
                current.Next = node;
                current = node;
            }
            current.Next = null;

            return dummyHead.Next;
        }

        public ListNode DivideAndConquer(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0)
                return null;

            var interval = 1;
            while (interval < lists.Length)
            {
                for (int i = 0; i + interval < lists.Length; i += interval * 2)
                    lists[i] = MergeTwoLists(lists[i], lists[i + interval]);

                interval <<= 1;
            }

            return lists[0];
        }

        private ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null && list2 == null)
                return null;

            var a = list1;
            var b = list2;
            var firstNode = new ListNode();
            var c = firstNode;

            while (a != null && b != null)
            {
                if (a.Value < b.Value)
                {
                    c.Next = a;
                    a = a.Next;
                }
                else
                {
                    c.Next = b;
                    b = b.Next;
                }
                c = c.Next;
            }

            if (a != null)
                c.Next = a;
            if (b != null)
                c.Next = b;

            return firstNode.Next;
        }
    }
}
