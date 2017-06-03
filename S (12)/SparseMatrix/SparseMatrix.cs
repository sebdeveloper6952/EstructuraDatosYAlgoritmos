using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparseMatrix
{
    public class SparseMatrix
    {
        public int rows, columns;
        public int totalElements;

        private List<List<MEntry>> matrix;
        
        /// <summary>
        /// Constructor takes a matrix as a 2D array, and compresses it.
        /// </summary>
        /// <param name="mArray"></param>
        public SparseMatrix(int [,] mArray)
        {
            matrix = new List<List<MEntry>>();
            rows = mArray.GetLength(0);
            columns = mArray.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                matrix.Add(new List<MEntry>());
            }
            totalElements = 0;
            Compress(mArray);
        }

        /// <summary>
        /// Takes this SparseMatrix and converts it into a matrix represented
        /// as a 2D array.
        /// </summary>
        /// <returns></returns>
        public int[,] Decompress()
        {
            int[,] mArray = new int[rows, columns];
            int rowI = 0, colI = 0;
            foreach (List<MEntry> row in matrix)
            {
                foreach(MEntry e in row)
                {
                    int n = e.times;
                    while(n > 0)
                    {
                        mArray[rowI, colI] = e.value;
                        colI++;
                        n--;
                    }
                }
                rowI++;
                colI = 0;
            }
            return mArray;
        }

        /// <summary>
        /// Sets the value at index [row, col] to the new specified
        /// value.
        /// </summary>
        /// <param name="row">Row index of the value.</param>
        /// <param name="col">Column index of the value.</param>
        /// <param name="value">New value to set.</param>
        /// <returns>The old value at the specified index.</returns>
        public int Set(int row, int col, int value)
        {
            throw new Exception();
        }

        /// <summary>
        /// Gets the value stored in the matrix at index [row, col]
        /// </summary>
        /// <param name="row">Row of the value.</param>
        /// <param name="col">Column of the value.</param>
        /// <returns></returns>
        public int Get(int row, int col)
        {
            throw new Exception();
        }

        /// <summary>
        /// String representation of the compressed matrix.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";
            foreach(List<MEntry> row in matrix)
            {
                result += "[";
                foreach(MEntry e in row)
                {
                    result += e.value + "x" + e.times + " , ";
                }
                result += "]";
            }
            return result;
        }

        /// <summary>
        /// Turns a 2D array of integers into a compressed matrix.
        /// The compressed matrix is a list of lists of MEntry objects.
        /// </summary>
        /// <param name="mArray"></param>
        private void Compress(int[,] mArray)
        {
            totalElements = 0;
            for(int row = 0; row < rows; row++)
            {
                int times = 0;
                for(int col = 0; col < columns; col++)
                {
                    if(mArray[row,col] == 0)
                    {
                        times++;
                    }
                    else
                    {
                        if(times > 0)
                        {
                            MEntry e0 = new MEntry(times, 0);
                            matrix[row].Add(e0);
                            times = 0;
                            totalElements++;
                        }
                        MEntry e = new MEntry(1, mArray[row, col]);
                        matrix[row].Add(e);
                        totalElements++;
                    }
                }
                if(times > 0)
                {
                    MEntry e = new MEntry(times, 0);
                    matrix[row].Add(e);
                    totalElements++;
                }
            }
        }
    }
}
