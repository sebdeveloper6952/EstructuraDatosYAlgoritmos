using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcerciseQueues
{
    class Node<T>
    {
        private T item;
        private Node<T> next;

        public Node(T item, Node<T> next)
        {
            this.item = item;
            this.next = next;
        }

        public void SetItem(T item)
        {
            this.item = item;
        }

        public T GetItem()
        {
            return item;
        }

        public void SetNext(Node<T> next)
        {
            this.next = next;
        }

        public Node<T> GetNext()
        {
            return next;
        }
    }
}
