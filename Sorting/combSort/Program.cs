using System;

namespace combSort
{
    class Program
    {
        static void CombSort(ref int[] array)
        {
            double gap = array.Length;
            bool sorted = true;
            while (sorted || gap>1)
            {
                gap /= 1.247330950103979;
                if (gap <= 1)
                {
                    gap = 1;
                }
                sorted = false;
                int i = 0;
                while (i + gap < array.Length)
                {
                    int iGap = i+(int)gap;
                    if (array[i] > array[iGap])
                    {
                        int swap = array[i];
                        array[i] = array[iGap];
                        array[iGap] = swap;
                        sorted = true;
                    }

                    i++;
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
                array[i] = rand.Next(101);
            }
            
            CombSort(ref array);
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("Element " + (i + 1) + "; Number: " + array[i]);
            }
        }
    }
}

// Луший - O(nlogn)
// Средний - 
// Худший - O(n^2)