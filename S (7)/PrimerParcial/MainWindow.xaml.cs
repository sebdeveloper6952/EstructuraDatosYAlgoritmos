using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PrimerParcial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CAdminJuguetes adminJuguetes;
        private CAdminOrden adminOrden;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            adminJuguetes = new CAdminJuguetes();
            adminOrden = new CAdminOrden();
            // llenar listview de juguetes
            CJuguete j = new CJuguete(0, "Pokebola", ETIPO.izquierda);
            adminJuguetes.AgregarJuguete(j);
            j = new CJuguete(6, "Pokebola", ETIPO.derecha);
            adminJuguetes.AgregarJuguete(j);
            j = new CJuguete(1, "Raichu", ETIPO.izquierda);
            adminJuguetes.AgregarJuguete(j);
            j = new CJuguete(5, "Raichu", ETIPO.derecha);
            adminJuguetes.AgregarJuguete(j);
            j = new CJuguete(2, "Pikachu", ETIPO.izquierda);
            adminJuguetes.AgregarJuguete(j);
            j = new CJuguete(4, "Pikachu", ETIPO.derecha);
            adminJuguetes.AgregarJuguete(j);
            j = new CJuguete(3, "Pichu", ETIPO.unico);
            adminJuguetes.AgregarJuguete(j);
            CUtilities.FillListView(lV_Juguetes, adminJuguetes.listaJuguetes);
        }

        private void btn_ColocarOrden_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (adminOrden.ValidarOrden())
                {
                    adminOrden.LimpiarOrden();
                    CUtilities.FillListView(lV_Orden, adminOrden.ordenJuguetes);
                    MessageBox.Show("Felicidades, tu orden a sido recibida.", "Gracias!",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Por favor revisa tu orden, las partes no concuerdan.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("No hay nada en tu orden!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_AgregarOrden_Click(object sender, RoutedEventArgs e)
        {
            if(lV_Juguetes.SelectedIndex >= 0)
            {
                CJuguete j = adminJuguetes.listaJuguetes[lV_Juguetes.SelectedIndex];
                adminOrden.AgregarJugueteOrden(j);
                CUtilities.FillListView(lV_Orden, adminOrden.ordenJuguetes);
            } 
        }

        private void btn_QuitarDeOrden_Click(object sender, RoutedEventArgs e)
        {
            // limpiar orden
            adminOrden.LimpiarOrden();
            CUtilities.FillListView(lV_Orden, adminOrden.ordenJuguetes);
        }
    }
}
