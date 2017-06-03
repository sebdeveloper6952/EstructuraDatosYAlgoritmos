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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace FeriaDeCiencia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // **** METODOS PARA CONTROLAR EL FLUJO DE LA FERIA ****
        public void ActivarFase1()
        {
            /* activar TabItem de Registro de Equipos y integrantes 
             y desactivar TabItem de Registro de Proyectos y busqueda
             de proyectos. */
            ti_RegistrarE.IsSelected = true;
            ti_RegistrarE.IsEnabled = true;
            ti_RegistrarP.IsEnabled = false;
            ti_BuscarP.IsEnabled = false;
        }

        public void ActivarFase2()
        {
            /* activar TabItem de Registro de Proyectos y busqueda 
              de proyectos, desactivar TabItem de Registro de Equipos
              y Integrantes. Luego, generar y desplegar reporte de
              equipos y integrantes inscritos.*/
            ti_RegistrarP.IsSelected = true;
            ti_RegistrarE.IsEnabled = false;
            ti_RegistrarP.IsEnabled = true;
            ti_BuscarP.IsEnabled = false;
            ReporteEquipos w_Rep = new ReporteEquipos();
            w_Rep.Show();
        }

        public void ActivarFase3()
        {
            /* activar TabItem de Busqeda de Proyectos, desactivar 
             * TabItem de Registro de Equipos y Integrantes y 
             Registro de Proyectos. */
            ti_BuscarP.IsSelected = true;
            ti_BuscarP.IsEnabled = true;
            ti_RegistrarE.IsEnabled = false;
            ti_RegistrarP.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rp_cB_Cat.SelectedIndex = 0;
            // instanciar ventana de control de flujo
            ControlFlujo w_cF = new ControlFlujo(this);
            w_cF.Show();
        }

        /// <summary>
        /// Registra un nuevo equipo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void re_btn_RegE_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            string n = re_tB_NombreE.Text;
            string f = re_tB_FraseE.Text;
            if(string.IsNullOrEmpty(n) || string.IsNullOrEmpty(f))
            {
                MessageBox.Show("Revisa los datos.", "Info", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            // crear nuevo objeto CEquipo
            CEquipo nE = new CEquipo(n, f);
            // agregar equipo a la lista de equipos
            CAdminEquipos.instance.AgregarEquipo(nE);
            // limpiar campos
            re_tB_NombreE.Text = string.Empty;
            re_tB_FraseE.Text = string.Empty;
            // actualizar los controles que muestran a los equipos actuales
            CUtilities.FillListBox(re_lB_Equipos, CAdminEquipos.instance.ListaEquipos());
            CUtilities.FillListBox(rp_lB_Equipos, CAdminEquipos.instance.ListaEquipos());
            MessageBox.Show("Se agrego el equipo.", "Info", MessageBoxButton.OK, 
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Registra un nuevo integrante para el equipo seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void re_btn_NuevoRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            if(re_lB_Equipos.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona un equipo.", "Info", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            string nombre = re_tB_NuevoNombre.Text;
            string id = re_tB_NuevoCarne.Text;
            string carrera = re_tB_NuevoCarrera.Text;
            if(string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(carrera))
            {
                MessageBox.Show("Revisa los datos.", "Info", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            // crear nuevo alumno
            CAlumno nA = new CAlumno(nombre, id, carrera);
            try
            {
                // tratar de agregar, si ya existe, se lanza una Excepcion
                CAdminAlumnos.instance.AgregarAlumno(nA);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("El no. de carne esta asociado a otro alumno ya existente.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CEquipo eSeleccionado = CAdminEquipos.instance.ObtenerEquipo(re_lB_Equipos.SelectedIndex);
            eSeleccionado.AgregarIntegrante(nA);
            // actualizar controles que muestran a los alumnos
            CUtilities.FillListBox(re_lB_Integrantes, eSeleccionado.ObtenerIntegrantes());
            // limpiar campos
            re_tB_NuevoNombre.Text = string.Empty;
            re_tB_NuevoCarne.Text = string.Empty;
            re_tB_NuevoCarrera.Text = string.Empty;
            nA = null;
        }

        /// <summary>
        /// Actualiza el listbox que muestra los integrantes del equipo que esta
        /// seleccionado en el listbox de equipos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void re_lB_Equipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = re_lB_Equipos.SelectedIndex;
            if(index >= 0)
            {
                CEquipo eS = CAdminEquipos.instance.ObtenerEquipo(index);
                CUtilities.FillListBox(re_lB_Integrantes, eS.ObtenerIntegrantes());
                eS = null;
            }
        }

        // ***** Metodos para restringir los caracteres permitidos en un TextBox *********
        /// <summary>
        /// Allow only numbers and dashes in a textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextValidationNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CUtilities.TextOnlyNumbers(e.Text);
        }

        /// <summary>
        /// Allow only letters in a textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextValidationLetters(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CUtilities.TextOnlyLetters(e.Text);
        }

        /// <summary>
        /// Actualiza el listbox que muestra los proyectos del equipo que
        /// esta seleccionado en el listbox de equipos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rp_lB_Equipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = rp_lB_Equipos.SelectedIndex;
            if(index >= 0)
            {
                CEquipo eS = CAdminEquipos.instance.ObtenerEquipo(index);
                try
                {
                    CUtilities.FillListBox(rp_lB_ProEquiSel, eS.ObtenerProyectos());
                }
                catch { }
                finally { eS = null; }
            }
        }

        /// <summary>
        /// Crea un nuevo proyecto para el equipo seleccionado y lo registra en
        /// el administrador de proyectos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rp_btn_RegistrarPro_Click(object sender, RoutedEventArgs e)
        {
            // validar que hay un equipo seleccionado
            int index = rp_lB_Equipos.SelectedIndex;
            if(index < 0)
            {
                MessageBox.Show("Selecciona un equipo.", "Info", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                // obtener y validar datos
                string nombre = rp_tB_NombrePro.Text;
                string desc = rp_tB_DescPro.Text;
                int cat = rp_cB_Cat.SelectedIndex;
                string palCla = rp_tB_PalCla.Text;
                string[] pC = palCla.Split(' ');
                if(string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(desc) ||
                    cat < 0 || string.IsNullOrEmpty(palCla) || pC.Length < 1 || pC.Length > 5)
                {
                    MessageBox.Show("Debes llenar todos los campos y especificar entre 1 y 5 palabras clave.",
                        "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                try
                {
                    // obtener el equipo seleccionado
                    CEquipo eS = CAdminEquipos.instance.ObtenerEquipo(index);
                    // crear un nuevo CProyecto
                    CProyecto p = new CProyecto(eS, nombre, desc, cat);
                    // agregar proyecto al equipo seleccionado
                    eS.AgregarProyecto(p);
                    // agregar palabras clave y proyecto al admin de busquedas
                    CAdminBusqueda.instance.AgregarProyecto(pC, p);
                    // actualizar el control que muestra los proyectos del equipo seleccionado
                    CUtilities.FillListBox(rp_lB_ProEquiSel, eS.ObtenerProyectos());
                    // cargar imagen
                    BitmapImage bi = LoadImage();
                    // si imagen es null, se asigna una por default.
                    if(bi == null)
                    {
                        bi = new BitmapImage(new Uri("pack://application:,,,/FeriaDeCiencia;component/Resources/science-fair2.png"));


                    }
                    // asignar imagen al proyecto
                    p.SetImage(bi);
                    MessageBox.Show("Se agrego el proyecto.", "Info", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    // limpiar campos
                    rp_tB_NombrePro.Text = string.Empty;
                    rp_tB_DescPro.Text = string.Empty;
                    rp_tB_PalCla.Text = string.Empty;
                }
                catch(ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /// <summary>
        /// Busca proyectos por palabras clave y los muestra los resultados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bp_btn_Buscar_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            string[] palCla = bp_tB_PalCla.Text.Split(' ');
            if(palCla.Length == 0)
            {
                MessageBox.Show("Debes introducir al menos una palabra clave para buscar.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                CAdminBusqueda.instance.BuscarProyecto(palCla);
                CUtilities.FillListBox(bp_lB_Pro, CAdminBusqueda.instance.resBus);
            }
            catch(ArgumentException)
            {
                MessageBox.Show("No se encontro proyecto con esos datos.", "Info", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                bp_lB_Pro.Items.Clear();
            }
        }

        /// <summary>
        /// Muestra los detalles del proyecto que esta seleccionado en la lista
        /// de resultados de busqueda de proyectos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bp_lB_Pro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // obtener proyecto seleccionado
                int index = bp_lB_Pro.SelectedIndex;
                if (index >= 0)
                {
                    // llenar campos con detalles del proyecto
                    CProyecto p = CAdminBusqueda.instance.resBus[index];
                    bp_tB_NombrePro.Text = p.nombre;
                    bp_tB_DescPro.Text = p.descripcion;
                    bp_tB_CatPro.Text = p.categoria.ToString();
                    bp_img_ImgPro.Source = p.image;
                    CUtilities.FillListBox(bp_lB_Integrantes, p.equipo.ObtenerIntegrantes());
                }
            }
            catch { }
        }

        /// <summary>
        /// Carga una imagen especificada por el usuario.
        /// </summary>
        /// <returns>BitMapImage especificada por el usuario. Null
        /// si el usuario no especifica nada.</returns>
        private BitmapImage LoadImage()
        {
            MessageBox.Show("Escoge una imagen para tu proyecto, si no, te asignaremos una.",
                "Infor", MessageBoxButton.OK, MessageBoxImage.Information);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Escoge la imagen para el proyecto.";
            ofd.DefaultExt = ".jpg";
            ofd.Filter = "Image files|*.jpg;*.png";
            bool? result = ofd.ShowDialog();
            string imgName = "";
            if (result == true)
            {
                imgName = ofd.FileName;
            }
            if (!string.IsNullOrEmpty(imgName))
            {
                BitmapImage bi = new BitmapImage(new Uri(imgName));
                return bi;
            }
            return null;
        }
    }
}
