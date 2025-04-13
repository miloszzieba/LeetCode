using BenchmarkDotNet.Attributes;
using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class PalindromeLinkedList
    {
        public bool Stack(ListNode head)
        {
            if (head == null || head.Next == null)
                return true;

            var stack = new Stack<int>();
            var node = head;
            while(node != null)
            {
                stack.Push(node.Value);
                node = node.Next;
            }

            var count = stack.Count / 2;
            for(int i = 0; i < count; i++)
            {
                if(stack.Pop() != head.Value)
                    return false;
                head = head.Next;
            }
            return true;
        }

        public bool GoToMiddleWhileReversingAndCompare(ListNode head)
        {
            if (head == null || head.Next == null)
                return true;

            if (head.Next.Next == null)
                return head.Value == head.Next.Value;

            ListNode previous = null, temp;
            ListNode slow = head, fast = head;
            while (fast.Next != null && fast.Next.Next != null)
            {
                fast = fast.Next.Next;
                temp = slow.Next;
                slow.Next = previous;
                previous = slow;
                slow = temp;
            }

            if (fast.Next != null)
            {
                if (slow.Value != slow.Next.Value)
                    return false;
                slow = slow.Next;
            }

            while (previous != null)
            {
                slow = slow.Next;
                if (previous.Value != slow.Value)
                    return false;
                previous = previous.Next;
            }

            return true;
        }
    }
}
