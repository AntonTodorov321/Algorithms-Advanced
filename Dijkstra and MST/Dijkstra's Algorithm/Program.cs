namespace Dijkstra_s_Algorithm
{
    using System;

    using Wintellect.PowerCollections;

    class Edge
    {
        public int FistNode { get; set; }

        public int SecondNode { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNode;
        private static double[] distance;
        private static int[] parent;

        static void Main(string[] args)
        {
            int edgesCount = int.Parse(Console.ReadLine());
            edgesByNode = new Dictionary<int, List<Edge>>();

            ReadGraph(edgesCount);

            int biggestNode = edgesByNode.Keys.Max();

            int start = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            distance = new double[biggestNode + 1];
            Array.Fill(distance, double.PositiveInfinity);
            distance[start] = 0;

            parent = new int[biggestNode + 1];
            Array.Fill(parent, -1);

            Dijkstra(start, destination);

            if (double.IsPositiveInfinity(distance[destination]))
            {
                Console.WriteLine("There is no such path.");
            }
            else
            {
                Stack<int> path = RetrievePath(destination);

                Console.WriteLine(distance[destination]);
                Console.WriteLine(string.Join(" ", path));
            }
        }

        private static Stack<int> RetrievePath(int node)
        {
            Stack<int> path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = parent[node];
            }

            return path;
        }

        private static void Dijkstra(int start, int destination)
        {
            OrderedBag<int> bag = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => (int)(distance[f] - distance[s])));
            bag.Add(start);

            while (bag.Count > 0)
            {
                int minNode = bag.RemoveFirst();

                if (double.IsPositiveInfinity(minNode))
                {
                    break;
                }

                if (minNode == destination)
                {
                    break;
                }

                foreach (var edge in edgesByNode[minNode])
                {
                    int otherNode = edge.FistNode == minNode ? edge.SecondNode : edge.FistNode;

                    if (double.IsPositiveInfinity(distance[otherNode]))
                    {
                        bag.Add(otherNode);
                    }

                    double newDistance = edge.Weight + distance[minNode];

                    if (newDistance < distance[otherNode])
                    {
                        parent[otherNode] = minNode;
                        distance[otherNode] = newDistance;

                        bag = new OrderedBag<int>(
                            bag, Comparer<int>.Create((f, s) => (int)(distance[f] - distance[s])));
                    }
                }
            }
        }

        private static void ReadGraph(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split(", ")
                                    .Select(int.Parse)
                                    .ToArray();

                int firstNode = edgeData[0];
                int secondNode = edgeData[1];

                if (!edgesByNode.ContainsKey(firstNode))
                {
                    edgesByNode[firstNode] = new List<Edge>();
                }

                if (!edgesByNode.ContainsKey(secondNode))
                {
                    edgesByNode[secondNode] = new List<Edge>();
                }

                Edge edge = new Edge
                {
                    FistNode = firstNode,
                    SecondNode = secondNode,
                    Weight = edgeData[2],
                };

                edgesByNode[firstNode].Add(edge);
                edgesByNode[secondNode].Add(edge);
            }
        }
    }
}
