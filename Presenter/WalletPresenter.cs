using Models;
using Storage;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Presenter
{
    public class WalletPresenter : IWalletPresenter
    {
        private readonly IWalletStorage _walletStorage;

        public WalletPresenter(IWalletStorage walletStorage)
        {
            _walletStorage = walletStorage ?? throw new ArgumentNullException(nameof(walletStorage));
        }

        // Метод для пополнения кошелька
        public async Task DepositAsync(Coin coin, int amount, CancellationToken token)
        {
            if (coin == null)
            {
                throw new ArgumentNullException(nameof(coin));
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Сумма должна быть больше нуля.");
            }

            // Проверяем на отмену операции
            token.ThrowIfCancellationRequested();

            // Пополняем кошелек через хранилище
            await _walletStorage.TypeDepositAsync(coin, amount, token);
        }

        // Метод для получения баланса по валюте
        public async Task<int> GetBalanceAsync(Coin coin, CancellationToken token)
        {
            if (coin == null)
            {
                throw new ArgumentNullException(nameof(coin));
            }

            // Проверяем на отмену операции
            token.ThrowIfCancellationRequested();

            return await _walletStorage.GetBalanceAsync(coin, token);
        }

        // Метод для получения всех доступных валют
        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync(CancellationToken token)
        {
            // Проверяем на отмену операции
            token.ThrowIfCancellationRequested();

            return await _walletStorage.GetAllWalletsAsync(token);
        }
    }
}


/*
{
    public class WalletPresenter
    {
        private readonly IWalletStorage _walletStorage;
        private readonly IWalletView _view;

        public WalletPresenter(IWalletStorage walletStorage, IWalletView view)
        {
            _walletStorage = walletStorage;
            _view = view;
        }

        public async Task ShowWalletBalanceAsync()
        {
            var wallet = await _walletStorage.GetWalletAsync();
            if (wallet != null)
            {
                _view.DisplayWalletBalance(wallet);
            }
            else
            {
                _view.ShowMessage("Wallet not found.");
            }
        }

        public async Task DepositAsync()
        {
            string currency = _view.GetInput("Enter the currency to deposit:");
            string amountInput = _view.GetInput("Enter the amount:");
            if (decimal.TryParse(amountInput, out decimal amount))
            {
                await _walletStorage.DepositAsync(currency, amount, CancellationToken.None);
                _view.ShowMessage("Deposit successful.");
            }
            else
            {
                _view.ShowMessage("Invalid amount.");
            }
        }

        public async Task ConvertAsync()
        {
            string fromCurrency = _view.GetInput("Enter the currency to convert from:");
            string toCurrency = _view.GetInput("Enter the currency to convert to:");
            string amountInput = _view.GetInput("Enter the amount to convert:");
            string rateInput = _view.GetInput("Enter the exchange rate:");
            
            if (decimal.TryParse(amountInput, out decimal amount) && decimal.TryParse(rateInput, out decimal exchangeRate))
            {
                await _walletStorage.ConvertAsync(fromCurrency, toCurrency, amount, exchangeRate, CancellationToken.None);
                _view.ShowMessage("Conversion successful.");
            }
            else
            {
                _view.ShowMessage("Invalid amount or exchange rate.");
            }
        }

        public async Task ShowTransactionHistoryAsync()
        {
            var transactions = await _walletStorage.GetTransactionHistoryAsync();
            _view.DisplayTransactionHistory(transactions);
        }
    }
}
*/