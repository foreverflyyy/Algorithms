using System.Numerics;

namespace AlgorithmKMP
{
    /// <summary>
    /// Класс с методами для чтения данных и поиска по образцу
    /// </summary>
    public class MethodsForSearch
    {
        public string Text;

        public MethodsForSearch(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Простой поиск образца в строке «методом грубой силы»
        /// </summary>
        /// <param name="sample"> Передаваемая строка образец </param>
        public int SimpleSearch(string sample)
        {
            // i-с какого места строки  ищем
            // j-с какого места образца ищем
            for (int i = 0; i < Text.Length; ++i)
            {
                for (int j = 0; ; ++j)
                {
                    if (Text[i + j] != sample[j]) 
                        break;

                    if ((j + 1) == sample.Length)
                        return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Реализация Алгоритма Кнута-Морриса-Пратта
        /// </summary>
        /// <param name="sample"> Передаваемая строка образец </param>
        /// <returns></returns>
        public void AlgorithmKMP(string sample)
        {
            var lengthText = Text.Length;
            var lengthSample = sample.Length;

            // Массив длин префиксов для образца (\сколько символов в образце)
            var pi = new int[lengthSample];
            // p[0]=0 всегда, p[1]=1, если начинается с двух одинаковых 
            pi[0] = 0; 

            // длина образца
            int l; 

            // Заполняем массив для префиксов
            for (l = 1; l < lengthSample; ++l)
            {
                int k = pi[l - 1];

                // Если совпадение, то берем ранее рассчитанное значение (начиная с максимально возможных)
                while ((k > 0) && (sample[l] != sample[k]))
                    k = pi[k - 1]; 

                if (sample[l] == sample[k])
                    ++k;

                pi[l] = k;
            }

            // количество совпавших символов, оно же индекс сравниваемого 
            // символа в образце. В строке сравниваемый символ будет иметь индекс i
            int j = 0;

            for (int i = 1; i < lengthText; ++i)
            {
                // Символ строки не совпал с символом в образце. Сдвигаем образец, зная, что первые
                // j символов образца совпали с символами строки и надо сравнить j+1й символ образца
                // (его индекс j) с i+1м символом строки. если j=0, то достигли начала образца
                while ((j > 0) && (Text[i] != sample[j]))
                    j = pi[j - 1];

                // есть совпадение очередного символа увеличиваем длину совпавшего фрагмента
                if (Text[i] == sample[j])
                    ++j;

                // Когда образец найден
                // Вычисляется и выводится индекс начала найденного в str образца 
                if (j == l)
                {
                    Console.WriteLine($"With this index start word in text: {(i - l + 1)}");

                    for (int k = 0; k < lengthSample; ++k)
                        Console.Write(Text[(i - l + 1) + k]);

                    // Можно не возвращать, а искать дальше совпадения
                    return;
                } 
            }
        }

        /// <summary>
        /// Реализация префикс функции
        /// </summary>
        public List<int> PrefixFunction()
        {
            int lengthText = Text.Length;
            var pi = new List<int>(lengthText); // в i-м элементе (его индекс i-1) количество совпавших символов в начале и конце для подстроки длины i. 
                                  // p[0]=0 всегда, p[1]=1, если начинается с двух одинаковых 
            for (int i = 1; i < lengthText; ++i)
            {
                // ищем, какой префикс-суффикс можно расширить
                int j = pi[i - 1]; // длина предыдущего префикса-суффикса, возможно нулевая
                while ((j > 0) && (Text[i] != Text[j])) // этот нельзя расширить,
                    j = pi[j - 1];   // берем длину меньшего префикса-суффикса

                if (Text[i] == Text[j])
                    ++j;  // расширяем найденный (возможно пустой) префикс-суффикс
                pi[i] = j;
            }
            return pi;
        }

        /// <summary>
        /// Реализация Алгоритма Бойера Мура
        /// </summary>
        /// <param name="sample"> Передаваемая строка образец </param>
        /// <returns></returns>
        public void AlgorithmBoyerMoore(string sample)
        {
            
        }
    }
}
