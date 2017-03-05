using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class CPersonaCompararFechaNacimiento : IComparer<CPersona>
    {
        public int Compare(CPersona x, CPersona y)
        {
            return x.fechaNacimiento.CompareTo(y.fechaNacimiento);
        }
    }
}
