using System;
using System.Windows;
using ControlVentas;
using System.IO;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LogSystem logSystem;
        private CAdminPacientes admin;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Ingresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // conseguir datos del paciente de controles
                string nombre = tB_Ingreso_Nombre.Text;
                string apellido = tB_Ingreso_Apellido.Text;
                string razon = tB_Ingreso_Razon.Text;
                string dpi = tB_DPI.Text;
                int dia = cB_Dia.SelectedIndex + 1;
                int mes = cB_Mes.SelectedIndex + 1;
                // validar fecha de nacimiento
                int year = Math.Min(int.Parse(tB_Year.Text), DateTime.UtcNow.Year);
                List<CAsistencia> asis = new List<CAsistencia>();
                CAsistencia a = new CAsistencia(DateTime.UtcNow.ToShortDateString(), tB_AsistPacientes_Observaciones.Text);
                asis.Add(a);
                DateTime fechaNac = new DateTime(year, mes, dia);
                ETipoDeSangre sangre = (ETipoDeSangre)cB_Ingreso_Sangre.SelectedIndex;
                // validar datos
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || sangre == ETipoDeSangre.Cualquiera
                    || string.IsNullOrEmpty(dpi) || string.IsNullOrEmpty(tB_Year.Text))
                {
                    MessageBox.Show("Revisa los datos.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    // ingresar paciente
                    CPaciente p = new CPaciente(nombre, apellido, dpi, fechaNac, razon, sangre,asis);
                    admin.AgregarPaciente(p);
                    CUtilities.FillListView(lV_Pacientes, admin.GetListaPacientes());
                    CUtilities.FillListView(lV_AsistPacientes, admin.GetListaPacientes());
                    logSystem.Loggear(ETipoLog.NuevoPaciente, p);
                    MessageBox.Show("El paciente ha sido agregado.", "Info", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    // limpiar campos de ingreso
                    tB_Ingreso_Nombre.Text = string.Empty;
                    tB_Ingreso_Apellido.Text = string.Empty;
                    tB_DPI.Text = string.Empty;
                    tB_Year.Text = string.Empty;
                    cB_Dia.SelectedIndex = 0;
                    cB_Mes.SelectedIndex = 0;
                    cB_Ingreso_Sangre.SelectedIndex = 0;
                    tB_Ingreso_Razon.Text = string.Empty;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al agregar paciente - " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logSystem = new LogSystem();
            if (!File.Exists(CUtilities.SAVE_FILE_NAME))
                File.Create(CUtilities.SAVE_FILE_NAME);
            admin = new CAdminPacientes();
            admin.Cargar();
            CUtilities.FillListView(lV_Pacientes, admin.GetListaPacientes());
            CUtilities.FillListView(lV_AsistPacientes, admin.GetListaPacientes());
            cB_Ingreso_Sangre.SelectedIndex = 0;
            cB_Dia.SelectedIndex = 0;
            cB_Mes.SelectedIndex = 0;
            cB_Rep_Dia.SelectedIndex = 0;
            cB_Rep_Dia2.SelectedIndex = 0;
            cB_Rep_Mes.SelectedIndex = 0;
            cB_Rep_Mes2.SelectedIndex = 0;
            cB_AsistPacientes_Dia.SelectedIndex = 0;
            cB_AsistPacientes_Mes.SelectedIndex = 0;
            cB_Rep_FechaAsist_Day.SelectedIndex = 0;
            cB_Rep_FechaAsist_Month.SelectedIndex = 0;
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
                        CPaciente p = admin.GetPaciente(lV_Pacientes.SelectedIndex);
                        admin.EliminarPaciente(lV_Pacientes.SelectedIndex);
                        CUtilities.FillListView(lV_Pacientes, admin.GetListaPacientes());
                        CUtilities.FillListView(lV_AsistPacientes, admin.GetListaPacientes());
                        logSystem.Loggear(ETipoLog.EliminoPaciente, p);
                    }
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        // text field allow only letters
        private void TextInputValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CUtilities.TextOnlyLetters(e.Text);
        }

        // text field allow only numbers
        private void TextInputValidationNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CUtilities.TextOnlyNumbers(e.Text);
        }
        
        // *************** metodos para ordernar lista de pacientes **************
        private void SortLV(object sender, RoutedEventArgs e)
        {
            // una manera
            //GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            //if(headerClicked != null)
            //{
            //    string sortBy = headerClicked.Content.ToString();
            //    sortBy = sortBy.ToLower();
            //    lV_Pacientes.Items.SortDescriptions.Clear();
            //    lV_Pacientes.Items.SortDescriptions.Add(new SortDescription(sortBy, ListSortDirection.Ascending));
            //}
        }

        // ordenar por nombres
        private void SortByName(object sender, RoutedEventArgs e)
        {
            lV_Pacientes.Items.Clear();
            IComparer<CPersona> comp = new CPersonaCompararNombre();
            admin.GetListaPacientes().Sort(comp);
            CUtilities.FillListView(lV_Pacientes, admin.GetListaPacientes());
        }
        // ordenar por apellidos
        private void SortByLastName(object sender, RoutedEventArgs e)
        {
            lV_Pacientes.Items.Clear();
            IComparer<CPersona> comp = new CPersonaCompararApellido();
            admin.GetListaPacientes().Sort(comp);
            CUtilities.FillListView(lV_Pacientes, admin.GetListaPacientes());
        }
        // ordenar por DPI
        private void SortByDPI(object sender, RoutedEventArgs e)
        {
            lV_Pacientes.Items.Clear();
            IComparer<CPersona> comp = new CPersonaCompararDPI();
            admin.GetListaPacientes().Sort(comp);
            CUtilities.FillListView(lV_Pacientes, admin.GetListaPacientes());
        }
        // ordenar por razon de ingreso
        private void SortByRazon(object sender, RoutedEventArgs e)
        {
            lV_Pacientes.Items.Clear();
            IComparer<CPaciente> comp = new CPersonaCompararRazon();
            admin.GetListaPacientes().Sort(comp);
            CUtilities.FillListView(lV_Pacientes, admin.GetListaPacientes());
        }
        //ordenar por edad
        private void SortByEdad(object sender, RoutedEventArgs e)
        {
            lV_Pacientes.Items.Clear();
            IComparer<CPaciente> comp = new CPersonaCompararEdad();
            admin.GetListaPacientes().Sort(comp);
            CUtilities.FillListView(lV_Pacientes, admin.GetListaPacientes());
        }
        //************************************************************************

        // ******************* METODOS DE BUSQUEDA *********************
        // por fecha de nacimiento
        private void btn_Rep_BuscarFechaNac_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // armar ambas fechas
                if (string.IsNullOrEmpty(tB_Rep_Year.Text) || string.IsNullOrEmpty(tB_Rep_Year2.Text))
                    MessageBox.Show("Revisa el año.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                int day = cB_Rep_Dia.SelectedIndex + 1;
                int month = cB_Rep_Mes.SelectedIndex + 1;
                int year = int.Parse(tB_Rep_Year.Text);
                DateTime f1 = new DateTime(year, month, day);
                day = cB_Rep_Dia2.SelectedIndex + 1;
                month = cB_Rep_Mes2.SelectedIndex + 1;
                year = int.Parse(tB_Rep_Year2.Text);
                DateTime f2 = new DateTime(year, month, day);
                // iterar por todos los pacientes y hacer comparacion
                List<CPaciente> set = admin.GetListaPacientes();
                List<CPaciente> subset = new List<CPaciente>();
                foreach (CPaciente p in set)
                    if (p.fechaNacimiento >= f1 && p.fechaNacimiento <= f2)
                        subset.Add(p);
                IComparer<CPaciente> c = new CPersonaCompararFechaNacimiento();
                subset.Sort(c);
                CUtilities.FillListView(lV_Rep_FechaNac, subset);
            }
            catch { }
        }

        // por fecha asistencia
        private void btn_Rep_FechaAsist_Buscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // obtener fecha de asistencia deseada
                if (string.IsNullOrEmpty(tB_Rep_FechaAsist_Year.Text))
                    MessageBox.Show("Revisa el año.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                int day = cB_Rep_FechaAsist_Day.SelectedIndex + 1;
                int month = cB_Rep_FechaAsist_Month.SelectedIndex + 1;
                int year = int.Parse(tB_Rep_FechaAsist_Year.Text);
                DateTime f1 = new DateTime(year, month, day);
                List<CPaciente> set = admin.GetListaPacientes();
                List<CPaciente> subset = new List<CPaciente>();
                foreach (CPaciente p in set)
                {
                    foreach (CAsistencia a in p.asistencias)
                    {
                        if (f1.ToShortDateString() == a.fechaString)
                        {
                            subset.Add(p);
                            break;
                        }
                    }
                }
                IComparer<CPaciente> c = new CPersonaCompararApellido();
                subset.Sort(c);
                CUtilities.FillListView(lV_Rep_FechaAsis, subset);
            }
            catch { }
        }

        // por cantidad de asistencias
        private void btn_Rep_CantAsis_Buscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int cant0 = int.Parse(tB_Rep_CantAsis_Cant0.Text);
                int cant1 = int.Parse(tB_Rep_CantAsis_Cant1.Text);
                List<CPaciente> set = admin.GetListaPacientes();
                List<CPaciente> subset = new List<CPaciente>();
                foreach (CPaciente p in set)
                    if (p.cantAsistencias >= cant0 && p.cantAsistencias <= cant1)
                        subset.Add(p);
                IComparer<CPaciente> c = new CPersonaCompararAsistencia();
                subset.Sort(c);
                CUtilities.FillListView(lV_Rep_CantAsis, subset);
            }
            catch
            {
                MessageBox.Show("Revisa los datos.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        // *************************************************************

        // ingresar asistencia a un paciente
        private void btn_AsistPacientes_IngresarAsistencia_Click(object sender, RoutedEventArgs e)
        {
            // agregar una asistencia al paciente seleccionado
            if(lV_AsistPacientes.SelectedIndex >= 0)
            {
                try
                {
                    int day = cB_AsistPacientes_Dia.SelectedIndex + 1;
                    int month = cB_AsistPacientes_Mes.SelectedIndex + 1;
                    // validar fecha de asistencia
                    int year = Math.Min(int.Parse(tB_AsistPacientes_Year.Text), DateTime.UtcNow.Year);
                    DateTime f = new DateTime(year, month, day);
                    string o = tB_AsistPacientes_Observaciones.Text;
                    if (string.IsNullOrEmpty(o))
                        throw new ArgumentException("La observacion no es valida.");
                    CPaciente p = admin.GetPaciente(lV_AsistPacientes.SelectedIndex);
                    // no se puede ingresar asistencia anterior a nacimiento del paciente
                    if(p.fechaNacimiento > f)
                    {
                        MessageBox.Show("Fecha de asistencia no es valida.", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }
                    p.AgregarAsistencia(f, o);
                    logSystem.Loggear(ETipoLog.AgregoAsistencia, p);
                    MessageBox.Show("Asistencia ingresada.", "Info", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Revisa los datos.", "Error.", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona un paciente.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lV_Pacientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // modificar paciente
            if (lV_Pacientes.SelectedIndex >= 0)
            {
                Window_ModificarPaciente window = new Window_ModificarPaciente(lV_Pacientes.SelectedIndex);
                window.ShowDialog();
                CUtilities.FillListView(lV_Pacientes, admin.GetListaPacientes());
                CUtilities.FillListView(lV_AsistPacientes, admin.GetListaPacientes());
            }
            else
            {
                MessageBox.Show("Haz doble click en un paciente para ver sus detalles.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
