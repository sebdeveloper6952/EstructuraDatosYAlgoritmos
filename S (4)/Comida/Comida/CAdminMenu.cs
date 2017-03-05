using System;
using System.Collections.Generic;
using System.Collections;
using ControlVentas;
using System.IO;

namespace Comida
{
    public class CAdminMenu : IGuardarYCargar, IEnumerable
    {
        private List<CItem> items;

        public CAdminMenu()
        {
            items = new List<CItem>();
        }

        public List<CItem> GetItems() { return this.items; }

        public void AddItem(CItem item) { items.Add(item); }

        public CItem GetItem(int index) { return items[index]; }

        public void Guardar()
        {
            StreamWriter sw = File.CreateText("menu.txt");
            foreach(CItem i in items)
            {
                sw.WriteLine(i.ToSavingFormat());
            }
            sw.Flush();
            sw.Close();
        }

        public void Cargar()
        {
            if (File.Exists("menu.txt"))
            {
                string[] lines = File.ReadAllLines("menu.txt");
                foreach (string line in lines)
                {
                    string[] item = line.Split('$');
                    string nombre = item[0];
                    float precio = float.Parse(item[1]);
                    CItem i = new CItem(nombre, null, precio);
                    items.Add(i);
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new ItemEnumerator(items);
        }
    }
}
