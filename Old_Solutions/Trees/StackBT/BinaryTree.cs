using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Trees
{
  public class BinaryTree<T> where T : IComparable<T>
  {
    public BinaryTree<T> parent, left, right;
    public T val;
    
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
    private IEnumerable<BinaryTree<T>> _printStack(BinaryTree<T> tree)
    {
        var stack = new Stack<BinaryTree<T>>();
        stack.Push(tree);
 
        while(stack.Count > 0)
        {
            var n = stack.Pop();
            yield return n;
            if (n.right != null) stack.Push(n.right);
            if (n.left != null) stack.Push(n.left);
        }
    }
    public IEnumerable<BinaryTree<T>> PrintStack()
    {
        return _printStack(this);
    }
  }
}