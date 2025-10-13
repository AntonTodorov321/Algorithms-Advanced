namespace _2._Cheap_Town_Tour
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
        private static List<Edge> edges;
        private static int[] parent;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            edges = new List<Edge>();
            ReadGraph(edgesCount);
            edges = edges.OrderBy(e => e.Weight).ToList();

            parent = new int[nodes];

            for (int node = 0; node < nodes; node++)
            {
                parent[node] = node;
            }

            int minimumCost = Kruskal();

            Console.WriteLine($"Total cost: {minimumCost}");
        }

        private static int Kruskal()
        {
            int minimumCost = 0;

            foreach (var edge in edges)
            {
                int firstNodeRoot = FindRoot(edge.First);
                int secondNodeRoot = FindRoot(edge.Second);

                if (firstNodeRoot != secondNodeRoot)
                {
                    minimumCost += edge.Weight;
                    parent[firstNodeRoot] = secondNodeRoot;
                }
            }

            return minimumCost;
        }

        private static int FindRoot(int node)
        {
            while (node != parent[node])
            {
                node = parent[node];
            }

            return node;
        }

        private static void ReadGraph(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeData = Console.ReadLine()
                                .Split(" - ")
                                .Select(int.Parse)
                                .ToArray();

                Edge edge = new Edge
                {
                    First = edgeData[0],
                    Second = edgeData[1],
                    Weight = edgeData[2],
                };

                edges.Add(edge);
            }
        }
    }
}
