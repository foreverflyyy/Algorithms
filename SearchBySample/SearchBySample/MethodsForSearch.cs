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

        // Для перевода(кодировки ASIIC) char в int и наоборот
        private readonly int allNumberSymbols = 256;

        public MethodsForSearch(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Простой поиск образца в строке «методом грубой силы»
        /// </summary>
        /// <param name="sample"> Передаваемая строка образец </param>
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
        /// Реализация Алгоритма Кнута-Морриса-Пратта
        /// </summary>
        public void AlgorithmKMP(string pattern)
        {
            int M = pattern.Length;
            int N = Text.Length;

            // create lps[] that will hold the longest prefix suffix values for pattern
            int[] lps = new int[M];
            int j = 0; // index for pat[]

            // Preprocess the pattern (calculate lps[] array)
            ComputeLPSArray(pattern, M, lps);

            int i = 0; // index for txt[]
            while ((N - i) >= (M - j))
            {
                if (pattern[j] == Text[i])
                {
                    j++;
                    i++;
                }

                if (j == M)
                {
                    Console.WriteLine("Found pattern at index " + (i - j));
                    j = lps[j - 1];
                }

                // mismatch after j matches
                else if (i < N && pattern[j] != Text[i])
                {
                    // Do not match lps[0..lps[j-1]] characters, they will match anyway
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i = i + 1;
                }
            }
        }

        public void ComputeLPSArray(string pat, int M, int[] lps)
        {
            // length of the previous longest prefix suffix
            int len = 0;
            int i = 1;
            lps[0] = 0; // lps[0] is always 0

            // the loop calculates lps[i] for i = 1 to M-1
            while (i < M)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else // (pat[i] != pat[len])
                {
                    // This is tricky. Consider the example. AAACAAAA and i = 7. The idea is similar to search step.
                    if (len != 0)
                    {
                        len = lps[len - 1];

                        // Also, note that we do not increment i here
                    }
                    else // if (len == 0)
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Реализация Алгоритма Бойера Мура
        /// </summary>
        public void AlgorithmBoyerMoore(string pattern)
        {
            /* A pattern searching function that uses Bad Character Heuristic of Boyer Moore Algorithm */

            int m = pattern.Length;
            int n = Text.Length;

            int[] badchar = new int[allNumberSymbols];

            /* Fill the bad character array by calling the preprocessing function badCharHeuristic() for given pattern */
            BadCharHeuristic(pattern, m, badchar);

            // s is shift of the pattern with respect to text
            int s = 0; 
            
            while (s <= (n - m))
            {
                int j = m - 1;

                /* Keep reducing index j of pattern while characters of pattern and text are matching at this shift s */
                while (j >= 0 && pattern[j] == Text[s + j])
                    j--;

                /* If the pattern is present at current shift, then index j will become -1 after the above loop */
                if (j < 0)
                {
                    Console.WriteLine("Patterns occur at shift = " + s);

                    /* Shift the pattern so that the next character in text aligns with the last occurrence of it in pattern.
                    The condition s+m < n is necessary for the case when pattern occurs at the end of text */
                    s += (s + m < n) ? m - badchar[Text[s + m]] : 1;
                }

                else
                    /* Shift the pattern so that the bad character in text aligns with the last occurrence of it in pattern. The max function is used to
                    make sure that we get a positive shift. We may get a negative shift if the last occurrence of bad character in pattern
                    is on the right side of the current character. */
                    s += Max(1, j - badchar[Text[s + j]]);
            }
        }

        private int Max(int a, int b) { return (a > b) ? a : b; }

        private void BadCharHeuristic(string str, int size, int[] badchar)
        {
            int i;

            // Initialize all occurrences as -1
            for (i = 0; i < allNumberSymbols; i++)
                badchar[i] = -1;

            // Fill the actual value of last occurrence
            // of a character
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

            int p = 0; // hash value for pattern
            int t = 0; // hash value for txt

            int h = 1;

            // The value of h would be "pow(d, M-1)%q"
            for (i = 0; i < patternNums - 1; i++)
                h = (h * allNumberSymbols) % q;

            // Calculate the hash value of pattern and first window of text
            for (i = 0; i < patternNums; i++)
            {
                p = (allNumberSymbols * p + pattern[i]) % q;
                t = (allNumberSymbols * t + Text[i]) % q;
            }

            // Slide the pattern over text one by one
            for (i = 0; i <= textNums - patternNums; i++)
            {
                // Check the hash values of current window of text and pattern. If the hash
                // values match then only check for characters one by one
                if (p == t)
                {
                    /* Check for characters one by one */
                    for (j = 0; j < patternNums; j++)
                    {
                        if (Text[i + j] != pattern[j])
                            break;
                    }

                    // if p == t and pat[0...M-1] = txt[i, i+1, ...i+M-1]
                    if (j == patternNums)
                        Console.WriteLine(
                            "Pattern found at index " + i);
                }

                // Calculate hash value for next window of text:
                // Remove leading digit, add trailing digit
                if (i < textNums - patternNums)
                {
                    t = (allNumberSymbols * (t - Text[i] * h) + Text[i + patternNums]) % q;

                    // We might get negative value of t,
                    // converting it to positive
                    if (t < 0)
                        t = (t + q);
                }
            }
        }

        /// <summary>
        /// Реализация поиска по образцу с помощью конечного автомата
        /// </summary>
        public void FiniteStateMachine(string pattern)
        {
            /* Prints all occurrences of pat in txt */
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
    }
}
