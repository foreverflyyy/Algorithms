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
            int checkingThings = 0; // Количество обработанных предметов

            // Максимальная суммарная стоимость, которую можно набрать
            // первыми i (строки вещей) предметами так, чтобы их вес не превосходил j (столбцы весов)
            int[,] maxAmountCost = new int[thingsWithValues.Count, maxWeight];

            for (int i = 0; i < thingsWithValues.Count; i++)
            {
                for (int j = 1; j <= maxWeight; j++)
                {
                    // Если вес совпал с текущим элементом, то 3 варианта
                    if (thingsWithValues[i].Weight == j)
                    {
                        // Если мы находимся в первой ячейке таблицы
                        if (j == 1 && i == 0)
                            maxAmountCost[i, j - 1] = thingsWithValues[i].Price;

                        // Если мы 
                        if (j == 1)
                        {

                        }
                        // 
                    }

                    // Проверка если вес текущего элемента подходим под вес столбца
                    if (thingsWithValues[i].Weight > j)
                    {
                        maxAmountCost[i, j - 1] = thingsWithValues[i].Price;
                    }



                    // Проверка если вес текущего элемента подходим под вес столбца
                    if (thingsWithValues[i].Weight >= j)
                        maxAmountCost[i, j - 1] = thingsWithValues[i].Price;

                    maxAmountCost[i, j - 1] = 0;
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
