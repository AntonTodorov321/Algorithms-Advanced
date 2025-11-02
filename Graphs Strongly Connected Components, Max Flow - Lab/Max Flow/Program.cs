namespace Max_Flow
{
    using System;

    internal class Program
    {
        private static int[,] graph;
        private static int[] parent;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            graph = new int[nodes, nodes];
            ReadGraph();

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            parent = new int[nodes];
            Array.Fill(parent, -1);

            int maxFlow = 0;

            while (BFS(source, destination))
            {
                int minFlow = GetMinFlow(destination);
                maxFlow += minFlow;

                ReduceFlow(minFlow, destination);
            }

            Console.WriteLine($"Max flow = {maxFlow}");
        }

        private static void ReduceFlow(int minFlow, int node)
        {
            while (parent[node] != -1)
            {
                int from = parent[node];
                graph[from, node] -= minFlow;
                node = from;
            }
        }

        private static int GetMinFlow(int node)
        {
            int minFlow = int.MaxValue;

            while (parent[node] != -1)
            {
                int from = parent[node];
                int currentFlow = graph[from, node];

                if (currentFlow < minFlow)
                {
                    minFlow = currentFlow;
                }

                node = from;
            }

            return minFlow;
        }

        private static bool BFS(int source, int destination)
        {
            bool[] visited = new bool[graph.GetLength(0)];
            visited[source] = true;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();

                if (node == destination)
                {
                    return true;
                }

                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (graph[node, child] == 0 || visited[child])
                    {
                        continue;
                    }

                    queue.Enqueue(child);
                    parent[child] = node;
                    visited[child] = true;
                }
            }

            return false;
        }

        private static void ReadGraph()
        {
            for (int row = 0; row < graph.GetLength(0); row++)
            {
                int[] capacity = Console.ReadLine()
                                    .Split(", ")
                                    .Select(int.Parse)
                                    .ToArray();

                for (int col = 0; col < graph.GetLength(1); col++)
                {
                    graph[row, col] = capacity[col];
                }
            }
        }
    }
}
