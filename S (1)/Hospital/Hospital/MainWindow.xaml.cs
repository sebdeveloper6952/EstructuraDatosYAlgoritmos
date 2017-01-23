using System;
using System.Windows;
using ControlVentas;
using System.IO;
using System.Windows.Input;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CAdminPacientes admin;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Ingresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombre = tB_Ingreso_Nombre.Text;
                string apellido = tB_Ingreso_Apellido.Text;
                string razon = tB_Ingreso_Razon.Text;
                ETipoDeSangre sangre = (ETipoDeSangre)cB_Ingreso_Sangre.SelectedIndex;
                if(nombre == string.Empty || apellido == string.Empty || sangre == ETipoDeSangre.Cualquiera)
                {
                    MessageBox.Show("Revisa los datos.");
                }
                else
                {
                    // ingresar paciente
                    CPaciente p = new CPaciente(nombre, apellido, razon, sangre);
                    admin.AgregarPaciente(p);
                    CUtilities.FillListView(lV_Pacientes, admin.BuscarPacientes(string.Empty, string.Empty, ETipoDeSangre.Cualquiera));
                    MessageBox.Show("El paciente ha sido agregado.");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(CUtilities.SAVE_FILE_NAME))
                File.Create(CUtilities.SAVE_FILE_NAME);
            admin = new CAdminPacientes();
            admin.Cargar();
            CUtilities.FillListView(lV_Pacientes, admin.BuscarPacientes(string.Empty, string.Empty, ETipoDeSangre.Cualquiera));
            cB_Ingreso_Sangre.SelectedIndex = 0;
        }

        private void btn_Buscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombre = tB_Ingreso_Nombre.Text;
                string apellido = tB_Ingreso_Apellido.Text;
                ETipoDeSangre sangre = (ETipoDeSangre)cB_Ingreso_Sangre.SelectedIndex;
                CUtilities.FillListView(lV_Pacientes, admin.BuscarPacientes(nombre, apellido, sangre));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                admin.Guardar();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_Ingreso_Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if(lV_Pacientes.SelectedIndex >= 0)
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Eliminar paciente?", "Confirmar",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if(result == MessageBoxResult.Yes)
                    {
                        admin.EliminarPaciente(lV_Pacientes.SelectedIndex);
                        CUtilities.FillListView(lV_Pacientes, admin.BuscarPacientes(string.Empty,
                            string.Empty, ETipoDeSangre.Cualquiera));
                    }
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void TextInputValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CUtilities.TextOnlyLetters(e.Text);
        }

    }
}
