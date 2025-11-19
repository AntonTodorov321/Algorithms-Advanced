namespace Cable_Merchant
{
    using System;

    internal class Program
    {
        private static List<int> prices;
        private static int[] bestPrices;

        static void Main(string[] args)
        {
            prices = new List<int>() { 0 };
            prices.AddRange(Console.ReadLine()
                            .Split()
                            .Select(int.Parse));
            bestPrices = new int[prices.Count];

            int connectorPrice = int.Parse(Console.ReadLine());

            CutRoad(prices.Count - 1, connectorPrice);

            Console.WriteLine(string.Join(" ", bestPrices.Skip(1)));
        }

        private static int CutRoad(int length, int connectorPrice)
        {
            if (bestPrices[length] != 0)
            {
                return bestPrices[length];
            }

            int bestPrice = prices[length];

            for (int i = 1; i < length; i++)
            {
                int currentPrice =
                    prices[i] + CutRoad(length - i, connectorPrice) - 2 * connectorPrice;

                if (currentPrice > bestPrice)
                {
                    bestPrice = currentPrice;
                }
            }

            bestPrices[length] = bestPrice;
            return bestPrice;
        }
    }
}
