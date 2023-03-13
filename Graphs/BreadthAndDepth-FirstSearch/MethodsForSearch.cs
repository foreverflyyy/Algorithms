


namespace Graphs
{

    /// <summary>
    /// Класс с методами поиска в графе
    /// </summary>
    /// <typeparam name="T"> Тип данных, который будет у значения узла </typeparam>
    public class MethodsForSearch<T>
    {
        public Dictionary<T, List<T>> nodes;

        public MethodsForSearch(Dictionary<T, List<T>> nodes)
        {
            this.nodes = nodes;
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

            // Заполняем первый уровень поиска
            var connectedNodes = nodes[start];
            foreach(var node in connectedNodes)
                queue.Enqueue(node);
            
            List<T> checkedNode = new List<T>();
            var recervedQueue = new Queue<T>();

            int ways = 1;   // Счётчик пути

            while (queue.Count != 0)
            {
                var first = queue.Dequeue();

                // Проверка, а не смотрели ли мы уже этот узел
                if (checkedNode.Contains(first))
                    continue;

                // Проверка соответствию нашей цели
                if (first?.ToString() == end?.ToString())
                    return ways;

                // Добавление следующего уровня поиска
                connectedNodes = nodes[first];

                foreach (var node in connectedNodes)
                    recervedQueue.Enqueue(node);

                // Добавление в список проверенных узлов
                checkedNode.Add(first);

                // Если текущий уровень проверки закончился, переходим к следующему
                if(queue.Count == 0)
                {
                    for(int i = 0; i < recervedQueue.Count(); i++)
                        queue.Enqueue(recervedQueue.Dequeue());
                    ways++;
                    recervedQueue.Clear();
                }
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

            int currentWays = 1;    // Счётчик сколько прошли
            int shortWay = 1;       // Кратчайшее расстояние

            var stack = new Stack<T>();

            // Заполняем первый уровень поиска
            var connectedNodes = nodes[start];
            foreach (var node in connectedNodes)
                stack.Push(node);

            List<T> checkedNode = new List<T>();

            while (stack.Count != 0)
            {
                var first = stack.Pop();

                // Проверка соответствию нашей цели
                if (first?.ToString() == end?.ToString())
                {
                    if (shortWay == 0)
                    {
                        shortWay = currentWays;
                    }
                    else if (shortWay > currentWays)
                    {
                        shortWay = currentWays;
                        currentWays--;
                    }
                    continue;
                }

                // Проверка, смотрели ли мы уже этот узел
                if (checkedNode.Contains(first))
                    continue;

                // Добавление в список проверенных узлов
                checkedNode.Add(first);

                // Добавление следующего уровня поиска
                connectedNodes = nodes[first];

                foreach (var node in connectedNodes)
                    stack.Push(node);
            }

            return shortWay;
        }

        /// <summary>
        /// Поиск в глубину (поиск кратчайшего расстояния до узла)
        /// </summary>
        /// <param name="start"> Стартовый узел </param>
        /// <param name="end"> Конечный узел </param>
        /// <param name="currentWays"> Сколько ребер графа прошли</param>
        /// <param name="shortWay"> Кратчашее найденное расстояние </param>
        /// <param name="checkedNode"> Проверенные узлы </param>
        public void RecurtionDFS(T start, T end, ref int currentWays, ref int shortWay, List<T> checkedNode)
        {
            // Проверка на соотвествие концу пути
            if (start?.ToString() == end?.ToString()) 
            {
                if (shortWay == 0)
                {
                    shortWay = currentWays;
                    return;
                }
                else if(shortWay > currentWays)
                {
                    shortWay = currentWays;
                    currentWays--;
                    return;
                }
            }

            // Проверяем, был ли уже этот узел
            if (checkedNode.Contains(start))
                return;

            // Добавление в список проверенных узлов
            checkedNode.Add(start);

            // Заполняем уровень поиска
            var connectedNodes = nodes[start];

            foreach (var node in connectedNodes)
            {
                currentWays++;
                RecurtionDFS(node, end, ref currentWays, ref shortWay, checkedNode);
                currentWays--;
            }

            return;
        }
  }
}