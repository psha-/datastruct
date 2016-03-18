using System;

namespace AvlTreeLab
{
    public class Node<T> where T : IComparable
    {
        private Node<T> _left;
        private Node<T> _right;

        public T Value { get; private set; }
        public Node<T> Parent { get; set; }
        public int BalanceFactor { get; set; }

        public Node<T> Left
        {
            get { return _left; }
            set
            {
                if (null != value)
                {
                    value.Parent = this;
                }
                _left = value;
            }
        }

        public Node<T> Right
        {
            get { return _right; }
            set
            {
                if (null != value)
                {
                    value.Parent = this;
                }
                _right = value;
            }
        }

        public bool IsLeft
        {
            get
            {
                return null != Parent && Parent.Left == this;
            }
        }

        public bool IsRight
        {
            get
            {
                return null != Parent && Parent.Right == this;
            }
        }

        public int ChildrenCount
        {
            get
            {
                var count = 0;
                if (null != Left)
                {
                    count++;
                }
                if (null != Right)
                {
                    count++;
                }
                return count;
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public Node(T val)
        {
            Value = val;
            BalanceFactor = 0;
        }

        public bool IsRoot
        {
            get
            {
                return null == Parent;
            }
        }

        public void RotateLeft()
        {
            var child = Right;
            if (!IsRoot)
            {
                if (IsLeft)
                {
                    Parent.Left = child;
                } else
                {
                    Parent.Right = child;
                }
            }
            else
            {
                child.Parent = null;
            }
            Right = child.Left;
            child.Left = this;
        }
        public void RotateRight()
        {
            var child = Left;
            if (!IsRoot)
            {
                if (IsLeft)
                {
                    Parent.Left = child;
                }
                else
                {
                    Parent.Right = child;
                }
            }
            else
            {
                child.Parent = null;
            }
            Left = child.Right;
            child.Right = this;
        }
    }
}

