using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTreeProject
{
    public class RedBlackTree<T> where T : IComparable<T>
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
                node.IsRed = false;
                return node;
            }
            else if (node.RightChild != null && node.RightChild.IsRed && node.LeftChild.IsRed)
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
                node = RotateLeft(node);
            }

            if(node.LeftChild.IsRed && node.LeftChild.LeftChild.IsRed)
            {
                node = RotateRight(node);
            }


            return node;
        }

        public void FlipColor(Node<T> node)
        {
            node.IsRed = !node.IsRed;
            node.LeftChild.IsRed = !node.LeftChild.IsRed;
            node.RightChild.IsRed = !node.RightChild.IsRed;
        }

        public Node<T> RotateLeft(Node<T> node)
        {
            bool wasRed = node.IsRed;
            Node<T> temp = node;
            node = node.RightChild;
            temp.RightChild = node.LeftChild;
            node.LeftChild = temp;
            node.IsRed = wasRed;
            temp.IsRed = true;

            return node;
        }

        public Node<T> RotateRight(Node<T> node)
        {
            bool wasRed = node.IsRed;
            Node<T> temp = node;
            node = node.LeftChild;
            temp.LeftChild = node.RightChild;
            node.RightChild = temp;
            node.IsRed = wasRed;
            temp.IsRed = true;

            return node;
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
                   node = MoveRedLeft(node);
                }
                node = RemoveHelper(node.LeftChild, value);
            }
            else
            {
                FlipColor(node);

                if(node.LeftChild != null && !node.LeftChild.IsRed)
                {
                   node = RotateRight(node);
                }

                if (!node.IsRed && node.RightChild != null && !node.RightChild.IsRed)
                {
                   node = MoveRedRight(node);
                }

                if (node.Value.Equals(value))
                {
                    if (node.LeftChild == null && node.RightChild == null)
                    {
                        node = null;
                        Count--;
                    }
                    else if (node.LeftChild == null)
                    {
                        node = Minimum(node.RightChild);
                        RemoveHelper(node.RightChild, node.Value);
                    }
                    else if (node.RightChild == null)
                    {
                        node = Maximum(node.LeftChild);
                        RemoveHelper(node.LeftChild, node.Value);
                    }
                }
                else
                {
                    node = RemoveHelper(node.RightChild, value);
                }
            }

            node = FixUp(node);
            return node;
        }

        public Node<T> MoveRedRight(Node<T> node)
        {
            if(!node.RightChild.IsRed)
            {
         
                FlipColor(node);
                if(node.LeftChild.LeftChild.IsRed)
                {
                    node = RotateRight(node);
                    FlipColor(node);
                }

            }

            return node;
        }

        public Node<T> MoveRedLeft(Node<T> node)
        {
            if (!node.RightChild.IsRed)
            {
                FlipColor(node);

                if (node.RightChild.LeftChild.IsRed)
                { 
                    node = RotateLeft(RotateRight(node));
                    FlipColor(node);
                }


                if(node.RightChild.RightChild.IsRed)
                {
                    node = RotateLeft(node);
                }
            }

            return node;
        }

        public Node<T> FixUp(Node<T> node)
        {
            if (node.RightChild.IsRed)
                    node = RotateRight(node);

            if(node.LeftChild.IsRed && node.LeftChild.LeftChild.IsRed)
            {
                node = RotateRight(node);
            }

            if(node.LeftChild.IsRed && node.RightChild.IsRed)
            {
                node = FixUp(node);
            }

            if(node.LeftChild.RightChild != null && node.LeftChild.LeftChild == null)
            {
                node.LeftChild = RotateRight(node.LeftChild);
                if (node.LeftChild.IsRed && node.LeftChild.LeftChild.IsRed)
                {
                    node = RotateRight(node);
                }   

            }

            return null;
        }

        public Node<T> Minimum(Node<T> current)
        {

            while (current.LeftChild != null)
            {
                current = current.LeftChild;
            }
            return current;
        }

        public Node<T> Maximum(Node<T> current)
        {
            while (current.RightChild != null)
            {
                current = current.RightChild;
            }
            return current;
        }
    }
}
