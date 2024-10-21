using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Storage
{
    public class WalletStorage : IWalletStorage
    {
        private readonly List<Wallet> _wallets = new List<Wallet>();

        public Task TypeDepositAsync(Coin coin, int amount, CancellationToken token)
        {
            var wallet = _wallets.FirstOrDefault(w => w.Cur == coin);
            if (wallet == null)
            {
                // Если кошелька с такой валютой нет, добавляем новый
                wallet = new Wallet(coin, amount);
                _wallets.Add(wallet);
            }
            else
            {
                // Если кошелек с этой валютой уже существует, увеличиваем баланс
                wallet.Amount += amount;
            }

            return Task.CompletedTask; // Имитация асинхронного метода
        }

        public Task<int> GetBalanceAsync(Coin coin, CancellationToken token)
        {
            var wallet = _wallets.FirstOrDefault(w => w.Cur == coin);
            return Task.FromResult(wallet?.Amount ?? 0);
        }

        public Task<IEnumerable<Wallet>> GetAllWalletsAsync(CancellationToken token)
        {
            return Task.FromResult<IEnumerable<Wallet>>(_wallets);
        }
    }
}
/*
//PresentRepository
/*public class WalletStorage : IWalletStorage;
{
    private readonly FileStorage<Wallet> _repository;

    public WalletStorage() {
        _repository = new FileStorage<Wallet>("../../data/Wallet.json");
    }
*/
/*
using Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Storage;
    public class WalletStorage : IWalletStorage
    {
        private readonly FileStorage<Wallet> _repository;

        public WalletStorage()
        {
            _repository = new FileStorage<Wallet>("../../data/Wallet.json", "wallet.json");
        }
   


        // Получение текущего состояния кошелька
        public async Task<Wallet> GetWalletAsync()
        {
            var wallets = await _repository.GetAllAsync();
            return wallets.FirstOrDefault(); // Возвращаем первый (и единственный) кошелек
        }
        
        // Метод для пополнения кошелька
        public async Task DepositAsync(string currency, decimal amount, CancellationToken token)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            token.ThrowIfCancellationRequested();

            var wallet = await GetWalletAsync();
            wallet.Deposit(currency, amount);

            // Обновление состояния кошелька в репозитории
            await _repository.UpdateAsync(w => true, wallet);
        }

        // Метод для конвертации валют
        public async Task ConvertAsync(string fromCurrency, string toCurrency, decimal amount, decimal exchangeRate, CancellationToken token)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            token.ThrowIfCancellationRequested();

            var wallet = await GetWalletAsync();
            wallet.Convert(fromCurrency, toCurrency, amount, exchangeRate);

            // Обновление состояния кошелька в репозитории
            await _repository.UpdateAsync(w => true, wallet);
        }

        // Получение истории транзакций
        public async Task<IReadOnlyCollection<Transaction>> GetTransactionHistoryAsync()
        {
            var wallet = await GetWalletAsync();
            return wallet.TransactionHistory;
        }
    }



using Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Storage
{
    public class WalletStorage : IWalletStorage
    {
        private readonly FileStorage<Wallet> _repository;

        public WalletStorage()
        {
            _repository = new FileStorage<Wallet>("../../data/Wallet.json", "wallet.json");
        }

        // Получение текущего состояния кошелька
        public async Task<Wallet> GetWalletAsync()
        {
            var wallets = await _repository.GetAllAsync();
            return wallets.FirstOrDefault(); // Возвращаем первый (и единственный) кошелек
        }

        // Метод для пополнения кошелька
        public async Task DepositAsync(string currency, decimal amount, CancellationToken token)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            token.ThrowIfCancellationRequested();

            var wallet = await GetWalletAsync();
            wallet.Deposit(currency, amount);

            // Перезаписываем файл с новым состоянием кошелька
            await SaveWalletAsync(wallet);
        }

        // Метод для конвертации валют
        public async Task ConvertAsync(string fromCurrency, string toCurrency, decimal amount, decimal exchangeRate, CancellationToken token)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            token.ThrowIfCancellationRequested();

            var wallet = await GetWalletAsync();
            wallet.Convert(fromCurrency, toCurrency, amount, exchangeRate);

            // Перезаписываем файл с новым состоянием кошелька
            await SaveWalletAsync(wallet);
        }
        
        

        // Метод для сохранения обновленного кошелька
        private async Task SaveWalletAsync(Wallet wallet)
        {
            // Перезаписываем данные кошелька в файл
            await _repository.SaveAllAsync(new List<Wallet> { wallet });
        }

        // Получение истории транзакций
        public async Task<IReadOnlyCollection<Transaction>> GetTransactionHistoryAsync()
        {
            var wallet = await GetWalletAsync();
            return wallet.TransactionHistory;
        }

        public async Task ConvertAsync(string fromCurrency, string toCurrency, int amount, decimal exchangeRate)
        {
            throw new NotImplementedException();
        }
    }
}
*/