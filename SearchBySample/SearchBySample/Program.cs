namespace AlgorithmKMP
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\dream\Algorithms\SearchBySample\SearchBySample\data2.txt";
            //string path = @"D:\workSpaceNU\primat\Algorithms\SearchBySample\SearchBySample\data.txt";
            var fileWithText = new StreamReader(path);
            var text = fileWithText.ReadToEnd();
            var methods = new MethodsForSearch(text);

            //string sample = "ABCDAEABVABCDEFEEEAAF";
            //string sample = "EAF";
            //string sample = "ERG";
            string sample = "AABA";

            //methods.FiniteStateMachine(sample);
            //methods.AlgorithmKMP(sample);
            //methods.AlgorithmBoyerMoore(sample);
            methods.AlgorithmRabinCarp(sample);
        }
    }
}

/*
    Реализовать алгоритм поиска по образцу с помощью конечного автомата
    Реализовать алгоритм Кнута-Морриса-Пратта для поиска по образцу
    Реализовать алгоритм Бойера-Мура для поиска по образцу
    Реализовать алгоритм Рабина для поиска по образцу
*/
