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

namespace VentaDisolvente
{
    /// <summary>
    /// Lógica de interacción para Devolucion.xaml
    /// </summary>
    public partial class Devolucion : Window
    {
        public Devolucion()
        {
            InitializeComponent();
        }

        private void btDevolver_Click(object sender, RoutedEventArgs e)
        {            
            int claveCompra = Int16.Parse(txtClave.Text);
            MessageBox.Show(claveCompra.ToString());
            Compra c = new Compra(claveCompra);
            if (c.devolucion() > 0)
                MessageBox.Show("devolucion exitosa: " + DateTime.Now);
            else
                MessageBox.Show("no se pudo hacer la devolucion: " + DateTime.Now);
        }

        private void btRegresar_Click(object sender, RoutedEventArgs e)
        {
            Alta a = new Alta();
            a.Show();
            this.Close();         
        }
    }
}
