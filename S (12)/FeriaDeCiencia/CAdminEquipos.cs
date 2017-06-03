using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FeriaDeCiencia
{
    public class CAdminEquipos
    {
        public static CAdminEquipos instance = new CAdminEquipos();

        private List<CEquipo> equipos;
        
        // constructor privado para el patron Singleton
        private CAdminEquipos() { equipos = new List<CEquipo>(); }

        public CAdminEquipos getInstance() { return instance; }

        public void AgregarEquipo(CEquipo e)
        {
            // solucionar que no se ingresen equipos repetidos
            equipos.Add(e);
        }

        public CEquipo ObtenerEquipo(int i)
        {
            if (i < 0 || i > equipos.Count - 1)
                throw new ArgumentException("El indice especificado es invalido!");
            return equipos[i];
        }

        public IList<CEquipo> ListaEquipos()
        {
            ReadOnlyCollection<CEquipo> dummy =
                new ReadOnlyCollection<CEquipo>(equipos);
            return dummy;
        }

        /// <summary>
        /// Valida que todos los equipos tengan al menos un integrante.
        /// </summary>
        /// <returns></returns>
        public bool ValidarEquipos()
        {
            foreach (CEquipo e in equipos)
                if (e.ObtenerIntegrantes().Count == 0)
                    return false;
            return true;
        }
    }
}
