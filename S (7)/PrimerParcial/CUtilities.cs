using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrimerParcial
{
    public class CUtilities
    {
        public static void FillListView<T>(ListView lv, List<T> list)
        {
            lv.Items.Clear();
            foreach (T t in list)
                lv.Items.Add(t.ToString());
        }
    }
}
