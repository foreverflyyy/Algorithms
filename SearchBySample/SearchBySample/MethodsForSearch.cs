namespace AlgorithmKMP
{
    /// <summary>
    /// Класс с методами для чтения данных и поиска по образцу
    /// </summary>
    public class MethodsForSearch
    {
        public string Text;

        // Для перевода(кодировки ASIIC) char в int и наоборот
        private readonly int allNumberSymbols = 256;

        public MethodsForSearch(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Простой поиск образца в строке «методом грубой силы»
        /// </summary>
        public void SimpleSearch(string pattern)
        {
            // i-с какого места строки  ищем
            // j-с какого места образца ищем
            for (int i = 0; i < Text.Length; ++i)
            {
                for (int j = 0; ; ++j)
                {
                    if (Text[i + j] != pattern[j])
                        break;

                    if ((j + 1) == pattern.Length)
                        Console.Write($"Substring is finded. It starts with {(i + 1)} elements!");
                }
            }

            Console.Write($"Substring wasn't finded.");
        }

        /// <summary>
        /// Реализация поиска по образцу с помощью конечного автомата
        /// </summary>
        public void FiniteStateMachine(string pattern)
        {
            int patternNums = pattern.Length;
            int textNums = Text.Length;

            // Создаем двухмерный массив (столбцы - кол-во символов в образце, строки - все возможные символы)
            int[][] setValues = new int[patternNums + 1][];

            for (int array1 = 0; array1 < (patternNums + 1); array1++)
                setValues[array1] = new int[allNumberSymbols];

            // покажет Finite Automata для переданного pattern
            int state, x;
            for (state = 0; state <= patternNums; ++state)
                for (x = 0; x < allNumberSymbols; ++x)
                    setValues[state][x] = GetNextState(pattern, patternNums, state, x);

            // Вывод индексов первых символов совпадений 
            int num = 0;
            for (int i = 0; i < textNums; i++)
            {
                num = setValues[num][Text[i]];
                if (num == patternNums)
                    Console.WriteLine("Pattern found at index " + (i - patternNums + 1));
            }
        }

        public int GetNextState(string pattern, int patternNums, int state, int x)
        {
            // Если текущий символ образца меньше всех его символов и совпал символ(в виде int) с символом из образца
            // то записываем что нашли похоже символ, тобишь записываем правильное значение (+1 к state)
            if (state < patternNums && (char)x == pattern[state])
                return state + 1;

            // ns покажет где следующий будет у нас state
            // начинаем с самого большого возможного значения и заканчиваем когда префикс является также и суффиксом
            for (int ns = state; ns > 0; ns--)
            {
                int i;
                // условие, что предыдущий наш символ совпадает с текущим x
                if (pattern[ns - 1] == (char)x)
                {
                    // через циксл смотрим чтоб рядом не повторялись элементы
                    for (i = 0; i < ns - 1; i++)
                        if (pattern[i] != pattern[state - ns + 1 + i])
                            break;

                    if (i == ns - 1)
                        return ns;
                }
            }

            return 0;
        }

        /// <summary>
        /// Реализация Алгоритма Кнута-Морриса-Пратта
        /// </summary>
        public void AlgorithmKMP(string pattern)
        {
            int patternLength = pattern.Length;
            int textLength = Text.Length;

            // Построение массива, с повторяющимися элементами с начала
            var repeatStartBegin = ComputeLPSArray(pattern);

            int i = 0;
            int j = 0;

            // Пока не дошли до конца текста, где уже не может быть подстроки
            while ((textLength - i) >= (patternLength - j))
            {
                // Если совпали символы с текста и образца
                if (pattern[j] == Text[i])
                {
                    j++;
                    i++;
                }

                // Если получили полное совпадение
                if (j == patternLength)
                {
                    Console.WriteLine("Found pattern at index " + (i - j));
                    j = repeatStartBegin[j - 1]; // Индекс на сколько смещаемся
                }

                else if (i < textLength && pattern[j] != Text[i])
                {
                    if (j != 0)
                        j = repeatStartBegin[j - 1]; // Индекс на сколько смещаемся
                    else
                        i++;
                }
            }
        }

        public int[] ComputeLPSArray(string pattern)
        {
            int patternLength = pattern.Length;
            int[] repeatStartBegin = new int[patternLength];
            repeatStartBegin[0] = 0;

            int indexPart = 0;
            int index = 1;

            while (index < patternLength)
            {
                if (pattern[index] == pattern[indexPart])
                {
                    indexPart++;
                    repeatStartBegin[index] = indexPart;
                    index++;
                    continue;
                }

                // Если мы еще на первом элементе
                if (indexPart == 0)
                {
                    repeatStartBegin[index] = 0;
                    index++;
                }
                else
                    indexPart = repeatStartBegin[indexPart - 1];
            }

            return repeatStartBegin;
        }

        /// <summary>
        /// Реализация Алгоритма Бойера Мура
        /// </summary>
        public void AlgorithmBoyerMoore(string pattern)
        {
            int m = pattern.Length;
            int n = Text.Length;

            int[] badchar = new int[allNumberSymbols];

            BadCharHeuristic(pattern, m, badchar);

            int s = 0; 
            
            while (s <= (n - m))
            {
                int j = m - 1;

                while (j >= 0 && pattern[j] == Text[s + j])
                    j--;

                if (j < 0)
                {
                    Console.WriteLine("Patterns occur at shift = " + s);

                    s += (s + m < n) ? m - badchar[Text[s + m]] : 1;
                }

                else
                    s += Max(1, j - badchar[Text[s + j]]);
            }
        }

        private int Max(int a, int b) 
        { 
            return (a > b) ? a : b; 
        }

        private void BadCharHeuristic(string str, int size, int[] badchar)
        {
            int i;

            for (i = 0; i < allNumberSymbols; i++)
                badchar[i] = -1;

            for (i = 0; i < size; i++)
                badchar[(int)str[i]] = i;
        }

        /// <summary>
        /// Реализация Алгоритма Рабина-Карпа
        /// </summary>
        public void AlgorithmRabinCarp(string pattern)
        {
            // Главный номер
            int q = 101;

            int patternNums = pattern.Length;
            int textNums = Text.Length;

            int i, j;

            int p = 0;
            int t = 0;

            int h = 1;

            for (i = 0; i < patternNums - 1; i++)
                h = (h * allNumberSymbols) % q;

            for (i = 0; i < patternNums; i++)
            {
                p = (allNumberSymbols * p + pattern[i]) % q;
                t = (allNumberSymbols * t + Text[i]) % q;
            }

            for (i = 0; i <= textNums - patternNums; i++)
            {
                if (p == t)
                {
                    for (j = 0; j < patternNums; j++)
                    {
                        if (Text[i + j] != pattern[j])
                            break;
                    }

                    if (j == patternNums)
                        Console.WriteLine(
                            "Pattern found at index " + i);
                }

                if (i < textNums - patternNums)
                {
                    t = (allNumberSymbols * (t - Text[i] * h) + Text[i + patternNums]) % q;

                    if (t < 0)
                        t = (t + q);
                }
            }
        }
    }
}
