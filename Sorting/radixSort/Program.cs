using System;
using System.Globalization;

namespace radixSort
{
    class Program
    {
        static int MaxVal(int[] array, int size)
        {
            var maxVal = array[0];
            for (int i = 1; i < size; i++)
                if (array[i] > maxVal)
                    maxVal = array[i];
            return maxVal;
        }
        static int[] RadixSort (int[] array, int size)
        {
            var maxVal = MaxVal(array, size);
            for (int exponent = 1; maxVal / exponent > 0; exponent *= 10)
                SplitingByIndexes(array, size, exponent);
            return array;
        }
        static void SplitingByIndexes(int[] array, int size, int exponent)
        {
            var arrayResult = new int[size];
            var remains = new int[10];
            for (int i = 0; i < 10; i++)
                remains[i] = 0;
            for (int i = 0; i < size; i++)
                remains[(array[i] / exponent) % 10]++;
            for (int i = 1; i < 10; i++)
                //прибавляем мы чтобы ориентироваться какие числам в каким диапазонах будут стоять
                //то есть если у нулевого индекса значение 4, то значит все числа заканчивающиеся на 0 будут до 3 индекса (с 0го по 3ий)
                // если у 9 индекса значение 20 то значит цифры оканчивающиеся на 9 будут до 19 символа(туда входят и числа до девятки включительно)
                remains[i] += remains[i - 1];
            for (int i = size - 1; i >= 0; i--)
            {
                arrayResult[remains[(array[i] / exponent) % 10] - 1] = array[i];
                remains[(array[i] / exponent) % 10]--;
            }
            for (int i = 0; i < size; i++)
                array[i] = arrayResult[i];
        }

        static void Main(string[] args)
        {
            const int numbers = 20;
            var rand = new Random();
            int[] array = new int[numbers];
            for (int i = 0; i < numbers; i++)
            {
                array[i] = rand.Next(1001);
            }

            int[] arrayNew = RadixSort(array, array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("Element " + (i + 1) + "; Number: " + arrayNew[i]);
            }
        }
    }
}

