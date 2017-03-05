using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcerciseQueues
{
    class ListQueue<T> : IQueue<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int size;

        public ListQueue()
        {
            size = 0;
            head = tail = null;
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new EmptyQueueException("The queue is empty.");
            T item = head.GetItem();
            head = head.GetNext();
            size--;
            if (Size() == 0)
                tail = null;
            return item;
        }

        public void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>(item, null);
            if(Size() == 0)
            {
                head = tail = newNode;
            }
            else
            {
                tail.SetNext(newNode);
                tail = newNode;
            }
            size++;
        }

        public T Front()
        {
            if (IsEmpty())
                throw new EmptyQueueException("The queue is empty.");
            return head.GetItem();
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public int Size()
        {
            return size;
        }

        public override string ToString()
        {
            string s = "[";
            Node<T> n = head;
            while(n != null)
            {
                s += n.GetItem() + " ";
                n = n.GetNext();
            }
            s += "]";
            return s;
        }
    }
}
