using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class CAsistencia : IComparable
    {
        private DateTime fecha;
        public string fechaString;
        public string observacion;

        public CAsistencia(DateTime f, string o)
        {
            fecha = f;
            fechaString = fecha.ToShortDateString();
            observacion = o;
        }

        public CAsistencia(string f, string o)
        {
            string[] array = f.Split('/');
            int month = int.Parse(array[0]);
            int day = int.Parse(array[1]);
            int year = int.Parse(array[2]);
            fecha = new DateTime(year, month, day);
            fechaString = fecha.ToShortDateString();
            observacion = o;
        }

        /// <summary>
        /// Separar asistencias con un # y separar fecha y observacion usando
        /// un _
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return fechaString + "_" + observacion + "#";
        }

        public int CompareTo(object obj)
        {
            CAsistencia a = obj as CAsistencia;
            if (a == null)
                throw new ArgumentNullException("Null argument passed.");
            return fecha.CompareTo(a.fecha);
        }
    }
}
