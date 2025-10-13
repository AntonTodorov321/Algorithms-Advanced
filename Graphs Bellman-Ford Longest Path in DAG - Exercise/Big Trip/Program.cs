namespace Big_Trip
{
    using System;

    class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge>[] graph;
        private static double[] distance;
        private static int[] prev;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            ReadGraph(nodes, edges);

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            distance = new double[graph.Length];
            prev = new int[graph.Length];

            for (int node = 1; node < graph.Length; node++)
            {
                distance[node] = double.NegativeInfinity;
                prev[node] = -1;
            }

            distance[source] = 0;

            Stack<int> sorted = TopologicalSort();
            FindLongestPath(sorted);

            Stack<int> path = ReconstructPath(destination);

            Console.WriteLine(distance[destination]);
            Console.WriteLine(string.Join(" ", path));
        }

        private static Stack<int> ReconstructPath(int node)
        {
            Stack<int> path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static void FindLongestPath(Stack<int> sorted)
        {
            while (sorted.Count > 0)
            {
                int currentNode = sorted.Pop();

                foreach (var edge in graph[currentNode])
                {
                    double newDistance = distance[currentNode] + edge.Weight;

                    if (newDistance > distance[edge.To])
                    {
                        distance[edge.To] = newDistance;
                        prev[edge.To] = currentNode;
                    }
                }
            }
        }

        private static Stack<int> TopologicalSort()
        {
            Stack<int> result = new Stack<int>();
            bool[] visited = new bool[graph.Length];

            for (int node = 1; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    DFS(node, visited, result);
                }
            }

            return result;
        }

        private static void DFS(int node, bool[] visited, Stack<int> result)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(child.To, visited, result);
            }

            result.Push(node);
        }

        private static void ReadGraph(int nodes, int edges)
        {
            graph = new List<Edge>[nodes + 1];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

                Edge edge = new Edge
                {
                    From = edgeData[0],
                    To = edgeData[1],
                    Weight = edgeData[2],
                };

                graph[edge.From].Add(edge);
            }
        }
    }
}
