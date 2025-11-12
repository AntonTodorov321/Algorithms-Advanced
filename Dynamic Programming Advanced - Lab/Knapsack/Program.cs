namespace Knapsack
{
    class Item
    {
        public string Name { get; set; }

        public int Weight { get; set; }

        public int Value { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int maximumCapacity = int.Parse(Console.ReadLine());

            List<Item> items = new List<Item>();

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "end")
                {
                    break;
                }

                string[] itemData = line.Split(" ");

                Item item = new Item
                {
                    Name = itemData[0],
                    Weight = int.Parse(itemData[1]),
                    Value = int.Parse(itemData[2]),
                };

                items.Add(item);
            }

            int[,] dp = new int[items.Count + 1, maximumCapacity + 1];
            bool[,] used = new bool[items.Count + 1, maximumCapacity + 1];

            for (int item = 1; item < dp.GetLength(0); item++)
            {
                Item currentItem = items[item - 1];

                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    int excluding = dp[item - 1, capacity];

                    if (currentItem.Weight > capacity)
                    {
                        dp[item, capacity] = excluding;
                        continue;
                    }

                    int including = currentItem.Value + dp[item - 1, capacity - currentItem.Weight];

                    if (including > excluding)
                    {
                        dp[item, capacity] = including;
                        used[item, capacity] = true;
                    }
                    else
                    {
                        dp[item, capacity] = excluding;
                    }
                }
            }

            SortedSet<string> usedItems = new SortedSet<string>();
            int currentCapacity = maximumCapacity;
            int totalWeight = 0;

            for (int row = used.GetLength(0) - 1; row > 0; row--)
            {
                if (used[row, currentCapacity])
                {
                    Item item = items[row - 1];

                    usedItems.Add(item.Name);
                    currentCapacity -= item.Weight;
                    totalWeight += item.Weight;

                    if (currentCapacity == 0)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine($"Total Weight: {totalWeight}");
            Console.WriteLine($"Total Value: {dp[items.Count, maximumCapacity]}");
            Console.WriteLine(string.Join(Environment.NewLine, usedItems));
        }
    }
}
