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

    public class CPersona : IComparable
    {
        #region Public Variables
        public string nombre { get; protected set; }
        public string apellido { get; protected set; }
        public string dpi { get; protected set; }
        public DateTime fechaNacimiento { get; protected set; }
        public string fechaNacS { get; protected set; }
        public int edad { get; protected set; }
        #endregion

        #region Public Methods
        public void SetNombre(string n)
        {
            if (string.IsNullOrEmpty(n))
                throw new ArgumentException("Passed name not valid.");
            this.nombre = n;
        }

        public void SetApellido(string n)
        {
            if (string.IsNullOrEmpty(n))
                throw new ArgumentException("Passed last name not valid.");
            this.apellido = n;
        }

        public void SetDPI(string dpi)
        {
            if (string.IsNullOrEmpty(dpi))
                throw new ArgumentException("Passed dpi not valid.");
            this.dpi = dpi;
        }

        //public DateTime GetFechaNacimiento() { return this.fechaNacimiento; }

        public void SetFechaNacimiento(DateTime fecha) { fechaNacimiento = fecha; }

        public virtual int CompareTo(object obj)
        {
            if (obj == null)
                throw new InvalidOperationException("Null object passed.");
            CPersona p = (CPersona)obj;
            int result;
            result = apellido.CompareTo(p.apellido);
            return result;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CPersona))
                return false;
            CPersona p = (CPersona)obj;
            return dpi.Equals(p.dpi);
        }

        public override int GetHashCode()
        {
            return dpi.GetHashCode();
        }
        #endregion

        #region Operators
        public static bool operator > (CPersona op1, CPersona op2)
        {
            return op1.CompareTo(op2) == 1;
        }

        public static bool operator < (CPersona op1, CPersona op2)
        {
            return op1.CompareTo(op2) == -1;
        }

        public static bool operator >= (CPersona op1, CPersona op2)
        {
            return op1.CompareTo(op2) >= 0;
        }

        public static bool operator <= (CPersona op1, CPersona op2)
        {
            return op1.CompareTo(op2) <= 0;
        }

        public static bool operator == (CPersona op1, CPersona op2)
        {
            return op1.Equals(op2);
        }

        public static bool operator != (CPersona op1, CPersona op2)
        {
            return !op1.Equals(op2);
        }
        #endregion

        #region Private Methods
        protected void CalcularEdad()
        {
            DateTime today = DateTime.UtcNow;
            int age = today.Year - fechaNacimiento.Year;
            if (fechaNacimiento > today.AddYears(-age))
                age--;
            edad = age;
        }
        #endregion
    }
}
