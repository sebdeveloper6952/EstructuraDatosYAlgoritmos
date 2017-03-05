using System;
using System.Collections.Generic;
using ControlVentas;

namespace Hospital
{
    public class CPaciente : CPersona
    {
        #region Private Variables
        public string razon { get; private set; }
        public ETipoDeSangre sangre { get; private set; }
        public string sangreS { get; private set; }
        //public List<string> asistencias { get; private set; }
        public List<CAsistencia> asistencias { get; private set; }
        public string asisReciente { get; private set; }
        public int cantAsistencias { get; private set; }
        #endregion


        #region Constructors
        public CPaciente(string nombre, string apellido, string dpi, DateTime f, string razon, 
            ETipoDeSangre sangre, List<CAsistencia> asistencias)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dpi = dpi;
            fechaNacimiento = f;
            fechaNacS = fechaNacimiento.ToShortDateString();
            this.razon = razon;
            this.sangre = sangre;
            this.sangreS = CUtilities.StringValueOf(sangre);
            CalcularEdad();
            this.asistencias = asistencias;
            cantAsistencias = asistencias.Count;
            asisReciente = asistencias[cantAsistencias - 1].fechaString;
        }
        #endregion

        #region Public Methods
        public string GetRazon()
        {
            return razon;
        }

        public void SetRazon(string s)
        {
            razon = s;
        }

        public ETipoDeSangre GetSangre()
        {
            return sangre;
        }

        public void SetSangre(ETipoDeSangre s)
        {
            sangre = s;
        }

        public string GetSangreAsString()
        {
            return sangreS;
        }

        public void AgregarAsistencia(DateTime f, string observacion)
        {
            CAsistencia a = new CAsistencia(f, observacion);
            asistencias.Add(a);
            asistencias.Sort();
            asisReciente = asistencias[asistencias.Count - 1].fechaString;
            cantAsistencias = asistencias.Count;
        }

        /// <summary>
        /// Formato para Guardar
        /// nombre,apellido,dpi,fechaNacimiento.Year,fechaNacimiento.Month,
        /// fechaNacimiento.Day, razon, tipoDeSangre
        /// </summary>
        /// <returns></returns>
        public string ToSaveString()
        {
            string output = nombre + "$" + apellido + "$" + dpi + "$" + fechaNacimiento.Year + "$"
                + fechaNacimiento.Month + "$" + fechaNacimiento.Day + "$" + razon + "$" + (int)sangre + "$";
            foreach (CAsistencia a in asistencias)
                output += a;
            return output;
        }
        #endregion
    }
}
