using CardSort.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSort.Models.Moves
{
    internal class SquashMove : Move
    {
        public List<Coord> Coords { get; set; }
        public Coord Target { get; private set; }

        public SquashMove(List<Coord> coords, Coord target)
            : base(MoveType.Squash)
        {
            this.Coords = coords;
            this.Target = target;
        }
    }
}
