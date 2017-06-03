using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaDeCiencia
{
    public class CAlumno
    {
        private string nombreCompleto;
        private string id;
        private string carrera;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Nombre completo.</param>
        /// <param name="i">ID del alumno.</param>
        /// <param name="c">Carrera que cursa.</param>
        public CAlumno(string n, string i, string c)
        {
            nombreCompleto = n;
            id = i;
            carrera = c;
        }

        public override string ToString()
        {
            return nombreCompleto;
        }

        public override bool Equals(object obj)
        {
            CAlumno other = (CAlumno)obj;
            return id == other.id;
        }
    }
}
