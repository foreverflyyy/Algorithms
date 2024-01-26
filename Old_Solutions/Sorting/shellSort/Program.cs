using System;

namespace shellSort
{
    class Program
    {
        static void ShellSort(ref int[] array)
        {
            int h = array.Length / 2;
            while (h > 0)
            {
                for (int i = h; i < array.Length; i++)
                {
                    int key = array[i];
                    int j = i - h;
                    while (j >= 0 && key < array[j])
                    {
                        array[j + h] = array[j];
                        j -= h;
                    }
                    array[j + h] = key;
                }
                h = h / 2;
            }
        }

        static void Main(string[] args)
        {
            const int numbers = 50;
            var rand = new Random();
            int[] array = new int[numbers];
            for (int i = 0; i < numbers; i++)
            {
                array[i] = rand.Next(101);
            }
            
            ShellSort(ref array);
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("Element " + (i + 1) + "; Number: " + array[i]);
            }
        }
    }
}

// Луший - O(nlog^2n)
// Худший - O(n^2)