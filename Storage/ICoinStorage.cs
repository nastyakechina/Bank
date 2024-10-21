using Models;
using System.Threading;
using System.Threading.Tasks;

namespace Presenter
{
    public interface ICoinStorage
    {
        Task AddNewCoinAsync(Coin coin, CancellationToken token);
        Task<bool> CurrencyExistsAsync(string currency, CancellationToken token);

    }
}