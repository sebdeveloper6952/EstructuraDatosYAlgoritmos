using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerParcial
{
    public enum ETIPO { izquierda, derecha, unico }
    public class CJuguete : IComparable<CJuguete>
    {
        private int orden;
        public string nombre { get; private set; }
        public ETIPO tipo { get; private set; }

        public CJuguete(int orden, string nombre, ETIPO tipo)
        {
            this.orden = orden;
            this.nombre = nombre;
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return nombre + " " + tipo.ToString();
        }

        public int CompareTo(CJuguete other)
        {
            return orden.CompareTo(other.orden);
        }
    }
}
