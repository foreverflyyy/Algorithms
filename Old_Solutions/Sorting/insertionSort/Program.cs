using System;

namespace insertionSort
{
    class Program
    {
        static void InsertionSort(ref int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;
                while (j>=0 && key < array[j])
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }
        }

        static void Main(string[] args)
        {
            const int numbers = 15;
            var rand = new Random();
            int[] array = new int[numbers];
            for (int i = 0; i < numbers; i++)
            {
                array[i] = rand.Next(51);
            }
            
            InsertionSort(ref array);
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("Element " + (i + 1) + "; Number: " + array[i]);
            }
        }
    }
}

// Луший - O(n)
// Средний - O(n^2)
// Худший - O(n^2)