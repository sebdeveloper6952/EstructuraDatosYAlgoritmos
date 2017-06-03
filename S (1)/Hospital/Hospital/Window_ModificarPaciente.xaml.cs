using System;
using System.ComponentModel;
using System.Windows;


namespace Hospital
{
    /// <summary>
    /// Interaction logic for Window_ModificarPaciente.xaml
    /// </summary>
    public partial class Window_ModificarPaciente : Window
    {
        private int index;
        private CPaciente paciente;
        private bool cambiosGuardados;

        public Window_ModificarPaciente(int index)
        {
            InitializeComponent();
            this.index = index;
            paciente = CAdminPacientes.instance.GetPaciente(index);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tB_ModPac_Nombres.Text = paciente.nombre;
            tB_ModPac_Apellidos.Text = paciente.apellido;
            tB_ModPac_DPI.Text = paciente.dpi;
            cB_ModPac_TipoSangre.SelectedIndex = (int)paciente.sangre - 1;
            tB_ModPac_FechaYear.Text = paciente.fechaNacimiento.Year.ToString();
            cB_ModPac_FechaDia.SelectedIndex = paciente.fechaNacimiento.Day - 1;
            cB_ModPac_FechaMes.SelectedIndex = paciente.fechaNacimiento.Month - 1;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!cambiosGuardados)
            {
                // Preguntar si quiere guardar cambios o no
                MessageBoxResult result = MessageBox.Show("Guardar cambios?", "Confirmar cambios.",
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    // guardar cambios
                    GuardarCambios();
                }
            }
        }

        private void btn_ModPac_Guardar_Click(object sender, RoutedEventArgs e)
        {
            // guardar cambios
            GuardarCambios();
            Close();
        }

        private void GuardarCambios()
        {
            // validar datos
            string nNombre = tB_ModPac_Nombres.Text;
            string nApellido = tB_ModPac_Apellidos.Text;
            string dpi = tB_ModPac_DPI.Text;
            int day = cB_ModPac_FechaDia.SelectedIndex + 1;
            int month = cB_ModPac_FechaMes.SelectedIndex + 1;
            int year = int.Parse(tB_ModPac_FechaYear.Text);
            ETipoDeSangre s = (ETipoDeSangre)cB_ModPac_TipoSangre.SelectedIndex + 1;
            if (string.IsNullOrEmpty(nNombre) || string.IsNullOrEmpty(nApellido) ||
                string.IsNullOrEmpty(dpi) || year < 0 || year > DateTime.UtcNow.Year)
            {
                MessageBox.Show("Revisa los datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // guardar cambios
            CPaciente p = CAdminPacientes.instance.GetPaciente(index);
            DateTime f = new DateTime(year, month, day);
            p.SetNombre(nNombre);
            p.SetApellido(nApellido);
            p.SetDPI(dpi);
            p.SetFechaNacimiento(f);
            p.SetSangre(s);
            cambiosGuardados = true;
            MessageBox.Show("Cambios guardados.", "Info", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void btn_ModPac_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
