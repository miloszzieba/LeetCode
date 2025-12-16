using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class LinkedListCycle
    {
        public bool Hashset(ListNode head)
        {
            var hashset = new HashSet<ListNode>();
            while (head != null)
            {
                if (hashset.Contains(head))
                    return true;

                hashset.Add(head);
                head = head.Next;
            }

            return false;
        }

        public bool TwoPointer(ListNode head)
        {
            if (head == null || head.Next == null) return false;

            var fastPointer = head;

            while (fastPointer != null && fastPointer.Next != null)
            {
                head = head.Next;
                fastPointer = fastPointer.Next.Next;
                if (head == fastPointer)
                    return true;
            }

            return false;
        }
    }
}
