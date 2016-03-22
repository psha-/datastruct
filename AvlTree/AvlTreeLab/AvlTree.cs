using System.Collections.Generic;

namespace AvlTreeLab
{
    using System;
    
    public class AvlTree<T> where T : IComparable
    {
        private Node<T> root;

        public int Count { get; private set; }
        
        public void Add(T item)
        {
            var newNode = new Node<T>(item);
            if (0 == Count)
            {
                root = newNode;
                Count++;
                return;
            }
            if ( Contains(item))
            {
                return;
            }
            var node = root;
            while ( true )
            {
                if (item.CompareTo(node.Value) < 0)
                {
                    if (null == node.Left) {
                        node.Left = newNode;
                        node.BalanceFactor++;
                        node.LeftCount = 1;
                        break;
                    }
                    node = node.Left;
                }
                else
                {
                    if (null == node.Right)
                    {
                        node.Right = newNode;
                        node.BalanceFactor--;
                        break;
                    }
                    node = node.Right;
                }
            }
            Count++;
            Retrace(node);
        }

        private void Retrace(Node<T> node)
        {
            var balanced = false;
            var parent = node.Parent;
            for ( ; !node.IsRoot; node=parent, parent = node.Parent )
            {
                if( node.IsLeft )
                {
                    parent.LeftCount++;
                }
                if (balanced || 0 == node.BalanceFactor)
                {
                    balanced = true;
                    continue;
                }
                parent.BalanceFactor += node.IsLeft ? 1 : -1;
                if (Math.Abs(parent.BalanceFactor) > 1)
                {
                    if (node.IsLeft)
                    {
                        if (-1 == node.BalanceFactor)
                        {
                            // Left Right case
                            node.RotateLeft();
                        }
                        if (parent.IsRoot)
                        {
                            root = parent.Left;
                        }
                        parent.RotateRight();
                        parent.LeftCount++;
                    }
                    else if (node.IsRight)
                    {
                        if ( 1 == node.BalanceFactor) {
                            // Right left case
                            node.RotateRight();
                        }
                        if (parent.IsRoot)
                        {
                            root = parent.Right;
                        }
                        parent.RotateLeft();
                    }
                    parent.BalanceFactor = 0;
                    node.BalanceFactor = 0;
                    // No need to retrace the parent. It was rotated and it's new parent was traced already as his child.
                    parent = parent.Parent;
                }
            }
        }

        public T this[int index]
        {
            get
            {
                if (index > Count - 1)
                {
                    throw new IndexOutOfRangeException();
                }
                var node = root;
                while(index != node.LeftCount) {
                    if( index > node.LeftCount )
                    {
                        index -= node.LeftCount + 1;
                        node = node.Right;
                    } else
                    {
                        node = node.Left;
                    }
                }
                return node.Value;
            }
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
            if (0 == Count)
            {
                return;
            }
            InorderDFS(root, 1, action);
        }

        private void InorderDFS( Node<T> node, int depth, Action<int, T> action)
        {
            if (null != node.Left)
            {
                InorderDFS(node.Left, depth+1, action);
            }
            action(depth, node.Value);
            if (null != node.Right)
            {
                InorderDFS(node.Right, depth + 1, action);
            }
        }

        public void Range(T from, T to)
        {
            RangeDFS(root, from, to);
        }

        private void RangeDFS(Node<T> node, T from, T to)
        {
            if (null == node)
            {
                return;
            }
            if (from.CompareTo(node.Value) < 0)
            {
                RangeDFS(node.Left, from, to);
            }
            if (from.CompareTo(node.Value) <= 0 && to.CompareTo(node.Value) >= 0)
            {
                Console.Write(node.Value + " ");
            }
            if (to.CompareTo(node.Value) > 0 )
            {
                RangeDFS(node.Right, from, to);
            }

        }
    }
}
