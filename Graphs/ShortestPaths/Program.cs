using Graphs;

namespace ShortestPaths {

    public class Program
    {
        static void Main(string[] args)
        {
            // Берём файл для чтения матрицы
            var matrixAdjacency = new FileStream("matrixInput5.txt", FileMode.OpenOrCreate);

            // Создаём граф, по которому будем выполнять обход
            var graph = MethodsShortestPaths<char>.CreateGraph(matrixAdjacency);
            var methods = new MethodsShortestPaths<char>(graph);
            
            //methods.AlgorithmDijkstra('A', 'D');

            methods.AlgorithmBellmanFord('A');
        }
    }
}

// Кратчайшие пути из одной вершины
// Реализовать алгоритм Дейкстры поиска кратчайших путей из одной вершины, используя в качестве приоритетной очереди обычный массив
// Реализовать алгоритм Беллмана-Форда поиска кратчайших путей из одной вершины