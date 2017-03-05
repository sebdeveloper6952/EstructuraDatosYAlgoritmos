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

namespace OperacionesPila
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Stack<string> stack;
        private ArrayStack<string> stack;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stack = new ArrayStack<string>(10);
            l_Cantidad.Content = string.Concat("Elementos en la pila: ", stack.Size());
        }

        private void btn_Push_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = tB_Push.Text;
                if (!string.IsNullOrEmpty(s))
                    stack.Push(s);
                l_Cantidad.Content = string.Concat("Elementos en la pila: ", stack.Size());
            }
            catch(FullStackException ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_Pop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = stack.Pop();
                l_ElementoExtraido.Content = string.Concat("Elemento extraido: ", s);
                l_Cantidad.Content = string.Concat("Elementos en la pila: ", stack.Size());
            }
            catch(EmptyStackException ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_Peek_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = stack.Peek();
                l_ElementoDevuelto.Content = string.Concat("Elemento devuelto: ", s);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
