using Graphs;

namespace ShortestPaths {

    public class Program
    {
        static void Main(string[] args)
        {
            //ReadDataFromFile();

            ReadDataFromConsole();
        }

        /// <summary>
        /// Чтение матрицы смежности/индицентности с файла
        /// </summary>
        static void ReadDataFromFile()
        {
            string path = @"D:\dream\Algorithms\Graphs\ShortestPaths\matrixInput2.txt";
            //string path = @"D:\workSpaceNU\primat\Algorithms\Graphs\ShortestPaths\matrixInput1.txt";
            var matrixAdjacency = new FileStream(path, FileMode.OpenOrCreate);

            var methods = new MethodsShortestPaths(matrixAdjacency, TypeConvertFromFile.ToArray);

            //methods.DijkstraAlgorithm("A", "D");
            //methods.BellmanFordAlgorithm("A");
            methods.FloydWarshallAlgorithm("A", "D");
        }
        
        /// <summary>
        /// Чтение данных графа с консоли
        /// </summary>
        static void ReadDataFromConsole()
        {
            // Example:
            // (0,3,5);(0,7,16);(1,4,18);(1,5,14);(1,6,16);(1,7,17);(2,3,10);(2,4,17);(4,0,11);(4,1,18);(4,7,19);(5,2,16);(5,4,15);(5,6,14);(5,7,18);(6,4,16);(6,5,19);(7,2,11)

            Console.Write("Please, write info about graph: ");
            string data = Console.ReadLine() ?? "";

            var methods = new MethodsShortestPaths(data);

            //methods.DijkstraAlgorithm("A", "D");
            //methods.BellmanFordAlgorithm("A");
            //methods.FloydWarshallAlgorithm("A", "D");
        }
    }
}

// Кратчайшие пути из одной вершины
// Реализовать алгоритм Дейкстры поиска кратчайших путей из одной вершины, используя в качестве приоритетной очереди обычный массив
// Реализовать алгоритм Беллмана-Форда поиска кратчайших путей из одной вершины