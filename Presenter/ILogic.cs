using Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presenter;

public interface ILogic
{
    Task DepositWalletAsync(string currency, int amount, CancellationToken token);
    Task<int> ConversionCoinAsync(string curFrom, string curTo, int amount, CancellationToken token);
    Task<List<Transaction>> GetHistoryAsync(CancellationToken token);
    Task AddNewCoinAsync(Coin coin, CancellationToken token);
    Task<Dictionary<string, int>> GetBalanceAsync(CancellationToken token);
}