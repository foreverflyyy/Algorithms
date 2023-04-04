using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

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
        public void SimpleSearch(string sample)
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
                        Console.Write($"Substring is finded. It starts with {(i + 1)} elements!");
                }
            }

            Console.Write($"Substring wasn't finded.");
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
        /// Реализация Алгоритма Бойера Мура (недоработанная)
        /// </summary>
        /// <param name="sample"> Передаваемая строка образец </param>
        /// <returns></returns>
        public void AlgorithmBoyerMoore_2(string sample)
        {
            // Создаём таблицу смещений и суффиксов
            var offset = CreateOffsetTable(sample);
            var suffix = CreateSuffixTable(sample);
            var suffix2 = CreateSuffixTable_2(sample);

            //всегда либо bound = 0, либо bound = m - suffshift[0]
            int j, bound = 0;

            int m = sample.Length;
            int n = Text.Length;

            for (int i = 0; i <= n - m; i += suffix[j + 1])
                for (j = m - 1; j >= bound && sample[j] == Text[i + j]; j--)
                {
                    if (j < bound)
                    {
                        Console.Write($"Substring is finded. It starts with {i} elements!");
                        bound = m - suffix[0];
                        j = -1; //установить j так, как будто мы прочитали весь шаблон s, а не только до границы bound
                    }
                    else
                        bound = 0;
                }

            Console.Write($"Substring wasn't finded.");

            /*int t = 0;
            int last = sample.Length - 1;

            while (t < Text.Length - last)
            {
                int p = last;

                while (p >= 0 && Text[t + p] == sample[p])
                    p--;

                if (p == -1)
                {
                    Console.Write($"Substring is finded. It starts with {t} elements!");
                    return;
                }

                // Возвращает большее из двух чисел
                t += Math.Max(p - offset[Text[t + p]], suffix[p + 1]);
            }

            Console.Write($"Substring wasn't finded.");*/
        }

        /// <summary>
        /// Построение таблицы смещений
        /// </summary>
        public Dictionary<char, int> CreateOffsetTable(string pattern)
        {
            // количество символов зависит от алфавита с которым мы работаем
            var offset = new Dictionary<char, int>();

            // Заполняем таблицу смещений
            // Заполняем каждого символа его последнее упоминание в образце (элементы считаются с позиции 1)
            for (int i = 0; i < pattern.Length - 1; i++)
            {
                if (!offset.TryAdd(pattern[i], i + 1))
                    offset[pattern[i]] = i + 1;
            }

            return offset;
        }

        /// <summary>
        /// Построение таблицы суффиксов
        /// </summary>
        public List<int> CreateSuffixTable(string pattern)
        {
            int m = pattern.Length;
            List<int> suffshift = new List<int>(m + 1);

            for (int i = 0; i < m + 1; i++)
                suffshift.Add(m);

            List<int> z = new List<int>(m);

            for (int i = 0; i < m; i++)
                z.Add(0);

            for (int j = 1, maxZidx = 0, maxZ = 0; j < m; ++j)
            {
                if (j <= maxZ) 
                    z[j] = Math.Min(maxZ - j + 1, z[j - maxZidx]);

                while (j + z[j] < m && pattern[m - 1 - z[j]] == pattern[m - 1 - (j + z[j])]) 
                    z[j]++;

                if (j + z[j] - 1 > maxZ)
                {
                    maxZidx = j;
                    maxZ = j + z[j] - 1;
                }
            }

            // Первый цикл
            for (int j = m - 1; j > 0; j--) 
                suffshift[m - z[j]] = j;

            // Второй цикл
            for (int j = 1, r = 0; j <= m - 1; j++)
                if (j + z[j] == m)
                    for (; r <= j; r++)
                        if (suffshift[r] == m) suffshift[r] = j;

            return suffshift;
        }

        /// <summary>
        /// Построение таблицы суффиксов
        /// </summary>
        public List<int> CreateSuffixTable_2(string pattern)
        {
            var suffix = new List<int>(pattern.Length + 1);

            for (int i = 0; i < pattern.Length + 1; i++)
                suffix.Add(pattern.Length);

            suffix[pattern.Length] = 1;

            for(int i = pattern.Length - 1; i >= 0; i--)
                for (int at = i; at < pattern.Length; at++)
                {
                    string s = pattern.Substring(at);

                    for (int j = at - 1; j >= 0; j--)
                    {
                        string p = pattern.Substring(j, s.Length);
                        if (p == s)
                        {
                            suffix[i] = at - 1;
                            at = pattern.Length;
                            break;
                        }
                    }
                }

            return suffix;
        }

        /// <summary>
        /// Реализация Алгоритма Бойера Мура
        /// </summary>
        public void AlgorithmBoyerMoore(string pattern)
        {
            // s is shift of the pattern 
            // with respect to text
            int s = 0, j;
            int m = pattern.Length;
            int n = Text.Length;

            int[] bpos = new int[m + 1];
            int[] shift = new int[m + 1];

            // initialize all occurrence of shift to 0
            for (int i = 0; i < m + 1; i++)
                shift[i] = 0;

            // do preprocessing
            preprocess_strong_suffix(shift, bpos, pattern.ToCharArray(), m);
            preprocess_case2(shift, bpos, pattern.ToCharArray(), m);

            while (s <= n - m)
            {
                j = m - 1;

                /* Keep reducing index j of pattern while 
                characters of pattern and text are matching 
                at this shift s*/
                while (j >= 0 && pattern[j] == Text[s + j])
                    j--;

                /* If the pattern is present at the current shift, 
                then index j will become -1 after the above loop */
                if (j < 0)
                {
                    Console.Write("pattern occurs at shift = {0}\n", s);
                    s += shift[0];
                }
                else

                    /*pat[i] != pat[s+j] so shift the pattern
                    shift[j+1] times */
                    s += shift[j + 1];
            }
        }

        // preprocessing for strong good suffix rule
        private void preprocess_strong_suffix(int[] shift, int[] bpos, char[] pat, int m)
        {
            // m is the length of pattern 
            int i = m, j = m + 1;
            bpos[i] = j;

            while (i > 0)
            {
                /*if character at position i-1 is not 
                equivalent to character at j-1, then 
                continue searching to right of the
                pattern for border */
                while (j <= m && pat[i - 1] != pat[j - 1])
                {
                    /* the character preceding the occurrence of t 
                    in pattern P is different than the mismatching 
                    character in P, we stop skipping the occurrences 
                    and shift the pattern from i to j */
                    if (shift[j] == 0)
                        shift[j] = j - i;

                    //Update the position of next border 
                    j = bpos[j];
                }
                /* p[i-1] matched with p[j-1], border is found.
                store the beginning position of border */
                i--; j--;
                bpos[i] = j;
            }
        }

        //Preprocessing for case 2
        private void preprocess_case2(int[] shift, int[] bpos, char[] pat, int m)
        {
            int i, j;
            j = bpos[0];
            for (i = 0; i <= m; i++)
            {
                /* set the border position of the first character 
                of the pattern to all indices in array shift
                having shift[i] = 0 */
                if (shift[i] == 0)
                    shift[i] = j;

                /* suffix becomes shorter than bpos[0], 
                use the position of next widest border
                as value of j */
                if (i == j)
                    j = bpos[j];
            }
        }

        /// <summary>
        /// Реализация Алгоритма Рабина-Карпа
        /// </summary>
        public void AlgorithmRabinCarp(string pattern)
        {
            
        }
    }
}
