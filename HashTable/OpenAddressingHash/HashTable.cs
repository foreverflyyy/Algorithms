using System.Collections.Generic;
using System.Linq;

namespace hashTable;

// Используется метод цепочек (открытое хеширование)
public class HashTable
{
        private readonly byte _maxSizeKey = 25;
        private byte startCapacityTable = 8;
        const double rehash_size = 0.75; // кф, при котором произойдет увеличение таблицы
        
        int nodeSize; // сколько элементов у нас сейчас в таблице (без учета deleted)
        int nodeSizeWithDeleted; // сколько элементов у нас сейчас в в таблице (с учетом deleted)
        int tableSize; // размер самой таблицы, сколько памяти выделено под хранение нашей таблицы
        
        public struct Node
        {
            public string key;
            public string value;
            public bool state; // если значение флага state = false, значит элемент массива был удален (deleted)
        }
        private Node[] _arrayHash;

        public HashTable()
        {
            _arrayHash = new Node[startCapacityTable];
            tableSize = startCapacityTable;
            nodeSize = 0;
            nodeSizeWithDeleted = 0;
        }

        // Добавить данные в хеш таблицу.
        public void Add(string key)
        {
            CheckKey(key);
            
            if (nodeSize + 1 > Convert.ToInt32(rehash_size * tableSize))
                Resize();
            else if (nodeSizeWithDeleted > 2 * nodeSize)
                Rehash(); // происходит рехеш, так как слишком много deleted-элементов
            
            string value = GetValue(key);
            
            var keyTableItem = Array.Find(_arrayHash, x => x.state && x.key == key);
            if (keyTableItem.key != null)
                return;

            // находим место для нового элемента
            int index = Array.FindIndex(_arrayHash, x => !x.state);
            _arrayHash[index].key = key;
            _arrayHash[index].value = value;
            _arrayHash[index].state = true;
            
            ++nodeSize; // и в любом случае мы увеличили количество элементов
            ++nodeSizeWithDeleted;
        }
        public void Resize()
        {
            tableSize *= 2;
            Array.Resize(ref _arrayHash, tableSize);
        }
        public void Rehash()
        {
            nodeSizeWithDeleted = nodeSize;
            _arrayHash = Array.FindAll(_arrayHash, x => x.state);
        }
        public void Delete(string key)
        {
            CheckKey(key);

            // Если таблица не содержит такого ключа,то завершаем.
            var keyTableItem = Array.Find(_arrayHash, x => x.state && x.key == key);
            if(keyTableItem.key == "")
            {
                Console.WriteLine("Element didn't found");
                return;
            }

            int index = Array.FindIndex(_arrayHash, x => x.state && x.key == key);
            _arrayHash[index].state = false;
            nodeSize--;
        }
        // Поиск значения по ключу.
        public string Search(string key)
        {
            CheckKey(key);
            // Получаем название ключа.
            var value = GetValue(key);

            // Если таблица не содержит такого ключа,то завершаем.
            var keyTableItem = Array.Find(_arrayHash, x => x.state && x.key == key);
            if(keyTableItem.key == null) 
                return "Элемент не был найден";
            else
                return keyTableItem.value;
        }
        private void _showHashTable(HashTable hashTable)
        {
            // Проверяем входные аргументы.
            if(hashTable == null)
                throw new ArgumentNullException(nameof(hashTable));
            
            // Выводим все имеющие пары хеш-значение
            foreach (var item in hashTable._arrayHash)
                if(item.state)
                    Console.WriteLine(item.key + " - " + item.value);
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
        // Проверяем на корректность.
        private void CheckKey(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (value.Length > _maxSizeKey)
                throw new ArgumentException($"Максимальная длина значения составляет {_maxSizeKey} символов.", nameof(value));
        }
}