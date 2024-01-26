using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Trees
{
  public class BinaryTree<T> where T : IComparable<T>
  {
    private BinaryTree<T> parent, left, right;
    private T val;
    
    public BinaryTree(T val, BinaryTree<T> parent)
    {
          this.val = val;
          this.parent = parent;
    }
    
    public void Add(T val)
    {
          if(val.CompareTo(this.val) < 0){
              if(this.left==null)
                  this.left = new BinaryTree<T>(val, this);
              else if(this.left != null)
                  this.left.Add(val);
          }
          else if (val.CompareTo(this.val) == 0) 
              return;
          else{
              if(this.right==null)
                  this.right = new BinaryTree<T>(val, this);
              else if(this.right != null)
                  this.right.Add(val);
          }
    }
    
    private BinaryTree<T> _search(BinaryTree<T> tree, T val)
    {
          if(tree == null) return null;
          switch (val.CompareTo(tree.val)){
              case 1: return _search(tree.right, val);
              case -1: return _search(tree.left, val);
              case 0: return tree;
              default: return null;
          }
    }
    public BinaryTree<T> Search(T val)
    {
          return _search(this, val);
    }
    public T SearchVertex()
    {
          return this.val;
    }
    
    public bool Remove(T val)
    {
          //Проверяем, существует ли данный узел
          BinaryTree<T> tree = Search(val);
          //Если узла не существует, вернем false
          if (tree == null) return false;
          
          BinaryTree<T> curTree;

          //Если удаляем корень
          if(tree == this){
              if(tree.right!=null)
                  curTree = tree.right;
              else 
                  curTree = tree.left;

              while (curTree.left != null)
                  curTree = curTree.left;
              
              T temp = curTree.val;
              this.Remove(temp);
              tree.val = temp;
              return true;
          }

          //Удаление листьев (слева и справа нет потомков)
          if(tree.left==null && tree.right==null && tree.parent != null){
              if(tree == tree.parent.left)
                  tree.parent.left = null;
              else
                  tree.parent.right = null;
              
              return true;
          }

          //Удаление узла, имеющего левое поддерево, но не имеющее правого поддерева
          if(tree.left != null && tree.right == null){
              //Меняем родителя
              tree.left.parent = tree.parent;
              
              if(tree == tree.parent.left)
                  tree.parent.left = tree.left;
              else if(tree == tree.parent.right)
                  tree.parent.right = tree.left;
              
              return true;
          }

          //Удаление узла, имеющего правое поддерево, но не имеющее левого поддерева
          if(tree.left == null && tree.right != null){
              //Меняем родителя
              tree.right.parent = tree.parent;
              if(tree == tree.parent.left){
                  tree.parent.left = tree.right;
              }
              else if(tree == tree.parent.right){
                  tree.parent.right = tree.right;
              }
              return true;
          }

          //Удаляем узел, имеющий поддеревья с обеих сторон
          if(tree.right!=null && tree.left!=null) {
              curTree = tree.right;

              while (curTree.left != null) {
                  curTree = curTree.left;
              }

              //Если самый левый элемент является первым потомком
              if(curTree.parent == tree) {
                  curTree.left = tree.left;
                  tree.left.parent = curTree;
                  curTree.parent = tree.parent;
                  if (tree == tree.parent.left) {
                      tree.parent.left = curTree;
                  } else if (tree == tree.parent.right) {
                      tree.parent.right = curTree;
                  }
                  return true;
              }
              //Если самый левый элемент НЕ является первым потомком
              else {
                  if (curTree.right != null) {
                      curTree.right.parent = curTree.parent;
                  }
                  curTree.parent.left = curTree.right;
                  curTree.right = tree.right;
                  curTree.left = tree.left;
                  tree.left.parent = curTree;
                  tree.right.parent = curTree;
                  curTree.parent = tree.parent;
                  if (tree == tree.parent.left) {
                      tree.parent.left = curTree;
                  } else if (tree == tree.parent.right) {
                      tree.parent.right = curTree;
                  }

                  return true;
              } 
          } 
          return false;
  }

      private void _printStraight(BinaryTree<T> tree)
      {
          if(tree == null) return;
          Console.WriteLine(tree.val + " ");
          _printStraight(tree.left); // обойти правое поддерево
          _printStraight(tree.right); // обойти левое поддерево
      }
      public void PrintStraight()
      {
          _printStraight(this);
      }
      // обход дерева в ширину (используется очередь)
      private void _printAcross(BinaryTree<T> tree)
      {
          var queue = new Queue<BinaryTree<T>>(); // создать новую очередь
          queue.Enqueue(tree); // поместить в очередь первый уровень
          while (queue.Count!=0) // пока очередь не пуста
          {    
              //если у текущей ветви есть листья, их тоже добавить в очередь
              if (queue.Peek().left != null)
                  queue.Enqueue(queue.Peek().left);
              if (queue.Peek().right != null)
                  queue.Enqueue(queue.Peek().right);

              Console.Write(queue.Peek().val.ToString() + " ");
              //извлечь из очереди информационное поле последнего элемента
              queue.Dequeue();
          }
      }
      public void PrintAcross()
      {
          _printAcross(this);
      }
      
      // обход дерева в ширину (используется очередь)
      private void _printGraph(BinaryTree<T> tree, int num)
      {
          var queue = new Queue<BinaryTree<T>>(); // создать новую очередь
          queue.Enqueue(tree); // поместить в очередь первый уровень (метод добавляет элемент в конец очереди)
          int numInLine = 1;
          int numNow = 1;
          while (queue.Count!=0) // пока очередь не пуста
          {    
              //если у текущей ветви есть листья, их тоже добавить в очередь
              if (queue.Peek().left != null) // метод просто просматривает первый элемент очереди
                  queue.Enqueue(queue.Peek().left);
              if (queue.Peek().right != null)
                  queue.Enqueue(queue.Peek().right);
              
              //делаем отступы под ряд элементов дерева
              if(queue.Peek() == tree || numNow*2 == numInLine)
                  for (int i = 0; i < num / 2; i++)
                      Console.Write(" ");
              Console.Write(queue.Peek().val.ToString());
              
              //проверяем сколько у нас элементов будет в ряде (если дальше будут ещё элементы, то просто отступ делаем)
              //если это был последний в ряде, то переводим на новую строку и говорим что дальше будет больше
              if (numInLine == numNow)
              {
                  Console.Write("\n");
                  num -= 2;
                  numInLine *= 2;
              } else
              {
                  Console.Write(" ");
                  numNow++;
              }
              
              //извлечь из очереди информационное поле последнего элемента
              queue.Dequeue();// убрать последний элемент очереди
          }
      }
      public void PrintGraph()
      {
          int num = 32;
          _printGraph(this, num);
      }

      private void LeafsInLine(BinaryTree<T> tree, ref string line)
      {
          if (tree.left == null && tree.right == null) return;
          if (tree.left != null)
          {
              line += "(";
              line += Convert.ToString(tree.left.val);
              LeafsInLine(tree.left, ref line);
          }
          if (tree.left == null) line += "(";
          line += ",";
          
          if (tree.right != null)
          {
              line += Convert.ToString(tree.right.val);
              LeafsInLine(tree.right, ref line);
          }
          line += ")";
      }
      private void _printLine(BinaryTree<T> tree)
      {
          string line = "";
          //добавляем вершину
          line += Convert.ToString(tree.val);
          LeafsInLine(tree, ref line);
          Console.WriteLine(line);
      }
      public void PrintLine()
      {
          _printLine(this);
      }
  }
}