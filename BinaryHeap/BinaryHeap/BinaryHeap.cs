using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        heap = new List<T>();
    }

    public BinaryHeap(T[] elements)
    {
        heap = new List<T>(elements);
        for (var i = heap.Count / 2; i >= 0; i-- )
        {
            HeapifyDown(i);
        }
    }

    public int Count
    {
        get
        {
            return heap.Count;
        }
    }

    public T ExtractMax()
    {
        var element = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        if (heap.Count > 0)
        {
            HeapifyDown(0);
        }
        return element;
    }

    public T PeekMax()
    {
        return heap[0];
    }

    public void Insert(T node)
    {
        heap.Add(node);
        HeapifyUp(heap.Count - 1);
    }

    private int Left(int i)
    {
        return 2 * i + 1;
    }

    private int Right(int i)
    {
        return 2 * i + 2;
    }

    private int Parent(int i)
    {
        return (i - 1) / 2;
    }

    private void HeapifyDown(int i)
    {
        var largest = i;
        if (Left(i) < heap.Count && heap[Left(i)].CompareTo(heap[largest]) > 0)
        {
            largest = Left(i);
        }
        if (Right(i) < heap.Count && heap[Right(i)].CompareTo(heap[largest]) > 0)
        {
            largest = Right(i);
        }
        if( largest != i )
        {
            var tmp = heap[largest];
            heap[largest] = heap[i];
            heap[i] = tmp;
            HeapifyDown(largest);
        }
    }

    private void HeapifyUp(int i)
    {
        while( i > 0 && heap[Parent(i)].CompareTo(heap[i]) < 0 )
        {
            var tmp = heap[i];
            heap[i] = heap[Parent(i)];
            heap[Parent(i)] = tmp;
            i = Parent(i);
        }
    }
}
