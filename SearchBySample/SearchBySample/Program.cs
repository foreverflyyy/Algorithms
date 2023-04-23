namespace AlgorithmKMP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Берём файл для чтения матрицы
            string path = @"D:\dream\Algorithms\SearchBySample\SearchBySample\data2.txt";
            //string path = @"D:\workSpaceNU\primat\Algorithms\SearchBySample\SearchBySample\data.txt";
            var fileWithText = new StreamReader(path);
            var text = fileWithText.ReadToEnd();
            fileWithText?.Close();

            //string sample = "игла";
            //string sample = "ERG";
            string sample = "AABA";

            var methods = new MethodsForSearch(text);

            //methods.SimpleSearch(sample);
            //methods.AlgorithmKMP(sample);
            //methods.AlgorithmBoyerMoore(sample);
            //methods.AlgorithmRabinCarp(sample);
            methods.FiniteStateMachine(sample);
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
