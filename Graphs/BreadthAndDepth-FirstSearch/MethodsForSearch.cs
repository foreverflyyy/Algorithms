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
        /// Поиск в глубину (поиск кратчайшего расстояния до узла)
        /// </summary>
        /// <param name="start"> Стартовый узел </param>
        /// <param name="end"> Конечный узел </param>
        /// <returns></returns>
        public int DFS(T start, T end)
        {
            if (start.ToString() == end.ToString())
                return 0;

            int currentWays = 1;    // Счётчик сколько прошли пути
            var stack = new Stack<T>();

            // Список пройденных узлов
            List<T> checkedNode = new List<T>();
            checkedNode.Add(start);

            // Заполняем первый уровень поиска (у начального узла вводим смежные ему узлы)
            var connectedNodes = nodes[start];
            foreach (var node in connectedNodes)
                stack.Push(node);

            while (stack.Count != 0)
            {
                var first = stack.Pop();

                // Проверка, смотрели ли мы уже этот узел
                if (checkedNode.Contains(first))
                {
                    currentWays--;
                    continue;
                }

                // Проверка соответствию нашей цели
                if (first?.ToString() == end?.ToString())
                    return currentWays;

                // Добавление в список проверенных узлов
                checkedNode.Add(first);

                // Добавление следующего уровня поиска
                connectedNodes = nodes[first].Where(x => !checkedNode.Contains(x)).ToList();

                if (connectedNodes.Count == 0)
                {
                    currentWays--;
                    continue;
                }

                foreach (var node in connectedNodes)
                    stack.Push(node);

                currentWays++;
            }

            return 0;
        }

        /// <summary>
        /// Поиск в глубину (поиск кратчайшего расстояния до узла)
        /// </summary>
        /// <param name="start"> Стартовый узел </param>
        /// <param name="end"> Конечный узел </param>
        /// <returns></returns>
        public int DFSWithLevels(T start, T end)
        {
            if (start.ToString() == end.ToString())
                return 0;

            int currentWays = 1;    // Счётчик сколько прошли пути
            int shortWays = 1;    // Счётчик кратчайшего пути
            var stack = new Stack<Node<T>>();

            var firstNode = nodesList.FirstOrDefault(x => x.Name?.ToString() == start?.ToString());
            firstNode.Visited = true;
            firstNode.CheckedConnectedNodes = 1;

            var previous = firstNode;

            // Заполняем первый уровень поиска (у начального узла вводим смежные ему узлы)
            foreach (var node in firstNode.ConnectedNodes)
                stack.Push(node);

            while (stack.Count != 0)
            {
                var first = stack.Pop();

                // Проверка соответствию нашей цели
                if (first?.ToString() == end?.ToString())
                {
                    if(shortWays == 0)
                    {
                        shortWays = currentWays;
                        continue;
                    }
                }
                first.Previous = previous;

                // Проверка, смотрели ли мы уже этот узел
                if (first.Visited)
                {
                    previous = FindWayBack(previous, ref currentWays)?.Previous;
                    continue;
                }

                var connectedNode = first.ConnectedNodes.Where(x => !x.Visited).ToList();

                // Добавление следующего уровня поиска
                /*foreach (var node in connectedNodes)
                    stack.Push(node);

                if (connectedNodes.Count == 0)
                {
                    currentWays--;
                    continue;
                }

                foreach (var node in connectedNodes)
                    stack.Push(node);*/

                currentWays++;
            }

            return 0;
        }

        public Node<T> FindWayBack(Node<T> previous, ref int currentWays)
        {
            while (true)
            {
                var different = previous.ConnectedNodes.Count - previous.CheckedConnectedNodes;
                if (different == 0)
                {
                    previous = previous.Previous;
                    currentWays--;
                    continue;
                }

                currentWays--;
                break;
            }

            return previous;
        }

        /// <summary>
        /// Перевод из словаря в список с классом Node
        /// </summary>
        /// <returns></returns>
        public void ConversationDictToListNodes()
        {
            foreach (var node in nodes)
            {
                nodesList.Add(new Node<T>() { Name = node.Key, ConnectedNodes = new List<Node<T>>(), Visited = false, CheckedConnectedNodes = 0});
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

        /*public void FindPath(T start, T end)
        {
            foreach (var i in nodes)
                i.Visited = false;
            DFS(start, end);
        }*/

        /// <summary>
        /// Поиск компонентов связности
        /// </summary>
        /// <returns></returns>
        public int SearchConnectivityComponent()
        {


            return 0;
        }

        /// <summary>
        /// Поиск количества путей ко всем узлам через заданный поиск.
        /// </summary>
        /// <param name="start"> Стартовый узел </param>
        /// <returns></returns>
        public Dictionary<T, int> ShortWaysToAllNodes(T start, TypeSearch type)
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
                        result = DFS(start, nameNode);
                        break;
                }

                allWays.Add(nameNode, result);
            }

            return allWays;
        }
    }

    public class Node<T>
    {
        public T Name { get; set; }
        public List<Node<T>> ConnectedNodes { get; set; }
        public bool Visited { get; set; }
        public Node<T> Previous { get; set; }
        public int CheckedConnectedNodes { get; set; }
    }
}