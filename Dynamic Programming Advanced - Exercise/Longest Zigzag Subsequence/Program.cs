namespace Longest_Zigzag_Subsequence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                              .Split()
                              .Select(int.Parse)
                              .ToArray();

            int[,] dp = new int[2, numbers.Length];
            dp[0, 0] = 1;
            dp[1, 0] = 1;

            int[,] parent = new int[2, numbers.Length];
            parent[0, 0] = -1;
            parent[1, 0] = -1;

            int bestLength = 0;
            int lastCol = 0;
            int lastRow = 0;

            for (int current = 1; current < numbers.Length; current++)
            {
                int currentNumber = numbers[current];

                for (int prev = current - 1; prev >= 0; prev--)
                {
                    int prevNumber = numbers[prev];

                    if (currentNumber > prevNumber
                     && dp[1, prev] + 1 >= dp[0, current])
                    {
                        dp[0, current] = dp[1, prev] + 1;
                        parent[0, current] = prev;
                    }

                    if (prevNumber > currentNumber
                     && dp[0, prev] + 1 >= dp[1, current])
                    {
                        dp[1, current] = dp[0, prev] + 1;
                        parent[1, current] = prev;
                    }
                }

                if (dp[0, current] > bestLength)
                {
                    bestLength = dp[0, current];
                    lastCol = current;
                    lastRow = 0;
                }

                if (dp[1, current] > bestLength)
                {
                    bestLength = dp[1, current];
                    lastCol = current;
                    lastRow = 1;
                }
            }

            Stack<int> path = new Stack<int>();

            while (lastCol != -1)
            {
                path.Push(numbers[lastCol]);

                lastCol = parent[lastRow, lastCol];
                lastRow = lastRow == 0 ? 1 : 0;
            }

            Console.WriteLine(string.Join(" ", path));
        }
    }
}
