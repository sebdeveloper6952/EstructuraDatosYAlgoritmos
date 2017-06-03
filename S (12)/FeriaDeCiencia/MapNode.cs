using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDataStructures.Lists;

namespace MyDataStructures.Maps
{
    public class MapNode<T, K> : IEquatable<MapNode<T,K>>
    {
        protected T key;
        protected DLinkedList<K> list;

        public MapNode(T k, K v)
        {
            key = k;
            list = new DLinkedList<K>();
            list.AddLast(v);
        }

        public T GetKey() { return key; }
        public void Add(K value) { list.AddLast(value); }
        public DLinkedList<K> GetList() { return list; }

        public bool Equals(MapNode<T, K> other)
        {
            return other.GetKey().Equals(key);
        }
    }
}
