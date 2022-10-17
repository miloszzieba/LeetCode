using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    internal class MiddleOfTheLinkedList
    {
        public ListNode MiddleNode(ListNode head)
        {
            if (head == null || head.next == null)
                return head;
            if (head.next.next == null)
                return head.next;

            ListNode fast = head;
            while (fast.next != null && fast.next.next != null)
            {
                fast = fast.next.next;
                head = head.next;
            }

            if (fast.next != null)
                return head.next;
            return head;
        }
    }
}
