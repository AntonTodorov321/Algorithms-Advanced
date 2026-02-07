namespace Dora_the_Explorer
{
    class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static Dictionary<int, List<Edge>> graph;
        private static double[] distance;
        private static int[] parent;

        static void Main(string[] args)
        {
            int edges = int.Parse(Console.ReadLine());

            int maxNode = ReadGraph(edges);

            int exploringCityDelay = int.Parse(Console.ReadLine());
            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            distance = new double[maxNode + 1];
            parent = new int[maxNode + 1];

            for (int node = 0; node < maxNode + 1; node++)
            {
                distance[node] = double.PositiveInfinity;
                parent[node] = -1;
            }

            distance[source] = 0;

            Dijkstra(source, destination, exploringCityDelay);
            Stack<int> path = ReconstructPath(destination);

            Console.WriteLine($"Total time: {distance[destination] - exploringCityDelay}");

            foreach (var node in path)
            {
                Console.WriteLine(node);
            }
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

        private static void Dijkstra(int source, int destination, int exploringCityDelay)
        {
            PriorityQueue<int, double> queue = new PriorityQueue<int, double>();
            queue.Enqueue(source, distance[source]);

            while (queue.Count > 0)
            {
                int currentNode = queue.Dequeue();

                if (currentNode == destination)
                {
                    break;
                }

                foreach (var edge in graph[currentNode])
                {
                    int otherNode = edge.First == currentNode ? edge.Second : edge.First;
                    double newDistance = distance[currentNode] + edge.Weight + exploringCityDelay;

                    if (newDistance < distance[otherNode])
                    {
                        distance[otherNode] = newDistance;
                        parent[otherNode] = currentNode;

                        queue.Enqueue(edge.Second, distance[edge.Second]);
                    }
                }
            }
        }

        private static int ReadGraph(int edges)
        {
            int maxNode = int.MinValue;

            graph = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split(", ")
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

                if (!graph.ContainsKey(firstNode))
                {
                    graph[firstNode] = new List<Edge>();
                }

                if (!graph.ContainsKey(secondNode))
                {
                    graph[secondNode] = new List<Edge>();
                }

                graph[firstNode].Add(edge);
                graph[secondNode].Add(edge);

                maxNode = Math.Max(maxNode, Math.Max(firstNode, secondNode));
            }

            return maxNode;
        }
    }
}
