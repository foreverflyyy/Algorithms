using Graphs;

namespace BreadthAndDepth_FirstSearch
{

    class Program
    {
        static void Main()
        {
            // Берём файл для чтения матрицы
            var matrixAdjacency = new FileStream("matrixInput3.txt", FileMode.OpenOrCreate);

            // Создаём граф, по которому будем выполнять обход
            var graph = MethodsForSearch<char>.CreateGraph(matrixAdjacency);
            var methods = new MethodsForSearch<char>(graph);

            matrixAdjacency?.Close();

            char startNode = 'A';
            var dictWays = methods.ShortWaysToNodes(startNode, TypeSearch.BreadthFirstSearch);

            Console.WriteLine($"All ways from node - {startNode}");

            foreach(var node in dictWays)
                Console.WriteLine($"To node: {node.Key}, short road: {node.Value}");

            /*methods.ConnectivityComponent(TypeSearch.BreadthFirstSearch);
            Console.WriteLine();
            methods.ConnectivityComponent(TypeSearch.DepthFirstSearch);

            Console.WriteLine();
            methods.StrongConnectivityComponent();*/
        }
    }

}
// Исходный граф вводится с помощью матрицы смежности, записанной в некоторый файл с данными.
// Программа читает информацию из файла и реализует соответствующий алгоритм. Вывод результата записывается в итоговый файл.
// Матрица будет записана со значениями 1 и 0 (где 1 - есть соотвествующее ребро, и 0 - нету)

// Поиск в ширину
// Найти в заданном графе кратчайшие пути из заданной вершины до всех остальных вершин с помощью поиска в ширину
// Найти в заданном графе количество и состав компонент связности с помощью поиска в ширину

//Поиск в глубину
// Найти в заданном графе количество и состав компонент связности с помощью поиска в глубину
// Найти в заданном орграфе количество и состав сильно связных компонент с помощью поиска в глубину.



// typeof(T).Equals(typeof(Generator))