using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class CPersonaCompararRazon : IComparer<CPaciente>
    {
        public int Compare(CPaciente p1, CPaciente p2)
        {
            if (p1 == null || p2 == null)
                throw new ArgumentNullException("Null argument passed.");
            return p1.razon.CompareTo(p2.razon);
        }
    }
}
