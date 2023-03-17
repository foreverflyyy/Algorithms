using BreadthAndDepth_FirstSearch;

namespace Graphs
{
    /// <summary>
    /// Класс с методами поиска в графе
    /// </summary>
    /// <typeparam name="T"> Тип данных, который будет у значения узла </typeparam>
    public class MethodsForSearch<T>
    {
        public Dictionary<T, List<T>> nodes = new Dictionary<T, List<T>>();
        public List<Node<T>> nodesList = new List<Node<T>>();
        public List<Node<T>> newUndirectedGraph = new List<Node<T>>();

        // Список вершин в текущей компоненте связности
        public List<Node<T>> Component = new List<Node<T>>(); 

        public MethodsForSearch(Dictionary<T, List<T>> nodes)
        {
            this.nodes = nodes;
            ConversationDictToListNodes();
        }

        /// <summary>
        /// Поиск в ширину (поиск кратчайшего расстояния до узла)
        /// </summary>
        /// <param name="start"> Стартовый узел </param>
        /// <param name="end"> Конечный узел </param>
        /// <returns></returns>
        public int BFS(T start, T end)
        {
            if (start?.ToString() == end?.ToString())
                return 0;

            var queue = new Queue<T>();
            List<T> checkedNode = new List<T>();
            var recervedQueue = new Queue<T>();

            // Заполняем первый уровень поиска
            var connectedNodes = nodes[start];
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
                connectedNodes = nodes[first].Where(x => !checkedNode.Contains(x)).ToList();

                foreach (var node in connectedNodes)
                    recervedQueue.Enqueue(node);
            }

            return 0;
        }
        
