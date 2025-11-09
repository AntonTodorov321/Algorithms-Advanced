namespace Maximum_Tasks
{
    using System;

    internal class Program
    {
        private static bool[,] graph;
        private static int[] parent;

        static void Main(string[] args)
        {
            int people = int.Parse(Console.ReadLine());
            int tasks = int.Parse(Console.ReadLine());
            int nodes = people + tasks + 2;

            ReadGraph(people, tasks, nodes);

            parent = new int[nodes];
            Array.Fill(parent, -1);

            int destination = nodes - 1;

            while (BFS(0, destination))
            {
                ClearPath(destination);
            }

            for (int task = people + 1; task <= people + tasks; task++)
            {
                for (int i = 0; i < nodes; i++)
                {
                    if (graph[task, i])
                    {
                        Console.WriteLine($"{(char)(i + 64)}-{task - people}");
                    }
                }
            }
        }

        private static void ClearPath(int node)
        {
            while (parent[node] != -1)
            {
                int from = parent[node];
                graph[node, from] = true;
                graph[from, node] = false;
                node = from;
            }
        }

        private static bool BFS(int start, int destination)
        {
            bool[] visited = new bool[graph.GetLength(0)];
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(start);
            visited[start] = true;

            while (queue.Count > 0)
            {
                int currentNode = queue.Dequeue();

                if (currentNode == destination)
                {
                    return true;
                }

                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (graph[currentNode, child] && !visited[child])
                    {
                        queue.Enqueue(child);
                        visited[child] = true;
                        parent[child] = currentNode;
                    }
                }
            }

            return visited[destination];
        }

        private static void ReadGraph(int people, int tasks, int nodes)
        {
            graph = new bool[nodes, nodes];

            for (int person = 1; person <= people; person++)
            {
                graph[0, person] = true;
            }

            for (int task = people + 1; task <= people + tasks; task++)
            {
                graph[task, nodes - 1] = true;
            }

            for (int person = 1; person <= people; person++)
            {
                string personTasks = Console.ReadLine();

                for (int task = 0; task < personTasks.Length; task++)
                {
                    if (personTasks[task] == 'Y')
                    {
                        graph[person, task + 1 + people] = true;
                    }
                }
            }
        }
    }
}
