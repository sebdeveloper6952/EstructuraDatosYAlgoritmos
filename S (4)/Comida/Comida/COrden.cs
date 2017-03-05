using System;
using System.Collections.Generic;
using System.Collections;

namespace Comida
{
    public class COrden : IEnumerable
    {
        private List<CItem> items;
        private List<int> cantidadItems;
        private float totalOrden;

        public COrden()
        {
            items = new List<CItem>();
            cantidadItems = new List<int>();
            totalOrden = 0f;
        }

        public List<CItem> GetItems() { return items; }
        public List<int> GetCantidad() { return cantidadItems; }

        public void AddItem(CItem item)
        {
            if (items.Contains(item))
            {
                int index = items.IndexOf(item);
                cantidadItems[index]++;
            }
            else
            {
                items.Add(item);
                int index = items.IndexOf(item);
                cantidadItems.Insert(index, 0);
                cantidadItems[index]++;
            }
            totalOrden += item.GetPrecio();
        }

        public void RemoveItem(int index)
        {
            totalOrden -= items[index].GetPrecio();
            cantidadItems[index]--;
            if(cantidadItems[index] == 0)
            {
                items.RemoveAt(index);
                cantidadItems.RemoveAt(index);
            }
        }

        public float GetTotalOrden() { return this.totalOrden; }

        public override string ToString()
        {
            string orden = "";
            foreach(CItem i in items)
            {
                int index = items.IndexOf(i);
                orden += cantidadItems[index] + "-" + i.GetNombre() + "\n";
            }
            return orden;
        }

        public IEnumerator GetEnumerator()
        {
            return new ItemEnumerator(items);
        }
    }
}
