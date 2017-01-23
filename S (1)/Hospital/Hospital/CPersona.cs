using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace Hospital
{
    public enum ESEXO { Masculino, Femenino}
    public enum ETipoDeSangre {Cualquiera, [DescriptionAttribute("O-")] ONeg,
    [DescriptionAttribute("O+")]OPos, [DescriptionAttribute("A-")]ANeg,
    [DescriptionAttribute("A+")]APos, [DescriptionAttribute("B-")]BNeg,
    [DescriptionAttribute("B+")]BPos, [DescriptionAttribute("AB-")]ABNeg,
    [DescriptionAttribute("AB+")]ABPos }

    public class CPersona
    {
        public string nombre { get; set; }
        public string apellido { get; set; }

        public CPersona() { }
    }
}
