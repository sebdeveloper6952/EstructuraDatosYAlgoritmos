using MyDataStructures.Lists;
using System.Windows;


namespace ListasEnlazadas
{
    public partial class MainWindow : Window
    {
        private SLinkedList<string> list;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            list = new SLinkedList<string>();
        }

        private void btn_AddFirst_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tB_Elemento.Text))
            {
                MessageBox.Show("Error, campo de añadir vacio.");
                return;
            }
            string s = tB_Elemento.Text;
            list.AddFirst(s);
            l_ListaSize.Content = string.Concat("List size: ", list.Size());
            lV_Lista.Items.Clear();
            foreach (string t in list)
                lV_Lista.Items.Add(t);
                
        }

        private void btn_RemoveFirst_Click(object sender, RoutedEventArgs e)
        {
            if(list.Size() > 0)
            {
                list.RemoveFirst();
                l_ListaSize.Content = string.Concat("List size: ", list.Size());
                lV_Lista.Items.Clear();
                if(list.Size() > 0)
                {
                    foreach (string s in list)
                        lV_Lista.Items.Add(s);
                }
            }
        }

        private void btn_AddLast_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tB_Elemento.Text))
            {
                MessageBox.Show("Error, campo de añadir vacio.");
                return;
            }
            string s = tB_Elemento.Text;
            list.AddLast(s);
            l_ListaSize.Content = string.Concat("List size: ", list.Size());
            lV_Lista.Items.Clear();
            foreach (string t in list)
                lV_Lista.Items.Add(t);
        }
    }
}
