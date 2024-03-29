﻿using ShortestPaths.Enum;
using System.Text;
using Graph;

namespace MethodsUndirectedGraph
{
    /// <summary>
    /// Класс с методами для работы с неориентированным графом
    /// </summary>
    public class UndirectedGraph
    {
        // Хэш-таблица с узлами и их ребрами с весами
        private Dictionary<string, List<string>> DictionaryGraph = new Dictionary<string, List<string>>();
        private List<string> checkedNodes = new List<string>();
        
        private List<Node<string>> nodesList = new List<Node<string>>();
        private List<Node<string>> newUndirectedGraph = new List<Node<string>>();
        public List<Node<string>> Component = new List<Node<string>>();

        private readonly string ALL_SYMBOLS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public UndirectedGraph(FileStream file, TypeMatrixFile type)
        {
            if (type == TypeMatrixFile.Adjacency)
                GraphByMatrixAdjacency(file);
            else if (type == TypeMatrixFile.Incidence)
                GraphByMatrixIncidence(file);
        }

        public UndirectedGraph(string data)
        {
            if (data == "")
                throw new Exception("Please, write correct info about graph!");

            CreateGraphByLineInfo(data);
        }

        /// <summary>
        /// Создание графа из матрицы смежности
        /// </summary>
        public void GraphByMatrixAdjacency(FileStream file)
        {
            var rowsMatrix = GetArrayLineFromFile(file);

            // Работаем с каждым узлом графа в отдельности
            for (int i = 0; i < rowsMatrix.Length; i++)
            {
                var valuesLine = rowsMatrix[i].Split(new char[] { ' ' });

                // Создаём новый узел и добавляем в него связанные узлы
                DictionaryGraph.Add(ALL_SYMBOLS[i].ToString(), new List<string>() { });

                for (int j = 0; j < valuesLine.Length; j++)
                    if (Convert.ToInt32(valuesLine[j]) == 1)
                        DictionaryGraph[ALL_SYMBOLS[j].ToString()].Add(ALL_SYMBOLS[j].ToString());
            }
        }
        
        public void GraphByMatrixIncidence(FileStream file)
        {
            var rowsMatrix = GetArrayLineFromFile(file);

            // Работаем с каждым узлом графа в отдельности
            for (int i = 0; i < rowsMatrix.Length; i++)
            {
                var valuesLine = rowsMatrix[i].Split(new char[] { ' ' });

                // Создаём новый узел и добавляем в него связанные узлы
                DictionaryGraph.Add(ALL_SYMBOLS[i].ToString(), new List<string>() { });

                for (int j = 0; j < valuesLine.Length; j++)
                    if (Convert.ToInt32(valuesLine[j]) == 1)
                        DictionaryGraph[ALL_SYMBOLS[j].ToString()].Add(ALL_SYMBOLS[j].ToString());
            }
        }

        private string[] GetArrayLineFromFile(FileStream file)
        {
            // выделяем массив для считывания данных из файла
            byte[] buffer = new byte[file.Length];
            // считываем данные
            file.Read(buffer, 0, buffer.Length);
            // декодируем байты в строку (получаем строку)
            string matrixFromFile = Encoding.Default.GetString(buffer);

            string[] rowsMatrix;

            if (matrixFromFile.Contains("\r"))
                rowsMatrix = matrixFromFile.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            else
                rowsMatrix = matrixFromFile.Split(new string[] { "\n" }, StringSplitOptions.None);

            return rowsMatrix;
        }

        /// <summary>
        /// Создание графа по данным с строки
        /// </summary>
        private void CreateGraphByLineInfo(string data)
        {
            string[] infoAboutNodes = data.Split(new string[] { ";" }, StringSplitOptions.None);

            // Работаем с каждым узлом графа в отдельности
            for (int i = 0; i < infoAboutNodes.Length; i++)
            {
                // Удаляю первую скобку и последнюю
                string valuesLine = infoAboutNodes[i].Remove(infoAboutNodes[i].Length - 1, 1).Remove(0, 1);

                // Разбиваем на три числа (1е - начальная вершина, 2е - конечная вершина, 3е - вес)
                string[] infoAboutNode = valuesLine.Split(new string[] { "," }, StringSplitOptions.None);

                var nameStartNode = ALL_SYMBOLS[Convert.ToInt32(infoAboutNode[0])].ToString();
                var nameEndNode = ALL_SYMBOLS[Convert.ToInt32(infoAboutNode[1])].ToString();

                if (DictionaryGraph.ContainsKey(nameStartNode))
                    DictionaryGraph[nameStartNode].Add(nameEndNode);
                else
                    DictionaryGraph.Add(nameStartNode, new List<string>());
            }
        }

