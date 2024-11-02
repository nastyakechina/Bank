using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Storage
{
    public class TransactionStorage : ITransactionStorage
    {
        private readonly IFileStorage<Transaction> _storage;

        public TransactionStorage()
        {
            _storage = new FileStorage<Transaction>("../../data/transactions.json", "transactions.json");
        }

        public TransactionStorage(IFileStorage<Transaction> storage)
        {
            _storage = storage;
        }

        public async Task AddTransactionAsync(Transaction transaction, CancellationToken token)
                {
                    if (transaction == null)
                    {
                        throw new ArgumentNullException(nameof(transaction));
                    }
                    token.ThrowIfCancellationRequested();
                    await _storage.AddAsync(transaction, token);
                }
        
        public async Task<List<Transaction>> GetHistoryAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            return await _storage.GetAllAsync(token);
        }

        
    }
}


