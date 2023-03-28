using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class MethodsShortestPaths<T>
    {
        private Dictionary<T, Dictionary<T, double>> Graph = new Dictionary<T, Dictionary<T, double>>();  // Хэш-таблица с узлами и их ребрами с весами
        private Dictionary<T, double> Costs = new Dictionary<T, double>();    // Хэш-таблица со стоимостью всех узлов
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
        /// <param name="index"></param>
        /// <returns></returns>
        static public char GetNeedSymbol(int index)
        {
            string allSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return allSymbols[index];
        }

        /// <summary>
        /// Медленные Алгоритм Крускала для поиска минимального покрывающего (остовного) дерева
        /// O (M log N + N2) - сложность.
        /// </summary>
        public void MoreLongerAlgorithmKruskala()
        {
            // Подсчитаем количество ребер
            int allEdges = 0;

            foreach(var node in Graph)
                allEdges += node.Value.Count;

            allEdges /= 2;

            // Отсортированный список ребер с узлами
            var listWithEdges = new List<Dictionary<int, Dictionary<int, int>>>(allEdges);


            listWithEdges.Sort();

            int cost = 0; // вес
            var res = new List<Dictionary<int, int>>();

            // Список принадлежности вершины тому или иному дереву
            var treeId = new List<T>(Graph.Count);

            foreach(var node in Graph)
                treeId.Add(node.Key);

            /*for (int i = 0; i < allEdges; ++i)
            {
                int a = listWithEdges[i].second.first, b = listWithEdges[i].second.second, l = g[i].first;
                if (treeId[a].ToString() != treeId[b].ToString())
                {
                    cost += l;
                    res.Add(new Dictionary<int, int>(a, b));

                    var oldId = treeId[b];
                    var newId = treeId[a];

                    for (int j = 0; j < Graph.Count; ++j)
                        if (treeId[j] == oldId)
                            treeId[j] = newId;
                }
            }*/


        }

        /// <summary>
        /// Более быстрый Алгоритм Крускала через систему непересекающихся множеств для поиска минимального остовного дерева
        /// </summary>
        public void AlgorithmKruskala()
        {
            // Создаем новый граф со всеми вершинами основного графа (он и будет в итоге минимальным остовным)
            var underGraph = new T[Graph.Count];

            foreach (var node in Graph)
                underGraph.Append(node.Key);

            
        }

        /// <summary>
        /// Алгоритм Прима для поиска минимального покрывающего (остовного) дерева
        /// </summary>
        public void AlgorithmPrima()
        {

        }
    }
}
