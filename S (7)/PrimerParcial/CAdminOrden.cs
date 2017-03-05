using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerParcial
{
    public class CAdminOrden
    {
        public List<CJuguete> ordenJuguetes { get; private set; }

        private CJuguete[] ordenArray;
        private Stack<CJuguete> stack;

        public CAdminOrden()
        {
            ordenJuguetes = new List<CJuguete>();
            stack = new Stack<CJuguete>();
        }

        public void AgregarJugueteOrden(CJuguete j)
        {
            ordenJuguetes.Add(j);
        }

        public void RemoverJugueteOrden(int index)
        {
            ordenJuguetes.RemoveAt(index);
        }
        
        public void LimpiarOrden()
        {
            ordenJuguetes.Clear();
        }

        public bool ValidarOrden()
        {
            ordenJuguetes.Sort();
            stack.Clear();
            ordenArray = new CJuguete[ordenJuguetes.Count];
            ordenJuguetes.CopyTo(ordenArray);
            /* Si despues de ordenar la lista el primer objeto es
             * de tipo derecho, quiere decir que las mitades no 
             * corresponden. */
            if (ordenArray[0].tipo == ETIPO.derecha)
                return false;
            /* loopear por cada juguete, revisando si es izquierdo 
            o derecho. */
            foreach(CJuguete j in ordenArray)
            {
                if(j.tipo == ETIPO.izquierda)
                {
                    // si es izquierdo lo pusheamos
                    stack.Push(j);
                }
                else if(j.tipo == ETIPO.derecha)
                {
                    /* si es derecho y el nombre concuerda con el 
                     * nombre del top del stack, lo popeamos */
                     try
                    {
                        if (j.nombre == stack.Peek().nombre)
                        {
                            stack.Pop();
                        }
                    }
                    /* este catch atrapa la excepcion de un Stack
                     * vacio, lo cual quiere decir que aun hay elementos
                     * de tipo derecho pero ya no hay izquierdos. */
                    catch { return false; }
                }
            }
            // si al final el stack esta vacio, las mitades concuerdan
            return stack.Count == 0;
        }
    }
}
