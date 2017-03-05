using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Comida;

namespace ControlVentas
{
    public static class CUtilities
    {
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

        public static void FillListView<T>(ListView lV, List<T> list)
        {
            lV.Items.Clear();
            foreach(T item in list)
            {
                lV.Items.Add(item.ToString());
            }
        }

        public static void FillListView<T,E>(ListView lV, List<T> list1, List<E> list2)
        {
            lV.Items.Clear();
            foreach(T item in list1)
            {
                int index = list1.IndexOf(item);
                string s = list2[index] + "-" + item.ToString();
                lV.Items.Add(s);
            }
        }
    }
}
