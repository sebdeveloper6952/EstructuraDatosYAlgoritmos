using System;
using System.IO;
namespace Hospital
{
    public enum ETipoLog { Error, NuevoPaciente, EliminoPaciente, AgregoAsistencia}

    public class LogSystem
    {
        private const string logFileName = "log.txt";

        public LogSystem()
        {
            if (!File.Exists(logFileName))
                File.Create(logFileName);
        }

        public void Loggear(ETipoLog tipo, object data)
        {
            string logString = "[";
            CPaciente p;
            switch (tipo)
            {
                case ETipoLog.Error:
                    logString += ETipoLog.Error.ToString() + "]";
                    logString += DateTime.UtcNow.ToString();
                    string d = data as string;
                    if (d == null)
                        throw new ArgumentNullException("Null data object passed.");
                    logString += "-> " + data;
                    Log(logString);
                    break;
                case ETipoLog.NuevoPaciente:
                    p = data as CPaciente;
                    if (p == null)
                        throw new ArgumentException("Object data is not of type CPaciente.");
                    logString += ETipoLog.NuevoPaciente.ToString() + "]";
                    logString += DateTime.UtcNow.ToString() + "-> ";
                    logString += p.nombre + " " + p.apellido;
                    Log(logString);
                    break;
                case ETipoLog.EliminoPaciente:
                    p = data as CPaciente;
                    if (p == null)
                        throw new ArgumentException("Object data is not of type CPaciente.");
                    logString += ETipoLog.EliminoPaciente.ToString() + "]";
                    logString += DateTime.UtcNow.ToString() + "-> ";
                    logString += p.nombre + " " + p.apellido;
                    Log(logString);
                    break;
                case ETipoLog.AgregoAsistencia:
                    p = data as CPaciente;
                    if (p == null)
                        throw new ArgumentException("Object data is not of type CPaciente.");
                    logString += ETipoLog.AgregoAsistencia.ToString() + "]";
                    logString += DateTime.UtcNow.ToString() + "-> ";
                    logString += p.nombre + " " + p.apellido;
                    Log(logString);
                    break;
                default:
                    break;
            }
        }

        private void Log(string text)
        {
            StreamWriter sw = File.AppendText(logFileName);
            sw.WriteLine(text);
            sw.Flush();
            sw.Close();
        }
    }
}
