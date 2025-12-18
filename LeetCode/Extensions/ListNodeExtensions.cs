using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Extensions
{
    public static class ListNodeExtensions
    {
        public static ListNode ToLinkedList(this int[] array)
        {
            if (array == null || array.Length == 0)
                return null;
            var result = new ListNode(array[0]);
            var currentNode = result;
            for (int i = 1; i < array.Length; i++)
            {
                currentNode.Next = new ListNode(array[i]);
                currentNode = currentNode.Next;
            }
            return result;
        }

        public static ListNode ToLinkedList(this IEnumerable<int> enumerable)
        {
            if (enumerable == null || !enumerable.Any())
                return null;

            var result = new ListNode(enumerable.First());
            var currentNode = result;

            foreach (var (item, index) in enumerable.Select((value, i) => (value, i)))
            {
                if (index == 0) continue;
                currentNode.Next = new ListNode(item);
                currentNode = currentNode.Next;
            }

            return result;
        }
    }
}
