namespace Cable_Network
{
    using System;
    using System.Linq;
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
        private static HashSet<int> spanningTree;

        static void Main(string[] args)
        {
            int budget = int.Parse(Console.ReadLine());
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            graph = new List<Edge>[nodes];
            spanningTree = new HashSet<int>();

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            ReadGraph(edges);

            int budgetUsed = Prim(budget);
            Console.WriteLine($"Budget used: {budgetUsed}");
        }

        private static int Prim(int budget)
        {
            int budgetUsed = 0;

            OrderedBag<Edge> bag =
                new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.Weight.CompareTo(s.Weight)));

            foreach (var node in spanningTree)
            {
                bag.AddMany(graph[node]);
            }

            while (bag.Count > 0)
            {
                Edge edge = bag.RemoveFirst();

                if (budgetUsed + edge.Weight > budget)
                {
                    return budgetUsed;
                }

                int nonTreeNode = -1;

                if (!spanningTree.Contains(edge.First) && spanningTree.Contains(edge.Second))
                {
                    nonTreeNode = edge.First;
                }

                if (spanningTree.Contains(edge.First) && !spanningTree.Contains(edge.Second))
                {
                    nonTreeNode = edge.Second;
                }

                if (nonTreeNode != -1)
                {
                    spanningTree.Add(nonTreeNode);
                    bag.AddMany(graph[nonTreeNode]);
                    budgetUsed += edge.Weight;
                }
            }

            return budgetUsed;
        }

        private static void ReadGraph(int edges)
        {
            for (int i = 0; i < edges; i++)
            {
                string[] edgeData = Console.ReadLine()
                                    .Split();

                Edge edge = new Edge
                {
                    First = int.Parse(edgeData[0]),
                    Second = int.Parse(edgeData[1]),
                    Weight = int.Parse(edgeData[2]),
                };

                if (edgeData.Length == 4)
                {
                    spanningTree.Add(edge.First);
                    spanningTree.Add(edge.Second);
                }

                graph[edge.First].Add(edge);
                graph[edge.Second].Add(edge);
            }
        }
    }
}
