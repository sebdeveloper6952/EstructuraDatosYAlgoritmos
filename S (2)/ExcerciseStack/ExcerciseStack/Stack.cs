using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcerciseStack
{
    class Stack<T>
    {
        private T[] entries;
        private const int maxStack = 10;
        private int count;

        public Stack()
        {
            entries = new T[maxStack];
            count = 0;
        }

        public void push(T item)
        {
            if(count < maxStack)
            {
                entries[count++] = item;
            }
        }

        public T pop()
        {
            if (count > 0)
                return entries[--count];
            return default(T);
        }

        public T top()
        {
            return entries[count];
        }

        public bool isEmpty()
        {
            return count == 0;
        }

        public override string ToString()
        {
            string s = "[ ";
            for(int i = count-1; i >= 0; i--)
            {
                s += entries[i] + " ";
            }
            s += "]";
            return s;
        }
    }
}
