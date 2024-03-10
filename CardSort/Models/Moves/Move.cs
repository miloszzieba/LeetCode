using CardSort.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSort.Models.Moves
{
    internal abstract class Move
    {
        public MoveType Type { get; set; }

        protected Move(MoveType type)
        {
            Type = type;
        }
    }
}
