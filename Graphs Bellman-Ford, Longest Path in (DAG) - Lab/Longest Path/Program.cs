namespace Longest_Path
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
        private static Dictionary<int, List<Edge>> edgesByNode;
        private static double[] distance;
        private static int[] prev;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            edgesByNode = new Dictionary<int, List<Edge>>();
            ReadGraph(edges);

            Stack<int> sortedNodes = TopologicalSorting();

            int start = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            distance = new double[nodes + 1];
            Array.Fill(distance, double.NegativeInfinity);
            distance[start] = 0;

            prev = new int[nodes + 1];
            Array.Fill(prev, -1);

            FindLongestPath(sortedNodes);
            Stack<int> path = FindPath(destination);

            Console.WriteLine(distance[destination]);
            Console.WriteLine(string.Join(" ", path));
        }

        private static Stack<int> FindPath(int destination)
        {
            Stack<int> path = new Stack<int>();
            int node = destination;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static void FindLongestPath(Stack<int> sortedNodes)
        {
            while (sortedNodes.Count > 0)
            {
                int current = sortedNodes.Pop();

                foreach (var child in edgesByNode[current])
                {
                    double newDistance = distance[child.From] + child.Weight;

                    if (newDistance > distance[child.To])
                    {
                        distance[child.To] = newDistance;
                        prev[child.To] = child.From;
                    }
                }
            }
        }

        private static Stack<int> TopologicalSorting()
        {
            Stack<int> sorted = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();

            foreach (var node in edgesByNode.Keys)
            {
                if (!visited.Contains(node))
                {
                    DFS(node, sorted, visited);
                }
            }

            return sorted;
        }

        private static void DFS(int node, Stack<int> sorted, HashSet<int> visited)
        {
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);

            foreach (var child in edgesByNode[node])
            {
                DFS(child.To, sorted, visited);
            }

            sorted.Push(node);
        }

        private static void ReadGraph(int edges)
        {
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

                if (!edgesByNode.ContainsKey(edge.From))
                {
                    edgesByNode[edge.From] = new List<Edge>();
                }

                if (!edgesByNode.ContainsKey(edge.To))
                {
                    edgesByNode[edge.To] = new List<Edge>();
                }

                edgesByNode[edge.From].Add(edge);
            }
        }
    }
}
