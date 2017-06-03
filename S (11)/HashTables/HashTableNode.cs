using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDataStructures;

namespace HashTables
{
    /// <summary>
    /// Nodo que almacena una llave y un objeto de datos.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class HashTableNode<IComparable, K> : Node<K>
    {
        private IComparable key;

        public HashTableNode(IComparable key, K data)
        {
            this.key = key;
            item = data;
        }

        public IComparable GetKey() { return key; }
    }
}
