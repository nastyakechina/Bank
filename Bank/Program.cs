using System;
using System.Threading.Tasks;
using Storage;

class Program
{
    static async Task Main(string[] args)
    {
        var walletRepository = new WalletStorage();

        await walletRepository.DepositAsync("USD", 100, CancellationToken.None);
        await walletRepository.ConvertAsync("USD", "EUR", 50, 0.85m);

        var wallet = await walletRepository.GetWalletAsync();
        Console.WriteLine($"USD Balance: {wallet.GetBalance("USD")}");
        Console.WriteLine($"EUR Balance: {wallet.GetBalance("EUR")}");
        
        Console.WriteLine("Transaction History:");
        foreach (var transaction in wallet.TransactionHistory)
        {
            Console.WriteLine($"{transaction.Type}: {transaction.Amount} {transaction.Currency} on {transaction.Date}");
        }
    }
}