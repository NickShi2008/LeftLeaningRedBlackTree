using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTreeProject
{
    class RedBlackTree<T> where T : IComparable<T>
    {
        Node<T> Root;

        public int Count { get; private set; }
        public RedBlackTree()
        {
            Root = null;
            Count = 0;
        }
        public void Add(T value)
        {
            Root = AddHelper(Root, value);
            Count++;
        }

        public Node<T> AddHelper(Node<T> node, T value)
        {
            if (node == null)
            {
                Node<T> current = new Node<T>(value);
                node = current;
                return node;
            }
            else if (node.RightChild.IsRed && node.LeftChild.IsRed)
            {
                FlipColor(node);
            }

            if (node.Value.CompareTo(value) < 0)
            {
                node = AddHelper(node.RightChild, value);
            }
            else
            {
                node = AddHelper(node.LeftChild, value);
            }

            if(node.RightChild.IsRed)
            {
                RotateLeft(node);
            }

            if(node.LeftChild.IsRed && node.LeftChild.LeftChild.IsRed)
            {
                RotateRight(node);
            }

            return node;
        }

        public void FlipColor(Node<T> node)
        {
            node.IsRed = !node.IsRed;
            node.LeftChild.IsRed = !node.LeftChild.IsRed;
            node.RightChild.IsRed = !node.RightChild.IsRed;
        }

        public void RotateLeft(Node<T> node)
        {
            bool wasRed = node.IsRed;
            Node<T> temp = node;
            node = node.RightChild;
            temp.RightChild = node.LeftChild;
            node.LeftChild = temp;
            node.IsRed = wasRed;
            temp.IsRed = true;
        }

        public void RotateRight(Node<T> node)
        {
            bool wasRed = node.IsRed;
            Node<T> temp = node;
            node = node.LeftChild;
            temp.LeftChild = node.RightChild;
            node.RightChild = temp;
            node.IsRed = wasRed;
            temp.IsRed = true;
        }

        public bool Remove(T val)
        {
            int startingCount = Count;
            if(Root == null)
            {
                return false;
            }

            Root = RemoveHelper(Root, val);
            return startingCount == Count;
        }

        public Node<T> RemoveHelper(Node<T> node, T value)
        {
            if(node.Value.CompareTo(value) > 0)
            {
                if (!node.IsRed && !node.LeftChild.IsRed)
                {
                    MoveRedLeft(node);
                }
                node = RemoveHelper(node.LeftChild, value);
            }
            else if (node.Value.CompareTo(value) >= 0)
            {
                FlipColor(node);

                if(node.LeftChild != null && !node.LeftChild.IsRed)
                {
                    RotateRight(node);
                }

                if (!node.IsRed && node.RightChild != null && !node.RightChild.IsRed)
                {
                    MoveRedRight(node);
                }

                if(node.LeftChild == null && node.RightChild == null && node.Value.Equals(value))
                {
                    node = null;
                    Count--;
                    return null;
                }
                else
                {
                    node = FindClosest(node);
                }
                node = RemoveHelper(node.RightChild, value);
            }

            return node;
        }

        public Node<T> MoveRedRight(Node<T> node)
        {
            return null;
        }

        public Node<T> MoveRedLeft(Node<T> node)
        {
            return null;
        }

        public void FixUp()
        {

        }

        private Node<T> FindClosest(Node<T> node)
        {
            Node<T> temp = node.LeftChild;
            while(node.RightChild != null)
            {
                temp = temp.RightChild;
            }


            return temp;
        }
    }
}
