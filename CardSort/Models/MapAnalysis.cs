using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardSort.Models.Moves;

namespace CardSort.Models
{
    internal class MapAnalysis
    {
        internal MapAnalysis(int maxX, int maxY)
        {
            this.FieldStates = new (int, int?)[maxX, maxY];
        }

        internal int ReleasedFields { get; set; }
        internal int Bingos { get; set; }
        internal List<Move> Moves { get; set; } = new List<Move>();
        internal (int offset, int? tempNumber)[,] FieldStates { get; set; }
        internal List<Coord> FieldsToCheck { get; set; } = new List<Coord>();

        private MapAnalysis() { }

        internal MapAnalysis GetCopy()
        {
            return new MapAnalysis()
            {
                ReleasedFields = ReleasedFields,
                Bingos = Bingos,
                Moves = new List<Move>(this.Moves),
                FieldStates = this.FieldStates.Clone() as (int, int?)[,]
            };
        }
    }
}
