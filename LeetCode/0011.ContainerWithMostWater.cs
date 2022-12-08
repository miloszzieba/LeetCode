using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class ContainerWithMostWater
    {
        public int MaxAreaBruteForce(int[] height)
        {
            var max = 0;

            for (int i = 0; i < height.Length; i++)
                for (int j = i + 1; j < height.Length; j++)
                    max = Math.Max(max, (j - i) * Math.Min(height[i], height[j]));

            return max;
        }

        public int MaxAreaFromBothSides(int[] height)
        {
            int maxArea = 0;
            int i = 0;
            int j = height.Length - 1;
            while (i < j)
            {
                var left = height[i];
                var right = height[j];
                maxArea = Math.Max(maxArea, Math.Min(left, right) * (j - i));
                if (left <= right)
                    i++;
                if (left >= right)
                    j--;
            }
            return maxArea;
        }
    }
}