        /// <summary>
        /// Перевод из матрицы инцидентности в матрицу смежности
        /// </summary>
        public void TransferIncidenceToAdjacency()
        {
            /*var rowsMatrix = GetArrayLineFromFile(file);

            var defaultMatrix = new List<List<string>>();

            for (int i = 0; i < rowsMatrix.Length; i++)
                defaultMatrix.Add(rowsMatrix[i].Split(new char[] { ' ' }).ToList());

            var matrixAdjacency = new string[defaultMatrix.Count, defaultMatrix.Count];

            // Ищем по каждому столбцу символы "-1" - в какой узел приходит и "1" - из какого выходит
            // и на основе этого формируем матрицу смежности 
            for (int i = 0; i < defaultMatrix[0].Count; i++)
            {
                int numberRow = -1;
                int numberColumn = -1;

                if(typeGraph == TypeGraph.WeightedUndirectedGraph)
                {
                    for (int j = 0; j < defaultMatrix.Count; j++)
                    {
                        if (defaultMatrix[j][i] == "1" && numberRow == -1)
                            numberRow = j;
                        else if (defaultMatrix[j][i] == "1" && numberRow != -1)
                            numberColumn = j;
                        else if (defaultMatrix[j][i] == "-1")
                            numberColumn = j;
                    }

                    // Заполняем матрицу смежности по найденным координатам ряда и столбца, остальные значения присваиваем нуль
                    for (int j = 0; j < defaultMatrix.Count; j++)
                    {
                        if (j == numberColumn)
                        {
                            matrixAdjacency[numberRow, numberColumn] = "1";
                            matrixAdjacency[numberColumn, numberRow] = "1";
                        }
                        else if (matrixAdjacency[numberRow, j] == null)
                        {
                            matrixAdjacency[numberRow, j] = "0";
                            matrixAdjacency[j, numberRow] = "0";
                        }
                    }
                }
                else if(typeGraph == TypeGraph.WeightedOrientedGraph)
                {
                    for (int j = 0; j < defaultMatrix.Count; j++)
                    {
                        if (defaultMatrix[j][i] == "1")
                            numberRow = j;
                        else if (defaultMatrix[j][i] == "-1")
                            numberColumn = j;
                    }

                    // Заполняем матрицу смежности по найденным координатам ряда и столбца, остальные значения присваиваем нуль
                    for (int j = 0; j < defaultMatrix.Count; j++)
                    {
                        if (j == numberColumn)
                        {
                            matrixAdjacency[numberRow, numberColumn] = "1";
                        }
                        else if (matrixAdjacency[numberRow, j] == null)
                        {
                            matrixAdjacency[numberRow, j] = "0";
                        }
                    }
                }
            }

            string[] resultAdjacencyMatrix = new string[matrixAdjacency.GetLength(0)];

            // На основе полученной матрицы переводим её в массив строк с пробелами для записи в файл
            for (int i = 0; i < matrixAdjacency.GetLength(0); i++)
            {
                for (int j = 0; j < matrixAdjacency.GetLength(1); j++)
                {
                    if (j != matrixAdjacency.GetLength(1) - 1)
                        resultAdjacencyMatrix[i] += matrixAdjacency[i, j] + " ";
                    else
                        resultAdjacencyMatrix[i] += matrixAdjacency[i, j];
                }
            }

            string path = @"D:\dream\Algorithms\Graphs\BreadthAndDepth-FirstSearch\matrixInput8.txt";
            using (StreamWriter streamWrite = new StreamWriter(path))
            {
                foreach (var line in resultAdjacencyMatrix)
                    streamWrite.WriteLine(line);
            }*/

            // BUG: самое правое нижнее значение не отображается в полученном файле
        }

        /// <summary>
        /// Транспонирование матрицы
        /// </summary>
        static public List<List<N>> TranspositionMatrix<N>(List<List<N>> matrix)
        {
            List<List<N>> newMatrix = new List<List<N>>();

            for (int i = 0; i < matrix.First().Count; i++)
                    newMatrix.Add(new List<N>());

            for (int i = 0; i < matrix.First().Count; i++)
                for (int j = 0; j < matrix.Count; j++)
                    newMatrix[i].Add(matrix[j][i]);

            return newMatrix;
        }

