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
