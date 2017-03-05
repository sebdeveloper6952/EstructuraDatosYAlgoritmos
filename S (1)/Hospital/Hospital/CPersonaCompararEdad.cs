using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class CPersonaCompararEdad : IComparer<CPersona>
    {
        public int Compare(CPersona x, CPersona y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException("Null argument passed.");
            return x.edad.CompareTo(y.edad);
        }
    }
}
