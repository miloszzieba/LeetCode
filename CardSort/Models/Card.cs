using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardSort.Models.Enums;

namespace CardSort.Models
{
    public struct Card
    {
        public Card(Color color, int number, Coord coord)
        {
            this.Color = color;
            this.Number = number;
            this.Coord = coord;
        }

        public Color Color { get; }
        public int Number { get; }
        public Coord Coord { get; }

        public Card WithNumber(int number)
        {
            return new Card(this.Color, number, this.Coord);
        }
    }
}
