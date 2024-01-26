using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static int binarySearch(int[] array, int num)
    {
        int low = 0;
        int high = array.Length;
        int numeric = 0;
        int mid;
            
        while (low <= high)
        {
            mid = (low + high) / 2;
                
            numeric++;
            Console.WriteLine("Numeric: " + numeric + "; Middle: " + mid);
            if (array[mid] == num)
            {
                return mid;
            }

            if (array[mid] < num)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }
            
        return 0;
    }
        
    static void Main(string[] args)
    {
        int numeric = 50;
        int[] array = new int [numeric];
        for (int i = 0; i < numeric; i++)
        {
            array[i] = i;
        }

        Console.WriteLine(binarySearch(array, 1));
    }
}

namespace binarySearch
{
}