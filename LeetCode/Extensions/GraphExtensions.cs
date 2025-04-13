using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Extensions
{
    public static class GraphExtensions
    {
        public static GraphNode GenerateGraph(this int[][] adjacencyArray)
        {
            if (adjacencyArray.Length == 0) return null;
            var array = new GraphNode[adjacencyArray.Length];
            for (int i = 0; i < adjacencyArray.Length; i++)
            {
                array[i] = new GraphNode(i + 1);
            }
            for (int i = 0; i < adjacencyArray.Length; i++)
                for (int j = 0; j < adjacencyArray[i].Length; j++)
                    array[i].Nodes.Add(array[adjacencyArray[i][j] - 1]);

            return array[0];
        }
    }
}
