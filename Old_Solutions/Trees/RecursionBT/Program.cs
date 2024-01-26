using LineBracket;
using Trees;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = "8(3(1,6(4,7)),10(,14(13,)))";
            //string line = "14(13(1,64(456,7)),9(,3(2,)))";
            
            LineBracketBT lineBracket = new LineBracketBT();
            BinaryTree<int>tree = lineBracket.CreateTreeWithLine(line);
            
            Console.WriteLine("------------Straight------------");
            tree.PrintStraight();
            Console.WriteLine("\n-------------Center-----------");
            tree.PrintCenter();
            Console.WriteLine("\n-------------Reverse-----------");
            tree.PrintReverse();
        }
    }
}

/*Лаба №15 “Рекурсивные обходы (прямой, центральный, концевой)”*/