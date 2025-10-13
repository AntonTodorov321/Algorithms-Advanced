namespace Undefined
{
    using System;

    class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge> graph;
        private static double[] distance;
        private static int[] prev;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            graph = new List<Edge>();
            ReadGraph(edges);

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            prev = new int[nodes + 1];
            distance = new double[nodes + 1];

            for (int node = 1; node < nodes + 1; node++)
            {
                prev[node] = -1;
                distance[node] = double.PositiveInfinity;
            }

            distance[source] = 0;
            bool isCyclic = BellmanFord(nodes);

            if (isCyclic)
            {
                Console.WriteLine("Undefined");
            }
            else
            {
                Stack<int> path = ReconstructPath(destination);

                Console.WriteLine(string.Join(" ", path));
                Console.WriteLine(distance[destination]);
            }
        }

        private static Stack<int> ReconstructPath(int node)
        {
            Stack<int> path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static bool BellmanFord(int nodes)
        {
            for (int i = 0; i < nodes - 1; i++)
            {
                bool updated = false;

                foreach (var edge in graph)
                {
                    if (double.IsPositiveInfinity(distance[edge.From]))
                    {
                        continue;
                    }

                    double newDistance = distance[edge.From] + edge.Weight;

                    if (newDistance < distance[edge.To])
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

            foreach (var edge in graph)
            {
                double newDistance = distance[edge.From] + edge.Weight;

                if (newDistance < distance[edge.To])
                {
                    return true;
                }
            }

            return false;
        }

        private static void ReadGraph(int edges)
        {
            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

                Edge edge = new Edge
                {
                    From = edgeData[0],
                    To = edgeData[1],
                    Weight = edgeData[2],
                };

                graph.Add(edge);
            }
        }
    }
}
