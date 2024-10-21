using Core;
using System.Collections.Generic;

namespace IWalletView
{
    public interface IWalletView
    {
        void ShowMessage(string message);
        void ShowError(string error);
        void DisplayTransactions(IReadOnlyCollection<Transaction> transactions);
    }
}

