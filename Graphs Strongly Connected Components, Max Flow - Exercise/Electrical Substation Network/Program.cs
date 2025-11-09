namespace Electrical_Substation_Network
{
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            List<int>[] graph = new List<int>[nodes];
            List<int>[] reversedGraph = new List<int>[nodes];

            ReadGraph(edges, graph, reversedGraph);

            Stack<int> sorted = TopologicalSort(graph);

            bool[] visited = new bool[graph.Length];

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();
                Stack<int> component = new Stack<int>();

                if (!visited[node])
                {
                    DFS(node, visited, component, reversedGraph);

                    Console.WriteLine(string.Join(", ", component));
                }
            }
        }

        private static Stack<int> TopologicalSort(List<int>[] graph)
        {
            Stack<int> result = new Stack<int>();
            bool[] visited = new bool[graph.Length];

            for (int node = 0; node < graph.Length; node++)
            {
                DFS(node, visited, result, graph);
            }

            return result;
        }

        private static void DFS(int node, bool[] visited, Stack<int> result, List<int>[] graph)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(child, visited, result, graph);
            }

            result.Push(node);
        }

        private static void ReadGraph(int edges, List<int>[] graph, List<int>[] reversedGraph)
        {
            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
                reversedGraph[node] = new List<int>();
            }

            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split(", ")
                                    .Select(int.Parse)
                                    .ToArray();

                int node = edgeData[0];
                IEnumerable<int> children = edgeData.Skip(1);

                foreach (var child in children)
                {
                    graph[node].Add(child);
                    reversedGraph[child].Add(node);
                }
            }
        }
    }
}
