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
            foreach (var node in nodes)
                nodesList.Add(new Node<T>() { Name = node.Key, ConnectedNodes = node.Value, Visited = false });
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
        /*public int DFSWithLevels(T start, T end)
        {
            if (start.ToString() == end.ToString())
                return 0;

            int currentWays = 1;    // Счётчик сколько прошли пути
            var stack = new Stack<T>();

            // Список пройденных узлов
            var firstNode = nodesList.FirstOrDefault(x => x.Name?.ToString() == start?.ToString());
            firstNode.Visited = true;

            // Заполняем первый уровень поиска (у начального узла вводим смежные ему узлы)
            var connectedNodes = nodesList[start];
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
        }*/

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
        public List<T> ConnectedNodes { get; set; }
        public bool Visited { get; set; }
        public int Level { get; set; }
    }
}