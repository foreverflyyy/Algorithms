using System;

namespace selectionSort{
    class Program
    {
        static void SelectionSort(ref int[] array)
        {
            for (int i = 0; i < array.Length-1; i++)
            {
                int numMin = i;
                for (int j = i+1; j < array.Length; j++)
                {
                    if (array[j] < array[numMin])
                    {
                        numMin = j;
                    }
                }
                if (numMin != i)
                {
                    int swap = array[i];
                    array[i] = array[numMin];
                    array[numMin] = swap;
                }
            }
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
            
            SelectionSort(ref array);
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("Element " + i + "; Number: " + array[i]);
            }
        }
    }
}

// Луший - O(n^2)
// Средний - O(n^2)
// Худший - O(n^2)