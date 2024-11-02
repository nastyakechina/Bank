//Управляет логикой конвертации из одной валюты в другую.
namespace Models;

public class Conversion
{
    public string CurrencyFrom { get; set; }
    public string CurrencyTo { get; set; }
    public int Amount { get; set; }

    public Conversion(string curFrom, string curTo, int amount)
    {
        CurrencyFrom = curFrom;
        CurrencyTo = curTo;
        Amount = amount;
    }
}


//public record Conversion(Coin CurFrom, Coin CurTo, int Amount);
