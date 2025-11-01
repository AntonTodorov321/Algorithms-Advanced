namespace Strongly_Connected_Components
{
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            List<int>[] graph = new List<int>[nodes];
            List<int>[] reverseGraph = new List<int>[nodes];

            ReadGraph(edges, graph, reverseGraph);

            Stack<int> sorted = new Stack<int>();
            bool[] visited = new bool[graph.Length];

            for (int node = 0; node < graph.Length; node++)
            {
                DFS(visited, node, sorted, graph);
            }

            Console.WriteLine("Strongly Connected Components:");

            visited = new bool[graph.Length];

            while (sorted.Count > 0)
            {
                int node = sorted.Pop();
                Stack<int> strongComponent = new Stack<int>();

                if (!visited[node])
                {
                    DFS(visited, node, strongComponent, reverseGraph);
                    Console.WriteLine($"{{{string.Join(", ", strongComponent)}}}");
                }
            }
        }

        private static void DFS(bool[] visited, int node, Stack<int> result, List<int>[] graph)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(visited, child, result, graph);
            }

            result.Push(node);
        }

        private static void ReadGraph(int edges, List<int>[] graph, List<int>[] reverseGraph)
        {
            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
                reverseGraph[node] = new List<int>();
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
                    reverseGraph[child].Add(node);
                }
            }
        }
    }
}
