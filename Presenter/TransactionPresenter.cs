using Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Storage;

namespace Presenter
{
    public class TransactionPresenter : ITransactionPresenter
    {
        private readonly ITransactionStorage _transactionStorage;
        private readonly Wallet _wallet;
        private readonly ConversionPresenter _conversionPresenter;

        public TransactionPresenter(ITransactionStorage transactionStorage, Wallet wallet, ConversionPresenter conversionPresenter)
        {
            _transactionStorage = transactionStorage;
            _wallet = wallet;
            _conversionPresenter = conversionPresenter;
        }

        // Метод для пополнения кошелька
        public async Task DepositAsync(string currency, decimal amount, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentNullException(nameof(currency));
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            // Проверяем на отмену операции
            token.ThrowIfCancellationRequested();

            // Пополняем кошелек
            _wallet.DepositAsync(currency, amount);

            // Сохраняем информацию о транзакции
            var transaction = new Transaction("Deposit", amount);
            await _transactionStorage.SaveTransactionAsync(transaction, token);
        }

        // Метод для конвертации валют
        public async Task ConversionAsync(string curFrom, string curTo, decimal amount, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(curFrom) || string.IsNullOrWhiteSpace(curTo))
            {
                throw new ArgumentNullException("Currency names cannot be null or empty.");
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            // Проверяем на отмену операции
            token.ThrowIfCancellationRequested();

            // Проверяем наличие достаточных средств
            if (!_wallet.HasSufficientFunds(curFrom, amount))
            {
                throw new InvalidOperationException("Insufficient funds in the wallet.");
            }

            // Получаем валюты из хранилища
            var coinFrom = await _coinStorage.GetCoinAsync(curFrom, token);
            var coinTo = await _coinStorage.GetCoinAsync(curTo, token);

            // Используем ConversionPresenter для выполнения конвертации
            decimal convertedAmount = await _conversionPresenter.ConvertAsync(coinFrom, coinTo, amount, token);

            // Обновляем кошелек
            _wallet.DepositAsync(curTo, convertedAmount);

            // Сохраняем информацию о транзакции
            var transaction = new Transaction("Conversion", convertedAmount);
            await _transactionStorage.SaveTransactionAsync(transaction, token);
        }
    }
}
