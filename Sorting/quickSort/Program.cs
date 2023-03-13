using System;

namespace quickSort
{
    class Program
    {
        static void QuickSort(ref int[] array, int lower, int high)
        {
            if (lower >= 0 && high >= 1 && lower < high)
            {
                int p = Partition(ref array, lower, high);
                QuickSort(ref array, lower, p);
                QuickSort(ref array, p+1, high);
            }
        }
        static int Partition(ref int[] array, int lower, int high)
        {
            int pivot = array[(lower + high) / 2];
            int i = lower - 1;
            int j = high+1;
            
            while (true)
            {
                do
                {
                    i = i + 1;
                } while (array[i] < pivot);

                do
                {
                    j = j - 1;
                } while (array[j] > pivot);

                if (i >= j)
                {
                    return j;
                }
                int swap = array[i];
                array[i] = array[j];
                array[j] = swap;
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
            
            QuickSort(ref array, 0, array.Length-1);
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("Element " + (i + 1) + "; Number: " + array[i]);
            }
        }
    }
}

// Луший - O(nlogn)
// Средний - O(nlogn)
// Худший - O(n^2)