using LineBracket;
using Trees;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            //string line = "8(3(1,6(4,7)),10(,14(13,)))";
            //string line = "12(10(8,11),15(,16))";
            //string line = "10(7(6(,4(3,)),8(,9)),11(,15(13,20)))";
            Console.WriteLine("Please, enter your tree:");
            string line = Console.ReadLine();
            while (true)
            {
                if (line == "")
                {
                    Console.WriteLine("Please, enter correct tree!");
                    line = Console.ReadLine();
                }
                else break;
            }
            LineBracketBT lineBracket = new LineBracketBT();
            BinaryTree<int>tree = lineBracket.CreateTreeWithLine(line);

            while (true)
            {
                Console.WriteLine("\n////////////TREE//////////////");
                tree.PrintLine();
                Console.WriteLine("//////////////////////////////");
                Console.WriteLine("\nAvailable operations: ADD, DELETE, SEARCH VERTEX");
                Console.WriteLine("If you want exit enter EXIT\n");
                Console.WriteLine("What you want: ");
                string request = Console.ReadLine();
                if (request == "ADD" || request == "Add" || request == "add")
                {
                    Console.WriteLine("Value: ");
                    int num = Convert.ToInt32(Console.ReadLine());
                    tree.Add(num);
                    continue;
                } else if (request == "DELETE" || request == "Delete" || request == "delete")
                {
                    Console.Write("Value: ");
                    int num = Convert.ToInt32(Console.ReadLine());
                    tree.Remove(num);
                    continue;
                } else if (request == "SEARCH VERTEX" || request == "search vertex" || request == "Search Vertex")
                {
                    int num = tree.SearchVertex();
                    Console.Write("VERTEX: " + num);
                    continue;
                } else if (request == "PRINT ACROSS" || request == "print across" || request == "Print Across")
                {
                    tree.PrintAcross();
                    continue;
                } else if (request == "EXIT" || request == "exit" || request == "Exit")
                    break;
                else
                {
                    Console.WriteLine("Incorrect enter your request!");
                    continue;
                }
            }
            Console.WriteLine("\n////////////TREE//////////////");
            tree.PrintLine();
            Console.WriteLine("//////////////////////////////");
        }
    }
}

/*Лаба №17 “Операции над БНП: поиск, добавление, удаление”
Дерево вводится в программу в формате линейно-скобочной записи. 
Затем появляется меню, в котором доступна операция добавления, 
удаления и поиска вершины БДП. После выполнения операции программа 
должна возвращаться снова в меню. При выходе их него до завершения программы 
на экран должно быть выведено БДН любым способом (в виде линейно-скобочной 
записи или в графической форме).*/
