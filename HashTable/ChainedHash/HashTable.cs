using System.Collections.Generic;
using System.Linq;

namespace hashTable;

// Используется метод цепочек (открытое хеширование)
public class HashTable
{
        private readonly byte _maxSize = 25;
        
        // Проверяем на корректность.
        private void CheckKey(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (value.Length > _maxSize)
                throw new ArgumentException($"Максимальная длинна значения составляет {_maxSize} символов.", nameof(value));
        }

        // Коллекция хранимых данных, словарь (та же хэш функция)
        // ключ - первый элемент значения, а значение это список слов начинающихся на один и тот же ключ.
        private Dictionary<string, List<Item>> _items = null;

        // Коллекция хранимых данных в хеш-таблице в виде пар Ключ-Значения.
        public IReadOnlyCollection<KeyValuePair<string, List<Item>>> Items => _items?.ToList()?.AsReadOnly();

        // Создать новый экземпляр класса HashTable.
        public HashTable()
        {
            // Инициализируем коллекцию максимальным количество элементов.
            _items = new Dictionary<string, List<Item>>(27);
        }

        // Добавить данные в хеш таблицу.
        public void Insert(string keyList)
        { 
            // Проверяем входные данные на корректность.
            CheckKey(keyList);
            var value = GetValue(keyList);
            var keyTableHash = GetNumberTable(keyList);
            // Создаем новый экземпляр данных.
            var item = new Item(keyList, value);

            // Если коллекция(список) не пустая, значит значения с таким хешем уже существуют, добавляем туда ещё один
            // Если коллекция пустая, значит значений с таким хешем ключа ранее не было, создаем новую
            List<Item> hashTableItem = null;
            if (_items.ContainsKey(keyTableHash))
            {
                // Получаем элемент хеш таблицы.
                hashTableItem = _items[keyTableHash];

                // Проверяем наличие внутри коллекции значения с полученным ключом.
                // Если такой элемент найден, то сообщаем об ошибке(в нашем случае просто не будем добавлять элемент(т.к он уже есть)).
                var elementWithKey = hashTableItem.SingleOrDefault(i => i.Key == item.Key);
                if (elementWithKey != null)
                    //throw new ArgumentException($"Уже содержится элемент с ключом {key}. Ключ должен быть уникален.", nameof(key));
                    return;
                
                // Добавляем элемент данных в коллекцию элементов хеш таблицы.
                _items[keyTableHash].Add(item);
            } else
            {
                // Создаем новую коллекцию.
                hashTableItem = new List<Item>{ item };

                // Добавляем данные в таблицу.
                _items.Add(keyTableHash, hashTableItem);
            }
        }

        // Удалить данные из хеш таблицы по ключу.
        public void Delete(string keyList)
        {
            CheckKey(keyList);
            
            // Получаем ключ в таблице.
            var keyTableHash = GetNumberTable(keyList);

            // Если значения с таким хешем нет в таблице, 
            // то завершаем выполнение метода.
            if (!_items.ContainsKey(keyTableHash)) return;

            // Получаем коллекцию элементов по ключу.
            var keyTableItem = _items[keyTableHash];

            // Получаем элемент коллекции по ключу.
            var item = keyTableItem.SingleOrDefault(i => i.Key == keyList);

            // Если элемент коллекции найден, то удаляем его из коллекции.
            if (item != null)
                keyTableItem.Remove(item);
        }

        // Поиск значения по ключу.
        public string Search(string keyList)
        {
            CheckKey(keyList);
            
            // Получаем название ключа.
            var keyTableHash = GetNumberTable(keyList);

            // Если таблица не содержит такого ключа,то завершаем.
            if(!_items.ContainsKey(keyTableHash)) return null;

            // Если ключ найден, то ищем значение в коллекции по ключу.
            var keyTableItem = _items[keyTableHash];

            // Если хеш найден, то ищем значение в коллекции по ключу.
            if (keyTableItem != null)
            {
                // Получаем элемент коллекции по ключу.
                var item = keyTableItem.SingleOrDefault(i => i.Key == keyList);

                // Если элемент коллекции найден, то возвращаем значение.
                if (item != null)
                   return item.Value;
            }
            // если ничего найдено.
            return "Элемент не был найден.";
        }
        private void _showHashTable(HashTable hashTable)
        {
            // Проверяем входные аргументы.
            if(hashTable == null)
                throw new ArgumentNullException(nameof(hashTable));
            
            // Выводим все имеющие пары хеш-значение
            foreach (var item in hashTable.Items)
            {
                // Выводим ключ именно хэш таблицы
                Console.WriteLine(item.Key);

                // Выводим все значения хранимые под этим ключом.
                foreach(var value in item.Value)
                    Console.WriteLine($"\t{value.Key} - {value.Value}");
            }
            Console.WriteLine();
        }

        public void ShowHashTable()
        {
            _showHashTable(this);
        }
        // Возвращает первый элемент ключа.
        private string GetValue(string line)
        {
            return Convert.ToString(line.Length);
        }
        private string GetNumberTable(string line)
        {
            return Convert.ToString(line[0]);
        }
        
}