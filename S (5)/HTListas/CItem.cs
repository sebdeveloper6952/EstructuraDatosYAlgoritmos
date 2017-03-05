using System;

namespace Comida
{
    public class CItem
    {
        private string nombre;
        private string tooltip;
        private float precio;

        public CItem(string n, string t, float p)
        {
            nombre = n;
            tooltip = t;
            precio = p;
        }

        public string GetNombre()
        {
            return nombre;
        }

        public float GetPrecio()
        {
            return precio;
        }

        public void SetNombre(string n)
        {
            nombre = n;
        }

        public void SetPrecio(float p)
        {
            precio = p;
        }

        public override string ToString()
        {
            return nombre + " - " + precio.ToString("0.00");
        }

        public string ToSavingFormat()
        {
            return nombre + "$" + precio.ToString("0.00") + "$";
        }
    }
}
