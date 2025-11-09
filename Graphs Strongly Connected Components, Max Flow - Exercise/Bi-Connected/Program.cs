namespace Bi_Connected
{
    using System;

    internal class Program
    {
        private static List<int>[] graph;
        private static int[] depth;
        private static int[] lowpoin;
        private static bool[] visited;
        private static int[] parent;

        private static Stack<int> stack;
        private static List<HashSet<int>> components;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            parent = new int[nodes];
            depth = new int[nodes];
            lowpoin = new int[nodes];
            visited = new bool[nodes];

            ReadGraph(nodes, edges);

            stack = new Stack<int>();
            components = new List<HashSet<int>>();

            Console.Clear();

            for (int node = 0; node < nodes; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoints(node, 1);
                }

            }

            HashSet<int> lastComponent = stack.ToHashSet();
            components.Add(lastComponent);
            
            Console.WriteLine(string.Join(", ", lastComponent));
            Console.WriteLine($"Number of bi-connected components: {components.Count}");
        }

        private static void FindArticulationPoints(int node, int currentDepth)
        {
            visited[node] = true;
            depth[node] = currentDepth;
            lowpoin[node] = currentDepth;

            int childCount = 0;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    stack.Push(node);
                    stack.Push(child);

                    parent[child] = node;

                    FindArticulationPoints(child, currentDepth + 1);

                    childCount += 1;

                    if (parent[node] == -1 && childCount > 1
                     || parent[node] != -1 && lowpoin[child] >= depth[node])
                    {
                        HashSet<int> component = new HashSet<int>();

                        while (true)
                        {
                            int stackChild = stack.Pop();
                            int stackNode = stack.Pop();

                            component.Add(stackChild);
                            component.Add(stackNode);

                            if (stackChild == child && stackNode == node)
                            {
                                break;
                            }
                        }

                        components.Add(component);

                        Console.WriteLine(string.Join(", ", component));
                    }

                    lowpoin[node] = Math.Min(lowpoin[node], lowpoin[child]);
                }
                else if (parent[node] != child
                      && depth[child] < lowpoin[node])
                {
                    stack.Push(node);
                    stack.Push(child);

                    lowpoin[node] = depth[child];
                }
            }
        }

        private static void ReadGraph(int nodes, int edges)
        {
            graph = new List<int>[nodes];

            for (int node = 0; node < nodes; node++)
            {
                graph[node] = new List<int>();
                parent[node] = -1;
            }

            for (int i = 0; i < edges; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                    .Select(int.Parse)
                                    .ToArray();

                int firstNode = edgeData[0];
                int secondNode = edgeData[1];

                graph[firstNode].Add(secondNode);
                graph[secondNode].Add(firstNode);
            }
        }
    }
}
