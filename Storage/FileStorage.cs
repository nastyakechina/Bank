using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class FileStorage<T>
{
    private readonly string _filePath;

    public FileStorage(string dataWalletJson, string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<T>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<T>();
        }

        using (var stream = new FileStream(_filePath, FileMode.Open))
        {
            return await JsonSerializer.DeserializeAsync<List<T>>(stream);
        }
    }

    public async Task AddAsync(T item)
    {
        var items = await GetAllAsync();
        items.Add(item);
        await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(items));
    }
    
    // Метод для сохранения всех объектов
    public async Task SaveAllAsync(IEnumerable<T> items)
    {
        var json = JsonSerializer.Serialize(items);
        await File.WriteAllTextAsync(_filePath, json);
    }

    // Другие методы для обновления и удаления элементов могут быть добавлены здесь
}