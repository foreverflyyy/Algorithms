

using System.Text;

namespace AlgorithmKMP
{
    /// <summary>
    /// Класс с методами для чтения данных и поиска по образцу
    /// </summary>
    /// <typeparam name="T"> Тип данных, который будет у значения узла </typeparam>
    public class MethodsForSearch<T>
    {


        public MethodsForSearch(Dictionary<T, List<T>> nodes)
        {
            
        }

        /// <summary>
        /// Получение данных из файла
        /// </summary>
        /// <param name="data"> Передаваемые данные файла </param>
        /// <returns></returns>
        static public Dictionary<char, List<char>> CreateGraphFromMatrixAdjacency(FileStream data)
        {
            // выделяем массив для считывания данных из файла
            byte[] buffer = new byte[matrixAdjacency.Length];
            // считываем данные
            matrixAdjacency.Read(buffer, 0, buffer.Length);
            // декодируем байты в строку (получаем строку)
            string matrixFromFile = Encoding.Default.GetString(buffer);

            string[] rowsMatrix = matrixFromFile.Split(new string[] { "\n" }, StringSplitOptions.None);

            // Работаем с каждым узлом графа в отдельности
            for (int i = 0; i < rowsMatrix.Length; i++)
            {
                var valuesLine = rowsMatrix[i].Split(new char[] { ' ' });

                // Создаём новый узел и добавляем в него связанные узлы
                allNodes.Add(GetNeedSymbol(i), new List<char>() { });

                for (int j = 0; j < valuesLine.Length; j++)
                    if (Convert.ToInt32(valuesLine[j]) == 1)
                        allNodes[GetNeedSymbol(i)].Add(GetNeedSymbol(j));
            }

            return allNodes;
        }
    }
}
