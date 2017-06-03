using System.Collections.Generic;
using ControlVentas;
using System.IO;
using System;

namespace Hospital
{
    public class CAdminPacientes : IGuardarYCargar
    {
        #region Singleton
        public static CAdminPacientes instance;
        #endregion

        #region Private Variables
        private List<CPaciente> listaPacientes;
        #endregion

        #region Constructors
        public CAdminPacientes()
        {
            this.listaPacientes = new List<CPaciente>();
            if (instance == null)
                instance = this;
        }
        #endregion

        #region Public Methods
        public List<CPaciente> GetListaPacientes()
        {
            List<CPaciente> copy = new List<CPaciente>();
            foreach (CPaciente p in listaPacientes)
                copy.Add(p);
            return copy;
        }

        /// <summary>
        /// Agrega un nuevo paciente a la lista de pacientes.
        /// </summary>
        /// <param name="p">El objeto paciente que se desea agregar a la lista.</param>
        public void AgregarPaciente(CPaciente p)
        {
            listaPacientes.Add(p);
        }

        /// <summary>
        /// Elimina el paciente de la lista de pacientes.
        /// </summary>
        /// <param name="p">El objeto paciente que se desea eliminar de la lista.</param>
        public void EliminarPaciente(CPaciente p)
        {
            if(p != null)
                listaPacientes.Remove(p);
        }

        /// <summary>
        /// Elimina al paciente especificado de la lista de pacientes.
        /// </summary>
        /// <param name="index">El indice en la lista del paciente que se desea eliminar.</param>
        public void EliminarPaciente(int index)
        {
            if(index >= 0)
                listaPacientes.RemoveAt(index);
        }

        public CPaciente GetPaciente(int index)
        {
            if (index > listaPacientes.Count)
                throw new IndexOutOfRangeException("Index out of range.");
            return listaPacientes[index];
        }

        /// <summary>
        /// Busca pacientes en la lista de pacientes, dependiendo de los criterios de
        /// busqueda que el usuario especifique. Esta feo pero no se como usar LINQ o 
        /// bases de datos :/. Por ahora supongo que le hace ganas.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="sangre"></param>
        /// <returns></returns>
        public List<CPaciente> BuscarPacientes(string nombre, string apellido, ETipoDeSangre sangre)
        {
            List<CPaciente> subconjunto = new List<CPaciente>();
            // ningun filtro
            if (nombre == string.Empty && apellido == string.Empty && sangre == ETipoDeSangre.Cualquiera)
            {
                foreach (CPaciente p in listaPacientes)
                    subconjunto.Add(p);
            }
            // por nombre
            else if (nombre != string.Empty && apellido == string.Empty && sangre == ETipoDeSangre.Cualquiera)
            {
                foreach (CPaciente p in listaPacientes)
                {
                    if (p.nombre == nombre)
                        subconjunto.Add(p);
                }
            }
            // por nombre y sangre
            else if (nombre != string.Empty && apellido == string.Empty && sangre != ETipoDeSangre.Cualquiera)
            {
                foreach (CPaciente p in listaPacientes)
                {
                    if (p.nombre == nombre && p.GetSangre() == sangre)
                        subconjunto.Add(p);
                }
            }

            // por apellido
            else if (nombre == string.Empty && apellido != string.Empty && sangre == ETipoDeSangre.Cualquiera)
            {
                foreach (CPaciente p in listaPacientes)
                {
                    if (p.apellido == apellido)
                        subconjunto.Add(p);
                }
            }
            // por apellido y sangre
            else if (nombre == string.Empty && apellido != string.Empty && sangre != ETipoDeSangre.Cualquiera)
            {
                foreach (CPaciente p in listaPacientes)
                {
                    if (p.apellido == apellido && p.GetSangre() == sangre)
                        subconjunto.Add(p);
                }
            }
            // por tipo de sangre
            else if (nombre == string.Empty && apellido == string.Empty)
            {
                foreach (CPaciente p in listaPacientes)
                {
                    if (p.GetSangre() == sangre)
                        subconjunto.Add(p);
                }
            }
            // por nombre, apellido y sangre
            else
            {
                foreach (CPaciente p in listaPacientes)
                {
                    if (p.nombre == nombre && p.apellido == apellido && p.GetSangre() == sangre)
                        subconjunto.Add(p);
                }
            }
           return subconjunto;
        }

        /// <summary>
        /// Regresa la cantidad de pacientes que hay actualmente ingresados.
        /// </summary>
        /// <returns></returns>
        public uint CantidadPacientes()
        {
            return (uint)listaPacientes.Count;
        }

        /// <summary>
        /// Guarda a los pacientes en un archivo de texto.
        /// </summary>
        public void Guardar()
        {
            File.Delete(CUtilities.SAVE_FILE_NAME);
            TextWriter tw = File.AppendText(CUtilities.SAVE_FILE_NAME);
            foreach(CPaciente p in listaPacientes)
            {
                tw.WriteLine(p.ToSaveString());
            }
            tw.Close();
        }

        /// <summary>
        /// Carga pacientes de un archivo de texto.
        /// </summary>
        public void Cargar()
        {
            string[] lines = File.ReadAllLines(CUtilities.SAVE_FILE_NAME);
            foreach(string line in lines)
            {
                string[] lineArray = line.Split('$');
                string nombre = lineArray[0];
                string apellido = lineArray[1];
                string dpi = lineArray[2];
                int year = int.Parse(lineArray[3]);
                int month = int.Parse(lineArray[4]);
                int day = int.Parse(lineArray[5]);
                string razon = lineArray[6];
                DateTime f = new DateTime(year, month, day);
                ETipoDeSangre sangre = (ETipoDeSangre)int.Parse(lineArray[7]);
                string[] asisArray = lineArray[8].Split('#');
                List<CAsistencia> asisList = new List<CAsistencia>();
                foreach(string s in asisArray)
                {
                    if (string.IsNullOrEmpty(s))
                        continue;
                    string[] asistenciaIndividual = s.Split('_');
                    CAsistencia a = new CAsistencia(asistenciaIndividual[0], asistenciaIndividual[1]);
                    asisList.Add(a);
                }
                CPaciente p = new CPaciente(nombre, apellido, dpi,f, razon, sangre,asisList);
                listaPacientes.Add(p);
            }
        }
        #endregion
    }
}
