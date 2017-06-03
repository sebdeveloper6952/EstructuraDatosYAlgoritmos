using MyDataStructures.Lists;
using System.Windows.Controls;

namespace ListasEnlazadas_Secuencia
{
    public class CUtilities
    {
        public static void FillListView<T>(ListView lv, Sequence<T> list)
        {
            lv.Items.Clear();
            foreach (T item in list)
                lv.Items.Add(item);
        }
    }
}
