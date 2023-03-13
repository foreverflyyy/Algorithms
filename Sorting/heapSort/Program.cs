using System;

namespace heapSort
{
    class Program
    {
        static void HeapSort(int[] array)
        {
            int n = array.Length;

            //делим на два потому что у половины кучи точно есть потомки
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            for (int i=n-1; i>=0; i--)
            {
                // тут я как бы нахожу самый большой элемент в функции а потом тут вот меняю его на место
                int swap = array[0];
                array[0] = array[i];
                array[i] = swap;

                Heapify(array, i, 0);
            }
        }

        static void Heapify(int[] array, int n, int i)
        {
            int largest = i;
            
            // i - это родитель, а l and r - его потомки, левый и правый, умножаем
            //на 2 чтоб дойти до потомков как раз
            int l = 2*i + 1;
            int r = 2*i + 2;

            if (l < n && array[l] > array[largest])
                largest = l;

            if (r < n && array[r] > array[largest])
                largest = r;

            if (largest != i)
            {
                //меняем если вдруг родитель оказался меньше
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                //спускаемся ещё ниже по куче ищя чтоб родитель не был меньше потомков
                Heapify(array, n, largest);
            }
        }

        static void Main(string[] args)
        {
            const int numbers = 100;
            var rand = new Random();
            int[] array = new int[numbers];
            for (int i = 0; i < numbers; i++)
                array[i] = rand.Next(201);
            
            HeapSort(array);
            for (int i = 0; i < array.Length; i++)
                Console.WriteLine("Element " + (i + 1) + "; Number: " + array[i]);
        }
    }
}

// Луший - O(nlogn)
// Средний - O(nlogn)
// Худший - O(nlogn)