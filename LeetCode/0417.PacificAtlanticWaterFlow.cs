using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class PacificAtlanticWaterFlow
    {
        public IList<IList<int>> BFS(int[][] heights)
        {
            if (heights == null || heights.Length == 0 || heights[0].Length == 0)
                return Array.Empty<IList<int>>();

            int n = heights.Length;
            int m = heights[0].Length;
            var directions = new (int row, int col)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

            var pacificConnected = TraverseOcean(heights, n, m, directions, GetPacificBorder(n, m));
            var atlanticConnected = TraverseOcean(heights, n, m, directions, GetAtlanticBorder(n, m));

            var result = new List<IList<int>>();
            for (int row = 0; row < n; row++)
                for (int col = 0; col < m; col++)
                    if (pacificConnected[row, col] && atlanticConnected[row, col])
                        result.Add(new List<int> { row, col });

            return result;
        }

        private static bool[,] TraverseOcean(
            int[][] heights,
            int rows,
            int cols,
            (int row, int col)[] directions,
            IEnumerable<(int row, int col)> borderCells)
        {
            var visited = new bool[rows, cols];
            var queue = new Queue<(int row, int col)>();

            foreach (var (row, col) in borderCells)
            {
                if (visited[row, col])
                    continue;

                visited[row, col] = true;
                queue.Enqueue((row, col));
            }

            while (queue.Count > 0)
            {
                var (row, col) = queue.Dequeue();
                foreach (var (dRow, dCol) in directions)
                {
                    int nextRow = row + dRow;
                    int nextCol = col + dCol;
                    if (nextRow < 0 || nextCol < 0 || nextRow >= rows || nextCol >= cols)
                        continue;
                    if (visited[nextRow, nextCol])
                        continue;
                    if (heights[nextRow][nextCol] < heights[row][col])
                        continue;

                    visited[nextRow, nextCol] = true;
                    queue.Enqueue((nextRow, nextCol));
                }
            }

            return visited;
        }

        private static IEnumerable<(int row, int col)> GetPacificBorder(int rows, int cols)
        {
            for (int col = 0; col < cols; col++)
                yield return (0, col);
            for (int row = 0; row < rows; row++)
                yield return (row, 0);
        }

        private static IEnumerable<(int row, int col)> GetAtlanticBorder(int rows, int cols)
        {
            for (int col = 0; col < cols; col++)
                yield return (rows - 1, col);
            for (int row = 0; row < rows; row++)
                yield return (row, cols - 1);
        }

        public IList<IList<int>> DFS(int[][] heights)
        {
            //When visiting, 0 means neither, 1 means P, 2 means A, 3 means both	
            int m = heights.Length, n = heights[0].Length;

            bool[,] visitedPacific = new bool[m, n];
            bool[,] visitedAlantic = new bool[m, n];
            int[,] map = new int[m, n];

            // Rows
            for (int j = 0; j < n; j++)
            {
                map[0, j] = 1; // Pacific
                map[m - 1, j] = 2; // Atlantic
            }

            // Cols
            for (int i = 0; i < m; i++)
            {
                map[i, 0] |= 1; // Pacific
                map[i, n - 1] |= 2; // Atlantic
            }

            // do DFS for Pacific top row
            for (int j = 0; j < n; j++)
                DFSRecursive(0, j, m, n, 1, heights, visitedPacific, map);

            // do DFS for Pacific left row
            for (int i = 0; i < m; i++)
                DFSRecursive(i, 0, m, n, 1, heights, visitedPacific, map);

            // do DFS for Atlantic bottom row
            for (int j = 0; j < n; j++)
                DFSRecursive(m - 1, j, m, n, 2, heights, visitedAlantic, map);

            // do DFS for Atlantic right row
            for (int i = 0; i < m; i++)
                DFSRecursive(i, n - 1, m, n, 2, heights, visitedAlantic, map);


            List<IList<int>> results = new();

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (map[i, j] == 3)
                        results.Add(new List<int> { i, j });

            return results;
        }

        public void DFSRecursive(int i, int j, int m, int n, int val, int[][] heights, bool[,] visited, int[,] map)
        {
            if (visited[i, j])
                return;

            visited[i, j] = true;
            map[i, j] |= val; // pacific or atlantic

            // find neighbors
            // up
            if (i - 1 >= 0 && heights[i][j] <= heights[i - 1][j])
                DFSRecursive(i - 1, j, m, n, val, heights, visited, map);

            // down
            if (i + 1 < m && heights[i][j] <= heights[i + 1][j])
                DFSRecursive(i + 1, j, m, n, val, heights, visited, map);

            // left
            if (j - 1 >= 0 && heights[i][j] <= heights[i][j - 1])
                DFSRecursive(i, j - 1, m, n, val, heights, visited, map);

            if (j + 1 < n && heights[i][j] <= heights[i][j + 1])
                DFSRecursive(i, j + 1, m, n, val, heights, visited, map);
        }
    }
}
