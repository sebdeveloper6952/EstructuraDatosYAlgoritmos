using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class CPersonaCompararDPI : IComparer<CPersona>
    {
        public int Compare(CPersona p1, CPersona p2)
        {
            if (p1 == null || p2 == null)
                throw new ArgumentNullException("Null argument passed.");
            return p1.dpi.CompareTo(p2.dpi);
        }
    }
}
