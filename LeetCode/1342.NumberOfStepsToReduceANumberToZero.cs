namespace LeetCode
{
    public class NumberOfStepsToReduceANumberToZero
    {
        public int NumberOfSteps(int num)
        {
            int counter = 0;
            while (num != 0)
            {
                if (num % 2 != 0)
                    counter++;
                num = num / 2;
                counter++;
            }
            return counter - 1;
        }
    }
}
