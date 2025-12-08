namespace Reaper_Man
{
    using System;
    using System.Security;

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
        private static int[] parent;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());
            int[] pathData = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

            ReadGraph(nodes, edges);

            int source = pathData[0];
            int destination = pathData[1];

            distance = new double[nodes];
            parent = new int[nodes];

            for (int node = 0; node < nodes; node++)
            {
                distance[node] = double.PositiveInfinity;
                parent[node] = -1;
            }

            distance[source] = 0;

            Dijkstra(source, destination);

            Stack<int> path = ReconstructPath(destination);

            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distance[destination]);
        }

        private static Stack<int> ReconstructPath(int node)
        {
            Stack<int> path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = parent[node];
            }

            return path;
        }

        private static void Dijkstra(int source, int destination)
        {
            PriorityQueue<int, double> queue = new PriorityQueue<int, double>();
            queue.Enqueue(source, distance[source]);

            while (queue.Count > 0)
            {
                int currentNode = queue.Dequeue();

                if (currentNode == destination)
                {
                    return;
                }

                foreach (var edge in graph[currentNode])
                {
                    if (double.IsPositiveInfinity(distance[edge.To]))
                    {
                        queue.Enqueue(edge.To, distance[edge.To]);
                    }

                    double newDistance = distance[edge.From] + edge.Weight;

                    if (newDistance < distance[edge.To])
                    {
                        distance[edge.To] = newDistance;
                        parent[edge.To] = currentNode;

                        queue.Enqueue(edge.To, distance[edge.To]);
                    }
                }
            }
        }

        private static void ReadGraph(int nodes, int edges)
        {
            graph = new List<Edge>[nodes];
            Dictionary<int, Dictionary<int, int>> tempGraph =
                new Dictionary<int, Dictionary<int, int>>();

            for (int node = 0; node < nodes; node++)
            {
                graph[node] = new List<Edge>();
                tempGraph[node] = new Dictionary<int, int>();
            }

            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

                int from = edgeData[0];
                int to = edgeData[1];
                int weight = edgeData[2];

                tempGraph[from][to] = weight;
            }

            foreach (var from in tempGraph)
            {
                foreach (var to in from.Value)
                {
                    Edge edge = new Edge
                    {
                        From = from.Key,
                        To = to.Key,
                        Weight = to.Value
                    };

                    graph[edge.From].Add(edge);
                }
            }
        }
    }
}
