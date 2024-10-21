using Core;
using System;
using System.Collections.Generic;


namespace WalletView
{
    public class ConsoleWalletView : IWalletView.IWalletView
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {error}");
            Console.ResetColor();
        }

        public void DisplayTransactions(IReadOnlyCollection<Transaction> transactions)
        {
            if (transactions == null || transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            Console.WriteLine("Transaction History:");
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"- {transaction.Amount} {transaction.Currency} on {transaction.Date}");
            }
        }
    }
}