using System;
using System.Collections;

namespace hashTable
{
    class Programm
    {
        static string[] WithFileInArr(string path)
        {
            StreamReader file = new StreamReader(path);
            //берём с файла весь текст и приводим к нижнему регистру
            string text = file.ReadToEnd().ToLower();
            //пробегаемся по циклу и удаляем запятые и точки
            for (int i = 0; i < text.Length; i++)
                if (text[i] == ',' || text[i] == '.')
                {
                    text = text.Remove(i, 1);
                    i--;
                }
            //помещаем все слова в массив
            string[] allWords = text.Split(new char[] { ' ' });
            return allWords;
        }
        
        static void Main(string[] args)
        {
            string filePath = @"D:\dream\C#\HashTable\OpenAddressingHash\text.txt";;
            string[] allWords = WithFileInArr(filePath);
            
            // Создаем новую хеш таблицу.
            var hashTable = new HashTable();
            // Добавляем данные в хеш таблицу в виде пар Ключ-Значение.
            for (int i = 0; i < allWords.Length; i++)
                hashTable.Add(allWords[i]);
            
            // Выводим хранимые значения на экран.
            hashTable.ShowHashTable();
            Console.ReadLine();


            Console.WriteLine("########################################################");
            Console.WriteLine("########################################################");
            // Удаляем элемент из хеш таблицы по ключу
            hashTable.Delete("considered");
            hashTable.Delete("one");
            hashTable.Delete("situated");
            hashTable.ShowHashTable();
            Console.ReadLine();
            
            Console.WriteLine("########################################################");
            Console.WriteLine("########################################################");
            // Получаем хранимое значение из таблицы по ключу.
            Console.WriteLine(hashTable.Search("amazing"));
            Console.WriteLine(hashTable.Search("one"));
            Console.WriteLine(hashTable.Search("onescscs"));
        }
    }
}

/*Дан текстовый файл с некоторым текстом на русском или английском языках 
произвольной длины (организовать чтение). Выбрав некоторую 
хеш-функцию, создать хеш-таблицу с:
Лаба №13 “с наложением” (метод открытой адресации)
Таблицу записать в результирующий файл.*/
