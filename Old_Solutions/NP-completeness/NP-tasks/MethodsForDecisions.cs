using Graphs;

namespace NpTasks
{
    /// <summary>
    /// Класс с методами для решения NP-задач
    /// </summary>
    public static class MethodsForDecisions
    {
        /// <summary>
        /// Решение задачи о рюкзаке с помощью динамического программирования
        /// </summary>
        /// <param name="thingsWithValues"> Список элементов с их весом и ценой </param>
        /// <param name="maxWeight"> Вместимость рюкзака </param>
        public static void TaskAboutBackpack(List<Thing> thingsWithValues, int maxWeight)
        {
            // Максимальная суммарная стоимость, которую можно набрать
            // первыми i (строки вещей) предметами так, чтобы их вес не превосходил j (столбцы весов)
            // Добавляем +1 чтобы первая колонка и первая строка были пустые (при обращении к ним не выдавало ошибку)
            int[,] maxAmountCost = new int[thingsWithValues.Count + 1, maxWeight + 1];

            for (int i = 0; i <= thingsWithValues.Count; i++)
            {
                for (int j = 0; j <= maxWeight; j++)
                {
                    // Если мы находимся в первой стобце или первой строке таблицы
                    if (j == 0 || i == 0)
                    {
                        maxAmountCost[i, j] = 0;
                    }
                    // Если у нас подходит по весу и возможно есть свободное место
                    else if (thingsWithValues[i - 1].Weight <= j)
                    {
                        // Присваиваем большее число, либо прайс текущего элемента + наибоьшего элемента что будет на остаток, либо прайс предыдущей строки 
                        maxAmountCost[i, j] = Math.Max(
                            thingsWithValues[i - 1].Price + maxAmountCost[i - 1, j - thingsWithValues[i - 1].Weight], 
                            maxAmountCost[i - 1, j]);
                    }
                    // Если текущий элемент не подходит по весу, берём из строчки выше
                    else
                    {
                        maxAmountCost[i, j] = maxAmountCost[i - 1, j];
                    }
                }
            }

            Console.WriteLine($"Final price: {maxAmountCost[thingsWithValues.Count, maxWeight]}");
        }
        
        /// <summary>
        /// Решение задачи о раскраске графа (жадный алгоритм)
        /// </summary>
        public static void TaskAboutColoringGraph(List<GraphInfo> graph, List<string> colors)
        {
            // Количество вершин
            int amountVertex = graph.Count;

            for(int i = 0; i < amountVertex; i++)
            {
                // устанавливаем для хранения цвета смежных вершин `u`
                var resolvedColors = new List<string>();

                // проверяем цвета смежных вершин `u` и сохраняем их в наборе
                foreach (var node in graph[i].ConnectedNodes)
                    if(node.Color != null)
                        resolvedColors.Add(node.Color);

                // проверяем первый свободный цвет
                string freeColor = "";

                for (int j = 0; j < colors.Count; j++)
                    if (!resolvedColors.Contains(colors[j]))
                    {
                        freeColor = colors[j];
                        break;
                    }
                        

                // назначаем вершине i первый доступный цвет
                graph[i].Color = freeColor;
            }

            Console.WriteLine("The colors of vertexes: ");

            foreach (var node in graph)
                Console.WriteLine($"{node.Name} - {node.Color}");
        }

        /// <summary>
        /// Решение задачи о раскладке по ящикам (жадный алгоритм)
        /// </summary>
        public static void TaskLayoutByBoxes(List<Box> listBoxes, List<Element> listElements)
        {
            var amountBoxes = listBoxes.Count;
            var amountElements = listElements.Count;

            // Алгоритм помещает всякий предмет таким образом, чтобы после его укладки в ящике оставалось как можно меньше места
            // Укладка в новый ящик происходит только в том случае, если очередной объект не помещается ни в какой из имеющихся ящиков

            var listBoxesWithElem = new List<Box>();

            for(int i = 0; i < amountElements; i++)
            {
                Box checkingBox = listBoxesWithElem.FirstOrDefault(x => (x.FreePlace - listElements[i].Size) > 0);

                // Если у нас нет свободного ящика под текущий элемент, то берём новый
                if(checkingBox == null)
                {
                    checkingBox = listBoxes.FirstOrDefault(x => (x.FreePlace - listElements[i].Size) > 0);

                    // Если и нового нет, то лови исключение
                    if (checkingBox == null)
                        throw new Exception($"For element {listElements[i].Name} did't find the suitable box!");

                    checkingBox.FreePlace -= listElements[i].Size;
                    checkingBox.ListElements.Add(listElements[i]);
                    listBoxesWithElem.Add(checkingBox);

                    continue;
                }

                // Если в ящике уже с элементами есть ещё место под элемент
                checkingBox.FreePlace -= listElements[i].Size;
                checkingBox.ListElements.Add(listElements[i]);
            }

            Console.WriteLine("List boxes with elements: ");

            foreach(Box box in listBoxesWithElem)
            {
                Console.Write($"\nBox: {box.Name}, including elements: ");
                foreach(Element element in box.ListElements)
                    Console.Write($"{element.Name}, ");
            } 
        }

        /// <summary>
        /// Решение задачи о суммах подмножеств (жадный алгоритм)
        /// </summary>
        public static void isSubsetSum(HashSet<string> allComponents, Dictionary<string, HashSet<string>> tableSubsetsStations)
        {
            // Хранение итогового набора станций
            var finalStations= new List<string>();

            while (allComponents.Any())
            {
                // Поиск станции, которая обслуживает больше всего штантов, не входящих в текущее покрытие
                string bestStation = "";

                // Содержит все штаты, обслуживаемые этой станцией, которые ещё не входят в текущее покрытие
                List<string> statesCovered = new List<string>();

                // Перебор всех станций и поиск наилучшей
                foreach (var subsets in tableSubsetsStations)
                {
                    var covered = allComponents.Intersect(subsets.Value); // Пересечение множеств

                    if (covered.Count() > statesCovered.Count())
                    {
                        bestStation = subsets.Key;
                        statesCovered = covered.ToList();
                    }
                }

                // Удаляем добаленные станции из общего списка и добавляем множество в итоговый список
                foreach(var state in statesCovered)
                    allComponents.Remove(state);

                finalStations.Add(bestStation);
            }

            Console.WriteLine("List with components: ");

            foreach (var component in finalStations)
                Console.Write($"{component}, ");
        }
    }
}
