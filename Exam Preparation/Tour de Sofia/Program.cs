namespace Tour_de_Sofia
{
    class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge>[] graph;
        private static double[] distance;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());
            int startNode = int.Parse(Console.ReadLine());

            ReadGraph(nodes, edges);

            distance = new double[nodes];
            Array.Fill(distance, double.PositiveInfinity);

            Dijkstra(startNode);

            if (double.IsPositiveInfinity(distance[startNode]))
            {
                Console.WriteLine(distance.Count(d => !double.IsPositiveInfinity(d)) + 1);
            }
            else
            {
                Console.WriteLine(distance[startNode]);
            }
        }

        private static void Dijkstra(int startNode)
        {
            PriorityQueue<int, double> queue = new PriorityQueue<int, double>();

            foreach (var edge in graph[startNode])
            {
                distance[edge.To] = edge.Weight;
                queue.Enqueue(edge.To, distance[edge.To]);
            }

            while (queue.Count > 0)
            {
                int minNode = queue.Dequeue();

                if (minNode == startNode)
                {
                    break;
                }

                foreach (var edge in graph[minNode])
                {
                    if (double.IsPositiveInfinity(distance[edge.To]))
                    {
                        queue.Enqueue(edge.To, distance[edge.To]);
                    }

                    double newDistance = edge.Weight + distance[minNode];

                    if (newDistance < distance[edge.To])
                    {
                        distance[edge.To] = newDistance;
                        queue.Enqueue(edge.To, edge.Weight);
                    }
                }
            }
        }

        private static void ReadGraph(int nodes, int edges)
        {
            graph = new List<Edge>[nodes];

            for (int node = 0; node < nodes; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

                int from = edgeData[0];

                graph[from].Add(new Edge
                {
                    From = from,
                    To = edgeData[1],
                    Weight = edgeData[2]
                });
            }
        }
    }
}
