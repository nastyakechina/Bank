using Models;
using System.Threading;
using System.Threading.Tasks;

namespace Presenter
{
    public interface ITransactionPresenter
    {
        Task DepositAsync(decimal amount, string currency, CancellationToken token);
        Task ConversionAsync(decimal amount, Coin curFrom, Coin curTo, CancellationToken token);
        Task<IReadOnlyCollection<Transaction>> GetTransactionHistoryAsync(CancellationToken token);
    }
}