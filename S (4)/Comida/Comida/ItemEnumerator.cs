using System;
using System.Collections;
using System.Collections.Generic;

namespace Comida
{
    public class ItemEnumerator : IEnumerator
    {
        private List<CItem> items;
        private int index;

        public ItemEnumerator(List<CItem> items)
        {
            index = -1;
            this.items = items;
        }

        public object Current
        {
            get
            {
                try
                {
                    return items[index];
                }
                catch(IndexOutOfRangeException e) { throw new InvalidOperationException("Index out of range."); }
            }
        }

        public bool MoveNext()
        {
            index++;
            return (index < items.Count);
        }

        public void Reset() { index = -1; }
    }
}
