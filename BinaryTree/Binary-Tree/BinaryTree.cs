using System;

public class BinaryTree<T>
{
    public T Value;
    public BinaryTree<T> Left { get; private set; }
    public BinaryTree<T> Right { get; private set; }

    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        Value = value;
        Left = leftChild;
        Right = rightChild;
    }

    public void PrintIndentedPreOrder(int indent = 0)
    {
        Console.Write(new string(' ', 2*indent));
        Console.WriteLine(Value);
        if (Left != null)
        {
            Left.PrintIndentedPreOrder(indent + 1);
        }
        if (Right != null)
        {
            Right.PrintIndentedPreOrder(indent + 1);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        if (Left != null)
        {
            Left.EachInOrder(action);
        }
        action(Value);
        if (Right != null)
        {
            Right.EachInOrder(action);
        }
    }


    public void EachPostOrder(Action<T> action)
    {
        if (Left != null)
        {
            Left.EachPostOrder(action);
        }
        if (Right != null)
        {
            Right.EachPostOrder(action);
        }
        action(Value);
    }
}
