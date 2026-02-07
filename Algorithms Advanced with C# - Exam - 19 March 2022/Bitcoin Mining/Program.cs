namespace Bitcoin_Mining
{
    class Transaction
    {
        public string Hash { get; set; }

        public int Size { get; set; }

        public int Fees { get; set; }

        public string From { get; set; }

        public string To { get; set; }
    }

    internal class Program
    {
        private const int MAXIMUM_SIZE = 1_000_000;

        static void Main(string[] args)
        {
            int transactionCount = int.Parse(Console.ReadLine());
            PriorityQueue<Transaction, int> pendingTransactions =
                new PriorityQueue<Transaction, int>();

            for (int i = 0; i < transactionCount; i++)
            {
                string[] transactionData = Console.ReadLine().Split();

                Transaction transaction = new Transaction
                {
                    Hash = transactionData[0],
                    Size = int.Parse(transactionData[1]),
                    Fees = int.Parse(transactionData[2])
                };

                pendingTransactions.Enqueue(transaction, -transaction.Fees);
            }

            int totalSize = 0;
            int totalFees = 0;
            List<string> doneTransactionNames = new List<string>();

            while (pendingTransactions.Count > 0)
            {
                Transaction currentTransaction = pendingTransactions.Dequeue();

                if (totalSize + currentTransaction.Size > MAXIMUM_SIZE)
                {
                    break;
                }

                totalSize += currentTransaction.Size;
                totalFees += currentTransaction.Fees;
                doneTransactionNames.Add(currentTransaction.Hash);
            }

            Console.WriteLine($"Total Size: {totalSize}");
            Console.WriteLine($"Total Fees: {totalFees}");

            foreach (var transactionName in doneTransactionNames)
            {
                Console.WriteLine(transactionName);
            }
        }
    }
}
