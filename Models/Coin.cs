//Представляет валюту (название, курс по отношению к базовой валюте, количество средств)
namespace Models;

public record Coin (Guid id, string Name, decimal Course);