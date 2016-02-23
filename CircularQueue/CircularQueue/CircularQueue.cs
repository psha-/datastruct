using System;
using System.Collections.Generic;

public class CircularQueue<T>
{
    public int Count { get; private set; }

    private T[] arr; 

    public int Head { get; private set; }
    public int Tail { get; private set; }

    public CircularQueue(int capacity=1)
    {
        arr = new T[capacity];
        Head = Tail = 0;
    }

    public void Enqueue(T element)
    {
        if (Count>=arr.Length)
        {
            Grow();
        }
        arr[Tail] = element;
        Tail = (Tail + 1) % arr.Length;

        Count++;
    }

    public T Dequeue()
    {
        if (0 == Count)
        {
            throw new InvalidOperationException("This queue is empty");
        }
        var res = arr[Head];
        Head = (Head + 1)%arr.Length;
        arr[Head] = default(T);
        Count--;
        return res;
    }

    private void Grow()
    {
        var newArr = new T[arr.Length*2];
        CopyElementsTo(newArr);
        arr = newArr;
        Head = 0;
        Tail = Count;
    }

    private void CopyElementsTo(T[] newArr)
    {
        var j = Head;
        for (int i = 0; i < Count; i++)
        {
            newArr[i] = arr[j];
            j = (j + 1) % arr.Length;
        }
    }

    public T[] ToArray()
    {
        var newArr = new T[Count];
        var j = Head;
        for( int i = 0; i<Count; i++)
        {
            newArr[i] = arr[j];
            j = (j + 1)%arr.Length;
        }
        return newArr;
    }
}


class Example
{
    static void Main()
    {
        var queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        var first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
