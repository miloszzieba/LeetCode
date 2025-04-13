using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Models
{
    public class GraphNode
    {
        public int Value { get; set; }
        public List<GraphNode> Nodes { get; set; } = new List<GraphNode>();

        public GraphNode(int value) {  Value = value; }
    }
}
