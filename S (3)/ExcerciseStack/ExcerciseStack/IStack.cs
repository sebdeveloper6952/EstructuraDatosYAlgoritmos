using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcerciseStack
{
    interface IStack<T>
    {
        void Push(T item);
        T Pop();
        T Peek();
        bool IsEmpty();
        int Size();
    }
}
