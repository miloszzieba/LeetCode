using CardSort.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSort.Models.Moves
{
    internal class BingoMove : Move
    {
        public List<Coord> Coords { get; set; }

        public BingoMove(List<Coord> coords)
            : base(MoveType.Bingo)
        {
            this.Coords = coords;
        }
    }
}