        /// <summary>
        /// Поиск в ширину (для отыскания компонент связности)
        /// </summary>
        public void BFS(Node<T> node)
        {
            var queue = new Queue<Node<T>>();

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
        public void DFS(Node<T> node)
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
            var checkListNodes = new List<Node<T>>();

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
            // Для каждой пары вершин определяем, являются ли они сильно связанные
            // это значит, что существует путь из первой вершины по вторую и из второй в первую
            var strongComponentList = new List<List<Node<T>>>();

            for(int i = 0; i < nodesList.Count(); i++)
            {
                for(int j = 0; j < nodesList.Count(); j++)
                {
                    if (i == j) continue;

                    var firstStrongComponent = BFS(nodesList[i].Name, nodesList[j].Name);
                    var secondStrongComponent = BFS(nodesList[j].Name, nodesList[i].Name);

                    if(firstStrongComponent != 0 && secondStrongComponent != 0)
                        strongComponentList.Add(new List<Node<T>> { nodesList[i], nodesList[j] });
                }
            }

            // на основе сильной связанности вершин строим неориентированный граф
            newUndirectedGraph = new List<Node<T>>();

            for (int i = 0; i < strongComponentList.Count(); i++)
            {
                // Берём первый элемент из списка - узел, к которому будут привязываться другие узлы
                var firstKey = strongComponentList[i].First();

                // Проверяем создавали мы уже этот ОСНОВНОЙ наш узел. Если да, то ищем другой основной узел для создания
                var currentMainNode = newUndirectedGraph.FirstOrDefault(x => x.Name?.ToString() == firstKey.Name?.ToString());

                if (currentMainNode == null)
                {
                    currentMainNode = new Node<T> { Name = firstKey.Name, ConnectedNodes = new List<Node<T>>() };
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
                            var newNode = new Node<T> { Name = strongComponentList[j][1].Name, ConnectedNodes = new List<Node<T>>() };
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
        /// Поиск количества путей ко всем узлам через заданный поиск.
        /// </summary>
        /// <param name="start"> Стартовый узел </param>
        /// <returns></returns>
        public Dictionary<T, int> ShortWaysToNodes(T start, TypeSearch type)
        {
            var allWays = new Dictionary<T, int>();

            foreach (var node in nodes)
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

        /// <summary>
        /// Перевод из словаря в список с классом Node
        /// </summary>
        /// <returns></returns>
        public void ConversationDictToListNodes()
        {
            foreach (var node in nodes)
            {
                nodesList.Add(new Node<T>() { Name = node.Key, ConnectedNodes = new List<Node<T>>(), Visited = false });
            }
            foreach (var nodeDict in nodes)
            {
                var listNodes = nodeDict.Value;
                var needNode = nodesList.FirstOrDefault(x => x.Name?.ToString() == nodeDict.Key?.ToString());
                var choiceNodes = new List<Node<T>>();

                foreach (var linkNode in listNodes)
                {
                    var oneOfNode = nodesList.FirstOrDefault(x => x.Name?.ToString() == linkNode?.ToString());
                    choiceNodes.Add(oneOfNode);
                }

                needNode.ConnectedNodes.AddRange(choiceNodes);
            }
        }

        #region Незаконченная реализация

        /// <summary>
        /// Недозаконченная реализация поиска в глубину кратчайшего пути
        /// </summary>
        /// <param name="start"> Стартовый узел </param>
        /// <param name="end"> Конечный узел </param>
        /// <returns></returns>
        public int DFSWithLevels(T start, T end)
        {
            // Если стартовая это и есть конечная
            if (start.ToString() == end.ToString())
                return 0;

            int currentWays = 1;    // Счётчик сколько прошли пути
            int shortWays = 0;    // Счётчик кратчайшего пути
            var stack = new Stack<Node<T>>();

            var firstNode = nodesList.FirstOrDefault(x => x.Name?.ToString() == start?.ToString());
            firstNode.Visited = true;

            var previous = firstNode;

            // Заполняем первый уровень поиска (у начального узла вводим смежные ему узлы)
            foreach (var node in firstNode.ConnectedNodes)
                stack.Push(node);

            // Реализация поиска
            while (stack.Count != 0)
            {
                // берём верхний элемент с стека
                var first = stack.Pop();
                first.Previous = previous;

                // Проверка соответствию нашей цели
                if (first.Name?.ToString() == end?.ToString())
                {
                    if (shortWays == 0 || shortWays < currentWays)
                    {
                        shortWays = currentWays;
                        first.Visited = true;
                        previous = FindWayBack(previous, ref currentWays, end);
                        continue;
                    }
                }

                if (first.Visited == true)
                {
                    previous = FindWayBack(previous, ref currentWays, end);
                    continue;
                }

                first.Visited = true;
                // Берём связанные узлы, только те, которые не привязали
                var connectedNodes = first.ConnectedNodes.Where(x => !x.Visited || x.Name?.ToString() == end?.ToString()).ToList();

                if (connectedNodes.Count == 0)
                {
                    previous = FindWayBack(previous, ref currentWays, end)?.Previous;
                    continue;
                }

                // Добавление следующего уровня поиска
                foreach (var node in connectedNodes)
                    stack.Push(node);

                currentWays++;
                previous = first;
            }

            return shortWays;
        }

        /// <summary>
        /// Поиск пути назад (ищем узел, через который будем осуществялть дальнейшую проверку)
        /// </summary>
        /// <param name="previous"> Предыдущий проверяемый узел </param>
        /// <param name="currentWays"> Сколько прошли на данный момент </param>
        /// <param name="end"> Узел до которого ищем путь</param>
        /// <returns></returns>
        public Node<T> FindWayBack(Node<T> previous, ref int currentWays, T end)
        {
            // Проверяем, сколько нам ещё надо пройти узлов через предыдущий узел
            int checkedConnectedNodes = previous.ConnectedNodes.Where(x => x.Visited).Count();
            var different = previous.ConnectedNodes.Count - checkedConnectedNodes;

            nodesList.FirstOrDefault(x => x.Name?.ToString() == end?.ToString()).Visited = false;

            // Значит, что в предыдущем узле остались недопроверенные связанные узлы
            if (different != 0)
            {
                currentWays--;
                return previous;
            }

            // Ищем предыдущий узел, где ещё можно проверить связанные узлы к нему
            previous = previous.Previous;
            currentWays--;

            while (true)
            {
                checkedConnectedNodes = previous.ConnectedNodes.Where(x => x.Visited).Count();
                different = previous.ConnectedNodes.Count - checkedConnectedNodes;
                bool flag = previous.ConnectedNodes.Contains(nodesList.FirstOrDefault(x => x.Name?.ToString() == end?.ToString()));

                if (different == 0 && !flag)
                {
                    previous = previous.Previous;
                    currentWays--;
                    continue;
                }

                break;
            }

            return previous;
        }

        #endregion
    }

    public class Node<T>
    {
        public T Name { get; set; }
        public List<Node<T>> ConnectedNodes { get; set; }
        public bool Visited { get; set; }
        public Node<T> Previous { get; set; }
    }
}