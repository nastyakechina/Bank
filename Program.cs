using System;
using System.Threading;
using System.Threading.Tasks;
using Models;
using Storage;
using Presenter;
using View;

namespace Bank
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Menu menu = new Menu();
            await menu.StartMenuAsync(new CancellationToken(false));
            
        }
    }
}

