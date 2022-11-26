using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class AddTwoNumbersTests
    {

        [Theory]
        [ClassData(typeof(AddTwoNumbersTestData))]
        public void TwoToOne(ListNode first, ListNode second, ListNode expectedResult)
        {
            var addTwoNumbers = new AddTwoNumbers();
            var result = addTwoNumbers.TwoToOne(first, second);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [ClassData(typeof(AddTwoNumbersTestData))]
        public void Sum(ListNode first, ListNode second, ListNode expectedResult)
        {
            var addTwoNumbers = new AddTwoNumbers();
            var result = addTwoNumbers.Sum(first, second);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class AddTwoNumbersTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new [] {9, 9, 1 }.ToLinkedList(),
                new [] {1 }.ToLinkedList(),
                new [] { 0, 0, 2 }.ToLinkedList()
            };
            yield return new object[] {
                new [] { 0 }.ToLinkedList(),
                new [] { 0 }.ToLinkedList(),
                new [] { 0 }.ToLinkedList()
            };
            yield return new object[] {
                new [] { 2, 4, 3 }.ToLinkedList(),
                new [] { 5, 6, 4 }.ToLinkedList(),
                new [] { 7, 0, 8 }.ToLinkedList()
            };
            yield return new object[] {
                new [] { 9, 9, 9, 9, 9, 9, 9 }.ToLinkedList(),
                new [] { 9, 9, 9, 9}.ToLinkedList(),
                new int[] { 8, 9, 9, 9, 0, 0, 0, 1 }.ToLinkedList()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}