using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0021_MergeTwoSortedListsTests
    {
        [Theory]
        [ClassData(typeof(MergeTwoSortedListsTestData))]
        public void Solution(ListNode list1, ListNode list2, ListNode expected)
        {
            var mergeTwoSortedLists = new MergeTwoSortedLists();
            var result = mergeTwoSortedLists.MergeTwoLists(list1, list2);
            result.Should().BeEquivalentTo(expected);
        }
    }

    public class MergeTwoSortedListsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new int[] { 1, 2, 4 }.ToLinkedList(),
                new int[] { 1, 3, 4 }.ToLinkedList(),
                new int[] { 1, 1, 2, 3, 4, 4 }.ToLinkedList()
            };
            yield return new object[] {
                new int[0].ToLinkedList(),
                new int[0].ToLinkedList(),
                new int[0].ToLinkedList()
            };
            yield return new object[] {
                new int[] { 0 }.ToLinkedList(),
                new int[0].ToLinkedList(),
                new int[] { 0 }.ToLinkedList()
            };
            yield return new object[] {
                new int[0].ToLinkedList(),
                new int[] { 0 }.ToLinkedList(),
                new int[] { 0 }.ToLinkedList()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

