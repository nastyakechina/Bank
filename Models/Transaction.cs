//Хранит информацию о транзакциях (сумма, тип операции: пополнение или конвертация).
namespace Models;

public class Transaction
{
    public string Type { get; set; } // "п" для пополнения, "к" для конвертации
    public int Amount { get; set; }

    public Transaction(string type, int amount)
    {
        Type = type;
        Amount = amount;
    }
}

//public record Transaction (string Type, int Amount);