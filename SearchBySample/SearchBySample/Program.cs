using System;
using System.IO;

namespace AlgorithmKMP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Берём файл для чтения матрицы
            string path = @"D:\dream\Algorithms\SearchBySample\AlgorithmKMP\data.txt";
            var fileWithText = new StreamReader(path);

            string? text = fileWithText.ReadLine();
            fileWithText?.Close();

            Console.WriteLine("Enter word sample: ");
            string sample = Console.ReadLine();

            var methods = new MethodsForSearch(text);
            methods.AlgorithmKMP(sample);
        }
    }
}
// Исходная строка считывается из некоторого файла. Строка поиска вводится с клавиатуры.

// Реализовать алгоритм Кнута-Морриса-Пратта для поиска по образцу