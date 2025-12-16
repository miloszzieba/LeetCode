using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class ReorderList
    {
        public void TwoPointers(ListNode head)
        {
            if (head == null || head.Next == null || head.Next.Next == null) return;

            // 1. Find the middle of the linked list
            var slowPointer = head;
            var fastPointer = head;
            while (fastPointer.Next != null && fastPointer.Next.Next != null)
            {
                slowPointer = slowPointer.Next;
                fastPointer = fastPointer.Next.Next;
            }
            var secondHalfPointer = slowPointer.Next;

            // 2. Reverse the second half of the linked list
            if (secondHalfPointer.Next != null)
            {
                var prev = secondHalfPointer;
                secondHalfPointer = secondHalfPointer.Next;
                prev.Next = null;
                while (secondHalfPointer.Next != null)
                {
                    var next = secondHalfPointer.Next;
                    secondHalfPointer.Next = prev;
                    prev = secondHalfPointer;
                    secondHalfPointer = next;
                }
                secondHalfPointer.Next = prev;
            }

            // 3. Merge two halves
            var firstHalfPointer = head;
            while (secondHalfPointer != null)
            {
                var next = firstHalfPointer.Next;
                firstHalfPointer.Next = secondHalfPointer;
                var nextSecondHalf = secondHalfPointer.Next;
                secondHalfPointer.Next = next;
                firstHalfPointer = next;
                secondHalfPointer = nextSecondHalf;
            }
            firstHalfPointer.Next = null;
        }
    }
}
