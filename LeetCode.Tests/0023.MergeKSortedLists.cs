using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0023_MergeKSortedListsTests
    {
        [Theory]
        [ClassData(typeof(MergeKSortedListsTestData))]
        public void DivideAndConquer(ListNode[] lists, ListNode expected)
        {
            var mergeKSortedLists = new MergeKSortedLists();
            var result = mergeKSortedLists.DivideAndConquer(lists);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(MergeKSortedListsTestData))]
        public void PriorityQueue(ListNode[] lists, ListNode expected)
        {
            var mergeKSortedLists = new MergeKSortedLists();
            var result = mergeKSortedLists.PriorityQueue(lists);
            result.Should().BeEquivalentTo(expected);
        }
    }

    public class MergeKSortedListsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                null,
                null
            };
            yield return new object[] {
                new ListNode[] { },
                null
            };
            yield return new object[] {
                new ListNode[] { null, null },
                null
            };
            yield return new object[] {
                new ListNode[] {
                    new int[] { 1,4,5 }.ToLinkedList(),
                    new int[] { 1,3,4 }.ToLinkedList(),
                    new int[] { 2,6 }.ToLinkedList()
                },
                new int[] { 1,1,2,3,4,4,5,6 }.ToLinkedList()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

