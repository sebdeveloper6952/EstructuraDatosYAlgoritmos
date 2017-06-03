using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDataStructures.Hashtable;
using MyDataStructures.Lists;
using System.Collections.ObjectModel;
using MyDataStructures.Maps;

namespace FeriaDeCiencia
{
    public class CAdminBusqueda
    {
        public static CAdminBusqueda instance = new CAdminBusqueda();
        public ReadOnlyCollection<CProyecto> resBus;

        private Map<string, CProyecto> mapa;

        // constructor privado para el patron Singleton
        private CAdminBusqueda()
        {
            mapa = new Map<string, CProyecto>();
            resBus = new ReadOnlyCollection<CProyecto>(new List<CProyecto>());
        }

        /// <summary>
        /// Devuelve una lista con todos los proyectos que fueron encontrados
        /// utilizando los criterios de busqueda especificados.
        /// </summary>
        /// <param name="palCla">Lista de palabras clave que se utilizan como
        /// criterio de busqueda.</param>
        /// <returns></returns>
        public List<CProyecto> BuscarProyecto(string[] palCla)
        {
            List<CProyecto> results = new List<CProyecto>();
            DLinkedList<CProyecto> temp = new DLinkedList<CProyecto>();
            foreach(string s in palCla)
            {
                temp = mapa.Get(s);
                if (temp != null)
                {
                    foreach (CProyecto p in temp)
                    {
                        if (results.Contains(p))
                            continue;
                        results.Add(p);
                    }
                }
            }
            resBus = new ReadOnlyCollection<CProyecto>(results);
            return results;
        }

        public void AgregarProyecto(string[] sA, CProyecto p)
        {
            foreach(string s in sA)
                mapa.Add(s, p);
        }
    }
}
