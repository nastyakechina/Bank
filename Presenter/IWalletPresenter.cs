using Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Presenter
{
    public interface IWalletPresenter
    {
        Task DepositAsync(Coin coin, int amount, CancellationToken token);
        Task<int> GetBalanceAsync(Coin coin, CancellationToken token);
        Task<IEnumerable<Wallet>> GetAllWalletsAsync(CancellationToken token);
    }
}