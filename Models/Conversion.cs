//Управляет логикой конвертации из одной валюты в другую.
namespace Models;

public record Conversion(Coin CurFrom, Coin CurTo, decimal Amount);
