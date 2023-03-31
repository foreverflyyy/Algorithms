using System.Text;

namespace Graphs
{
    public class MethodsShortestPaths<T>
    {
        // Хэш-таблица с узлами и их ребрами с весами
        private Dictionary<T, Dictionary<T, double>> Graph = new Dictionary<T, Dictionary<T, double>>();

        // Список проверенных узлов
        private List<T> checkedNodes = new List<T>();

        // Список вершин в текущей компоненте связности
        public Dictionary<T, T> Component = new Dictionary<T, T>();

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
                    if (valuesLine[j] == "oo") // латиница
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
        /// Эйлеров цикл (ДОРЕАЛИЗОВАТЬ ПОПЫТКУ ДОБАВЛЕНИЯ В СЛОВАРЬ ПО ЕЩЁ ОДНОМУ КЛЮЧУ)
        /// </summary>
        public void EulerianPath()
        {
            // Проверка степени вершин:
            // если вершин с нечётной степенью нет, то в графе есть эйлеров цикл
            // если есть 2 вершины с нечётной степенью, то в графе есть только эйлеров путь (эйлерова цикла нет)
            // если же таких вершин больше 2, то в графе нет ни эйлерова цикла, ни эйлерова пути

            var nodesWithEvenCount = new List<T>();
            var nodesWithUnevenCount = new List<T>();
            
            foreach (var node in Graph.Keys)
            {
                if (Graph[node].Count == 0 || Graph[node].Count % 2 == 0)
                    nodesWithEvenCount.Add(node);
                else if(Graph[node].Count % 2 == 1)
                    nodesWithUnevenCount.Add(node);
            }

            bool haveEulerianCicle = true;

            if (nodesWithUnevenCount.Count == 0)
            {
                Console.WriteLine("We have eulerian cicle!");
            }
            else if(nodesWithUnevenCount.Count == 2)
            {
                Console.WriteLine("We have only eulerian path!");
                haveEulerianCicle = false;
            }
            else
            {
                Console.WriteLine("We don't have eulerian cicle and eulerian path!");
                return;
            }

            // Чтобы найти эйлеров путь (не цикл), поступим таким образом: если V1 и V2 - это две вершины нечётной степени,
            // то просто добавим ребро (V1,V2), в полученном графе найдём эйлеров цикл , а затем удалим из ответа "фиктивное" ребро (V1,V2)
            if (!haveEulerianCicle)
            {
                Graph[nodesWithUnevenCount[0]].Add(nodesWithUnevenCount[1], 1);
                Graph[nodesWithUnevenCount[1]].Add(nodesWithUnevenCount[0], 1);
            }

            // Поиск Эйлерова Цикла:
            var answerList = new List<T>();

            // Создаём стэк и заполняем его первой вершиной
            var stack = new Stack<T>();
            stack.Push(Graph.First().Key);

            while (stack.Count != 0)
            {
                var node = stack.Peek();

                // Если у вершины степень равна нулю (нет ребер исходящих от неё)
                if (Graph[node].Values.Count == 0)
                {
                    // Добавляем в ответ и удаляем со стэка
                    answerList.Add(node);
                    stack.Pop();
                }
                else
                {
                    // Если есть исходящие ребра, то берём первый попавшийся и удаляем его с графа, а конец ребра записываем в стэк
                    var connectedNode = Graph[node].First().Key;
                    Graph[node].Remove(connectedNode);
                    Graph[connectedNode].Remove(node);
                    stack.Push(connectedNode);
                }
            }

            // Проверка связный граф или нет (если граф был не связный, то в графе останутся некоторые рёбра)
            foreach (var node in Graph.Keys)
            {
                if (Graph[node].Keys.Count != 0)
                {
                    Console.WriteLine("Graph isn't connected");
                    break;
                }
            }

            // Если граф имел только эйлеров путь, то удалим ранее созданное фиктивое ребро
            if (!haveEulerianCicle)
                for(int i = 0; i < answerList.Count; i++)
                    if ((answerList[i]?.ToString() == nodesWithUnevenCount[1]?.ToString() && answerList[i + 1]?.ToString() == nodesWithUnevenCount[0]?.ToString())
                        || (answerList[i + 1]?.ToString() == nodesWithUnevenCount[1]?.ToString() && answerList[i]?.ToString() == nodesWithUnevenCount[0]?.ToString()))
                    {
                        answerList.Remove(answerList[i]);
                        answerList.Remove(answerList[i+1]);
                        break;
                    }

            // Отображаем найденный эйлеров граф (либо цикл, либо путь):
            if (haveEulerianCicle)
                Console.WriteLine("\nEulerian Cicle:\n");
            else
                Console.WriteLine("\nEulerian Path (Cicle):\n");

            foreach (var node in answerList)
                Console.Write($"{node} - ");
        }

        /// <summary>
        /// Эйлеров цикл недоделанный
        /// </summary>
        public void EulerianPathUnfinished()
        {
            var singleCicle = new List<T>();

            FindAllCycles(Graph.First().Key, singleCicle);

            // Отображаем получившийся подграф:
            Console.WriteLine("\nEulerian Path (Cicle):\n");

            foreach (var node in singleCicle)
                Console.Write($"{node} - ");
        }

        /// <summary>
        /// Поиск всех циклов внутри графа
        /// </summary>
        public void FindAllCycles(T startNode, List<T> singleCicle)
        {
            var cicle = new List<T>();

            // Находим одну компоненту связности
            DFS(startNode, startNode, out bool flag);

            // Удаляем вершины из графа
            foreach (var comp in Component)
            {
                Graph[comp.Key].Remove(comp.Value);
                Graph[comp.Value].Remove(comp.Key);
                cicle.Add(comp.Key);
            }

            cicle = cicle.Where(x => !singleCicle.Contains(x)).ToList();
            singleCicle.AddRange(cicle);

            Component.Clear();

            foreach (var node in cicle)
            {
                checkedNodes.Clear();
                FindAllCycles(node, singleCicle);
            }
        }

        /// <summary>
        /// Поиск в глубину (для отыскания компонент связности)
        /// </summary>
        public void DFS(T start, T node, out bool flag)
        {
            flag = false;
            checkedNodes.Add(node);

            foreach (var connectedNode in Graph[node].Keys)
            {
                if (connectedNode?.ToString() == start?.ToString())
                {
                    flag = true;
                    Component.Add(node, connectedNode);
                    return;
                }

                if (!checkedNodes.Contains(connectedNode))
                {
                    Component.Add(node, connectedNode);
                    DFS(start, connectedNode, out flag);

                    if (flag)
                        return;
                }
            }
        }

    }
    public class NodeAndConnectedNode<N>
    {
        public N Name { get; set; }
        public N ConnectedNode { get; set; }
    }
}