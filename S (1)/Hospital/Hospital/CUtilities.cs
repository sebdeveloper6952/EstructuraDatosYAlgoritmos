using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Hospital;
using System.Windows.Controls;
using System.ComponentModel;
using System.Reflection;

namespace ControlVentas
{
    public static class CUtilities
    {
        public const string SAVE_FILE_NAME = "pacientes.txt";
        /// <summary>
        /// Allow only numbers and dashes in a textbox.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool TextOnlyNumbers(string e)
        {
            Regex rg = new Regex("[^0-9.]+");
            return !rg.IsMatch(e);
        }

        /// <summary>
        /// Allow only letters in a textbox.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool TextOnlyLetters(string e)
        {
            Regex rg = new Regex("^[^A-Za-z]+$");
            return !rg.IsMatch(e);
        }

        public static void FillListView(ListView lv, List<CPaciente> list)
        {
            lv.Items.Clear();
            foreach(CPaciente p in list)
            {
                lv.Items.Add(p);
            }
        }

        public static string StringValueOf(Enum value)
        {
            FieldInfo i = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])i.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
