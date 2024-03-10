using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardSort.Models.Enums;

namespace CardSort.Models
{
    internal class Snake
    {
        public Snake(Card card)
        {
            this.Cards.Add(card);
            this.Coords.Add(card.Coord);
            this.Color = card.Color;
        }

        public Color Color { get; }
        public List<Card> Cards { get; } = new List<Card>();
        public List<Coord> Coords { get; } = new List<Coord>();

        public void AddCard(Card card)
        {
            this.Cards.Add(card);
            this.Coords.Add(card.Coord);
        }
    }
}
