namespace Medivac
{
    using System;
    using System.Runtime.Intrinsics.Arm;

    class Item
    {
        public int Name { get; set; }

        public int Capacity { get; set; }

        public int Rating { get; set; }
    }

    internal class Program
    {
        private static List<Item> items;

        static void Main(string[] args)
        {
            int maxCapacity = int.Parse(Console.ReadLine());

            items = new List<Item>();
            ReadInput();

            int[,] dp = new int[items.Count + 1, maxCapacity + 1];
            bool[,] path = new bool[items.Count + 1, maxCapacity + 1];

            for (int row = 1; row < dp.GetLength(0); row++)
            {
                Item item = items[row - 1];

                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    int excluding = dp[row - 1, capacity];

                    if (capacity < item.Capacity)
                    {
                        dp[row, capacity] = excluding;
                        continue;
                    }

                    int including = item.Rating + dp[row - 1, capacity - item.Capacity];

                    if (including > excluding)
                    {
                        dp[row, capacity] = including;
                        path[row, capacity] = true;
                    }
                    else
                    {
                        dp[row, capacity] = excluding;
                    }
                }
            }

            List<Item> takenItems = GetTakenItems(path, dp);

            Console.WriteLine(takenItems.Sum(i => i.Capacity));
            Console.WriteLine(dp[items.Count, maxCapacity]);

            foreach (var item in takenItems)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static List<Item> GetTakenItems(bool[,] path, int[,] dp)
        {
            List<Item> takenItems = new List<Item>();
            int capacity = path.GetLength(1) - 1;

            for (int row = path.GetLength(0) - 1; row > 0; row--)
            {
                if (path[row, capacity])
                {
                    Item item = items[row - 1];
                    takenItems.Add(item);

                    capacity -= item.Capacity;

                    if (capacity == 0)
                    {
                        break;
                    }
                }
            }

            return takenItems;
        }

        private static void ReadInput()
        {
            while (true)
            {
                string line = Console.ReadLine();

                if (line == "Launch")
                {
                    break;
                }

                int[] itemData = line.Split()
                                    .Select(int.Parse)
                                    .ToArray();

                items.Add(new Item
                {
                    Name = itemData[0],
                    Capacity = itemData[1],
                    Rating = itemData[2],
                });
            }
        }
    }
}
