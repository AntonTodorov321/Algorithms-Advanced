namespace Battle_Points
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] energy = Console.ReadLine()
                            .Split()
                            .Select(int.Parse)
                            .ToArray();

            int[] battlePoints = Console.ReadLine()
                            .Split()
                            .Select(int.Parse)
                            .ToArray();

            int maximumEnergy = int.Parse(Console.ReadLine());
            int[,] dp = new int[energy.Length + 1, maximumEnergy + 1];

            for (int enemyIndex = 1; enemyIndex < dp.GetLength(0); enemyIndex++)
            {
                int enemyEnergy = energy[enemyIndex - 1];
                int enemyBattlePoints = battlePoints[enemyIndex - 1];

                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    int excluding = dp[enemyIndex - 1, capacity];

                    if (enemyEnergy > capacity)
                    {
                        dp[enemyIndex, capacity] = excluding;
                        continue;
                    }

                    int including = enemyBattlePoints + dp[enemyIndex - 1, capacity - enemyEnergy];
                    dp[enemyIndex, capacity] = Math.Max(including, excluding);
                }
            }

            Console.WriteLine(dp[energy.Length, maximumEnergy]);
        }
    }
}
