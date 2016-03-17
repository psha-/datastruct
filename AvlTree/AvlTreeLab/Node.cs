using System;

namespace AvlTreeLab
{
    public class Node<T> where T : IComparable
    {
        public T Value { get; private set; }
        public Node<T> Left { get; set;}
        public Node<T> Right { get; set;}
        public Node<T> Parent { get; set; }
        public int BalanceFactor { get; set; }

        public Node(T val)
        {
            Value = val;
            BalanceFactor = 0;
        }

        private void UpdateCount()
        {
            if (null != Left)
            {
                BalanceFactor -= Left.BalanceFactor;
            }
            if (null != Right)
            {
                BalanceFactor += Right.BalanceFactor;
            }
        }
    }
}

