using System;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Graphs
{
    public static class GraphRealization<T>
    {
        /// <summary>
        /// Создание графа из матрицы смежности
        /// </summary>
        /// <param name="matrixAdjacency"> Передаваемая матрица смежности </param>
        /// <returns></returns>
        static public List<GraphInfo> CreateGraph(FileStream matrixAdjacency)
        {
            var graph = new List<GraphInfo>();

            // выделяем массив для считывания данных из файла
            byte[] buffer = new byte[matrixAdjacency.Length];
            // считываем данные
            matrixAdjacency.Read(buffer, 0, buffer.Length);
            // декодируем байты в строку (получаем строку)
            string matrixFromFile = Encoding.Default.GetString(buffer);

            string[] rowsMatrix = new string[matrixFromFile.Length];

            if (matrixFromFile.Contains("\r"))
                rowsMatrix = matrixFromFile.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            else
                rowsMatrix = matrixFromFile.Split(new string[] { "\n" }, StringSplitOptions.None);

            // Работаем с каждым узлом графа в отдельности
            for (int i = 0; i < rowsMatrix.Length; i++)
            {
                var valuesLine = rowsMatrix[i].Split(new char[] { ' ' });

                // Создаём новый узел и добавляем в него связанные узлы
                var mainNode = graph.FirstOrDefault(x => x.Name == GetNeedSymbol(i));

                if (mainNode == null) 
                {
                    mainNode = new GraphInfo { Name = GetNeedSymbol(i), ConnectedNodes = new List<GraphInfo>() };
                    graph.Add(mainNode);
                }

                for (int j = 0; j < valuesLine.Length; j++)
                {
                    if (valuesLine[j] == "0") // латиница
                        continue;

                    var newConnectedNode = graph.FirstOrDefault(x => x.Name == GetNeedSymbol(j));

                    if(newConnectedNode == null)
                    {
                        newConnectedNode = new GraphInfo { Name = GetNeedSymbol(j), ConnectedNodes = new List<GraphInfo>() };
                        graph.Add(newConnectedNode);
                        mainNode.ConnectedNodes.Add(newConnectedNode);
                    }
                    else
                        mainNode.ConnectedNodes.Add(newConnectedNode);
                }
            }

            return graph;
        }

        /// <summary>
        /// Получение буквы по индексу алфавита
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static char GetNeedSymbol(int index)
        {
            string allSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return allSymbols[index];
        }
    }

    public class GraphInfo
    {
        public char Name { get; set; }
        public string? Color { get; set; }
        public List<GraphInfo> ConnectedNodes { get; set; }
    }
}