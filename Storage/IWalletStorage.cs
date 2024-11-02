using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

namespace Storage
{
    public interface IWalletStorage
    {
        Task CreateWallet(Wallet _wallet, CancellationToken token);
        Task AddMoneyAsync(Wallet _wallet, CancellationToken token);
        Task<Dictionary<int, Coin>> GetMoneyAsync(CancellationToken token);
        

    }
}

