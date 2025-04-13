using BenchmarkDotNet.Attributes;
using Iced.Intel;
using LeetCode.Extensions;
using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    //BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.2965/22H2/2022Update)
    //Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
    //.NET SDK 8.0.100
    //  [Host] : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
    //Job = InProcess  Toolchain=InProcessEmitToolchain

    //// * Summary for 10 graph nodes and 2 neighbours *
    //| Method | Mean     | Error    | StdDev   | Median   | Allocated |
    //|------- |---------:|---------:|---------:|---------:|----------:|
    //| BFS    | 635.9 ns | 22.50 ns | 64.91 ns | 615.3 ns |   1.16 KB |
    //| DFS    | 613.6 ns | 12.75 ns | 35.76 ns | 604.4 ns |   1.16 KB |

    //// * Summary for 10 graph nodes and 5 neighbours *
    //| Method | Mean     | Error     | StdDev    | Allocated |
    //|------- |---------:|----------:|----------:|----------:|
    //| BFS    | 1.956 us | 0.0186 us | 0.0165 us |   3.07 KB |
    //| DFS    | 1.835 us | 0.0366 us | 0.0500 us |    2.8 KB |

    //// * Summary for 10^2 graph nodes and 5 neighbours *
    //| Method | Mean     | Error    | StdDev   | Allocated |
    //|------- |---------:|---------:|---------:|----------:|
    //| BFS    | 18.95 us | 0.359 us | 0.336 us |  32.16 KB |
    //| DFS    | 27.36 us | 2.123 us | 6.260 us |  30.06 KB |

    //// * Summary for 10^2 graph nodes and 20 neighbours *
    //| Method | Mean     | Error    | StdDev   | Allocated |
    //|------- |---------:|---------:|---------:|----------:|
    //| BFS    | 70.40 us | 1.254 us | 1.674 us |  75.95 KB |
    //| DFS    | 72.77 us | 1.437 us | 1.817 us |  71.83 KB |

    //// * Summary for 10^3 graph nodes and 20 neighbours each *
    //| Method | Mean     | Error    | StdDev   | Allocated |
    //|------- |---------:|---------:|---------:|----------:|
    //| BFS    | 827.0 us | 16.53 us | 28.07 us | 756.38 KB |
    //| DFS    | 950.5 us | 18.08 us | 16.91 us |  724.2 KB |

    //// * Summary for 10^3 graph nodes and 40 neighbours each *
    //| Method | Mean     | Error     | StdDev    | Allocated |
    //|------- |---------:|----------:|----------:|----------:|
    //| BFS    | 1.817 ms | 0.0362 ms | 0.0594 ms |   1.25 MB |
    //| DFS    | 2.008 ms | 0.0408 ms | 0.0485 ms |   1.22 MB |

    //// * Summary for 5*10^3 graph nodes and 20 neighbours each *
    //// DFS with 10^4 graph nodes throws StackOverflow exception 
    //| Method | Mean     | Error    | StdDev   | Allocated |
    //|------- |---------:|---------:|---------:|----------:|
    //| BFS    | 14.25 ms | 0.245 ms | 0.205 ms |   3.61 MB |
    //| DFS    | 15.11 ms | 0.264 ms | 0.387 ms |   3.48 MB |

    [InProcess]
    [MemoryDiagnoser(false)]
    public class CloneGraph
    {
        private GraphNode _node;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            var range = 10;
            var neighbours = 2;
            var adjactentArray = Enumerable.Range(0, range)
                .Select(_ => Enumerable.Range(0, neighbours)
                    .Select(_ => Random.Shared.Next(1, range))
                    .ToArray())
                .ToArray();
            this._node = adjactentArray.GenerateGraph();
        }

        [Benchmark]
        public GraphNode BFS() => BFS(this._node);
        [Benchmark]
        public GraphNode DFS() => DFS(this._node);

        // Quite proud of this one.
        // I've figured it out myself to later find that it has its name.
        public GraphNode BFS(GraphNode node)
        {
            if (node == null)
                return null;

            var nodeClone = new GraphNode(node.Value);
            var dictionary = new Dictionary<GraphNode, GraphNode>()
                { {node,  nodeClone} };
            var queue = new Queue<(GraphNode, GraphNode)>();
            queue.Enqueue((node, nodeClone));

            while (queue.Any())
            {
                var (current, currentClone) = queue.Dequeue();
                foreach (var neighbour in current.Nodes)
                {
                    GraphNode neighbourClone;
                    if (!dictionary.TryGetValue(neighbour, out neighbourClone))
                    {
                        neighbourClone = new GraphNode(neighbour.Value);
                        dictionary.Add(neighbour, neighbourClone);
                        queue.Enqueue((neighbour, neighbourClone));
                    }
                    currentClone.Nodes.Add(neighbourClone);
                }
            }

            return nodeClone;
        }

        public GraphNode DFS(GraphNode node)
        {
            if (node == null) return null;

            var dictionary = new Dictionary<GraphNode, GraphNode>();
            return DFSCloneGraph(node, dictionary);
        }

        private GraphNode DFSCloneGraph(GraphNode node, Dictionary<GraphNode, GraphNode> dictionary)
        {
            if (dictionary.TryGetValue(node, out GraphNode? nodeClone))
                return nodeClone;

            nodeClone = new GraphNode(node.Value);
            dictionary.Add(node, nodeClone);
            foreach (var neighbour in node.Nodes)
                nodeClone.Nodes.Add(DFSCloneGraph(neighbour, dictionary));

            return nodeClone;
        }
    }
}
