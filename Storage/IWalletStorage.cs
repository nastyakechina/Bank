using Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Storage;


public interface IWalletStorage
{
    // Получение текущего состояния кошелька
    Task<Wallet> GetWalletAsync();

    // Метод для пополнения кошелька
    Task DepositAsync(string currency, decimal amount, CancellationToken token);

    // Метод для конвертации валют
    Task ConvertAsync(string fromCurrency, string toCurrency, decimal amount, decimal exchangeRate, CancellationToken token);

    // Получение истории транзакций
    Task<IReadOnlyCollection<Transaction>> GetTransactionHistoryAsync();
}

/*
using Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Storage
{
    public interface IWalletStorage
    {
        Task DepositAsync(Coin coin, int amount, CancellationToken token);
        Task<int> GetBalanceAsync(Coin coin, CancellationToken token);
        Task<IEnumerable<Wallet>> GetAllWalletsAsync(CancellationToken token);
    }
}
*/