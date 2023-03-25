using Graphs;

namespace ShortestPaths {

    public class Program
    {
        static void Main(string[] args)
        {
            // Берём файл для чтения матрицы
            var matrixAdjacency = new FileStream("matrixInput.txt", FileMode.OpenOrCreate);

            // Создаём граф, по которому будем выполнять обход
            var graph = MethodsShortestPaths<char>.CreateGraph(matrixAdjacency);
            var methods = new MethodsShortestPaths<char>(graph);
            

        }
    }
}

// Реализовать алгоритм нахождения эйлерова цикла в неориентированном графе, заданном матрицей смежности.