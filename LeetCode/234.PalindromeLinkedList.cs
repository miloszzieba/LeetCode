using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class PalindromeLinkedList
    {
        private ListNode _head;

        public bool Stack(ListNode head)
        {
            if (head == null || head.next == null)
                return true;

            var stack = new Stack<int>();
            var node = head;
            while(node != null)
            {
                stack.Push(node.val);
                node = node.next;
            }

            var count = stack.Count / 2;
            for(int i = 0; i < count; i++)
            {
                if(stack.Pop() != head.val)
                    return false;
                head = head.next;
            }
            return true;
        }

        public bool GoToMiddleWhileReversingAndCompare(ListNode head)
        {
            if (head == null || head.next == null)
                return true;

            if (head.next.next == null)
                return head.val == head.next.val;

            ListNode previous = null, temp;
            ListNode slow = head, fast = head;
            while (fast.next != null && fast.next.next != null)
            {
                fast = fast.next.next;
                temp = slow.next;
                slow.next = previous;
                previous = slow;
                slow = temp;
            }

            if (fast.next != null)
            {
                if (slow.val != slow.next.val)
                    return false;
                slow = slow.next;
            }

            while (previous != null)
            {
                slow = slow.next;
                if (previous.val != slow.val)
                    return false;
                previous = previous.next;
            }

            return true;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}
