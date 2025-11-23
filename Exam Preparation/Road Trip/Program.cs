namespace Road_Trip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] values = Console.ReadLine()
                            .Split(", ")
                            .Select(int.Parse)
                            .ToArray();

            int[] spaces = Console.ReadLine()
                           .Split(", ")
                           .Select(int.Parse)
                           .ToArray();

            int maximumCapacity = int.Parse(Console.ReadLine());

            int[,] dp = new int[values.Length + 1, maximumCapacity + 1];

            for (int row = 1; row < dp.GetLength(0); row++)
            {
                int itemSpace = spaces[row - 1];
                int itemValue = values[row - 1];

                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    int excluding = dp[row - 1, capacity];

                    if(itemSpace > capacity)
                    {
                        dp[row, capacity] = excluding;
                        continue;
                    }

                    int including = itemValue + dp[row - 1, capacity - itemSpace];
                    dp[row, capacity] = Math.Max(excluding, including);
                }
            }

            Console.WriteLine($"Maximum value: {dp[values.Length, maximumCapacity]}");
        }
    }
}
