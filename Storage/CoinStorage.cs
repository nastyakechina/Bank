using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Storage
{
    public class CoinStorage : ICoinStorage
    {
        private readonly IFileStorage<Coin> _storage;

        public CoinStorage()
        {
            _storage = new FileStorage<Coin>("../../data/coins.json", "coins.json");
        }

        public CoinStorage(IFileStorage<Coin> storage)
        {
            _storage = storage;
        }

        public async Task AddNewCoinAsync(Coin coin, CancellationToken token)
        {
            if (coin == null)
            {
                throw new ArgumentNullException(nameof(coin));
            }
            token.ThrowIfCancellationRequested();
            await _storage.AddAsync(coin, token);
        }

        public async Task<Coin> GetCoinAsync(string currencyName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var coins = await _storage.GetAllAsync(token);
            return coins.FirstOrDefault(c => c.Name.Equals(currencyName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
