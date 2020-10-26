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
    /// Lógica de interacción para Pedido.xaml
    /// </summary>
    public partial class Pedido : Window
    {
        public Pedido()
        {
            InitializeComponent();
        }

        private void btComprobar_Click(object sender, RoutedEventArgs e)
        {
            float present = Conexion.getPresentacion(cbAcidez.SelectedIndex);
            Disolvente d = new Disolvente(cbAcidez.SelectedIndex + 1, present);
            int cuantos = d.buscarCantidad();
            MessageBox.Show("la cantidad actual de disolvente es: " + cuantos);

        }

        private void btHacerPedido_Click(object sender, RoutedEventArgs e)
        {
            float present = Conexion.getPresentacion(cbAcidez.SelectedIndex);
            Disolvente d = new Disolvente(cbAcidez.SelectedIndex + 1, present);
            int nuevC= d.actualizarCantidad(1000);
            if (nuevC <= 0)
                MessageBox.Show("no se pudo realizar el pedido");

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Conexion.llenarDisolvente(cbAcidez, "acidez");
            Conexion.llenarDisolvente(cbPresent, "presentacion");
        }

        private void btRegresar_Click(object sender, RoutedEventArgs e)
        {
            Alta a = new Alta();
            a.Show();
            this.Close();
        }
    }
}
