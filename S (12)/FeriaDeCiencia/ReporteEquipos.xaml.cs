using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FeriaDeCiencia
{
    /// <summary>
    /// Interaction logic for ReporteEquipos.xaml
    /// </summary>
    public partial class ReporteEquipos : Window
    {
        public ReporteEquipos()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // llenar listboxes
            CUtilities.FillListBox(lB_Equipos, CAdminEquipos.instance.ListaEquipos());
            if(CAdminEquipos.instance.ListaEquipos().Count > 0)
                CUtilities.FillListBox(lB_Integrantes, CAdminEquipos.instance.ObtenerEquipo(0).
                    ObtenerIntegrantes());
        }

        private void lB_Equipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // actualizar listbox de integrantes
            int index = lB_Equipos.SelectedIndex;
            if(index >= 0)
            {
                CEquipo eS = CAdminEquipos.instance.ObtenerEquipo(index);
                CUtilities.FillListBox(lB_Integrantes, eS.ObtenerIntegrantes());
            }
        }

        private void btn_Continuar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
