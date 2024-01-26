using System;
using System.Diagnostics;

namespace polyPhaseMerge
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string filename = @"D:\dream\C#\binarySearch\polyPhaseMerge\input.txt";
            LargeFileGeneration(filename);
            Console.WriteLine("Before sort: ");
            OutputData(filename);
            DirectMerge dm = new DirectMerge(filename);
            dm.Sort();
            Console.WriteLine("After sort: ");
            OutputData(filename);
            Console.ReadKey();
        }

        public static void LargeFileGeneration(string file)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(file, 36)))
            {
                Random rnd = new Random();
                for (int i = 0; i < 256; i++)
                {
                    bw.Write(rnd.Next(0, 2560));
                }
            }
        }

        public static void OutputData(string file) // вывод первых 100 чисел для проверки
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                long length = br.BaseStream.Length;
                long position = 0;
                for (int i = 0; i < 256; i++)
                {
                    if (position == length)
                    {
                        break;
                    }
                    else
                    {
                        Console.Write($"{br.ReadInt32()} ");
                        position += 4;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}


// для замеров
/*Stopwatch sw = new Stopwatch();
sw.Start();
sw.Stop();
Console.WriteLine($"Elapsed: {(double)sw.ElapsedMilliseconds / 1000} seconds");*/