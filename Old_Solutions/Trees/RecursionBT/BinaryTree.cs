using System;
using System.Collections.Generic;

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
                  if(this.left==null){
                      this.left = new BinaryTree<T>(val, this);
                  }
                  else if(this.left != null)
                      this.left.Add(val);
              }
              else if (val.CompareTo(this.val) == 0) 
                  return;
              else{
                  if(this.right==null){
                      this.right = new BinaryTree<T>(val, this);
                  }
                  else if(this.right != null)
                      this.right.Add(val);
              }
          }
          private void _printStraight(BinaryTree<T> tree)
          { 
              if(tree == null) return;
              Console.Write(tree.val + " ");
              _printStraight(tree.left); // обойти правое поддерево
              _printStraight(tree.right); // обойти левое поддерево
          }
          private void _printCenter(BinaryTree<T> tree)
          {
              if (tree == null) return;
              _printCenter(tree.left);
              Console.Write(tree.val + " ");
              _printCenter(tree.right);
          }
          private void _printReverse(BinaryTree<T> tree)
          {
              if (tree == null) return;
              _printReverse(tree.left);
              _printReverse(tree.right);
              Console.Write(tree.val + " ");
          }

          public void PrintStraight()
          {
              _printStraight(this);
          }

          public void PrintCenter()
          {
              _printCenter(this);
          }

          public void PrintReverse()
          {
              _printReverse(this);
          }
  }
}