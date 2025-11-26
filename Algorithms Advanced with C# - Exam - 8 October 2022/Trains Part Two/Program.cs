namespace Trains_Part_Two
{
    using System;

    class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

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
            int[] searchingPathParts = Console.ReadLine()
                                        .Split()
                                        .Select(int.Parse)
                                        .ToArray();

            int source = searchingPathParts[0];
            int destination = searchingPathParts[1];

            ReadGraph(nodes, edges);

            parent = new int[nodes];
            distance = new double[nodes];

            for (int node = 0; node < nodes; node++)
            {
                parent[node] = -1;
                distance[node] = double.PositiveInfinity;
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
                int minNode = queue.Dequeue();

                if (minNode == destination)
                {
                    break;
                }

                foreach (var edge in graph[minNode])
                {
                    int otherNode = edge.First == minNode ? edge.Second : edge.First;

                    if (double.IsPositiveInfinity(distance[otherNode]))
                    {
                        queue.Enqueue(otherNode, distance[otherNode]);
                    }

                    double newDistance = edge.Weight + distance[minNode];

                    if (newDistance < distance[otherNode])
                    {
                        distance[otherNode] = newDistance;
                        queue.Enqueue(otherNode, distance[otherNode]);

                        parent[otherNode] = minNode;
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

                int firstNode = edgeData[0];
                int secondNode = edgeData[1];

                Edge edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = edgeData[2]
                };

                graph[firstNode].Add(edge);
                graph[secondNode].Add(edge);
            }
        }
    }
}
