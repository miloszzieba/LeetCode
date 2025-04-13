using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0133_CloneGraphTests
    {
        // For simplicity, each node's value is the same as the node's index(1-indexed).
        // For example, the first node with val == 1, the second node with val == 2, and so on.

        // The graph is represented in the test case using an adjacency list.
        // An adjacency list is a collection of unordered lists used to represent a finite graph.
        // Each list describes the set of neighbors of a node in the graph.

        [Theory]
        [ClassData(typeof(CloneGraphTestData))]
        public void BFS(int[][] adjacencyArray)
        {
            var head = adjacencyArray.GenerateGraph();
            var cloneGraph = new CloneGraph();
            var result = cloneGraph.BFS(head);
            if (adjacencyArray.Length == 0)
                result.Should().BeNull();
            else
            {
                result.Value.Should().Be(1);
                VerifyGraph(result, adjacencyArray);
            }
        }

        [Theory]
        [ClassData(typeof(CloneGraphTestData))]
        public void DFS(int[][] adjacencyArray)
        {
            var head = adjacencyArray.GenerateGraph();
            var cloneGraph = new CloneGraph();
            var result = cloneGraph.DFS(head);
            if (adjacencyArray.Length == 0)
                result.Should().BeNull();
            else
            {
                result.Value.Should().Be(1);
                VerifyGraph(result, adjacencyArray);
            }
        }

        private void VerifyGraph(GraphNode head, int[][] adjacencyArray)
        {
            var array = new GraphNode[adjacencyArray.Length];
            var queue = new Queue<GraphNode>();
            queue.Enqueue(head);
            while (queue.Any())
            {
                var node = queue.Dequeue();
                foreach (var neighbour in node.Nodes)
                    if (array[neighbour.Value - 1] == null)
                    {
                        array[neighbour.Value - 1] = neighbour;
                        queue.Enqueue(neighbour);
                    }
            }

            for (int i = 0; i < adjacencyArray.Length; i++)
                for (int j = 0; j < adjacencyArray[i].Length; j++)
                    array[i].Nodes.Should().Contain(x => x.Value == adjacencyArray[i][j]);
        }
    }

    public class CloneGraphTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new int[][] { [2, 4], [1, 3], [2, 4], [1, 3] }
            };
            yield return new object[] {
                new int[][] { [] }
            };
            yield return new object[] {
                new int[][] { }
            };
            yield return new object[]
            {
                new int[][] { [2, 5], [1, 3], [2, 4], [3, 5], [1, 4] }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

