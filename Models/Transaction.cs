//Хранит информацию о транзакциях (сумма, тип операции: пополнение или конвертация).
namespace Models;

public record Transaction (string Type, decimal Amount);