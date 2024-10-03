using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class MergeTwoSortedLists
    {
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null && list2 == null)
                return null;

            var a = list1;
            var b = list2;
            var firstNode = new ListNode();
            var c = firstNode;

            while (a != null && b != null)
            {
                if (a.Value < b.Value)
                {
                    c.Next = a;
                    a = a.Next;
                }
                else
                {
                    c.Next = b;
                    b = b.Next;
                }
                c = c.Next;
            }

            if (a != null)
                c.Next = a;
            if (b != null)
                c.Next = b;

            return firstNode.Next;
        }
    }
}
