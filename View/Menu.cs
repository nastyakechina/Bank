using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Presenter;
using Models;
using Storage;

namespace View
{
    public class Menu
    {
        private ILogic _logic;

        public Menu()
        {
            _logic = new Logic();
        }

        public async Task StartMenuAsync(CancellationToken cancellationToken)
        {
            Dictionary<string, Func<CancellationToken, Task>> menuOptions = new Dictionary<string, Func<CancellationToken, Task>>
            {
                { "1", AddCurrencyAsync },
                { "2", DepositAsync },
                { "3", ConvertAsync },
                { "4", ViewBalanceAsync },
                { "5", ViewHistoryAsync },
                { "0", ExitAsync }
            };

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                DisplayMainOptions();

                string choice = Console.ReadLine();
                if (menuOptions.ContainsKey(choice))
                {
                    await menuOptions[choice].Invoke(cancellationToken);
                }
                else
                {
                    Console.WriteLine("> Неверный ввод. Пожалуйста, введите число от 0 до 5.");
                }
            }
        }

        private void DisplayMainOptions()
        {
            Console.WriteLine("\n> Выберите действие:");
            Console.WriteLine("1. Добавить валюту");
            Console.WriteLine("2. Пополнить кошелек");
            Console.WriteLine("3. Конвертировать валюту");
            Console.WriteLine("4. Посмотреть баланс");
            Console.WriteLine("5. Просмотреть историю транзакций");
            Console.WriteLine("0. Выйти из программы");
        }

        private async Task AddCurrencyAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("> Введите название валюты:");
            string currency = Console.ReadLine();

            Console.WriteLine("> Введите курс валюты:");
            if (int.TryParse(Console.ReadLine(), out int rate))
            {
                await _logic.AddNewCoinAsync(new Coin(currency, rate), cancellationToken);
                Console.WriteLine($"> Валюта {currency} успешно добавлена.");
            }
            else
            {
                Console.WriteLine("> Ошибка: некорректный курс валюты.");
            }
        }

        private async Task DepositAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("> Введите валюту для пополнения:");
            string currency = Console.ReadLine();

            Console.WriteLine("> Введите сумму для пополнения:");
            if (int.TryParse(Console.ReadLine(), out int amount))
            {
                await _logic.DepositWalletAsync(currency, amount, cancellationToken);
                Console.WriteLine($"> Кошелек успешно пополнен на {amount} {currency}.");
            }
            else
            {
                Console.WriteLine("> Ошибка: некорректная сумма.");
            }
        }

        private async Task ConvertAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("> Введите валюту для конвертации:");
            string curFrom = Console.ReadLine();

            Console.WriteLine("> Введите целевую валюту:");
            string curTo = Console.ReadLine();

            Console.WriteLine("> Введите сумму для конвертации:");
            if (int.TryParse(Console.ReadLine(), out int amount))
            {
                int convertedAmount = await _logic.ConversionCoinAsync(curFrom, curTo, amount, cancellationToken);
                Console.WriteLine($"> Конвертация выполнена. Вы получили {convertedAmount} {curTo}.");
            }
            else
            {
                Console.WriteLine("> Ошибка: некорректная сумма.");
            }
        }

        private async Task ViewBalanceAsync(CancellationToken cancellationToken)
        {
            var balance = await _logic.GetBalanceAsync(cancellationToken);

            Console.WriteLine("> Текущий баланс:");
            foreach (var entry in balance)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }

        private async Task ViewHistoryAsync(CancellationToken cancellationToken)
        {
            List<Transaction> history = await _logic.GetHistoryAsync(cancellationToken);

            if (history.Count > 0)
            {
                Console.WriteLine("> История транзакций:");
                foreach (var transaction in history)
                {
                    Console.WriteLine($"{transaction.Type} - {transaction.Amount}");
                }
            }
            else
            {
                Console.WriteLine("> История пуста.");
            }
        }

        private Task ExitAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("> Завершение работы...");
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}
