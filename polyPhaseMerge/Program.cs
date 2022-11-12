using System;
using System.IO;
using System.Text;

namespace polyPhaseMerge
{
    class Program
    {
        static void CreateRuns(int s)
        {
            string path = @"D:\dream\C#\binarySearch\polyPhaseMerge\input.txt";
            string A = @"D:\dream\C#\binarySearch\polyPhaseMerge\output1.txt";
            string B = @"D:\dream\C#\binarySearch\polyPhaseMerge\output2.txt";
            if (File.Exists(path)) {
                // Поместим каждую строку в массив строк
                string[] linesA = File.ReadAllLines(path);
                string[] linesB = File.ReadAllLines(path);
                int[] currentFile = new int[linesA.Length + linesB.Length];
                //for (int i = 0; i < lines.Length; i++){}
                
                string[] differentA = linesA[0].Split(" ");
                string[] differentB = linesB[0].Split(" ");
                int[] numbersA = new int[differentA.Length];
                int[] numbersB = new int[differentB.Length];
                
                for (int j = 0; j < differentA.Length; j++)
                {
                    int.TryParse(differentA[j], out numbersA[j]);
                    int.TryParse(differentB[j], out numbersB[j]);
                }
                currentFile = numbersA;
                
                Array.Sort(numbersA);
                for (int i = 0; i < numbersA.Length; i++)
                {
                    File.WriteAllText(A, numbersA[i].ToString() + " ");
                    
                }
                if (currentFile == numbersA)
                    currentFile = numbersB;
                else
                    currentFile = numbersA;
            }
        }
        static void PolyPhaseMerge(int s)
        {
            string path = @"D:\dream\C#\binarySearch\polyPhaseMerge\input.txt";
            string A = @"D:\dream\C#\binarySearch\polyPhaseMerge\output1.txt";
            string B = @"D:\dream\C#\binarySearch\polyPhaseMerge\output2.txt";
            int size = s;
            string[] linesA = File.ReadAllLines(A);
            string[] linesB = File.ReadAllLines(B);
            string[] differentA = linesA[0].Split(" ");
            string[] differentB = linesA[0].Split(" ");

            int[] numbersA = new int[linesA.Length];
            int[] numbersB = new int[linesB.Length];
            
            int[] numbersC = new int[numbersA.Length+numbersB.Length];
            int[] numbersD = new int[numbersA.Length+numbersB.Length];
            int[] currentOutput = new int[numbersA.Length+numbersB.Length];

            if (File.Exists(B))
            {
                if (File.Exists(A))
                {
                    for (int j = 0; j < size; j++)
                    {
                        int.TryParse(differentA[j], out numbersA[j]);
                        int.TryParse(differentB[j], out numbersB[j]);
                    }
                    currentOutput = Merge(numbersA, numbersB);
                    if (currentOutput == numbersA)
                        currentOutput = numbersB;
                    else if (currentOutput == numbersB)
                        currentOutput = numbersA;
                    else if (currentOutput == numbersC)
                        currentOutput = numbersD;
                    else if (currentOutput == numbersD)
                        currentOutput = numbersC;
                }
                size *= 2;
                if (numbersA == numbersA)
                {
                    numbersA = numbersC;
                    numbersB = numbersD;
                    currentOutput = numbersA;
                }
                else
                {
                    numbersA = numbersA;
                    numbersB = numbersB;
                    currentOutput = numbersC;
                }
            }
        }
        static int[] Merge(int[] array1, int[] array2)
        {
            
            int a = 0, b = 0, k = 0;
            int[] arrayResult = new int[array1.Length + array2.Length];
            while (a < array1.Length && b < array2.Length)
                if (array1[a] <= array2[b])
                    arrayResult[k++] = array1[a++];
                else
                    arrayResult[k++] = array2[b++];
            return arrayResult;
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
            CreateRuns(40);
            //PolyPhaseMerge(ref array);
            //for (int i = 0; i < array.Length; i++)
            //{
            //    Console.WriteLine("Element " + (i + 1) + "; Number: " + array[i]);
            //}
        }
    }
}


//Array.ConvertAll(different[i].Split(), int.Parse);
//different[j].Select(x => int.Parse(x));
//File.WriteAllText(A, different[i]);
//Array.ConvertAll(different[i].Split(), int.Parse);
//Console.WriteLine(different[0] is string);

/*
string path = @"D:\dream\C#\binarySearch\polyPhaseMerge\input.txt";
char[] array = new char[s];
using (StreamReader reader = new StreamReader(path))
{
    reader.Read(array, 0, s);
    for (int i = 0; i < array.Length; i++)
    {
        Console.WriteLine("Element: " + array[i]);
    }
}*/