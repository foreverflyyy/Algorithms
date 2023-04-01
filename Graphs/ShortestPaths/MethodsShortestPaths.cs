using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class MethodsShortestPaths<T>
    {
        private Dictionary<T, Dictionary<T, double>> Graph = new Dictionary<T, Dictionary<T, double>>();  // Хэш-таблица с узлами и их ребрами с весами
        private Dictionary<T, double> Costs = new Dictionary<T, double>();    // Хэш-таблица со стоимостью всех узлов
        private Dictionary<T, T> Parents = new Dictionary<T, T>();      // Хэш-таблица с родителями узлов (1 - узел, 2 - его родитель)

        private List<T> checkedNodes = new List<T>(); // Список проверенных узлов
        
        private const double Infinity = Double.PositiveInfinity;

        public MethodsShortestPaths(Dictionary<T, Dictionary<T, double>> graph)
        {
            Graph = graph;
        }

        /// <summary>
        /// Создание графа из матрицы смежности
        /// </summary>
        /// <param name="matrixAdjacency"> Передаваемая матрица смежности </param>
        /// <returns></returns>
        static public Dictionary<char, Dictionary<char, double>> CreateGraphFrom(FileStream matrixAdjacency)
        {
            var graph = new Dictionary<char, Dictionary<char, double>>();

            // выделяем массив для считывания данных из файла
            byte[] buffer = new byte[matrixAdjacency.Length];
            // считываем данные
            matrixAdjacency.Read(buffer, 0, buffer.Length);
            // декодируем байты в строку (получаем строку)
            string matrixFromFile = Encoding.Default.GetString(buffer);

            string[] rowsMatrix = new string[matrixFromFile.Length];

            if (matrixFromFile.Contains("\r"))
                rowsMatrix = matrixFromFile.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            else
                rowsMatrix = matrixFromFile.Split(new string[] { "\n" }, StringSplitOptions.None);

            // Работаем с каждым узлом графа в отдельности
            for (int i = 0; i < rowsMatrix.Length; i++)
            {
                var valuesLine = rowsMatrix[i].Split(new char[] { ' ' });

                // Создаём новый узел и добавляем в него связанные узлы
                graph.Add(GetNeedSymbol(i), new Dictionary<char, double>() { });

                for (int j = 0; j < valuesLine.Length; j++)
                {
                    if (valuesLine[j] == "oo")
                        continue;

                    graph[GetNeedSymbol(i)].Add(GetNeedSymbol(j), Convert.ToDouble(valuesLine[j]));
                }
            }

            return graph;
        }
        
        /// <summary>
        /// Получение буквы по индексу алфавита
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        static public char GetNeedSymbol(int index)
        {
            string allSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return allSymbols[index];
        }

        /// <summary>
        /// Алгоритм Дейкстры
        /// </summary>
        public void AlgorithmDijkstra(T start, T end)
        {
            Parents.Clear();
            Costs.Clear();

            // Заполняем хэш-таблицу стоимости
            Costs[start] = 0;
            foreach(var node in Graph)
            {
                // если находим узел нашего начала, то записываем стоимость до соседних узлов
                if(node.Key?.ToString() == start?.ToString())
                {
                    foreach (var startNode in Graph[node.Key])
                        Costs[startNode.Key] = startNode.Value;
                }
                // до других узлов (не привязанных к начальному) ставим значение inf
                else
                {
                    if(!Costs.ContainsKey(node.Key))
                        Costs[node.Key] = Infinity;
                }
            }

            // Заполняем хэш-таблицу родителей
            Parents[start] = start;
            foreach (var node in Graph)
            {
                // если находим узел нашего начала, то записываем, что нач узел - это родитель привязанных узлов
                if (node.Key?.ToString() == start?.ToString())
                {
                    foreach (var startNode in Graph[node.Key])
                        Parents[startNode.Key] = node.Key;
                }
                else
                {
                    // до других узлов (не привязанных к начальному) ставим неопределенное значение
                    if (!Parents.ContainsKey(node.Key))
                        Parents[node.Key] = default;
                }
            }

            // находим наименьший узел из непроверенных
            var smallestNode = SearchLowestCostNode();

            // пока непроверенные узлы не закончатся
            while(smallestNode != null && smallestNode?.ToString() != default(T).ToString())
            {
                var cost = Costs[smallestNode];         // наименьшая стоимость
                var neighbours = Graph[smallestNode];   // соседи узла до которого наименьшая стоимость

                // просматриваем соседей наименьшего привязанного узла
                foreach(var node in neighbours.Keys)
                {
                    var newCost = cost + neighbours[node];

                    // если мы нашли меньшую стоимость до узла, то обновляем стоимость и родителя (путь до него)
                    if (Costs[node] > newCost)
                    {
                        Costs[node] = newCost;
                        Parents[node] = smallestNode;
                    }
                }

                // добавляем узел в список проверенных и ищем новый наименьший узел
                checkedNodes.Add(smallestNode); 
                smallestNode = SearchLowestCostNode();
            }

            /*// Через хэш-таблицу родителей можно обратный путь
            // из конечной точки до начальной
            var parentNode = Parents[end];

            while (parentNode?.ToString() != start?.ToString())
                parentNode = Parents[parentNode];*/

            Console.WriteLine("The shortest way equals: " + Costs[end]);
        }

        /// <summary>
        /// Найти узел с наименьшей стоимостью
        /// </summary>
        public T SearchLowestCostNode()
        {
            double lowestCost = Infinity;   // Наименьшее значение стоимости
            T lowestCostNode = default;     // Узел с наименьшей стоимостью

            // Проходим по всем непроверенным узлам
            foreach(var node in Costs)
            {
                // Смотрим стоимость до узла
                var cost = Costs[node.Key];

                // Если она меньше наименьшей, то обновляем данные
                if(cost < lowestCost && !checkedNodes.Contains(node.Key))
                {
                    lowestCost = cost;
                    lowestCostNode = node.Key;
                }
            }

            return lowestCostNode;
        }

        /// <summary>
        /// Алгорим Беллмана-Форда
        /// </summary>
        public void AlgorithmBellmanFord(T start)
        {
            Costs.Clear();

            // Заполняем хэш-таблицу стоимостями, стартовый узел - 0, остальные бесконечность
            Costs[start] = 0;

            foreach (var node in Graph)
                if(node.Key?.ToString() != start?.ToString())
                    Costs.Add(node.Key, Infinity);

            // Проходим ВСЕ ребра по n-1 раз, где n - количество вершин
            for(int i = 0; i < Graph.Count - 1; i++)
            {
                // Берём каждый узел и проходимся по каждому идущему от него ребру
                foreach(var node in Graph)
                {
                    foreach(var connectedNode in Graph[node.Key])
                    {
                        // Если мы нашли более короткий путь до привязанного узла(connectedNode), то обновляем его стоимость
                        // то есть стоимость node + стоимость от него до connectedNode оказалась меньше стоимости самой connectedNode
                        if (Costs[connectedNode.Key] > Costs[node.Key] + connectedNode.Value)
                            Costs[connectedNode.Key] = Costs[node.Key] + connectedNode.Value;
                    }
                }
            }

            ShowShortWays(start);

            // Проверка на присутствие в графе цикла отрицательной стоимости
            // Для этого проходим все ребра графа и проверяем, что нет более короткого пути для любой из вершин
            bool flag = false;

            foreach (var node in Graph)
                foreach (var connectedNode in Graph[node.Key])
                    if (Costs[connectedNode.Key] > Costs[node.Key] + connectedNode.Value)
                    {
                        Console.WriteLine("Graph contains a negative-weight cycle!!!");
                        Costs[connectedNode.Key] = Costs[node.Key] + connectedNode.Value;
                        flag = true;
                    }

            if(flag)
                ShowShortWays(start);
        }

        /// <summary>
        /// Вывод кратчайших путей до всех остадльных узлов
        /// </summary>
        /// <param name="start"> Node from we show to others nodes </param>
        public void ShowShortWays(T start)
        {
            // Вывод кратчайших путей до всех остадльных узлов
            Console.WriteLine($"Short way from node {start}: ");

            foreach (var node in Costs)
                if (node.Key?.ToString() != start?.ToString())
                    Console.WriteLine($"to node {node.Key} - {node.Value}");
        }
    }
}
