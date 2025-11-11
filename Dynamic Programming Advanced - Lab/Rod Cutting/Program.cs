namespace Rod_Cutting
{
    using System;

    internal class Program
    {
        private static int[] prices;
        private static int[] memo;
        private static int[] prev;

        static void Main(string[] args)
        {
            prices = Console.ReadLine()
                            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();

            int length = int.Parse(Console.ReadLine());

            memo = new int[length + 1];
            prev = new int[length + 1];

            int bestPrice = CutRod(length);
            List<int> parts = GetParts(length);

            Console.WriteLine(bestPrice);
            Console.WriteLine(string.Join(" ", parts));
        }

        private static List<int> GetParts(int length)
        {
            List<int> result = new List<int>();

            while (length != 0)
            {
                result.Add(prev[length]);
                length -= prev[length];
            }

            return result;
        }

        private static int CutRod(int length)
        {
            if (length == 0)
            {
                return 0;
            }

            if (memo[length] != 0)
            {
                return memo[length];
            }

            int bestPrice = prices[length];
            int bestCombo = length;

            for (int i = 1; i < length; i++)
            {
                int currentPrice = prices[i] + CutRod(length - i);

                if (currentPrice > bestPrice)
                {
                    bestPrice = currentPrice;
                    bestCombo = i;
                }
            }

            memo[length] = bestPrice;
            prev[length] = bestCombo;

            return bestPrice;
        }
    }
}
