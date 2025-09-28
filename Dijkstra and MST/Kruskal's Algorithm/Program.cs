namespace Kruskal_s_Algorithm
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
        private static List<Edge> forest;
        private static int[] parent;

        static void Main(string[] args)
        {
            int edgesCount = int.Parse(Console.ReadLine());

            edges = new List<Edge>();
            forest = new List<Edge>(); ;

            int maxNode = ReadGraph(edgesCount);

            parent = new int[maxNode + 1];
            edges = edges.OrderBy(edge => edge.Weight).ToList();

            for (int node = 0; node < parent.Length; node++)
            {
                parent[node] = node;
            }

            Kruskal();

            foreach (var edge in forest)
            {
                Console.WriteLine($"{edge.First} - {edge.Second}");
            }
        }

        private static void Kruskal()
        {
            foreach (var edge in edges)
            {
                int firstNodeRoot = FindRoot(edge.First);
                int secondNodeRoot = FindRoot(edge.Second);

                if (firstNodeRoot != secondNodeRoot)
                {
                    parent[firstNodeRoot] = secondNodeRoot;
                    forest.Add(edge);
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

        private static int ReadGraph(int edgesCount)
        {
            int maxNode = -1;

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split(", ")
                                    .Select(int.Parse)
                                    .ToArray();

                Edge edge = new Edge
                {
                    First = edgeData[0],
                    Second = edgeData[1],
                    Weight = edgeData[2],
                };

                if (maxNode < edge.First)
                {
                    maxNode = edge.First;
                }

                if (maxNode < edge.Second)
                {
                    maxNode = edge.Second;
                }

                edges.Add(edge);
            }

            return maxNode;
        }
    }
}
