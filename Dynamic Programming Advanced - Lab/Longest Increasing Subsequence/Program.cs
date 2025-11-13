namespace Longest_Increasing_Subsequence
{
    using System.Security;

    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                            .Split()
                            .Select(int.Parse)
                            .ToArray();

            int[] parent = new int[numbers.Length];
            int[] length = new int[numbers.Length];

            int bestIndex = 0;
            int bestLength = 0;

            for (int current = 0; current < numbers.Length; current++)
            {
                int currentLength = 1;
                int currentParent = -1;

                for (int prev = current - 1; prev >= 0; prev--)
                {
                    if (numbers[current] > numbers[prev]
                     && length[prev] + 1 >= currentLength)
                    {
                        currentLength = length[prev] + 1;
                        currentParent = prev;
                    }
                }

                length[current] = currentLength;
                parent[current] = currentParent;

                if (currentLength > bestLength)
                {
                    bestLength = currentLength;
                    bestIndex = current;
                }
            }

            Stack<int> result = new Stack<int>();

            while (bestIndex != -1)
            {
                result.Push(numbers[bestIndex]);
                bestIndex = parent[bestIndex];
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
