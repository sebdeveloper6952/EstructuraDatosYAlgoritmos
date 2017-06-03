using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FeriaDeCiencia
{
    public class CEquipo
    {
        private string nombre;
        private string frase;
        private DateTime fechaReg;
        private List<CAlumno> integrantes;
        private List<CProyecto> proyectos;

        public CEquipo(string n, string f)
        {
            nombre = n;
            frase = f;
            fechaReg = DateTime.UtcNow;
            integrantes = new List<CAlumno>();
            proyectos = new List<CProyecto>();
        }

        public void AgregarIntegrante(CAlumno a)
        {
            integrantes.Add(a);
        }

        public void EliminarIntegrante(CAlumno a)
        {
            if (integrantes.Contains(a))
                integrantes.Remove(a);
        }

        public void AgregarProyecto(CProyecto p)
        {
            if(proyectos.Count >= 3)
                throw new ArgumentException("El equipo ya tiene 3 proyectos.");
            proyectos.Add(p);
        }

        public void EliminarProyecto(CProyecto p)
        {
            if (proyectos.Contains(p))
                proyectos.Remove(p);
        }

        public IList<CAlumno> ObtenerIntegrantes()
        {
            ReadOnlyCollection<CAlumno> dummy = 
                new ReadOnlyCollection<CAlumno>(integrantes);
            return dummy;
        }

        public IList<CProyecto> ObtenerProyectos()
        {
            ReadOnlyCollection<CProyecto> dummy =
                new ReadOnlyCollection<CProyecto>(proyectos);
            return dummy;
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}
