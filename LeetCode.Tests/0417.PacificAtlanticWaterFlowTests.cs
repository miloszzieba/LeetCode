using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0417_PacificAtlanticWaterFlowTests
    {
        [Theory]
        [ClassData(typeof(PacificAtlanticWaterFlowTestData))]
        public void BFS(int[][] heights, IList<IList<int>> expected)
        {
            var pacificAtlanticWaterFlow = new PacificAtlanticWaterFlow();
            var result = pacificAtlanticWaterFlow.BFS(heights);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(PacificAtlanticWaterFlowTestData))]
        public void DFS(int[][] heights, IList<IList<int>> expected)
        {
            var pacificAtlanticWaterFlow = new PacificAtlanticWaterFlow();
            var result = pacificAtlanticWaterFlow.DFS(heights);
            result.Should().BeEquivalentTo(expected);
        }
    }

    public class PacificAtlanticWaterFlowTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new int[][] {
                    [1,2,2,3,5],
                    [3,2,3,4,4],
                    [2,4,5,3,1],
                    [6,7,1,4,5],
                    [5,1,1,2,4]
                },
                new List<IList<int>> {
                    new List<int> {0,4},
                    new List<int> {1,3},
                    new List<int> {1,4},
                    new List<int> {2,2},
                    new List<int> {3,0},
                    new List<int> {3,1},
                    new List<int> {4,0}
                }
            };
            yield return new object[] {
                new int[][] {
                    [2,1],
                    [1,2]
                },
                new List<IList<int>> {
                    new List<int> {0,0},
                    new List<int> {0,1},
                    new List<int> {1,0},
                    new List<int> {1,1}
                }
            };
            yield return new object[] {
                new int[][] { [1] },
                new List<IList<int>> {
                    new List<int> {0,0}
                }
            };
            yield return new object[] {
                new int[][] {
                    [10,10,10],
                    [10,1,10],
                    [10,10,10]
                },
                new List<IList<int>> {
                    new List<int> {0,0},
                    new List<int> {0,1},
                    new List<int> {0,2},
                    new List<int> {1,0},
                    new List<int> {1,2},
                    new List<int> {2,0},
                    new List<int> {2,1},
                    new List<int> {2,2}
                }
            };
            yield return new object[] {
                new int[][] {
                    [1,2,3],
                    [8,9,4],
                    [7,6,5]
                },
                new List<IList<int>> {
                    new List<int> {0,2},
                    new List<int> {1,0},
                    new List<int> {1,1},
                    new List<int> {1,2},
                    new List<int> {2,0},
                    new List<int> {2,1},
                    new List<int> {2,2}
                }
            };
            yield return new object[] {
                new int[][] {
                    [3,3,3,3,3],
                    [3,0,0,0,3],
                    [3,0,3,0,3],
                    [3,0,0,0,3],
                    [3,3,3,3,3]
                },
                new List<IList<int>> {
                    new List<int> {0,0},
                    new List<int> {0,1},
                    new List<int> {0,2},
                    new List<int> {0,3},
                    new List<int> {0,4},
                    new List<int> {1,0},
                    new List<int> {1,4},
                    new List<int> {2,0},
                    new List<int> {2,4},
                    new List<int> {3,0},
                    new List<int> {3,4},
                    new List<int> {4,0},
                    new List<int> {4,1},
                    new List<int> {4,2},
                    new List<int> {4,3},
                    new List<int> {4,4}
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

