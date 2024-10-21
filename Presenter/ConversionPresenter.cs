using Models;
using Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presenter
{
    public class ConversionPresenter : IConversionPresenter
    {
        private readonly ICoinStorage _coinStorage;

        public ConversionPresenter(ICoinStorage coinStorage)
        {
            _coinStorage = coinStorage ?? throw new ArgumentNullException(nameof(coinStorage));
        }

        // Метод для конвертации из одной валюты в другую
        public async Task<decimal> ConvertAsync(Coin curFrom, Coin curTo, decimal amount, CancellationToken token)
        {
            if (curFrom == null)
            {
                throw new ArgumentNullException(nameof(curFrom));
            }

            if (curTo == null)
            {
                throw new ArgumentNullException(nameof(curTo));
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Сумма должна быть больше нуля.");
            }

            // Проверяем на отмену операции
            token.ThrowIfCancellationRequested();

            // Получаем курсы валют из хранилища
            var fromRate = curFrom.Course; // Курс для исходной валюты
            var toRate = curTo.Course; // Курс для целевой валюты

            // Сначала переводим сумму в рубли
            decimal amountInRUB = amount * fromRate;

            // Затем переводим рубли в целевую валюту
            decimal convertedAmount = amountInRUB / toRate;

            return await Task.FromResult(convertedAmount); // Возвращаем результат
        }
    }
}