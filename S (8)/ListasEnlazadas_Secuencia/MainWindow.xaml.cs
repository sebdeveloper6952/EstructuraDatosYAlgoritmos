using System.Windows;
using MyDataStructures.Lists;
using MyDataStructures;


namespace ListasEnlazadas_Secuencia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Sequence<string> list;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            list = new Sequence<string>();
        }

        private void btn_AddPos_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            try
            {
                string item = tB_ItemToAdd.Text;
                int index = int.Parse(tB_AddAtIndex.Text);
                // realizar operacion
                list.Add(index, item);
                CUtilities.FillListView(lV_Items, list);
            }
            catch { MessageBox.Show("Revisa los datos."); }
        }

        private void btn_AddFirst_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            string item = tB_ItemToAdd.Text;
            if (string.IsNullOrEmpty(item))
                return;
            list.AddFirst(item);
            CUtilities.FillListView(lV_Items, list);
        }

        private void btn_AddLast_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            string item = tB_ItemToAdd.Text;
            if (string.IsNullOrEmpty(item))
                return;
            list.AddLast(item);
            CUtilities.FillListView(lV_Items, list);
        }

        private void btn_AddAfter_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            string item = tB_ItemToAdd.Text;
            int index = lV_Items.SelectedIndex;
            if (string.IsNullOrEmpty(item))
                return;
            if (index < 0 || index > list.Size())
                return;
            Node<string> node = list.AtIndex(index);
            // realizar operacion
            list.AddAfter(node, item);
            CUtilities.FillListView(lV_Items, list);
        }

        private void btn_AddBefore_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            string item = tB_ItemToAdd.Text;
            int index = lV_Items.SelectedIndex;
            if (string.IsNullOrEmpty(item))
                return;
            if (index < 0 || index > list.Size())
                return;
            Node<string> node = list.AtIndex(index);
            // realizar operacion
            list.AddBefore(node, item);
            CUtilities.FillListView(lV_Items, list);
        }

        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = int.Parse(tB_RemoveAtIndex.Text);
                list.Remove(index);
                CUtilities.FillListView(lV_Items, list);
            }
            catch { MessageBox.Show("Revisa los datos."); }
        }

        private void btn_RemoveFirst_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                list.RemoveFirst();
                CUtilities.FillListView(lV_Items, list);
            }
            catch
            {
                MessageBox.Show("Error.");
            }
        }

        private void btn_RemoveLast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                list.RemoveLast();
                CUtilities.FillListView(lV_Items, list);
            }
            catch
            {
                MessageBox.Show("Error.");
            }
        }

        private void btn_Set_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            try
            {
                string item = tB_ItemToAdd.Text;
                int index = lV_Items.SelectedIndex;
                if (string.IsNullOrEmpty(item))
                    return;
                list.Set(index, item);
            CUtilities.FillListView(lV_Items, list);
            }
            catch { MessageBox.Show("Revisa los datos."); }
        }
    }
}
