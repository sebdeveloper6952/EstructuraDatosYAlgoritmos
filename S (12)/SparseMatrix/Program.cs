using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparseMatrix
{
    public class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int[,] matrix = new int[10,10];
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    int prob = rand.Next(10);
                    if(prob > 7)
                    {
                        int n = rand.Next(1, 101);
                        matrix[r, c] = n;
                    }
                    else
                    {
                        matrix[r, c] = 0;
                    }
                }
            }
            Console.WriteLine(Array2DToString(matrix));
            SparseMatrix m = new SparseMatrix(matrix);
            int oElements = matrix.GetLength(0) * matrix.GetLength(1);
            Console.WriteLine("Original matrix number of elements: " + oElements);
            Console.WriteLine("Sparse matrix number of elements: " + m.totalElements);
            Console.WriteLine("Compressed representation:");
            Console.WriteLine(m);
            Console.WriteLine("Taking SparseMatrix and decompressing it...");
            Console.WriteLine(Array2DToString(m.Decompress()));
            Console.ReadLine();
        }

        public static string Array2DToString(int[,] array)
        {
            string s = "";
            for(int r = 0; r < array.GetLength(0); r++)
            {
                s += "[";
                for(int c = 0; c < array.GetLength(1); c++)
                {
                    s += array[r, c] + ",";
                }
                s += "]" + "\n";
            }
            return s;
        }
    }
}
