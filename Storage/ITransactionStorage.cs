using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

namespace Storage
{
    public interface ITransactionStorage
    {
        Task<List<Transaction>> GetHistoryAsync(CancellationToken cancellationToken);
        Task AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
    }
}