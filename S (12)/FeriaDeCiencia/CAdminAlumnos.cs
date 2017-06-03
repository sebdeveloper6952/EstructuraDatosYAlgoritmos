using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaDeCiencia
{
    public class CAdminAlumnos
    {
        public static CAdminAlumnos instance = new CAdminAlumnos();
        private List<CAlumno> alumnos;

        // constructor privado para el patron Singleton
        private CAdminAlumnos() { alumnos = new List<CAlumno>(); }

        public void AgregarAlumno(CAlumno a)
        {
            if (AlumnoYaExiste(a))
                throw new ArgumentException("Ya existe un alumno con ese numero de carne.");
            alumnos.Add(a);
        }

        public void EliminarAlumno(CAlumno a)
        {
            if (alumnos.Contains(a))
                alumnos.Remove(a);
        }

        public bool AlumnoYaExiste(CAlumno a)
        {
            foreach (CAlumno al in alumnos)
                if (a.Equals(al))
                    return true;
            return false;
        }

        public int Cantidad() { return alumnos.Count; }
    }
}