        /// <summary>
        /// Поиск в ширину (поиск кратчайшего расстояния до узла)
        /// </summary>
        public int BFS(string start, string end)
        {
            if (start?.ToString() == end?.ToString())
                return 0;

            var queue = new Queue<string>();
            List<string> checkedNode = new List<string>();
            var recervedQueue = new Queue<string>();

            // Заполняем первый уровень поиска
            var connectedNodes = DictionaryGraph[start];
            foreach (var node in connectedNodes)
                queue.Enqueue(node);

            int ways = 1;   // Счётчик пути

            while (queue.Count != 0 || recervedQueue.Count != 0)
            {
                // Если текущий уровень проверки закончился, переходим к следующему
                if (queue.Count == 0)
                {
                    while (recervedQueue.Count != 0)
                        queue.Enqueue(recervedQueue.Dequeue());
                    ways++;
                    recervedQueue.Clear();
                }

                var first = queue.Dequeue();

                // Проверка, а не смотрели ли мы уже этот узел
                if (checkedNode.Contains(first))
                    continue;

                // Добавление в список проверенных узлов
                checkedNode.Add(first);

                // Проверка соответствию нашей цели
                if (first?.ToString() == end?.ToString())
                    return ways;

                // Добавление следующего уровня поиска
                connectedNodes = DictionaryGraph[first].Where(x => !checkedNode.Contains(x)).ToList();

                foreach (var node in connectedNodes)
                    recervedQueue.Enqueue(node);
            }

            return 0;
        }
        
        /// <summary>
        /// Поиск в ширину (для отыскания компонент связности)
        /// </summary>
        public void BFS(Node<string> node)
        {
            var queue = new Queue<Node<string>>();

            // Заполняем первый уровень поиска
            foreach (var currentNode in node.ConnectedNodes)
                queue.Enqueue(currentNode);

            node.Visited = true;
            Component.Add(node);

            while (queue.Count != 0)
            {
                var first = queue.Dequeue();

                if (first.Visited)
                    continue;

                first.Visited= true;

                Component.Add(first);

                // Добавление следующего уровня поиска
                var connectedNodes = first.ConnectedNodes.Where(x => !x.Visited).ToList();

                foreach (var currentNode in connectedNodes)
                    queue.Enqueue(currentNode);
            }
        }

        /// <summary>
        /// Поиск в глубину (для отыскания компонент связности)
        /// </summary>
        public void DFS(Node<string> node)
        {
            node.Visited = true;
            Component.Add(node);

            for (int i = 0; i < node.ConnectedNodes.Count(); ++i)
            {
                var nextNode = node.ConnectedNodes[i];
                if (!nextNode.Visited)
                    DFS(nextNode);
            }
        }

        /// <summary>
        /// Поиск компонентов связности через поиск в ширину или поиск в глубину
        /// </summary>
        /// <param name="type"></param>
        #region Description
        /*
        Производим серию обходов: сначала запустим обход из первой вершины, и все вершины,
        которые он при этом обошёл — образуют первую компоненту связности. Затем найдём первую 
        из оставшихся вершин, которые ещё не были посещены, и запустим обход из неё, найдя тем 
        самым вторую компоненту связности. И так далее, пока все вершины не станут помеченными.

        Найти в нём все компоненты связности, т.е. разбить вершины графа на несколько групп так, 
        что внутри одной группы можно дойти от одной вершины до любой другой, а между разными группами — пути не существует.
        */
        #endregion
        public void ConnectivityComponent(TypeSearch type)
        {
            ConversationDictToListNodes();

            var checkListNodes = new List<Node<string>>();

            if(type == TypeSearch.StrongConnectivityComponent)
                checkListNodes = newUndirectedGraph;
            else
                checkListNodes = nodesList;
            
            for (int i = 0; i < checkListNodes.Count; ++i)
                checkListNodes[i].Visited = false;

            for (int i = 0; i < checkListNodes.Count; ++i)
                if (!checkListNodes[i].Visited)
                {
                    Component.Clear();

                    switch (type)
                    {
                        case TypeSearch.BreadthFirstSearch:
                            BFS(checkListNodes[i]);
                            break;
                        case TypeSearch.DepthFirstSearch:
                            DFS(checkListNodes[i]);
                            break;
                        case TypeSearch.StrongConnectivityComponent:
                            DFS(checkListNodes[i]);
                            break;
                    }

                    Console.WriteLine("Component: ");
                    for (int j = 0; j < Component.Count(); ++j)
                        Console.WriteLine($" {Component[j].Name}");
                }
        }

