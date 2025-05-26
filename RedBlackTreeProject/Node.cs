using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTreeProject
{
    public class Node<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public Node<T> LeftChild { get; set; }
        public Node<T> RightChild { get; set; }

        public bool IsRed { get; set; }

        public Node(T val)
        {
            Value = val;
            LeftChild = null;
            RightChild = null;
            IsRed = true;
        }


    }
}
