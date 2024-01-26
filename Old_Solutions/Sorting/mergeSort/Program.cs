using System;

namespace mergeSort
{
    class Program
    {
        static int[] MergeSort(int[] array, int lower, int high)
        {
            if (lower < high)
            {
                int mid = lower + (high - lower) / 2;
                MergeSort(array, lower, mid);
                MergeSort(array, mid + 1, high);
                Merge(array, lower, mid, high);
            }
            return array;
        }
        static void Merge(int[] array, int lower, int mid, int high)
        {
            var leftLength = mid - lower + 1;
            var rightLength = high - mid;
            var leftArray = new int[leftLength];
            var rightArray = new int[rightLength];
            
            for (int i = 0; i < leftLength; ++i)
                leftArray[i] = array[lower + i];
            for (int j = 0; j < rightLength; ++j)
                rightArray[j] = array[mid + 1 + j];
            
            int a = 0;
            int b = 0;
            int k = lower;
            while (a < leftLength && b < rightLength)
            {
                if (leftArray[a] <= rightArray[b])
                {
                    array[k++] = leftArray[a++];
                }
                else
                {
                    array[k++] = rightArray[b++];
                }
            }
            while (a < leftLength)
            {
                array[k++] = leftArray[a++];
            }
            while (b < rightLength)
            {
                array[k++] = rightArray[b++];
            }
        }

        /*public void MergeSortNotRecursion(int[] array, int size)
        {
            int width = 1;
            while (width < size)
            {
                int i = 1;
                while (i <= size)
                {
                    //Merge(array, i, i+width-1);
                    //Merge(array, i+width, i+2*width-1);
                    i = i + 2 * width;
                }

                width *= 2;
            }
        }*/

        static void Main(string[] args)
        {
            const int numeric = 20;
            var random = new Random();
            int[] array = new int[numeric];
            for (int i = 0; i < numeric; i++)
            {
                array[i] = random.Next(101);
            }

            int[] arrayNew = MergeSort(array, 0, numeric-1);
            
            for (int i = 0; i < arrayNew.Length; i++)
            {
                Console.WriteLine("Element " + (i+1) + "; Number: " + arrayNew[i]);
            }
        }
    }
}

// Луший - O(nlogn)
// Средний - O(nlogn)
// Худший - O(nlogn)