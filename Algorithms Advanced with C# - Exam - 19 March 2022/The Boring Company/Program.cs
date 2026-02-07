namespace The_Boring_Company
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge>[] graph;
        private static bool[] connectedNodes;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());
            int connectedNodesCount = int.Parse(Console.ReadLine());

            graph = new List<Edge>[nodesCount];
            connectedNodes = new bool[nodesCount];

            ReadGraph(nodesCount, edgesCount, connectedNodesCount);

            int minimumBudget = Prim(nodesCount);

            Console.WriteLine($"Minimum budget: {minimumBudget}");
        }

        private static int Prim(int nodesCount)
        {
            PriorityQueue<Edge, int> priorityQueue = new PriorityQueue<Edge, int>();

            for (int node = 0; node < nodesCount; node++)
            {
                if (connectedNodes[node])
                {
                    foreach (var edge in graph[node])
                    {
                        priorityQueue.Enqueue(edge, edge.Weight);
                    }
                }
            }

            int minimumBudget = 0;

            while (priorityQueue.Count > 0)
            {
                Edge minEdge = priorityQueue.Dequeue();

                if (connectedNodes[minEdge.From] && connectedNodes[minEdge.To])
                {
                    continue;
                }

                int newNode = connectedNodes[minEdge.From] ? minEdge.To : minEdge.From;
                minimumBudget += minEdge.Weight;
                connectedNodes[newNode] = true;

                foreach (var edge in graph[newNode])
                {
                    if (!connectedNodes[edge.To])
                    {
                        priorityQueue.Enqueue(edge, edge.Weight);
                    }
                }
            }

            return minimumBudget;
        }

        private static void ReadGraph(int nodesCount, int edgesCount, int connectedNodesCount)
        {
            for (int node = 0; node < nodesCount; node++)
            {
                graph[node] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeData = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

                int from = edgeData[0];
                int to = edgeData[1];

                Edge edge = new Edge
                {
                    From = from,
                    To = to,
                    Weight = edgeData[2]
                };

                graph[from].Add(edge);
                graph[to].Add(edge);
            }

            for (int i = 0; i < connectedNodesCount; i++)
            {
                int[] currentConnectedNodes = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

                connectedNodes[currentConnectedNodes[0]] = true;
                connectedNodes[currentConnectedNodes[1]] = true;
            }
        }
    }
}
