using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

namespace Storage
{
    public interface ICoinStorage
    {
        Task AddNewCoinAsync(Coin coin, CancellationToken cancellationToken);
        Task<Coin> GetCoinAsync(string currencyName, CancellationToken cancellationToken);
    }
}



/*using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Storage
{
    public interface ICoinStorage
    {
        Task AddNewCoinAsync(Coin coin, CancellationToken token);
        Task<IReadOnlyCollection<Coin>> GetCoinAsync(Coin coin, CancellationToken token);
    }
}


using Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Storage
{
    public interface ICoinStorage
    {
        // Добавить новую валюту
        Task AddCoinAsync(Coin coin, CancellationToken token);

        // Получить валюту по имени
        Task<Coin> GetCoinByNameAsync(string name, CancellationToken token);

        // Обновить данные валюты
        Task UpdateCoinAsync(Coin coin, CancellationToken token);

        // Удалить валюту по имени
        Task RemoveCoinByNameAsync(string name, CancellationToken token);

        // Получить список всех валют
        Task<IList<Coin>> GetAllCoinsAsync(CancellationToken token);
    }
}*/