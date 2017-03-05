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
using ExcerciseQueues;

namespace OperacionesQueue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListQueue<string> queue;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Boton para agregar un elemento a la cola
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Enqueue_Click(object sender, RoutedEventArgs e)
        {
            string input = tB_Enqueue.Text;
            if(string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Revisa los datos.");
                return;
            }
            else
            {
                queue.Enqueue(input);
                // actualizar representacion grafica de cola
                l_RepCola.Content = string.Concat("Cola: ", queue.ToString());
                l_Cantidad.Content = string.Concat("Cantidad de elementos en la cola: ", queue.Size());
                tB_Enqueue.Text = string.Empty;
            }
        }

        /// <summary>
        /// Boton para realizar la operacion dequeue en la cola.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Dequeue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ouptut = queue.Dequeue();
                l_ElementoExtraido.Content = string.Concat("Elemento removido: ", ouptut);
                l_Cantidad.Content = string.Concat("Cantidad de elementos en la cola: ", queue.Size());
                // actualizar representacion grafica de cola
                l_RepCola.Content = string.Concat("Cola: ", queue.ToString());
            }
            catch(EmptyQueueException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Boton para realizar la operacion Peek en la cola.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Peek_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ouptut = queue.Front();
                l_ElementoDevuelto.Content = string.Concat("Elemento devuelto: ", ouptut);
                // actualizar representacion grafica de cola
                l_RepCola.Content = string.Concat("Cola: ", queue.ToString());
            }
            catch (EmptyQueueException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // initialize
            queue = new ListQueue<string>();
            // actualizar representacion grafica de cola
            l_RepCola.Content = string.Concat("Cola: ", queue.ToString());
        }
    }
}
