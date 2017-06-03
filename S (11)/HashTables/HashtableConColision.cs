using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDataStructures.Interfaces;
using MyDataStructures.Lists;
using MyDataStructures;

namespace HashTables
{
    /// <summary>
    /// Hashtable que acepta colisiones. Se utiliza un arreglo de listas
    /// enlazadas de HashTableNode como estructura interna de almacenamiento.
    /// El HashTableNode es la estructura que nos permite busqueda y eliminacion
    /// de un unico elemento. El usuario especifica que dato desea usar como
    /// clave.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    public class HashtableConColision<T, K>
    {
        private DLinkedList<HashTableNode<T,K>>[] array;
        private int arraySize;
        private int count;

        public HashtableConColision(int initialSize)
        {
            arraySize = initialSize;
            array = new DLinkedList<HashTableNode<T,K>>[arraySize];
            count = 0;
        }

        public void Add(T key, K data)
        {
            if (HasKey(key))
                throw new ArgumentException("That key already exists!");
            int index = Hash(key);
            if (array[index] == null)
                array[index] = new DLinkedList<HashTableNode<T, K>>();
            HashTableNode<T, K> newNode = new HashTableNode<T, K>(key, data);
            array[index].AddLast(newNode);
            count++;
        }

        public K Get(T key)
        {
            int index = Hash(key);
            if (array[index] == null)
                throw new ArgumentException("No such key.");
            DLinkedList<HashTableNode<T, K>> list = array[index];
            foreach(HashTableNode<T, K> n in list)
            {
                if (key.Equals(n.GetKey()))
                    return n.GetItem();
            }
            throw new ArgumentException("No such key.");
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public void Remove(T key)
        {
            int index = Hash(key);
            if (array[index] == null)
                throw new ArgumentException("No such key.");
            DLinkedList<HashTableNode<T, K>> list = array[index];
            foreach(HashTableNode<T, K> n in list)
            {
                if(n.GetKey().Equals(key))
                {
                    list.Remove(n);
                    if (list.Size() == 0)
                        array[index] = null;
                    break;
                }
            }
        }

        public int Size()
        {
            return count;
        }

        /// <summary>
        /// Toma una llave y calcula y devuelve el indicc
        /// del arrgleo en el cual se debe insertar el elemento.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int Hash(T key)
        {
            int n = key.GetHashCode();
            return n % arraySize;
        }

        private bool HasKey(T key)
        {
            int index = Hash(key);
            if (array[index] == null)
                return false;
            DLinkedList<HashTableNode<T, K>> list = array[index];
            foreach(HashTableNode<T, K> n in list)
            {
                if (n.GetKey().Equals(key))
                    return true;
            }
            return false;
        }
    }
}
