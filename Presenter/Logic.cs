using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Storage;

namespace Presenter
{
    public class Logic : ILogic
    {
        private readonly ICoinStorage _coinStorage;
        private readonly ITransactionStorage _transactionStorage;
        private readonly IWalletStorage _walletStorage;
        
        public Logic()
        {
            _coinStorage = new CoinStorage();
            _transactionStorage = new TransactionStorage();
            _walletStorage = new WalletStorage();
        }
        
        public Logic(ICoinStorage coinStorage, ITransactionStorage transactionStorage , IWalletStorage walletStorage)
        {
            _coinStorage = coinStorage;
            _transactionStorage = transactionStorage;
            _walletStorage = walletStorage;
        }

        

        // Пополнение кошелька
        public async Task DepositWalletAsync(string currency, int amount, CancellationToken token)
        {
            var coin = await _coinStorage.GetCoinAsync(currency, token);
            var coinAmount = new Dictionary<, Coin>();
            Wallet wallet = new Wallet();
            await _walletStorage.AddMoneyAsync(currency, amount, token);
            var transaction = new Transaction("п", amount);
            await _transactionStorage.AddTransactionAsync(transaction, token);
        }

        // Конвертация валюты
        public async Task<int> ConversionCoinAsync(string curFrom, string curTo, int amount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            // Проверяем наличие и получаем данные о валюте
            var fromCoin = await _coinStorage.GetCoinAsync(curFrom, cancellationToken);
            var toCoin = await _coinStorage.GetCoinAsync(curTo, cancellationToken);

            if (fromCoin == null || toCoin == null)
            {
                throw new Exception("Одна из валют не найдена");
            }

            // Рассчитываем сумму для конвертации
            int convertedAmount = (int)(amount * (decimal.Divide(toCoin.Course, fromCoin.Course)));

            // Проверяем наличие средств и проводим операцию
            bool success = await _walletStorage.SellMoneyAsync(curFrom, amount, cancellationToken);
            if (!success)
            {
                throw new Exception("Недостаточно средств для конвертации");
            }

            // Добавляем средства в целевую валюту
            await _walletStorage.AddMoneyAsync(curTo, convertedAmount, cancellationToken);

            // Записываем транзакцию конвертации
            var transaction = new Transaction("к", amount);
            await _transactionStorage.AddTransactionAsync(transaction, cancellationToken);
            return convertedAmount;
        }

        // Добавление новой валюты
        public async Task AddNewCoinAsync(Coin coin, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            // Добавляем новую валюту, если она еще не существует
            var existingCoin = await _coinStorage.GetCoinAsync(coin.Name, cancellationToken);
            if (existingCoin != null)
            {
                throw new Exception("Валюта уже существует.");
            }
            await _coinStorage.AddNewCoinAsync(coin, cancellationToken);
            Console.WriteLine($"> Валюта {coin.Name} успешно добавлена.");
        }

        // Получение баланса всех валют в кошельке
        public async Task<Dictionary<string, int>> GetBalanceAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var balances = await _walletStorage.GetMoneyAsync(cancellationToken);
            return balances;
        }

        // Получение истории транзакций
        public async Task<List<Transaction>> GetHistoryAsync(CancellationToken token)
        {
            return await _transactionStorage.GetHistoryAsync(token);
        }
    }
}
