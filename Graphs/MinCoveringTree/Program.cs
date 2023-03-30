using Graphs;

namespace ShortestPaths {

    public class Program
    {
        static void Main(string[] args)
        {
            // Берём файл для чтения матрицы
            //string path = @"D:\dream\Algorithms\Graphs\MinCoveringTree\matrixInput2.txt";
            string path = @"D:\workSpaceNU\primat\Algorithms\Graphs\MinCoveringTree\matrixInput2.txt";
            var matrixAdjacency = new FileStream(path, FileMode.OpenOrCreate);

            // Создаём граф, по которому будем выполнять обход
            var graph = MethodsShortestPaths<char>.CreateGraph(matrixAdjacency);
            var methods = new MethodsShortestPaths<char>(graph);

            //methods.AlgorithmKruskala();
            methods.AlgorithmPrima();
        }
    }
}

// Минимальные покрывающие деревья
//	Реализовать алгоритм Крускала нахождения минимального покрывающего дерева.
//	Реализовать алгоритм Прима нахождения минимального покрывающего дерева.
