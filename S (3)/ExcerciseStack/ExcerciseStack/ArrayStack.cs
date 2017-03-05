using ExcerciseStack;
using System;

namespace ExcerciseStack
{
    class ArrayStack<T> : IStack<T>
    {
        private int top;
        private int maxSize;
        private T[] stack;

        public ArrayStack()
        {
            top = -1;
            maxSize = 10;
            stack = new T[maxSize];
        }

        public bool IsEmpty()
        {
            return top < 0;
        }

        public T Peek()
        {
            if (top < 0)
                throw new EmptyStackException("Stack is empty.");
            return stack[top];
        }

        public T Pop()
        {
            if (top < 0)
                throw new EmptyStackException("Stack is empty.");
            T item = stack[top];
            stack[top--] = default(T);
            return item;
        }

        public void Push(T item)
        {
            if (top == maxSize - 1)
                throw new FullStackException("Stack is full.");
            stack[++top] = item;
        }

        public int Size()
        {
            return top + 1;
        }

        public override string ToString()
        {
            string s = "[";
            for(int i = 0; i <= top; i++)
            {
                s += stack[i] + " ";
            }
            s += "]";
            return s;
        }
    }
}
