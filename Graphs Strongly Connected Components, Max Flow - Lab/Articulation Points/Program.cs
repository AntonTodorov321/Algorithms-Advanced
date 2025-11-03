namespace Articulation_Points
{
    using System;

    internal class Program
    {
        private static List<int>[] graph;
        private static bool[] visited;
        private static int[] lowpoint;
        private static int[] depth;
        private static int?[] parent;

        private static List<int> articulationPoints;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            ReadGraph(nodes, edges);

            visited = new bool[nodes];
            lowpoint = new int[nodes];
            depth = new int[nodes];
            parent = new int?[nodes];

            articulationPoints = new List<int>();

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoints(node, 1);
                }
            }

            Console.WriteLine($"Articulation points: {string.Join(", ", articulationPoints)}");
        }

        private static void FindArticulationPoints(int node, int currentDepth)
        {
            visited[node] = true;
            depth[node] = currentDepth;
            lowpoint[node] = currentDepth;

            int childCount = 0;
            bool isArticulationPoint = false;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parent[child] = node;
                    FindArticulationPoints(child, currentDepth + 1);

                    childCount += 1;

                    if (lowpoint[child] >= depth[node])
                    {
                        isArticulationPoint = true;
                    }

                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);
                }
                else if (parent[node] != child)
                {
                    lowpoint[node] = Math.Min(lowpoint[node], depth[child]);
                }

            }

            if (parent[node] == null && childCount > 1
             || parent[node] != null && isArticulationPoint)
            {
                articulationPoints.Add(node);
            }
        }

        private static void ReadGraph(int nodes, int edges)
        {
            graph = new List<int>[nodes];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
            }

            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                .Split(", ")
                                .Select(int.Parse)
                                .ToArray();

                int node = edgeData[0];
                IEnumerable<int> children = edgeData.Skip(1);

                graph[node].AddRange(children);
            }
        }
    }
}
