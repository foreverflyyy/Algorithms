using LineBracket;
using Trees;

class Program
{
    static void Main(string[] args)
    {
        //string line = "8(3(1,6),10(,14))";
        string line = "8(3(1,6(4,7)),10(,14(13,)))";
        //string line = "14(13(1,64(456,7)),9(,3(2,)))";
        //string line = "12(10(8,11),15(,16))";
        LineBracketBT lineBracket = new LineBracketBT();
        BinaryTree<int>tree = lineBracket.CreateTreeWithLine(line);

        foreach (var n in tree.PrintStack())
            Console.Write(n.val + ",");
    }
}

/*Лаба №16 “Не рекурсивный прямой обход” (реализуется с помощью стека).
В качестве выходных данных формируется строка обхода. Например:
Бинарное дерево поиска*/
