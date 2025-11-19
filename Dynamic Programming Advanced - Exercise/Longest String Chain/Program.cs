namespace Longest_String_Chain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] strings = Console.ReadLine().Split();

            int[] length = new int[strings.Length];
            int[] parent = new int[strings.Length];

            int bestLength = 0;
            int bestIndex = 0;

            for (int currentIndex = 0; currentIndex < strings.Length; currentIndex++)
            {
                string currentString = strings[currentIndex];

                int currentBestLength = 1;
                int currentParent = -1;

                for (int prevIndex = currentIndex - 1; prevIndex >= 0; prevIndex--)
                {
                    string prevString = strings[prevIndex];

                    if (currentString.Length > prevString.Length
                    && length[prevIndex] + 1 >= currentBestLength)
                    {
                        currentBestLength = length[prevIndex] + 1;
                        currentParent = prevIndex;
                    }
                }

                length[currentIndex] = currentBestLength;
                parent[currentIndex] = currentParent;

                if (currentBestLength > bestLength)
                {
                    bestLength = currentBestLength;
                    bestIndex = currentIndex;
                }
            }

            Stack<string> path = new Stack<string>();

            while (bestIndex != -1)
            {
                path.Push(strings[bestIndex]);
                bestIndex = parent[bestIndex];
            }

            Console.WriteLine(string.Join(" ", path));
        }
    }
}
