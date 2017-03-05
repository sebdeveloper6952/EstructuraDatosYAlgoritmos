using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comida
{
    public class CAdminCocina
    {
        private Queue<COrden> ordenes;

        public CAdminCocina()
        {
            ordenes = new Queue<COrden>();
        }

        public COrden GetFirstOrder()
        {
            if (ordenes.Count == 0)
                throw new NoMoreOrdersException("No hay mas ordenes.");
            return ordenes.Peek();
        }

        public void AddOrder(COrden order) { ordenes.Enqueue(order); }

        public COrden RemoveFirstOrder()
        {
            if (ordenes.Count == 0)
                throw new NoMoreOrdersException("No hay mas ordenes.");
            return ordenes.Dequeue();
        }

        public bool OrdersAvailable() { return ordenes.Count > 0; }
    }
}
