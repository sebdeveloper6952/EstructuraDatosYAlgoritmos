using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerParcial
{
    public class CAdminJuguetes
    {
        private List<CJuguete> _listaJuguetes;

        public List<CJuguete> listaJuguetes
        {
            get
            {
                return _listaJuguetes;
            }
        }

        public CAdminJuguetes()
        {
            _listaJuguetes = new List<CJuguete>();
        }

        public void AgregarJuguete(CJuguete j)
        {
            _listaJuguetes.Add(j);
        }

        public void RemoverJuguete(int index)
        {
            _listaJuguetes.RemoveAt(index);
        }
    }
}
