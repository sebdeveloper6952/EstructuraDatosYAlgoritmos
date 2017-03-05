using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperacionesPila
{
    class ArrayStack<T>
    {
        private T[] stack;
        private int top;
        private int maxSize;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxSize">El tamaño de la pila.</param>
        public ArrayStack(int maxSize)
        {
            this.maxSize = maxSize;
            stack = new T[maxSize];
            top = -1;
        }

        /// <summary>
        /// Entrada: nada
        /// Salidas: Devuelve la cantidad de elementos en la pila.
        /// Poscondicion: Los datos permanecen sin cambio.
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return top + 1;
        }

        /// <summary>
        /// Entradas: el elemento de tipo T que se desea ingresar a la pila
        /// Salidas: Lanza una FullStackException si la pila esta llena.
        /// Poscondicion: Si la pila no esta llena, se agrega el elemento al top de la pila.
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            // revisar que el stack no este lleno
            if (Size() == (maxSize))
                throw new FullStackException("The stack is full.");
            else
            {
                stack[++top] = item;
            }

        }

        /// <summary>
        /// Entradas: ninguna.
        /// Salidas: Si la pila esta vacia, lanza una EmptyStackException. Si no esta vacia, 
        ///          remueve y devuelve el elemento que esta en la posicion top de la pila.
        /// Poscondicion: Si la pila no esta vacia, se remueve el elemento top de la pila.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            // revisar que la pila no este vacia
            if (Size() == 0)
                throw new EmptyStackException("The stack is empty.");
            else
            {
                return stack[top--];
            }
        }

        /// <summary>
        /// Entradas:ninguna.
        /// Salidas: Si la pila esta vacia, lanza una EmptyStackException. Si no
        ///          esta vacia, devuelve el elemento en la posicion top de la pila.
        /// Poscondicion: Los datos permanecen sin cambio.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            // revisar que la pila no este vacia
            if (Size() == 0)
                throw new EmptyStackException("The stack is empty.");
            else
                return stack[top];
        }
    }
}
