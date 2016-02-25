using System;
using System.Collections.Generic;

public class Tree<T>
{
    public T Value;
    public IList<Tree<T>> Children { get; private set; }

    public Tree(T v, params Tree<T>[] children)
    {
        Value = v;
        this.Children = new List<Tree<T>>(children.Length);
        foreach (var child in children)
        {
            this.Children.Add(child);
        }

    }

    public void Print(int indent = 0)
    {
        Console.Write(new string(' ', 2*indent));
        Console.WriteLine(Value);
        foreach (var child in Children)
        {
            child.Print(indent+1);
        }
    }

    public void Each(Action<T> action)
    {
        action(Value);
        foreach (var child in Children)
        {
            child.Each(action);
        }
    }
}
