using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Myers
{
    public class Node
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }

        public Node(int coordinateX, int coordinateY)
        {
            this.CoordinateX = coordinateX;
            this.CoordinateY = coordinateY;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Node)
            {
                var node = (Node)obj;
                return this.CoordinateX == node.CoordinateX
                    && this.CoordinateY == node.CoordinateY;
            }

            return false;
        }
    }
}
