using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcerciseStack
{
    class Stack<T>
    {
        private int size;
        private int maxSize;
        private StackFrame<T> top;

        public Stack()
        {
            size = 0;
            maxSize = 10;
            top = null;
        }

        /// <summary>
        /// If the stack is not full, adds the specified item to the top of the stack.
        /// If it is full, it throws a FullStackException.
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            if (size == maxSize)
                throw new FullStackException("The stack is full.");
            top = new StackFrame<T>(item, top);
            size++;
        }

        /// <summary>
        /// If the stack is not empty, it removes the top frame from the stack.
        /// If it is empty, throws an EmptyStackException.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (IsEmpty())
                throw new EmptyStackException("Stack is empty.");
            T item = top.GetItem();
            top = top.GetNext();
            size--;
            return item;
        }

        /// <summary>
        /// If the stack is not empty, returns the top frame item.
        /// If it is empty, throws an EmptyStackException.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (IsEmpty())
                throw new EmptyStackException("Stack is empty.");
            return top.GetItem();
        }

        /// <summary>
        /// Returns the amount of frames in the stack.
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return size;
        }

        /// <summary>
        /// Returns true if the stack is empty, false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return top == null;
        }

        public override string ToString()
        {
            string s = "[";
            StackFrame<T> curr = top;
            while(curr != null)
            {
                s += curr.GetItem() + " ";
                curr = curr.GetNext();
            }
            s += "]";
            return s;
        }
    }
}
