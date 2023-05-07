using Graphs;

namespace ShortestPaths {

    public class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\dream\Algorithms\Graphs\ShortestPaths\matrixInput1.txt";
            //string path = @"D:\workSpaceNU\primat\Algorithms\Graphs\ShortestPaths\matrixInput1.txt";
            var matrixAdjacency = new FileStream(path, FileMode.OpenOrCreate);

            var methods = new MethodsShortestPaths(matrixAdjacency, TypeConvertFromFile.ToDictionary);

            //methods.DijkstraAlgorithm("A", "D");
            //methods.BellmanFordAlgorithm("A");
        }
    }
}

// Кратчайшие пути из одной вершины
// Реализовать алгоритм Дейкстры поиска кратчайших путей из одной вершины, используя в качестве приоритетной очереди обычный массив
// Реализовать алгоритм Беллмана-Форда поиска кратчайших путей из одной вершины