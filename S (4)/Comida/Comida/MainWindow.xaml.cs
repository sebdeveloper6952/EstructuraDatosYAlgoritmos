using System.Windows;
using System.Windows.Input;
using ControlVentas;

namespace Comida
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CAdminCocina adminCocina;
        private CAdminMenu adminMenu;
        private COrden ordenActual;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            adminCocina = new CAdminCocina();
            adminMenu = new CAdminMenu();
            ordenActual = new COrden();
            adminMenu.Cargar();
            CUtilities.FillListView(lV_Menu, adminMenu.GetItems());
        }

        private void lV_Menu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // anadir un item a la orden cuando hay un doble click sobre un item
            if(lV_Menu.SelectedIndex >= 0)
            {
                CItem selectedItem = adminMenu.GetItem(lV_Menu.SelectedIndex);
                ordenActual.AddItem(selectedItem);
                l_TotalOrden.Content = "Total orden: Q" + ordenActual.GetTotalOrden().ToString("0.00");
                CUtilities.FillListView(lV_Pedidos, ordenActual.GetItems(), ordenActual.GetCantidad());
            }
        }

        private void btn_QuitarDeOrden_Click(object sender, RoutedEventArgs e)
        {
            if (lV_Pedidos.SelectedIndex >= 0)
            {
                ordenActual.RemoveItem(lV_Pedidos.SelectedIndex);
                CUtilities.FillListView(lV_Pedidos, ordenActual.GetItems(), ordenActual.GetCantidad());
                l_TotalOrden.Content = "Total orden: Q" + ordenActual.GetTotalOrden().ToString("0.00");
            }
        }

        private void btn_Ordenar_Click(object sender, RoutedEventArgs e)
        {
            // pasar orden a la cocina y limpiar la listView de orden
            if(lV_Pedidos.Items.Count > 0)
            {
                adminCocina.AddOrder(ordenActual);
                COrden nuevaOrden = adminCocina.GetFirstOrder();
                lv_OrdenCocina.Items.Clear();
                lv_OrdenCocina.Items.Add(nuevaOrden.ToString());
                ordenActual = new COrden();
                CUtilities.FillListView(lV_Pedidos, ordenActual.GetItems());
                l_TotalOrden.Content = "Total orden: Q0.00";
            }
        }

        private void btn_OrdenLista_Click(object sender, RoutedEventArgs e)
        {
            // quitar orden actual y pasar a siguiente orden si  hay
            try
            {
                adminCocina.RemoveFirstOrder();
                lv_OrdenCocina.Items.Clear();
                COrden ordenActual = adminCocina.GetFirstOrder();
                lv_OrdenCocina.Items.Add(ordenActual.ToString());
            }
            catch(NoMoreOrdersException ex) { MessageBox.Show(ex.Message); }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            adminMenu.Guardar();
        }
    }
}
