namespace Prim_s_Algorithm
{
    using System;

    using Wintellect.PowerCollections;

    class Edge
    {
        public int FirstNode { get; set; }

        public int SecondNode { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static Dictionary<int, List<Edge>> graph;
        private static HashSet<int> forestNodes;
        private static List<Edge> forestEdges;

        static void Main(string[] args)
        {
            int edgesCount = int.Parse(Console.ReadLine());

            graph = new Dictionary<int, List<Edge>>();
            ReadGraph(edgesCount);

            forestEdges = new List<Edge>();
            forestNodes = new HashSet<int>();

            foreach (var node in graph.Keys)
            {
                if (!forestNodes.Contains(node))
                {
                    Prim(node);
                }
            }

            foreach (var edge in forestEdges)
            {
                Console.WriteLine($"{edge.FirstNode} - {edge.SecondNode}");
            }
        }

        private static void Prim(int start)
        {
            forestNodes.Add(start);
            OrderedBag<Edge> bag =
                new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => (f.Weight - s.Weight)));

            bag.AddMany(graph[start]);

            while (bag.Count > 0)
            {
                Edge minEdge = bag.RemoveFirst();

                int nonTreeNode = -1;

                if (!forestNodes.Contains(minEdge.FirstNode)
                    && forestNodes.Contains(minEdge.SecondNode))
                {
                    nonTreeNode = minEdge.FirstNode;
                }

                if (forestNodes.Contains(minEdge.FirstNode)
                    && !forestNodes.Contains(minEdge.SecondNode))
                {
                    nonTreeNode = minEdge.SecondNode;
                }

                if (nonTreeNode != -1)
                {
                    forestNodes.Add(nonTreeNode);
                    forestEdges.Add(minEdge);

                    bag.AddMany(graph[nonTreeNode]);
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

                Edge edge = new Edge
                {
                    FirstNode = edgeData[0],
                    SecondNode = edgeData[1],
                    Weight = edgeData[2],
                };

                if (!graph.ContainsKey(edge.FirstNode))
                {
                    graph[edge.FirstNode] = new List<Edge>();
                }

                if (!graph.ContainsKey(edge.SecondNode))
                {
                    graph[edge.SecondNode] = new List<Edge>();
                }

                graph[edge.FirstNode].Add(edge);
                graph[edge.SecondNode].Add(edge);
            }
        }
    }
}
