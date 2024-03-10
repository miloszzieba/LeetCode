using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSort.Models
{
    public class CardStack
    {
        public bool Blocked { get; set; }
        public Card[] Cards { get; set; } = new Card[0];
    }
}