        public void StrongConnectivityComponent()
        {
            ConversationDictToListNodes();

            // Для каждой пары вершин определяем, являются ли они сильно связанные
            // это значит, что существует путь из первой вершины по вторую и из второй в первую
            var strongComponentList = new List<List<Node<string>>>();

            for(int i = 0; i < nodesList.Count(); i++)
            {
                for(int j = 0; j < nodesList.Count(); j++)
                {
                    if (i == j) continue;

                    var firstStrongComponent = BFS(nodesList[i].Name, nodesList[j].Name);
                    var secondStrongComponent = BFS(nodesList[j].Name, nodesList[i].Name);

                    if(firstStrongComponent != 0 && secondStrongComponent != 0)
                        strongComponentList.Add(new List<Node<string>> { nodesList[i], nodesList[j] });
                }
            }

            // на основе сильной связанности вершин строим неориентированный граф
            newUndirectedGraph = new List<Node<string>>();

            for (int i = 0; i < strongComponentList.Count(); i++)
            {
                // Берём первый элемент из списка - узел, к которому будут привязываться другие узлы
                var firstKey = strongComponentList[i].First();

                // Проверяем создавали мы уже этот ОСНОВНОЙ наш узел. Если да, то ищем другой основной узел для создания
                var currentMainNode = newUndirectedGraph.FirstOrDefault(x => x.Name?.ToString() == firstKey.Name?.ToString());

                if (currentMainNode == null)
                {
                    currentMainNode = new Node<string> { Name = firstKey.Name, ConnectedNodes = new List<Node<string>>() };
                    newUndirectedGraph.Add(currentMainNode);
                }

                // Добавляем к основному узлу все привязанные узлы
                for (int j = 0; j < strongComponentList.Count(); j++)
                {
                    // Если основные узлы совпадают, то добавляем привязанные к нему узлы
                    if (firstKey.Name?.ToString() == strongComponentList[j].First().Name?.ToString())
                    {
                        // Если раньше уже добавляли связанный узел, то проматываем
                        if (currentMainNode.ConnectedNodes.Any(x => x.Name?.ToString() == strongComponentList[j][1].Name?.ToString()))
                            continue;

                        // Проверка, был ли у нас уже создан узел, который нам нужно привязать
                        // если был, то просто добавляем, если нет, то создаём
                        var connectedNode = newUndirectedGraph.FirstOrDefault(x => x.Name?.ToString() == strongComponentList[j][1].Name?.ToString());

                        if (connectedNode == null)
                        {
                            var newNode = new Node<string> { Name = strongComponentList[j][1].Name, ConnectedNodes = new List<Node<string>>() };
                            newUndirectedGraph.Add(newNode);
                            currentMainNode?.ConnectedNodes.Add(newNode);
                        }
                        else
                        {
                            currentMainNode?.ConnectedNodes.Add(connectedNode);
                        }
                    }
                }
            }

            // компоненты связности построенного графа будут являться компонентами сильной связности орграфа
            ConnectivityComponent(TypeSearch.StrongConnectivityComponent);
        }

        /// <summary>
        /// Перевод из словаря в список с классом Node
        /// </summary>
        public void ConversationDictToListNodes()
        {
            foreach (var node in DictionaryGraph)
            {
                nodesList.Add(new Node<string>() { Name = node.Key, ConnectedNodes = new List<Node<string>>(), Visited = false });
            }

            foreach (var nodeDict in DictionaryGraph)
            {
                var listNodes = nodeDict.Value;
                var needNode = nodesList.FirstOrDefault(x => x.Name?.ToString() == nodeDict.Key?.ToString());
                var choiceNodes = new List<Node<string>>();

                foreach (var linkNode in listNodes)
                {
                    var oneOfNode = nodesList.FirstOrDefault(x => x.Name?.ToString() == linkNode?.ToString());
                    choiceNodes.Add(oneOfNode);
                }

                needNode.ConnectedNodes.AddRange(choiceNodes);
            }
        }

        /// <summary>
        /// Поиск количества путей ко всем узлам через заданный поиск.
        /// </summary>
        /// <param name="start"> Стартовый узел </param>
        /// <returns></returns>
        public Dictionary<string, int> ShortWaysToNodes(string start, TypeSearch type)
        {
            var allWays = new Dictionary<string, int>();

            foreach (var node in DictionaryGraph)
            {
                var nameNode = node.Key;
                if (start?.ToString() == nameNode?.ToString())
                    continue;

                int result = 0;

                switch (type)
                {
                    case TypeSearch.BreadthFirstSearch:
                        result = BFS(start, nameNode);
                        break;
                    case TypeSearch.DepthFirstSearch:
                        //result = DFS(start, nameNode);
                        break;
                }

                allWays.Add(nameNode, result);
            }

            return allWays;
        }
    }
}