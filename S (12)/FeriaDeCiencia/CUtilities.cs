using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace FeriaDeCiencia
{
    public class CUtilities
    {
        public static void FillListBox<T>(ListBox lB, IList<T> list)
        {
            lB.Items.Clear();
            foreach(T t in list)
            {
                lB.Items.Add(t.ToString());
            }
        }


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
            Regex rg = new Regex("[^A-Z a-z]+");
            return !rg.IsMatch(e);
        }
    }
}
