using Graphs;

namespace ShortestPaths {

    public class Program
    {
        static void Main(string[] args)
        {
            // Берём файл для чтения матрицы
            var matrixAdjacency = new FileStream("matrixInput5.txt", FileMode.OpenOrCreate);

            // Создаём граф, по которому будем выполнять обход
            var graph = MethodsShortestPaths<char>.CreateGraph(matrixAdjacency);
            var methods = new MethodsShortestPaths<char>(graph);


        }
    }
}

// Минимальные покрывающие деревья
//	Реализовать алгоритм Крускала нахождения минимального покрывающего дерева.
//	Реализовать алгоритм Прима нахождения минимального покрывающего дерева.
