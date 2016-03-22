namespace QuadTree.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QuadTree<T> where T : IBoundable
    {
        public const int DefaultMaxDepth = 5;

        public readonly int MaxDepth;

        private Node<T> root;

        public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
        {
            root = new Node<T>(0, 0, width, height);
            this.Bounds = this.root.Bounds;
            this.MaxDepth = maxDepth;
        }

        public int Count { get; private set; }

        public Rectangle Bounds { get; private set; }

        public bool Insert(T item)
        {
            if( !item.Bounds.IsInside(Bounds))
            {
                return false;
            }
            var node = root;
            int depth = 1;
            while( null != node.Children )
            {
                var quadrant = GetQuadrant(node, item.Bounds);
                if( -1 == quadrant )
                {
                    break;
                }
                node = node.Children[quadrant];
                depth++;
            }
            node.Items.Add(item);
            Split(node, depth);
            Count++;
            return true;
        }

        private void Split(Node<T> node, int depth)
        {
            if (!node.ShouldSplit || depth >= MaxDepth)
            {
                return;
            }

            node.Children = new Node<T>[] {
                new Node<T>(GetQuadrantBounds(node, 0)),
                new Node<T>(GetQuadrantBounds(node, 1)),
                new Node<T>(GetQuadrantBounds(node, 2)),
                new Node<T>(GetQuadrantBounds(node, 3))
            };

            for ( var i = 0; i<node.Items.Count; i++ )
            {
                var item = node.Items[i];
                var quadrant = GetQuadrant(node, item.Bounds);
                if( -1 != quadrant )
                {
                    node.Children[quadrant].Items.Add(item);
                    node.Items.RemoveAt(i);
                    i--;
                }
            }
            foreach( var child in node.Children )
            {
                Split(child, depth + 1);
            }
        }

        private static int GetQuadrant(Node<T> node, Rectangle bounds)
        {
            if (bounds.IsInside(GetQuadrantBounds(node, 0)))
            {
                return 0;
            }
            if (bounds.IsInside(GetQuadrantBounds(node, 1)))
            {
                return 1;
            }
            if (bounds.IsInside(GetQuadrantBounds(node, 2)))
            {
                return 2;
            }
            if (bounds.IsInside(GetQuadrantBounds(node, 3)))
            {
                return 3;
            }
            return -1;
        }

        private static Rectangle GetQuadrantBounds(Node<T> node, int quadrant)
        {
            int midX = node.Bounds.MidX;
            int midY = node.Bounds.MidY;
            int width = node.Bounds.Width;
            int height = node.Bounds.Height;

            // mid points are ints - the sum of two neighbor quadrants should be the complete node width/height.
            switch (quadrant)
            {
                case 0:
                    return new Rectangle(0, 0, midX, midY);
                case 1:
                    return new Rectangle(midX, 0, width - midX, midY);
                case 2:
                    return new Rectangle(0, midY, midX, height - midY);
                case 3:
                    return new Rectangle(midX, midY, width - midX, height - midY);
            }
            throw new InvalidOperationException("Quadrant must be 0-3");
        }

        public List<T> Report(Rectangle bounds)
        {
            var result = new List<T>();
            if (bounds.IsInside(Bounds))
            {
                GetCollisionCandidates(result, root, bounds);
            }
            return result;
        }

        private static void GetCollisionCandidates( List<T> result, Node<T> node, Rectangle bounds)
        {
            var quadrant = GetQuadrant(node, bounds);
            if (-1 != quadrant)
            {
                GetCollisionCandidates(result, node.Children[quadrant], bounds);
                result.AddRange(node.Items);
            }
            else {
                GetSubtreeCandidates(result, node, bounds);
            }
        }

        private static void GetSubtreeCandidates(List<T> result, Node<T> node, Rectangle bounds)
        {
            if( null != node.Children)
            {
                foreach( var child in node.Children )
                {
                    GetSubtreeCandidates(result, child, bounds);
                }
            }
            result.AddRange(node.Items);
        }

        public void ForEachDfs(Action<List<T>, int, int> action)
        {
            ForEachDfs(root, action);
        }

        private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth=1, int quadrant=0)
        {
            if( null == node )
            {
                return;
            }
            if( node.Items.Any())
            {
                action(node.Items, depth, quadrant);
            }
            if (null != node.Children)
            {
                for (var q=0; q<4; q++)
                {
                    ForEachDfs(node.Children[q], action, depth + 1, q);
                }
            }
        }
    }
}
