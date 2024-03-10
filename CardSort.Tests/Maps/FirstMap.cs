using CardSort.Models;
using CardSort.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSort
{
    internal static class FirstMap
    {
        internal static CardStack[,] InitMap()
        {
            var map = new CardStack[5, 5];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    map[i, j] = new CardStack();
            map[0, 0].Blocked = true;
            map[0, 4].Blocked = true;
            map[4, 0].Blocked = true;
            map[4, 4].Blocked = true;

            map[1, 0].Cards = new Card[]
            {
                new Card(Color.LightBlue, 1, new Coord(1,0))
            };
            map[2, 0].Cards = new Card[]
            {
                new Card(Color.Violet, 2, new Coord(2,0))
            };
            map[3, 0].Cards = new Card[]
            {
                new Card(Color.Pink, 4, new Coord(3,0))
            };
            map[1, 1].Cards = new Card[]
            {
                new Card(Color.Orange, 3, new Coord(1,1)),
                new Card(Color.Yellow, 1, new Coord(1,1)),
                new Card(Color.Pink, 1, new Coord(1,1)),
            };
            map[2, 1].Cards = new Card[]
            {
                new Card(Color.Yellow, 3, new Coord(2,1)),
                new Card(Color.Violet, 2, new Coord(2,1))
            };
            map[3, 1].Cards = new Card[]
            {
                new Card(Color.DarkBlue, 1, new Coord(3, 1)),
            };
            map[0, 2].Cards = new Card[]
            {
                new Card(Color.Pink, 3, new Coord(0, 2)),
                new Card(Color.Yellow, 2, new Coord(0, 2)),
            };
            map[1, 2].Cards = new Card[]
            {
                new Card(Color.Yellow, 2, new Coord(1, 2)),
            };
            map[2, 2].Cards = new Card[]
            {
                new Card(Color.Violet, 2, new Coord(2, 2)),
            };
            map[3, 2].Cards = new Card[]
            {
                new Card(Color.Pink, 5, new Coord(3, 2)),
            };
            map[4, 2].Cards = new Card[]
            {
                new Card(Color.Yellow, 2, new Coord(4, 2)),
            };
            map[0, 3].Cards = new Card[]
            {
                new Card(Color.Yellow, 3, new Coord(0, 3)),
                new Card(Color.Violet, 2, new Coord(0, 3)),
            };
            map[1, 3].Cards = new Card[]
            {
                new Card(Color.Pink, 2, new Coord(1, 3)),
                new Card(Color.LightBlue, 2, new Coord(1, 3)),
            };
            map[2, 3].Cards = new Card[]
            {
                new Card(Color.Orange, 2, new Coord(2, 3)),
            };
            map[3, 3].Cards = new Card[]
            {
                new Card(Color.Yellow, 1, new Coord(3, 3)),
                new Card(Color.Orange, 1, new Coord(3, 3)),
            };
            map[4, 3].Cards = new Card[]
            {
                new Card(Color.DarkBlue, 1, new Coord(4, 3)),
            };
            map[2, 4].Cards = new Card[]
            {
                new Card(Color.Violet, 1, new Coord(2, 4)),
            };

            return map;
        }
    }
}
