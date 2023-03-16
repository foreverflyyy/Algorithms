using BreadthAndDepth_FirstSearch;
using System.Text;

namespace Graphs
{

    class Program
    {
        static void Main()
        {
            // Берём файл для чтения матрицы
            var matrixAdjacency = new FileStream("matrixInput.txt", FileMode.OpenOrCreate);

            // Создаём дерево, по которому будем выполнять обход
            Dictionary<char, List<char>> graph = CreateGraph(matrixAdjacency);
            matrixAdjacency?.Close();

            var methods = new MethodsForSearch<char>(graph);

            /*char startNode = 'A';
            var dictWays = methods.ShortWaysToNodes(startNode, TypeSearch.BreadthFirstSearch);

            Console.WriteLine($"All ways from node - {startNode}");

            foreach(var node in dictWays)
                Console.WriteLine($"To node: {node.Key}, short road: {node.Value}");*/

            methods.ConnectivityComponent(TypeSearch.BreadthFirstSearch);
            Console.WriteLine();
            methods.ConnectivityComponent(TypeSearch.DepthFirstSearch);
            Console.WriteLine();
            methods.StrongConnectivityComponent();
        }

        /// <summary>
        /// Создание графа из матрицы смежности
        /// </summary>
        /// <param name="matrixAdjacency"> Передаваемая матрица смежности </param>
        /// <returns></returns>
        static Dictionary<char, List<char>> CreateGraph(FileStream matrixAdjacency)
        {
            var allNodes = new Dictionary<char, List<char>>();

            // выделяем массив для считывания данных из файла
            byte[] buffer = new byte[matrixAdjacency.Length];
            // считываем данные
            matrixAdjacency.Read(buffer, 0, buffer.Length);
            // декодируем байты в строку (получаем строку)
            string matrixFromFile = Encoding.Default.GetString(buffer);

            string[] rowsMatrix = matrixFromFile.Split(new string[] { "\n" }, StringSplitOptions.None);

            // Работаем с каждым узлом графа в отдельности
            for(int i = 0; i < rowsMatrix.Length; i++)
            {
                var valuesLine = rowsMatrix[i].Split(new char[] { ' ' });
                var values = new int[valuesLine.Length];

                // Заполняем массив связанности узла значениями
                for (int j = 0; j < valuesLine.Length; j++)
                    values[j] = int.Parse(valuesLine[j]);

                // Создаём новый узел и добавляем в него связанные узлы
                allNodes.Add(GetNeedSymbol(i), new List<char>() { });

                for (int j = 0; j < valuesLine.Length; j++)
                    if (Convert.ToInt32(valuesLine[j]) == 1)
                        allNodes[GetNeedSymbol(i)].Add(GetNeedSymbol(j));
            }

            return allNodes;
        }

        /// <summary>
        /// Получение буквы по индексу алфавита
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        static char GetNeedSymbol(int index)
        {
            string allSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return allSymbols[index];
        }
    }

}
// Исходный граф вводится с помощью матрицы смежности, записанной в некоторый файл с данными.
// Программа читает информацию из файла и реализует соответствующий алгоритм. Вывод результата записывается в итоговый файл.
// Матрица будет записана со значениями 1 и 0 (где 1 - есть соотвествующее ребро, и 0 - нету)

// Поиск в ширину
// 1. найти в заданном графе кратчайшие пути из заданной вершины до всех остальных вершин с помощью поиска в ширину
// 2. найти в заданном графе количество и состав компонент связности с помощью поиска в ширину

//Поиск в глубину
// 4.	Найти в заданном графе количество и состав компонент связности с помощью поиска в глубину
// 5.	Найти в заданном орграфе количество и состав сильно связных компонент с помощью поиска в глубину.



// typeof(T).Equals(typeof(Generator))