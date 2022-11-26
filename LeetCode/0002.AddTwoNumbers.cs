using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class AddTwoNumbers
    {
        public ListNode TwoToOne(ListNode l1, ListNode l2)
        {
            short carry = 0;
            var result = l1;

            while (true)
            {
                l1.Value += l2.Value + carry;
                carry = 0;
                if (l1.Value > 9)
                {
                    carry = 1;
                    l1.Value %= 10;
                }

                if (l1.Next == null || l2.Next == null)
                    break;

                l1 = l1.Next;
                l2 = l2.Next;
            }

            if (l1.Next == null)
            {
                l1.Next = l2.Next;
            }

            while(l1.Next != null && carry != 0)
            {
                l1 = l1.Next;
                if(++l1.Value > 9)
                {
                    carry = 1;
                    l1.Value %= 10;
                } else carry = 0;
            }

            if (l1.Next == null && carry != 0)
                l1.Next = new ListNode(1);

            return result;
        }

        public ListNode Sum(ListNode l1, ListNode l2)
        {
            short carry = 0;
            var result = new ListNode();
            var currentNode = result;
            while(l1 != null || l2 != null || carry > 0)
            {
                int sum = carry;
                carry = 0;

                if(l1 != null)
                {
                    sum += l1.Value;
                    l1 = l1.Next;
                }

                if(l2 != null)
                {
                    sum += l2.Value;
                    l2 = l2.Next;
                }

                if(sum > 9)
                {
                    carry = 1;
                    sum -= 10;
                }

                currentNode.Next = new ListNode(sum);
                currentNode = currentNode.Next;
            }

            return result.Next;
        }
    }
}
