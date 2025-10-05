namespace Bellman_Ford
{
    class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge> edges;
        private static double[] distance;
        private static int[] prev;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            edges = new List<Edge>();
            ReadGraph(edgesCount);

            int start = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            distance = new double[nodes + 1];
            Array.Fill(distance, double.PositiveInfinity);
            distance[start] = 0;

            prev = new int[nodes + 1];
            Array.Fill(prev, -1);

            bool isNegativeCycle = BelmanFord(nodes);

            if (isNegativeCycle)
            {
                Console.WriteLine("Negative Cycle Detected");
            }
            else
            {
                Stack<int> path = ReconstructPath(destination);

                Console.WriteLine(string.Join(" ", path));
                Console.WriteLine(distance[destination]);
            }
        }

        private static Stack<int> ReconstructPath(int destination)
        {
            Stack<int> path = new Stack<int>();
            int node = destination;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static bool BelmanFord(int nodes)
        {
            for (int i = 0; i < nodes - 1; i++)
            {
                bool updated = false;

                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(distance[edge.From]))
                    {
                        continue;
                    }

                    double newDistance = distance[edge.From] + edge.Weight;

                    if (distance[edge.To] > newDistance)
                    {
                        distance[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                        updated = true;
                    }
                }

                if (!updated)
                {
                    return false;
                }
            }

            foreach (var edge in edges)
            {
                if (distance[edge.To] > distance[edge.From] + edge.Weight)
                {
                    return true;
                }
            }

            return false;
        }

        private static void ReadGraph(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split(" ")
                                    .Select(int.Parse)
                                    .ToArray();

                Edge edge = new Edge
                {
                    From = edgeData[0],
                    To = edgeData[1],
                    Weight = edgeData[2],
                };

                edges.Add(edge);
            }
        }
    }
}
