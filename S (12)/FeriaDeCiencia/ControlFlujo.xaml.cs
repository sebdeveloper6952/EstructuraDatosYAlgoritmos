using System;
using System.Windows;
using System.Windows.Controls;

namespace FeriaDeCiencia
{
    /// <summary>
    /// Interaction logic for ControlFlujo.xaml
    /// </summary>
    public partial class ControlFlujo : Window
    {
        private MainWindow parent;
        private RadioButton rB_Fase1;
        private RadioButton rB_Fase2;
        private RadioButton rB_Fase3;

        public ControlFlujo(MainWindow p)
        {
            InitializeComponent();
            parent = p;
            rB_Fase1 = new RadioButton();
            rB_Fase2 = new RadioButton();
            rB_Fase3 = new RadioButton();
            rB_Fase1.Margin = new Thickness(30, 20, 0, 0);
            rB_Fase2.Margin = new Thickness(100, 20, 0, 0);
            rB_Fase3.Margin = new Thickness(170, 20, 0, 0);
            rB_Fase1.Content = "Fase 1";
            rB_Fase2.Content = "Fase 2";
            rB_Fase3.Content = "Fase 3";
            rB_Fase1.Checked += rB_Fase1_Checked;
            rB_Fase2.Checked += rB_Fase2_Checked;
            rB_Fase3.Checked += rB_Fase3_Checkec;
            grid_Main.Children.Add(rB_Fase1);
            grid_Main.Children.Add(rB_Fase2);
            grid_Main.Children.Add(rB_Fase3);
            rB_Fase1.IsChecked = true;
        }

        private void rB_Fase1_Checked(object sender, RoutedEventArgs e)
        {
            parent.ActivarFase1();
        }

        private void rB_Fase2_Checked(object sender, RoutedEventArgs e)
        {
            if (CAdminEquipos.instance.ValidarEquipos())
                parent.ActivarFase2();
            else
            {
                rB_Fase1.IsChecked = true;
                MessageBox.Show("Todos los equipos deben tener al menos un integrante.",
                    "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void rB_Fase3_Checkec(object sender, RoutedEventArgs e)
        {
            parent.ActivarFase3();
        }
    }
}
