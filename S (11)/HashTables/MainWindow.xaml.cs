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
using MyDataStructures.Hashtable;
using MyDataStructures.Lists;

namespace HashTables
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HashtableConColision<int, CEmpleado> hashTEmpleados;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hashTEmpleados = new HashtableConColision<int, CEmpleado>(10);
        }

        private void btn_Ingresar_Click(object sender, RoutedEventArgs e)
        {
            // validar datos y crear CEmpleado
            if (string.IsNullOrEmpty(tB_Nombre.Text) ||
                    string.IsNullOrEmpty(tB_Apellido.Text))
            {
                MessageBox.Show("Revisa los datos.");
                return;
            }
            try
            {
                int n = int.Parse(tB_ID.Text);
                CEmpleado em = new CEmpleado(tB_Nombre.Text, tB_Apellido.Text,n);
                hashTEmpleados.Add(n, em);
                MessageBox.Show("Se ingreso el empleado.");
                tB_Nombre.Text = string.Empty;
                tB_Apellido.Text = string.Empty;
                tB_ID.Text = string.Empty;
            }
            catch(ArgumentException ex0) { MessageBox.Show("Ya existe un empleado con esa clave!"); }
            catch { MessageBox.Show("Revisa los datos."); }
        }

        /// <summary>
        /// Hemos trabajado con una hashtable que acepta colisiones. Como visto en clase,
        /// la hashtable contiene un arreglo de listas enlazadas. Elementos que caen en el 
        /// mismo indice son guardados en la misma lista enlazada. Asi que nuestro metodo
        /// de buscar en la hashtable devuelve una lista enlazada. Hemos decidido hacer nuestra
        /// hash table de esta manera para que se mas flixible, y que se el cliente el que
        /// decida que hacer con esa lista enlazada, si quiere mostrar todos los registros,
        /// o solo uno, el decide.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Buscar_Click(object sender, RoutedEventArgs e)
        {
            // validar datos
            try
            {
                // se consigue la clave, el numero de empleado
                int n = int.Parse(tB_B_ID.Text);
                
                CEmpleado em = hashTEmpleados.Get(n);
                /* si luego de recorrer la lisa, el objeto de CEmpleado aun
                   es null, entonces no existe empleado con el ID que se
                   especifico. */
                if(em != null)
                    l_NombreEmpleado.Content = em.nombre + " " + em.apellido;
            }
            catch(ArgumentException ex0)
            {
                l_NombreEmpleado.Content = "No hay resultado.";
                MessageBox.Show("No hay empleado con ese ID.");
            }
            catch(Exception ex1)
            {
                MessageBox.Show("Revisa los datos.");
            }
        }

        private void btn_Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tB_B_ID.Text))
            {
                MessageBox.Show("Revisa el ID.");
                return;
            }
            try
            {
                int id = int.Parse(tB_B_ID.Text);
                hashTEmpleados.Remove(id);
                MessageBox.Show("Se elimino el empleado.");
                tB_B_ID.Text = string.Empty;
                l_NombreEmpleado.Content = string.Empty;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
