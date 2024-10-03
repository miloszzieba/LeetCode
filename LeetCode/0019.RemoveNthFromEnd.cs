using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class RemoveNthFromEnd
    {
        public ListNode Solution(ListNode head, int n)
        {
            int i = 0;
            ListNode nthNode = null, iterator = head;
            while (iterator != null)
            {
                iterator = iterator.Next;
                if (i == n) nthNode = head;
                if (i > n) nthNode = nthNode.Next;
                i++;
            }
            if (nthNode == null) return head.Next;
            nthNode.Next = nthNode.Next.Next;
            return head;
        }
    }
}
