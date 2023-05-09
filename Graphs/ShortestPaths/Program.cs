using ShortestPaths.Enum;
using MethodsDirectedGraph;
using MethodsUndirectedGraph;

namespace ShortestPaths
{

    public class Program
    {
        static void Main(string[] args)
        {
            //WorkWithDirectedGraph();

            WorkWithUndirectedGraph();
        }

        static void WorkWithDirectedGraph()
        {
            var matrixAdjacency = ReadDataFromFile("ForDirectedGraph"); ;

            var methods = new DirectedGraph(matrixAdjacency, TypeConvertFromFile.ToArray);

            //methods.DijkstraAlgorithm("A", "D");
            //methods.BellmanFordAlgorithm("A");
            methods.FloydWarshallAlgorithm("A", "D");
        }
        
        static void WorkWithUndirectedGraph()
        {
            var fileMatrixIncidence = ReadDataFromFile("ForUndirectedGraph");


        }

        /// <summary>
        /// Чтение матрицы смежности/инцидентности с файла
        /// </summary>
        static FileStream ReadDataFromFile(string typeGraph = "ForDirectedGraph")
        {
            string path = @$"D:\dream\Algorithms\Graphs\ShortestPaths\{typeGraph}\matrixInput2.txt";
            //string path = @$"D:\workSpaceNU\primat\Algorithms\Graphs\ShortestPaths\{typeGraph}\matrixInput1.txt";

            return new FileStream(path, FileMode.OpenOrCreate);
        }
        
        /// <summary>
        /// Чтение данных графа с консоли
        /// </summary>
        static string ReadDataFromConsole()
        {
            // Example:
            // (0,3,5);(0,7,16);(1,4,18);(1,5,14);(1,6,16);(1,7,17);(2,3,10);(2,4,17);(4,0,11);(4,1,18);(4,7,19);(5,2,16);(5,4,15);(5,6,14);(5,7,18);(6,4,16);(6,5,19);(7,2,11)

            Console.Write("Please, write info about graph: ");
            return Console.ReadLine() ?? "";
        }
    }
}

// Поиск в ширину
// Найти в заданном графе кратчайшие пути из заданной вершины до всех остальных вершин с помощью поиска в ширину
// Найти в заданном графе количество и состав компонент связности с помощью поиска в ширину

//Поиск в глубину
// Найти в заданном графе количество и состав компонент связности с помощью поиска в глубину
// Найти в заданном орграфе количество и состав сильно связных компонент с помощью поиска в глубину.

// Кратчайшие пути из одной вершины
// Реализовать алгоритм Дейкстры поиска кратчайших путей из одной вершины, используя в качестве приоритетной очереди обычный массив
// Реализовать алгоритм Беллмана-Форда поиска кратчайших путей из одной вершины