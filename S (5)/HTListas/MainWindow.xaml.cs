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
using Comida;

namespace HTListas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CItem> itemsInput;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_UnirListas_Click(object sender, RoutedEventArgs e)
        {
            // llenar lista resultado con items de lista 1 y lista 2, en ese orden
            lV_Unir3.Items.Clear();
            List<string> source1 = new List<string>();
            List<string> source2 = new List<string>();
            List<string> output = new List<string>();
            foreach (string s in lV_Unir1.Items)
                source1.Add(s);
            foreach (string s in lV_Unir2.Items)
                source2.Add(s);
            UnirListas(source1, source2, output);
            foreach (string s in output)
                lV_Unir3.Items.Add(s);
        }

        private void btn_SepararListas_Click(object sender, RoutedEventArgs e)
        {
            /* separar lista input en 2 listas, en una los indices pares de input y
               en otra elementos impares */
            lV_Separar2.Items.Clear();
            lV_Separar3.Items.Clear();
            List<string> source = new List<string>();
            List<string> output1 = new List<string>();
            List<string> output2 = new List<string>();
            foreach (string s in lV_Separar1.Items)
                source.Add(s);
            SepararLista(source, output1, output2);
            foreach (string s in output1)
                lV_Separar2.Items.Add(s);
            foreach (string s in output2)
                lV_Separar3.Items.Add(s);
        }

        private void btn_RecorrerLista_Click(object sender, RoutedEventArgs e)
        {
            // recorrer cada elemento de la lista y incrementar su precio en Q1.00
            itemsInput.ForEach(RecorrerListaParaAumentarPrecio);
            lV_Recorrer2.Items.Clear();
            foreach (CItem i in itemsInput)
                lV_Recorrer2.Items.Add(i.ToString());
        }

        private void btn_UnirYOrdenar_Click(object sender, RoutedEventArgs e)
        {
            // unir ambas listas, el resultado debe estar ordenado
            lV_UnirOrd3.Items.Clear();
            List<string> source1 = new List<string>();
            List<string> source2 = new List<string>();
            List<string> output = new List<string>();
            foreach (string s in lV_UnirOrd1.Items)
                source1.Add(s);
            foreach (string s in lV_UnirOrd2.Items)
                source2.Add(s);
            UnirYOrdenarListas(source1, source2, output);
            foreach (string s in output)
                lV_UnirOrd3.Items.Add(s);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            itemsInput = new List<CItem>();
            // unir listas
            lV_Unir1.Items.Add("Mitsubishi");
            lV_Unir1.Items.Add("Honda");
            lV_Unir1.Items.Add("Nissan");
            lV_Unir2.Items.Add("Lancer");
            lV_Unir2.Items.Add("Civic");
            lV_Unir2.Items.Add("GT-R");
            // separar listas
            lV_Separar1.Items.Add("Mitsubishi");
            lV_Separar1.Items.Add("Lancer");
            lV_Separar1.Items.Add("Honda");
            lV_Separar1.Items.Add("Civic");
            lV_Separar1.Items.Add("Nissan");
            lV_Separar1.Items.Add("GT-R");
            lV_Separar1.Items.Add("Mazda");
            lV_Separar1.Items.Add("RX-7");
            // recorrer lista
            CItem item = new CItem("Hamburguesa", "", 15f);
            lV_Recorrer1.Items.Add(item);
            itemsInput.Add(item);
            item = new CItem("Papas", "", 5f);
            lV_Recorrer1.Items.Add(item);
            itemsInput.Add(item);
            item = new CItem("Quesoburguesa", "", 18f);
            lV_Recorrer1.Items.Add(item);
            itemsInput.Add(item);
            item = new CItem("Papuburguesa", "", 30f);
            lV_Recorrer1.Items.Add(item);
            itemsInput.Add(item);
            // unir y ordenar lista
            lV_UnirOrd1.Items.Add("Rojo");
            lV_UnirOrd1.Items.Add("Anaranjado");
            lV_UnirOrd1.Items.Add("Negro");
            lV_UnirOrd1.Items.Add("Blanco");
            lV_UnirOrd2.Items.Add("Azul");
            lV_UnirOrd2.Items.Add("Amarillo");
            lV_UnirOrd2.Items.Add("Verde");
        }

        private void UnirListas<T>(List<T> source1, List<T> source2, List<T> output)
        {
            output.Clear();
            foreach (T s in source1)
            {
                output.Add(s);
            }
            foreach (T s in source2)
            {
                output.Add(s);
            }
        }

        private void SepararLista<T>(List<T> source, List<T> output1, List<T> output2)
        {
            bool isEven = true;
            for(int i = 0; i < source.Count; i++)
            {
                if(isEven)
                {
                    output1.Add(source[i]);
                }
                else
                {
                    output2.Add(source[i]);
                }
                isEven = !isEven;
            }
        }

        private void RecorrerListaParaAumentarPrecio(CItem i)
        {
            float precioActual = i.GetPrecio();
            i.SetPrecio(precioActual + 1f);
        }

        private void UnirYOrdenarListas<T>(List<T> source1, List<T> source2, List<T> output)
        {
            output.Clear();
            foreach (T i in source1)
                output.Add(i);
            foreach (T i in source2)
                output.Add(i);
            output.Sort();
        }
    }
}
