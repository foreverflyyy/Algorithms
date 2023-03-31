using Graphs;

namespace ShortestPaths {

    public class Program
    {
        static void Main(string[] args)
        {
            // Берём файл для чтения матрицы
            //string path = @"D:\dream\Algorithms\Graphs\EulerianPath\matrixInput1.txt";
            string path = @"D:\workSpaceNU\primat\Algorithms\Graphs\EulerianPath\matrixInput2.txt";
            var matrixAdjacency = new FileStream(path, FileMode.OpenOrCreate);

            // Создаём граф, по которому будем выполнять обход
            var graph = MethodsShortestPaths<char>.CreateGraph(matrixAdjacency);
            var methods = new MethodsShortestPaths<char>(graph);

            methods.EulerianPath();
        }
    }
}

// Реализовать алгоритм нахождения эйлерова цикла в неориентированном графе, заданном матрицей смежности.