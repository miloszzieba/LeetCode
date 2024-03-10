using CardSort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

internal static class CoordExtensions
{
    internal static Coord ToLeft(this Coord target) => new Coord(target.X - 1, target.Y);
    internal static Coord ToUp(this Coord target) => new Coord(target.X, target.Y - 1);
    internal static Coord ToRight(this Coord target) => new Coord(target.X + 1, target.Y);
    internal static Coord ToDown(this Coord target) => new Coord(target.X, target.Y + 1);

    internal static void AddUnique(this List<Coord> list, List<Coord> newItems)
    {
        list.AddRange(newItems.Except(list));
    }
}
