namespace Code
{
    internal class Program
    {
        private static int[,] matrix;

        static void Main(string[] args)
        {
            int[] firstSequence = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

            int[] secondSequence = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

            matrix = new int[firstSequence.Length + 1, secondSequence.Length + 1];

            LCS(firstSequence, secondSequence);

            Stack<int> path = ReconstructPath(firstSequence, secondSequence);

            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(matrix[firstSequence.Length, secondSequence.Length]);
        }

        private static Stack<int> ReconstructPath(int[] firstSequence, int[] secondSequence)
        {
            Stack<int> path = new Stack<int>();

            int row = firstSequence.Length;
            int col = secondSequence.Length;

            while (row > 0 && col > 0)
            {
                if (firstSequence[row - 1] == secondSequence[col - 1])
                {
                    path.Push(firstSequence[row - 1]);

                    row--;
                    col--;
                }
                else if (matrix[row - 1, col] > matrix[row, col - 1])
                {
                    row--;
                }
                else
                {
                    col--;
                }
            }

            return path;
        }

        private static void LCS(int[] firstSequence, int[] secondSequence)
        {
            for (int row = 1; row < matrix.GetLength(0); row++)
            {
                for (int col = 1; col < matrix.GetLength(1); col++)
                {
                    if (firstSequence[row - 1] == secondSequence[col - 1])
                    {
                        matrix[row, col] = matrix[row - 1, col - 1] + 1;
                    }
                    else
                    {
                        matrix[row, col] = Math.Max(matrix[row - 1, col], matrix[row, col - 1]);
                    }
                }
            }
        }
    }
}
