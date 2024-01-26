namespace AlgorithmKMP
{
    /// <summary>
    /// Класс с методами для чтения данных и поиска по образцу
    /// </summary>
    public class MethodsForSearch
    {
        public string Text;

        // Для перевода(кодировки ASIIC) char в int и наоборот
        // (если использовать русские буквы, то уведичить число до +-1200)
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
            int patternLength = pattern.Length;
            int textLength = Text.Length;

            // Перевод символов образца в числа
            var numberChars = ArrayCharsToInt(pattern);

            int index = 0; 
            
            // Пока не дошли до индекса текста, где не может быть подстроки
            while (index <= (textLength - patternLength))
            {
                int j = patternLength - 1;

                // Ищем как бы с суффикса, идём с задней части 
                while (j >= 0 && pattern[j] == Text[index + j])
                    j--;

                // Значит нашли совпадение
                if (j < 0)
                {
                    Console.WriteLine("Patterns occur at shift = " + index);

                    if (index + patternLength < textLength)
                        index += patternLength - numberChars[Text[index + patternLength]];
                    else
                        index ++;
                }
                // Большее между 
                else
                    index += Math.Max(1, j - numberChars[Text[index + j]]);
            }
        }

        private int[] ArrayCharsToInt(string pattern)
        {
            int patternLength = pattern.Length;
            int[] numberChars = new int[allNumberSymbols];

            for (int i = 0; i < allNumberSymbols; i++)
                numberChars[i] = -1;

            for (int i = 0; i < patternLength; i++)
                numberChars[(int)pattern[i]] = i;

            return numberChars;
        }

        /// <summary>
        /// Реализация Алгоритма Рабина-Карпа
        /// </summary>
        public void AlgorithmRabinCarp(string pattern)
        {
            // Главный номер
            int ratio = 101;

            int patternLength = pattern.Length;
            int textLength = Text.Length;

            int hashPattern = 0;
            int hashText = 0;

            int h = 1;

            for (int i = 0; i < patternLength - 1; i++)
                h = (h * allNumberSymbols) % ratio;

            for (int i = 0; i < patternLength; i++)
            {
                hashPattern = (allNumberSymbols * hashPattern + pattern[i]) % ratio;    // Хэшируем подстроку (шиблон) в число (которое потом будем сравнивать)
                hashText = (allNumberSymbols * hashText + Text[i]) % ratio;             // Хэшируем первые символы из текста для дальнейшего сравнения
            }

            // Идём до индекса текста, когда уже не будет подстроки
            for (int i = 0; i <= textLength - patternLength; i++)
            {
                // Если символы совпадают 
                if (hashPattern == hashText)
                {
                    int j = 0;

                    // Идём по циклу, пока не найдём разницу в символах
                    for (j = 0; j < patternLength; j++)
                        if (Text[i + j] != pattern[j])
                            break;

                    // Если все символы совпали
                    if (j == patternLength)
                        Console.WriteLine("Pattern found at index " + i);
                }

                // Если символы не совпали
                if (i < textLength - patternLength)
                {
                    // Вычисление следующего хэш-значения
                    hashText = (allNumberSymbols * (hashText - Text[i] * h) + Text[i + patternLength]) % ratio;
                    
                    // Преобразование в положительное число
                    if (hashText < 0)
                        hashText = (hashText + ratio);
                }
            }
        }
    }
}
