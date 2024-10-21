using Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presenter
{
    public interface IConversionPresenter
    {
        Task<decimal> ConvertAsync(Coin curFrom, Coin curTo, decimal amount, CancellationToken token);
    }
}