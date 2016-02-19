using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
	public class Node<T>
	{
		public Node(T val)
		{
			this.val = val;
		}
		public T val { get; private set; }
		public Node<T> next;
		public Node<T> prev;
	}

	public int Count { get; private set; }

	public Node<T> Head { get; private set; }
	public Node<T> Tail { get; private set; }

    public void AddFirst(T element)
    {
		var node = new Node<T> (element);
		if (0 == Count) {
			Head = Tail = node;
		} else {
			node.next = Head;
			Head.prev = node;
			Head = node;
		}
		Count++;
    }

    public void AddLast(T element)
    {
		var node = new Node<T> (element);
		if (0 == Count) {
			Head = Tail = node;
		} else {
			node.prev = Tail;
			Tail.next = node;
			Tail = node;
		}
		Count++;
    }

    public T RemoveFirst()
    {
		if (0 == Count ) {
			throw new InvalidOperationException ();
		}
		T val = Head.val;
		if (1 == Count) {
			Head = Tail = null;
		} else {
			Head = Head.next;
			Head.prev = null;
		}
		Count--;
		return val;
    }

    public T RemoveLast()
    {
		if (0 == Count ) {
			throw new InvalidOperationException ();
		}
		T val = Tail.val;
		if (1 == Count) {
			Head = Tail = null;
		} else {
			Tail = Tail.prev;
			Tail.next = null;
		}
		Count--;
		return val;
    }

    public void ForEach(Action<T> action)
    {
		foreach (var node in this) {
			action (node);
		}
    }

    public IEnumerator<T> GetEnumerator()
    {
		var node = Head;
		while (null != node) {
			yield return node.val;
			node = node.next;
		}
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
		return GetEnumerator ();
    }

    public T[] ToArray()
    {
		T[] arr = new T[Count];
		int i = 0;
		foreach (var node in this) {
			arr [i++] = node;
		}
		return arr;
    }
}


class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
