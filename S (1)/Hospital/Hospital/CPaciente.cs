using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlVentas;

namespace Hospital
{
    public class CPaciente : CPersona
    {
        #region Public Variables
        public string razon { get; set; }
        public ETipoDeSangre sangre { get; set; }
        public string sangreS { get; set; }
        #endregion

        #region Constructors
        public CPaciente(string nombre, string apellido, string razon, ETipoDeSangre sangre)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.razon = razon;
            this.sangreS = CUtilities.StringValueOf(sangre);
        }
        #endregion

        #region Public Methods
        public string ToSaveString()
        {
            return nombre + "$" + apellido + "$" + razon + "$" + (int)sangre + "$";
        }
        #endregion
    }
}
