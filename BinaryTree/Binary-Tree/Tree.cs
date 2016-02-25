using System;
using System.Collections.Generic;

public class Tree<T>
{
    private T Value;
    private IList<Tree<T>> children;

    public Tree(T v, params Tree<T>[] children)
    {
        Value = v;
        this.children = new List<Tree<T>>(children.Length);
        foreach (var child in children)
        {
            this.children.Add(child);
        }

    }

    public void Print(int indent = 0)
    {
        Console.Write(new string(' ', 2*indent));
        Console.WriteLine(Value);
        foreach (var child in children)
        {
            child.Print(indent+1);
        }
    }

    public void Each(Action<T> action)
    {
        action(Value);
        foreach (var child in children)
        {
            child.Each(action);
        }
    }
}
