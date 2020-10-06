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
    /// Lógica de interacción para Venta.xaml
    /// </summary>
    public partial class Venta : Window
    {
        public Venta()
        {
            InitializeComponent();
        }

        private void btHacerVenta_Click(object sender, RoutedEventArgs e)
        {
            int acidez = cbAcidez.SelectedIndex + 1;
            float pres = float.Parse(cbPresent.Text);
            int cantidad = Int16.Parse(txtCantidad.Text);
            int id= Int32.Parse(txtCliente.Text);
            Cliente cl;
            if (id == 0)
            {
                cl = new Cliente();
                id = cl.getidCliente();
            }
            Acciones.hacerCompra(id, acidez, pres, cantidad);
        }

        private void btCalcular_Click(object sender, RoutedEventArgs e)
        {
            int acidez = cbAcidez.SelectedIndex + 1;
            float pres = float.Parse(cbPresent.Text);
            int cantidad = Int16.Parse(txtCantidad.Text);
            txtTotal.Text =(Acciones.buscarCantidad(acidez, pres) * cantidad).ToString();

        }

        private void Venta_Loaded(object sender, RoutedEventArgs e)
        {
            Acciones.llenarAcidez(cbAcidez);
            Acciones.llenarPresentacion(cbPresent);

        }
    }
}
