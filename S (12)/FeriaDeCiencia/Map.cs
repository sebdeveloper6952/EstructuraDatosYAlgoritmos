using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDataStructures.Lists;

namespace MyDataStructures.Maps
{
    public class Map<T, K> : IMap<T, K>
    {
        protected int count;
        protected DLinkedList<MapNode<T, K>> list;

        public Map()
        {
            count = 0;
            list = new DLinkedList<MapNode<T, K>>();
        }

        public void Add(T key, K value)
        {
            MapNode<T, K> newNode = new MapNode<T, K>(key, value);
            if (count == 0)
            {
                list.AddLast(newNode);
                list.First().GetItem().Add(value);
            }
            else
            {
                // find the node that belongs to the key
                MapNode<T, K> cur = null;
                foreach (MapNode<T, K> n in list)
                {
                    if (key.Equals(n.GetKey()))
                    {
                        cur = n;
                        break;
                    }
                }
                if (cur == null)
                {
                    // key does not exist
                    list.AddLast(newNode);
                }
                else
                {
                    // we have the node, insert the value last
                    cur.Add(value);
                }
            }
            count++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns a list that contains the items that match the key.
        /// Empty list if the key is not found.</returns>
        public DLinkedList<K> Get(T key)
        {
            MapNode<T, K> cur = null;
            foreach(MapNode<T,K> n in list)
            {
                if(n.GetKey().Equals(key))
                {
                    cur = n;
                }
            }
            if(cur != null)
                return cur.GetList();
            return null;
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public K Remove(T key)
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            return count;
        }
    }
}
