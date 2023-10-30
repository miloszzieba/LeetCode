using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myers
{
    internal class MyersOutput
    {
        public string GetMyersOutput(string[] oldLines, string[] newLines)
        {
            var myers = new Myers();
            var path = myers.Diff(oldLines, newLines);

            var prevNode = path.First();
            var sb = new StringBuilder();
            for (int i = 1; i < path.Count; i++)
            {
                var node = path[i];
                int gapDis = (node.CoordinateX - node.CoordinateY)
                    - (prevNode.CoordinateX - prevNode.CoordinateY);

                int posX = prevNode.CoordinateX;
                int posY = prevNode.CoordinateY;

                if (gapDis == 1)
                {
                    // right
                    sb
                        .Append("- " + oldLines[prevNode.CoordinateX])
                        .Append("\n");
                    posX++;
                }
                else if (gapDis == -1)
                {
                    // down
                    sb
                        .Append("+ " + newLines[prevNode.CoordinateY])
                        .Append("\n");
                    posY++;
                }

                for (; posX < node.CoordinateX && posY < node.CoordinateY
                    && posX < oldLines.Length && posY < newLines.Length; posX++, posY++)
                {
                    sb
                        .Append("  ")
                        .Append(oldLines[posX])
                        .Append("\n");
                }

                prevNode = node;
            }
            return sb.ToString();
        }
    }
}
