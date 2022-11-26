using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class MiddleOfTheLinkedList
    {
        public ListNode MiddleNode(ListNode head)
        {
            if (head == null || head.Next == null)
                return head;
            if (head.Next.Next == null)
                return head.Next;

            ListNode fast = head;
            while (fast.Next != null && fast.Next.Next != null)
            {
                fast = fast.Next.Next;
                head = head.Next;
            }

            if (fast.Next != null)
                return head.Next;
            return head;
        }
    }
}
