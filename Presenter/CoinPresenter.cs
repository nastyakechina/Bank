using Models;
using Storage;
using System.Threading;
using System.Threading.Tasks;

namespace Presenter
{
    public class CoinPresenter : ICoinPresenter
    {
        private readonly ICoinStorage _coinStorage;

        // Конструктор с зависимостью
        public CoinPresenter(ICoinStorage coinStorage)
        {
            _coinStorage = coinStorage ?? throw new ArgumentNullException(nameof(coinStorage));
        }

        // Добавление новой валюты
        public async Task AddNewCoinAsync(string name, decimal course, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (course <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(course), "Course should be greater than zero.");
            }

            Guid id = Guid.NewGuid();
            Coin coin = new Coin(id, name, course);

            // Проверяем на отмену операции
            token.ThrowIfCancellationRequested();

            // Асинхронно добавляем новую валюту через хранилище
            await _coinStorage.AddNewCoinAsync(coin, token);
        }
    }
}


