using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    /// <summary>
    /// Clase de prueba, simula un empleado de alguna empresa.
    /// </summary>
    public class CEmpleado : IComparable, IEquatable<CEmpleado>
    {
        public string nombre;
        public string apellido;
        public int numEmpleado;

        public CEmpleado(string n, string a, int id)
        {
            nombre = n;
            apellido = a;
            numEmpleado = id;
        }

        public int CompareTo(object obj)
        {
            CEmpleado other = (CEmpleado)obj;
            return numEmpleado.CompareTo(other.numEmpleado);
        }

        public bool Equals(CEmpleado other)
        {
            return numEmpleado == other.numEmpleado;
        }
    }
}
