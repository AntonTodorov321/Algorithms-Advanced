namespace Trains_Part_Three
{
    using System;

    internal class Program
    {
        private static int[,] graph;
        private static int[] parent;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());
            int[] pathData = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

            ReadGraph(nodes, edges);

            int source = pathData[0];
            int destination = pathData[1];

            parent = new int[nodes];
            Array.Fill(parent, -1);

            int maxFlow = 0;

            while (BFS(source, destination))
            {
                List<int> path = ReconstructPath(destination);

                int minFlow = GetMinFlow(path);
                maxFlow += minFlow;

                ReduceFlow(minFlow, path);
            }

            Console.WriteLine(maxFlow);
        }

        private static void ReduceFlow(int minFlow, List<int> path)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                graph[path[i], path[i + 1]] -= minFlow;
            }
        }

        private static int GetMinFlow(List<int> path)
        {
            int minFlow = int.MaxValue;

            for (int i = 0; i < path.Count - 1; i++)
            {
                int currentFlow = graph[path[i], path[i + 1]];

                if (currentFlow < minFlow)
                {
                    minFlow = currentFlow;
                }
            }

            return minFlow;
        }

        private static List<int> ReconstructPath(int node)
        {
            Stack<int> path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = parent[node];
            }

            return path.ToList();
        }

        private static bool BFS(int source, int destination)
        {
            bool[] visited = new bool[graph.GetLength(1)];
            visited[source] = true;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                int currentNode = queue.Dequeue();

                if (currentNode == destination)
                {
                    return true;
                }

                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (graph[currentNode, child] > 0 && !visited[child])
                    {
                        queue.Enqueue(child);
                        visited[child] = true;
                        parent[child] = currentNode;
                    }
                }
            }

            return false;
        }

        private static void ReadGraph(int nodes, int edges)
        {
            graph = new int[nodes, nodes];

            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

                int from = edgeData[0];
                int to = edgeData[1];
                int weight = edgeData[2];

                graph[from, to] = weight;
            }
        }
    }
}
