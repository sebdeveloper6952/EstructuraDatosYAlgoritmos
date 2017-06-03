using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FeriaDeCiencia
{
    // enum para las 3 categorias de proyectos
    public enum ECategoria { Educacion, Salud, Comunicacion}

    public class CProyecto
    {
        public string nombre { get; private set; }
        public string descripcion { get; private set; }
        public ECategoria categoria { get; private set; }
        public CEquipo equipo { get; private set; }
        public BitmapImage image { get; private set; }

        public CProyecto(CEquipo e, string n, string d, int c)
        {
            equipo = e;
            nombre = n;
            descripcion = d;
            categoria = (ECategoria)c;
        }

        public void SetImage(BitmapImage i) { image = i; }

        public override string ToString()
        {
            return nombre;
        }

        public override int GetHashCode()
        {
            int n = nombre.GetHashCode();
            n += descripcion.GetHashCode();
            n += categoria.GetHashCode();
            return n.GetHashCode();
        }
    }
}
