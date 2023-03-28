using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphs
{
    public class MethodsShortestPaths<T>
    {
        private Dictionary<T, Dictionary<T, double>> Graph = new Dictionary<T, Dictionary<T, double>>();  // Хэш-таблица с узлами и их ребрами с весами
        private Dictionary<T, double> Costs = new Dictionary<T, double>();    // Хэш-таблица со стоимостью всех узлов
        private List<T> checkedNodes = new List<T>(); // Список проверенных узлов

        // Список компонент связности
        public List<List<T>> Components = new List<List<T>>();

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
        static public Dictionary<char, Dictionary<char, double>> CreateGraph(FileStream matrixAdjacency)
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
        static public char GetNeedSymbol(int index)
        {
            string allSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return allSymbols[index];
        }

        /// <summary>
        /// Алгоритм Крускала для поиска минимального покрывающего (остовного) дерева
        /// O (M log N + N2) - сложность.
        /// </summary>
        public void AlgorithmKruskala()
        {
            // Создаем новый граф - который и будет минимальным остовным
            var underGraph = new List<Edge<T>>();  // Хэш-таблица с узлами и их ребрами с весами

            // Создаем множество, где будут храниться все ребра отсортированные по стоимости
            var allEdges = new List<Edge<T>>();

            foreach(var node in Graph)
                foreach(var connectedNode in node.Value)
                {
                    // Если ребра ещё нет в списке, то заносим его
                    if(!allEdges.Any(x => (x.FirstNode?.ToString() == node.Key?.ToString() && x.SecondNode?.ToString() == connectedNode.Key?.ToString())
                        || (x.FirstNode?.ToString() == connectedNode.Key?.ToString() && x.SecondNode?.ToString() == node.Key?.ToString())))
                    {
                        allEdges.Add(new Edge<T>() { FirstNode = node.Key, SecondNode = connectedNode.Key, Cost = connectedNode.Value });
                    }
                }

            allEdges.Sort(new EdgeComparer<T>());

            // Заносим каждую верщину как отдельное множество
            var nodesAsPlenty = new List<List<T>>();

            foreach (var node in Graph)
                nodesAsPlenty.Add(new List<T> { node.Key });

            // Проходимся по ребрам в порядке возрастания и проверяем, если оба узла ребра не находяться в одном множестве, то
            // добавляем в подграф и объединяем множества
            foreach(var edge in allEdges)
            {
                var firstPlenty = nodesAsPlenty.FirstOrDefault(x => x.Contains(edge.FirstNode));
                var secondPlenty = nodesAsPlenty.FirstOrDefault(x => x.Contains(edge.SecondNode));
                if(firstPlenty != secondPlenty)
                {
                    underGraph.Add(edge);
                    firstPlenty?.AddRange(secondPlenty);
                    nodesAsPlenty.Remove(secondPlenty);
                }
            }

            // Отображаем получившийся подграф:
            Console.WriteLine("Minimum spanning tree have edges:\n");

            foreach (var edge in underGraph)
                Console.Write($"{edge.FirstNode} - {edge.SecondNode}, ");

            Console.WriteLine();
        }

        /// <summary>
        /// Алгоритм Прима для поиска минимального покрывающего (остовного) дерева
        /// </summary>
        public void AlgorithmPrima()
        {
            checkedNodes.Clear();

            // Создаем новый граф - который и будет минимальным остовным
            var underGraph = new Dictionary<T, Dictionary<T, double>>();

            // Заносим в новый подграф любую вершину (в данном случае первую)
            var firstNode = Graph.First();
            underGraph.Add(Graph.First().Key, new Dictionary<T, double>());

            checkedNodes.Add(Graph.First().Key);

            // Заносим минимальные ребра в очередь с приоритетами (двоичная куча)
            // Приоритет вершины определяется значением, которое равно минимальному весу ребер вершины
            var priorityQueue = new PriorityQueue<T, double>();

            foreach(var node in Graph)
                if (!checkedNodes.Contains(node.Key))
                {
                    var minCostEdge = FindMinCostEdge(node.Value);
                    priorityQueue.Enqueue(node.Key, minCostEdge);
                }

            // Пока очередь не закончиться
            while(priorityQueue.Count != 0)
            {
                var minCount = priorityQueue.Dequeue();
            }
        }

        /// <summary>
        /// Поиск ребра с минимальным весом у вершины
        /// </summary>
        public double FindMinCostEdge(Dictionary<T, double> connectedNodes)
        {
            var minCostEdge = Infinity;

            foreach (var connectedNode in connectedNodes)
                if (minCostEdge > connectedNode.Value)
                    minCostEdge = connectedNode.Value;

            return minCostEdge;
        }
    }
    
    public class Edge<N>
    {
        public N FirstNode { get; set; }
        public N SecondNode { get; set; }
        public double Cost { get; set; }
    }

    public class EdgeComparer<N> : IComparer<Edge<N>>
    {
        public int Compare(Edge<N>? firstEdge, Edge<N>? secondEdge)
        {
            if (firstEdge is null || secondEdge is null)
                throw new ArgumentException("Некорректное значение параметра");
            return Convert.ToInt32(firstEdge.Cost - secondEdge.Cost);
        }

    }
}
