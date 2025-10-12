namespace Most_Reliable_Path
{
    using System;

    using Wintellect.PowerCollections;

    class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge>[] graph;
        private static int[] prev;
        private static double[] reliable;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            graph = new List<Edge>[nodes];
            ReadGraph(edges);

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            prev = new int[nodes];
            reliable = new double[nodes];

            for (int node = 0; node < nodes; node++)
            {
                prev[node] = -1;
                reliable[node] = double.NegativeInfinity;
            }

            reliable[source] = 100;

            Dijkstra(source, destination);
            Stack<int> path = ExtractPath(destination);

            Console.WriteLine($"Most reliable path reliability: {reliable[destination]:F2}%");
            Console.WriteLine(string.Join(" -> ", path));
        }

        private static Stack<int> ExtractPath(int destination)
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

        private static void Dijkstra(int source, int destination)
        {
            OrderedBag<int> bag = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => reliable[s].CompareTo(reliable[f])));
            bag.Add(source);

            while (bag.Count > 0)
            {
                int currentNode = bag.RemoveFirst();

                if (currentNode == destination)
                {
                    return;
                }

                foreach (var child in graph[currentNode])
                {
                    int otherNode = currentNode == child.First ? child.Second : child.First;

                    if (double.IsNegative(reliable[otherNode]))
                    {
                        bag.Add(otherNode);
                    }

                    double newReliability = reliable[currentNode] * child.Weight / 100;

                    if (newReliability > reliable[otherNode])
                    {
                        reliable[otherNode] = newReliability;
                        prev[otherNode] = currentNode;

                        bag = new OrderedBag<int>(
                            bag,
                            Comparer<int>.Create((f, s) => reliable[s].CompareTo(reliable[f])));
                    }
                }
            }
        }

        private static void ReadGraph(int edges)
        {
            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

                Edge edge = new Edge
                {
                    First = edgeData[0],
                    Second = edgeData[1],
                    Weight = edgeData[2],
                };

                graph[edge.First].Add(edge);
                graph[edge.Second].Add(edge);
            }
        }
    }
}
