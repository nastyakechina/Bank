using Models;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Presenter;

public interface ICoinPresenter
{
    Task AddNewCoin(string name, decimal amount);
}