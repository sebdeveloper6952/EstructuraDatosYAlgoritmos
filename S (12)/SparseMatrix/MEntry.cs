using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparseMatrix
{
    public class MEntry
    {
        public int value;
        public int times;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t">How many times is the value repeated.</param>
        /// <param name="v">Value for this entry.</param>
        public MEntry(int t, int v)
        {
            value = v;
            times = t;
        }
    }
}
