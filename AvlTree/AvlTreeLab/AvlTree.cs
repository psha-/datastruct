namespace AvlTreeLab
{
    using System;
    
    public class AvlTree<T> where T : IComparable
    {
        private Node<T> root;

        public int Count { get; private set; }
        
        public void Add(T item)
        {
            if( Contains(item))
            {
                return;
            }
            var newNode = new Node<T>(item);
            if ( 0 == Count )
            {
                root = newNode;
                Count++;
                return;
            }
            var node = root;
            while ( true )
            {
                if (item.CompareTo(node.Value) < 0)
                {
                    if (null == node.Left) {
                        node.Left = newNode;
                        newNode.Parent = node;
                        break;
                    }
                    node = node.Left;
                }
                else
                {
                    if (null == node.Right)
                    {
                        node.Right = newNode;
                        newNode.Parent = node;
                        break;
                    }
                    node = node.Right;
                }
            }
            Count++;
            Rebalance(node);
        }

        private void Rebalance(Node<T> node)
        {

        }

        public bool Contains(T item)
        {
            var node = root;
            while (null != node)
            {
                if (0 == item.CompareTo(node.Value))
                {
                    return true;
                }
                else if (item.CompareTo(node.Value) < 0)
                {
                    node = node.Left;
                }
                else
                {
                    node = node.Right;
                }
            }
            return false;
        }

        public void ForeachDfs(Action<int, T> action)
        {

        }
    }
}
