//Представляет валюту (название, курс по отношению к базовой валюте, количество средств)
namespace Models;
public class Coin
{
    public string Name { get; set; }
    public decimal Course { get; set; }
    

    public Coin(string name, int course)
    {
        Name = name;
        Course = course;
    }
}



/*public record Coin (string Name, int Course);*/