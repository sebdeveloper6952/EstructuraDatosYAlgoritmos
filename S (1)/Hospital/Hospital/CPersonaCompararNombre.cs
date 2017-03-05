using System;
using System.Collections.Generic;

namespace Hospital
{
    public class CPersonaCompararNombre : IComparer<CPersona>
    {
        public int Compare(CPersona p1, CPersona p2)
        {
            if (p1 == null || p2 == null)
                throw new ArgumentNullException("Null argument passed.");
            return p1.nombre.CompareTo(p2.nombre);
        }
    }
}
