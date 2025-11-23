namespace Chain_Lightning
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
        private static int[] damageByNode;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());
            int lightnings = int.Parse(Console.ReadLine());

            ReadGraph(nodes, edges);

            damageByNode = new int[graph.Length];

            for (int i = 0; i < lightnings; i++)
            {
                int[] lighteningData = Console.ReadLine()
                                        .Split()
                                        .Select(int.Parse)
                                        .ToArray();

                int node = lighteningData[0];
                int damage = lighteningData[1];

                Prim(node, damage);
            }

            Console.WriteLine(damageByNode.Max());
        }

        private static void Prim(int startNode, int damage)
        {
            HashSet<int> tree = new HashSet<int>() { startNode };

            int[] jumps = new int[graph.Length];
            damageByNode[startNode] += damage;

            PriorityQueue<Edge, double> queue = new PriorityQueue<Edge, double>();

            foreach (var edge in graph[startNode])
            {
                queue.Enqueue(edge, edge.Weight);
            }

            while (queue.Count > 0)
            {
                Edge minEdge = queue.Dequeue();

                int nonTreeNode = -1;
                int treeNode = -1;

                if (tree.Contains(minEdge.First) && !tree.Contains(minEdge.Second))
                {
                    nonTreeNode = minEdge.Second;
                    treeNode = minEdge.First;
                }

                if (!tree.Contains(minEdge.First) && tree.Contains(minEdge.Second))
                {
                    nonTreeNode = minEdge.First;
                    treeNode = minEdge.Second;
                }

                if (nonTreeNode != -1)
                {
                    foreach (var edge in graph[nonTreeNode])
                    {
                        queue.Enqueue(edge, edge.Weight);
                    }

                    tree.Add(nonTreeNode);
                    jumps[nonTreeNode] = jumps[treeNode] + 1;

                    damageByNode[nonTreeNode] += CalculateDamage(damage, jumps[nonTreeNode]);
                }
            }
        }

        private static int CalculateDamage(int damage, int jumps)
        {
            for (int i = 0; i < jumps; i++)
            {
                damage /= 2;
            }

            return damage;
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

                int first = edgeData[0];
                int second = edgeData[1];

                Edge edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = edgeData[2]
                };

                graph[first].Add(edge);
                graph[second].Add(edge);
            }
        }
    }
}
