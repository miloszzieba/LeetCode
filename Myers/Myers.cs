using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myers
{
    internal class Myers
    {
        public List<Node> Diff(string[] oldLines, string[] newLines)
        {
            var prevNodes = new Dictionary<int, int>();

            var totalLength = oldLines.Length + newLines.Length + 1;
            var mapGapToX = new TwiceArray(totalLength);

            for (int pathLen = 0; pathLen <= totalLength; pathLen++)
            {
                for (int gap = -pathLen; gap <= pathLen; gap += 2)
                {
                    int prevX;
                    int prevY;
                    int currX;
                    int currY;
                    if (pathLen == 0)
                    {
                        prevY = prevX = 0;
                        currY = currX = WalkDiagonal(prevX, prevY, oldLines, newLines);
                    }
                    else
                    {
                        var down = (gap == -pathLen)
                            || (gap != pathLen
                            && mapGapToX[gap - 1] < mapGapToX[gap + 1]);

                        var prevGap = down ? gap + 1 : gap - 1;

                        prevX = mapGapToX[prevGap];
                        prevY = prevX - prevGap;

                        currX = down ? prevX : prevX + 1;
                        currY = currX - gap;

                        currX = WalkDiagonal(currX, currY, oldLines, newLines);
                        currY = currX - gap;
                    }

                    mapGapToX[gap] = currX;

                    prevNodes.Add(currX * totalLength + currY, prevX * totalLength + prevY);

                    if (currX >= oldLines.Length && currY >= newLines.Length)
                    {
                        return GetPath(oldLines, newLines, prevNodes);
                    }
                }
            }

            return null;
        }

        private List<Node> GetPath(string[] oldLines, string[] newLines, Dictionary<int, int> prevNodes)
        {
            var path = new List<Node>();

            int totalLength = oldLines.Length + newLines.Length + 1;

            var node = new Node(oldLines.Length, newLines.Length);
            path.Add(node);

            // (node.x >= 0 && node.y > 0) || (node.x > 0 && node.y >= 0)
            // node.x and node.y are both greater or equal to 0.
            // But they cannot be equal to 0 in the meanwhile.
            while (node.CoordinateX >= 0 && node.CoordinateY >= 0
                && (node.CoordinateY + node.CoordinateX > 0))
            {
                int prevNode = prevNodes[node.CoordinateX * totalLength + node.CoordinateY];
                int prevX = prevNode / totalLength;
                int prevY = prevNode % totalLength;

                node = new Node(prevX, prevY);
                path.Add(node);
            }

            path.Reverse();
            return path;
        }


        // Walk through diagonal as far as we can, because the path through diagonal is 0.
        private int WalkDiagonal(int currX, int currY, string[] oldLines, string[] newLines)
        {
            while (CheckBoundary(currX, oldLines.Length)
                && CheckBoundary(currY, newLines.Length)
                && oldLines[currX] == (newLines[currY]))
            {
                currX++;
                currY++;
            }
            return currX;
        }

        private bool CheckBoundary(int value, int max, int min = 0)
        {
            return min <= value && max > value;
        }
    }


    //Array in range of (-length, length).
    internal class TwiceArray
    {
        private int _length;
        private int[] _list;

        public TwiceArray(int length)
        {
            this._length = length;
            this._list = new int[this._length * 2];
        }

        public int this[int i]
        {
            get { return this._list[i + this._length]; }
            set { this._list[i + this._length] = value; }
        }
    }
}
