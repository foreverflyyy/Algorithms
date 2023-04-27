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
        /// <param name="thingsWithValues"></param>
        /// <param name="maxWeight"> Вместимость рюкзака </param>
        public static void TaskAboutBackpack(List<Thing> thingsWithValues, int maxWeight)
        {
            // Максимальная суммарная стоимость, которую можно набрать
            // первыми i (строки вещей) предметами так, чтобы их вес не превосходил j (столбцы весов)
            int[,] maxAmountCost = new int[thingsWithValues.Count, maxWeight];

            for (int i = 0; i < thingsWithValues.Count; i++)
            {
                for (int j = 1; j <= maxWeight; j++)
                {
                    // Если мы находимся в первой ячейке таблицы
                    if (j == 1 && i == 0)
                    {
                        // Если вес подходит то присваиваем, если нет то 0
                        if(thingsWithValues[i].Weight <= j)
                            maxAmountCost[i, j - 1] = thingsWithValues[i].Price;
                        else
                            maxAmountCost[i, j - 1] = 0;

                        continue;
                    }

                    // Если текущий элемент не подходит по весу
                    if (thingsWithValues[i].Weight < j)
                    {
                        // То ищем элемент с соседнего столбца или строки
                        if (i == 0)
                        {
                            if (maxAmountCost[i, j - 2] > thingsWithValues[i].Price)
                                maxAmountCost[i, j - 1] = maxAmountCost[i, j - 2];
                            else
                                maxAmountCost[i, j - 1] = thingsWithValues[i].Price;

                            continue;
                        }

                        // Если мы на первом стобце, то либо закинуть текущую вещь или если дороже, то перенести другую вещь с верхней строки
                        if (j == 1)
                        {
                            if (maxAmountCost[i - 1, j - 1] > thingsWithValues[i].Price)
                                maxAmountCost[i, j - 1] = maxAmountCost[i - 1, j - 1];
                            else
                                maxAmountCost[i, j - 1] = thingsWithValues[i].Price;

                            continue;
                        }

                        maxAmountCost[i, j - 1] = thingsWithValues[i].Price;

                        maxAmountCost[i, j - 1] = 0;
                    }

                    // Если у нас подходит по весу и возможно есть свободное место
                    if (thingsWithValues[i].Weight >= j)
                    {
                        // Если мы на первой строке, то либо закинуть текущую вещь или если дороже, то закинуть вещь с левой ячейки строки
                        if (i == 0)
                        {
                            if (maxAmountCost[i, j - 2] > thingsWithValues[i].Price)
                                maxAmountCost[i, j - 1] = maxAmountCost[i, j - 2];
                            else
                                maxAmountCost[i, j - 1] = thingsWithValues[i].Price;

                            continue;
                        }

                        // Если мы на первом стобце, то либо закинуть текущую вещь или если дороже, то перенести другую вещь с верхней строки
                        if (j == 1)
                        {
                            if (maxAmountCost[i - 1, j - 1] > thingsWithValues[i].Price)
                                maxAmountCost[i, j - 1] = maxAmountCost[i - 1, j - 1];
                            else
                                maxAmountCost[i, j - 1] = thingsWithValues[i].Price;

                            continue;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Решение задачи о раскраске графа
        /// </summary>
        public static void TaskAboutColoringGraph()
        {
            
        }/// <summary>
        /// Решение задачи о раскладке по ящикам
        /// </summary>
        public static void TaskLayoutByBoxes()
        {
            
        }/// <summary>
        /// Решение задачи о суммах подмножеств, используя жадный алгоритм
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
