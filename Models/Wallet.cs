//Управляет валютами и балансами в кошельке.
namespace Models
{
    public class Wallet
    {

        private List<CoinAmount> _coinAmounts;

        public Wallet()
        {
            _coinAmounts = new List<CoinAmount>();
        }
        
        public async Task<List<CoinAmount>> GetCoinAmount()
        {
            return _coinAmounts;
        }
        public async Task AddCoinAmount( CoinAmount coinAmount)
        {
            _coinAmounts.Add(coinAmount);
        }
        
        // // Добавление монеты с проверкой на существование
        // public bool AddCoin(Coin coin)
        // {
        //     if (!Coins.ContainsKey(coin.Name))
        //     {
        //         Coins[coin.Name] = coin;
        //         return true; // Монета успешно добавлена
        //     }
        //     return false; // Монета уже существует
        // }
        //
        // // Получение количества валюты
        // public int GetAmount(string currency)
        // {
        //     return Coins.ContainsKey(currency) ? Coins[currency].Amount : 0;
        // }
        //
        // // Пополнение баланса указанной валюты
        // public void DepositAmount(string currency, int amount)
        // {
        //     if (Coins.ContainsKey(currency))
        //     {
        //         Coins[currency].Amount += amount;
        //     }
        //     else
        //     {
        //         throw new InvalidOperationException($"Валюта {currency} не найдена в кошельке.");
        //     }
        // }
        //
        // // Уменьшение баланса для указанной валюты
        // public bool SellAmount(string currency, int amount)
        // {
        //     if (Coins.ContainsKey(currency) && Coins[currency].Amount >= amount)
        //     {
        //         Coins[currency].Amount -= amount;
        //         return true;
        //     }
        //     return false; // Недостаточно средств для уменьшения
        // }
        //
        // // Получение всех балансов валют в кошельке
        // public Dictionary<string, int> GetAllBalances()
        // {
        //     return Coins.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Amount);
        // }

    }
}


//public record Wallet(Coin Cur, int Amount);
