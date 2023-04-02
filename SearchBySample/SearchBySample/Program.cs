using System;
using System.IO;

namespace AlgorithmKMP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Берём файл для чтения матрицы
            string path = @"D:\dream\Algorithms\SearchBySample\SearchBySample\data.txt";
            var fileWithText = new StreamReader(path);
            var text = fileWithText.ReadToEnd();

            Console.WriteLine("Enter word sample: ");
            //string? sample = fileWithText.ReadLine();
            string sample = "игла";
            fileWithText?.Close();

            var methods = new MethodsForSearch(text);
            methods.AlgorithmKMP(sample);
        }
    }
}
// Исходная строка считывается из некоторого файла. Строка поиска вводится с клавиатуры.

// аaabbaabaabaabbaaabaabaabaabaabbaabb
/*
    Реализовать алгоритм поиска по образцу с помощью конечного автомата
    Реализовать алгоритм Кнута-Морриса-Пратта для поиска по образцу
    Реализовать алгоритм Бойера-Мура для поиска по образцу
    Реализовать алгоритм Рабина для поиска по образцу
*/
