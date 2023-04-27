

namespace NpTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задача о рюкзаке
            TaskAboutBackpack();


            // Задача о раскраске графа
            //TaskAboutColoringGraph();


            // Задача о распределении в ящики
            //TaskLayoutByBoxes();


            // Задача о сумме подмножеств
            //isSubsetSum();
        }

        public static void TaskAboutBackpack()
        {
            var thingsWithValues = new Dictionary<string, HashSet<string>>();

            thingsWithValues.Add("kone", new HashSet<string> { "id", "nv", "ut" });
            thingsWithValues.Add("ktwo", new HashSet<string> { "wa", "id", "mt" });
            thingsWithValues.Add("kthree", new HashSet<string> { "or", "nv", "ca" });
            thingsWithValues.Add("kfour", new HashSet<string> { "nv", "ut" });
            thingsWithValues.Add("kfive", new HashSet<string> { "ca", "az" });

            MethodsForDecisions.TaskAboutBackpack();
        }

        public static void TaskAboutColoringGraph()
        {
            MethodsForDecisions.TaskAboutColoringGraph();
        }

        public static void TaskLayoutByBoxes()
        {
            MethodsForDecisions.TaskLayoutByBoxes();
        }
        
        public static void isSubsetSum()
        {
            var allComponents = new HashSet<string>()
            {
                "mt", "or", "wa", "id", "nv", "ut", "ca", "az"
            };
            var tableSubsetsStations = new Dictionary<string, HashSet<string>>();

            tableSubsetsStations.Add("kone", new HashSet<string> { "id", "nv", "ut" });
            tableSubsetsStations.Add("ktwo", new HashSet<string> { "wa", "id", "mt" });
            tableSubsetsStations.Add("kthree", new HashSet<string> { "or", "nv", "ca" });
            tableSubsetsStations.Add("kfour", new HashSet<string> { "nv", "ut" });
            tableSubsetsStations.Add("kfive", new HashSet<string> { "ca", "az" });

            MethodsForDecisions.isSubsetSum(allComponents, tableSubsetsStations);
        }
    }

    public class Thing
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
}
/*
  NP - полные задачи
    Решить задачу о раскраске графа.
    Решить дискретную задачу о рюкзаке.
    Решить задачу о раскладке по ящикам
    Решить задачу о суммах подмножеств, используя жадный алгоритм.
*/
