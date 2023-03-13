namespace hashTable;

public class Item
{
    // Ключ и хранимые данные.
    public string Key { get; private set; }
    public string Value { get; private set; }

    // Создать новый экземпляр хранимых данных Item.
    public Item(string key, string value)
    {
        // Проверяем входные данные на корректность.
        if(string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));
        
        if(string.IsNullOrEmpty(value))
            throw new ArgumentNullException(nameof(value));

        // Устанавливаем значения.
        Key = key;
        Value = value;
    }
}