using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0143_ReorderListTests
    {
        [Theory]
        [ClassData(typeof(ReorderListTestData))]
        public void TwoPointers(ListNode head, ListNode expected)
        {
            var reorderList = new ReorderList();
            reorderList.TwoPointers(head);
            head.Should().BeEquivalentTo(expected);
        }

        public class ReorderListTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new int[] { 1, 2 }.ToLinkedList(),
                    new int[] { 1, 2 }.ToLinkedList(),
                };
                yield return new object[] {
                    new int[] { 1, 2, 3 }.ToLinkedList(),
                    new int[] { 1, 3, 2 }.ToLinkedList(),
                };
                yield return new object[] {
                    new int[] { 1, 2, 3, 4 }.ToLinkedList(),
                    new int[] { 1, 4, 2, 3 }.ToLinkedList(),
                };
                yield return new object[] {
                    new int[] { 1, 2, 3, 4, 5 }.ToLinkedList(),
                    new int[] { 1, 5, 2, 4, 3 }.ToLinkedList(),
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

