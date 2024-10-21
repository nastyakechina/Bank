//Управляет валютами и балансами в кошельке.
namespace Models;

public record Wallet(Coin Cur, int Amount);
