using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDataStructures.Lists;

namespace MyDataStructures.Maps
{
    interface IMap<T, K>
    {
        void Add(T key, K value);
        DLinkedList<K> Get(T key);
        K Remove(T key);
        int Size();
        bool IsEmpty();
    }
}
