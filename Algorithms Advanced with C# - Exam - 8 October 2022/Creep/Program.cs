namespace Creep
{
    using System;
    using System.Xml.Linq;

    class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge> graph;
        private static int[] parent;
        private static List<Edge> mst;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            mst = new List<Edge>();
            graph = new List<Edge>();
            parent = new int[nodes + 1];

            ReadGraph(edges, nodes);
            Kruskal(nodes);

            foreach (var edge in mst)
            {
                Console.WriteLine($"{edge.From} {edge.To}");
            }

            Console.WriteLine(mst.Sum(e => e.Weight));
        }

        private static void Kruskal(int nodes)
        {
            graph = graph.OrderBy(e => e.Weight).ToList();

            for (int node = 0; node < nodes + 1; node++)
            {
                parent[node] = node;
            }

            foreach (var edge in graph)
            {
                int firstNodeRoot = FindRoot(edge.From);
                int secondNodeRoot = FindRoot(edge.To);

                if (firstNodeRoot != secondNodeRoot)
                {
                    parent[firstNodeRoot] = secondNodeRoot;
                    mst.Add(edge);
                }
            }
        }

        private static int FindRoot(int node)
        {
            while (parent[node] != node)
            {
                node = parent[node];
            }

            return node;
        }

        private static void ReadGraph(int edgesCount, int nodes)
        {
            Dictionary<int, Dictionary<int, int>> edges =
                new Dictionary<int, Dictionary<int, int>>();

            for (int node = 0; node < nodes; node++)
            {
                edges[node] = new Dictionary<int, int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

                int from = edgeData[0];
                int to = edgeData[1];
                int weight = edgeData[2];

                edges[from][to] = weight;
            }

            foreach (var node in edges)
            {
                foreach (var child in node.Value)
                {
                    Edge edge = new Edge
                    {
                        From = node.Key,
                        To = child.Key,
                        Weight = child.Value
                    };

                    graph.Add(edge);
                }
            }
        }
    }
}
