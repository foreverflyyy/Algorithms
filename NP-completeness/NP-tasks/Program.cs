using Graphs;

namespace NpTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задача о рюкзаке
            //TaskAboutBackpack();


            // Задача о раскраске графа
            //TaskAboutColoringGraph();


            // Задача о распределении в ящики
            //TaskLayoutByBoxes();


            // Задача о сумме подмножеств
            //isSubsetSum();
        }

        public static void TaskAboutBackpack()
        {
            var thingsWithValues = new List<Thing>();

            thingsWithValues.Add(new Thing { Name = "Laptop", Price = 2000, Weight = 2});
            thingsWithValues.Add(new Thing { Name = "Recorder", Price = 3000, Weight = 4});
            thingsWithValues.Add(new Thing { Name = "Guitar", Price = 1500, Weight = 1});
            thingsWithValues.Add(new Thing { Name = "Phone", Price = 1000, Weight = 1});

            MethodsForDecisions.TaskAboutBackpack(thingsWithValues, maxWeight: 4);
        }

        public static void TaskAboutColoringGraph()
        {
            // Берём файл для чтения матрицы
            //string path = @"D:\dream\Algorithms\Graphs\EulerianPath\matrixInput1.txt";
            string path = @"D:\workSpaceNU\primat\Algorithms\NP-completeness\NP-tasks\matrixInput.txt";
            var matrixAdjacency = new FileStream(path, FileMode.OpenOrCreate);

            // Создаём граф, по которому будем выполнять обход
            var graph = GraphRealization<char>.CreateGraph(matrixAdjacency);

            var colors = new List<string>
            {
                "BLUE", "GREEN", "RED", "YELLOW", "ORANGE", "PINK",
                "BLACK", "BROWN", "WHITE", "PURPLE", "VOILET"
            };

            // Никакие две соседние вершины не должны иметь одинаковый цвет
            MethodsForDecisions.TaskAboutColoringGraph(graph, colors);
        }

        public static void TaskLayoutByBoxes()
        {
            var listBoxes = new List<Box>();
            var listElements = new List<Element>();

            listBoxes.Add(new Box { Name = "1stBox", FreePlace = 1});
            listBoxes.Add(new Box { Name = "2ndBox", FreePlace = 1});
            listBoxes.Add(new Box { Name = "3rdBox", FreePlace = 1});
            listBoxes.Add(new Box { Name = "4thBox", FreePlace = 1});
            listBoxes.Add(new Box { Name = "5thBox", FreePlace = 1});
            listBoxes.Add(new Box { Name = "6thBox", FreePlace = 1});

            listElements.Add(new Element { Name = "1stEl", Size = 0.5 });
            listElements.Add(new Element { Name = "2ndEl", Size = 0.7 });
            listElements.Add(new Element { Name = "3rdEl", Size = 0.3 });
            listElements.Add(new Element { Name = "4thEl", Size = 0.9 });
            listElements.Add(new Element { Name = "5thEl", Size = 0.6 });
            listElements.Add(new Element { Name = "6thEl", Size = 0.8 });
            listElements.Add(new Element { Name = "7thEl", Size = 0.1 });
            listElements.Add(new Element { Name = "8thEl", Size = 0.4 });
            listElements.Add(new Element { Name = "9thEl", Size = 0.2 });
            listElements.Add(new Element { Name = "10thEl", Size = 0.1 });

            MethodsForDecisions.TaskLayoutByBoxes(listBoxes, listElements);
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

    public class Box
    {
        public string Name { get; set; }
        public double FreePlace { get; set; }
        public List<Element> ListElements { get; set; } = new List<Element>();
    }
    
    public class Element
    {
        public string Name { get; set; }
        public double Size { get; set; }
    }
}
/*
  NP - полные задачи
    Решить задачу о раскраске графа.
    Решить дискретную задачу о рюкзаке.
    Решить задачу о раскладке по ящикам
    Решить задачу о суммах подмножеств, используя жадный алгоритм.
*/
