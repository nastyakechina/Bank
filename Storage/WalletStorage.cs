using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Storage
{
    public class WalletStorage : IWalletStorage
    {
        private readonly IFileStorage<Wallet> _storage;
        
        public WalletStorage()
        {
            _storage = new FileStorage<Wallet>("../../data/wallets.json", "wallets.json");
        }

        public WalletStorage(IFileStorage<Wallet> storage)
        {
            _storage = storage;
        }

        public async Task CreateWallet(Wallet _wallet,CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
           await _storage.AddAsync(_wallet, token);
        }

        
        
        public async Task AddMoneyAsync(Wallet _wallet,  CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var wallets = await _storage.GetAllAsync(token);
            var wallet = wallets.First();
            await wallet.SetCoinAmount(await _wallet.GetCoinAmount());
            await _storage.UpdateAsync(_ => true, wallet, token); // Обновляем весь кошелек
        }

        
        public async Task<Dictionary<int, Coin>> GetMoneyAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var wallets = await _storage.GetAllAsync(token);
            var wallet = wallets.First(); // Предполагаем, что у нас только один кошелек
            return await wallet.GetCoinAmount();
        }

        // public async Task<bool> SellMoneyAsync(string currency, int amount, CancellationToken token)
        // {
        //     token.ThrowIfCancellationRequested();
        //     var wallets = await _storage.GetAllAsync(token);
        //     var wallet = wallets.FirstOrDefault(); // Предполагаем, что у нас только один кошелек
        //
        //     if (wallet == null)
        //     {
        //         throw new Exception("Wallet not found");
        //     }
        //
        //     bool success = wallet.SellAmount(currency, amount);
        //     if (success)
        //     {
        //         await _storage.UpdateAsync(_ => true, wallet, token); // Обновляем весь кошелек
        //     }
        //     return success;
        // }
    }
}
