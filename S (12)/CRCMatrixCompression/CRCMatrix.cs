using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCMatrixCompression
{
    public class CRCMatrix
    {
        private int[] arrNonZero, arrZero, arrIndex;
        private int rows, cols;

        public CRCMatrix(int[,] mArray)
        {
            rows = mArray.GetLength(0);
            cols = mArray.GetLength(1);
        }

        /// <summary>
        /// Reads the 2D array to fill this CRCMatrix object.
        /// </summary>
        private void Initialize(int[,] mArray)
        {

        }
    }
}
