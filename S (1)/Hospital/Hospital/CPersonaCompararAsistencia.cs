using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class CPersonaCompararAsistencia : IComparer<CPaciente>
    {
        public int Compare(CPaciente x, CPaciente y)
        {
            if(x == null || y == null)
                throw new ArgumentNullException("Null argument passed.");
            // negamos el resultado para obtener un orden descendente
            return -x.cantAsistencias.CompareTo(y.cantAsistencias);
        }
    }
}
