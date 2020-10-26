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
            float pres = Conexion.getPresentacion(cbPresent.SelectedIndex);
            int cantidad = Int16.Parse(txtCantidad.Text);
            int id= Int16.Parse(txtCliente.Text);         
            if (id == 0)
            {
                Cliente cl;
                cl = new Cliente();
                cl.crearCliente();
                id = cl.getidCliente();
            }
            Compra comp = new Compra(id, acidez, pres, cantidad);
            int totalC = comp.generarCompra();
            DateTime fecha = DateTime.Now;
            if (comp.generarCompra() > 0)
                MessageBox.Show("compra realizada:  total:  " + totalC + ".0 $ pesos" + fecha.ToString());
            else
                MessageBox.Show("error en la compra");
        }

        private void btCalcular_Click(object sender, RoutedEventArgs e)
        {
            float present = Conexion.getPresentacion(cbAcidez.SelectedIndex);
            MessageBox.Show("" + present);
            Disolvente d = new Disolvente(cbAcidez.SelectedIndex + 1, present);           
            int cantidad = Int16.Parse(txtCantidad.Text);
            int total = d.calcularPrecio(cantidad);
            if (total > 0)
                txtTotal.Text = total + ".0 $";
            else
                MessageBox.Show("error ");
        }

        private void Venta_Loaded(object sender, RoutedEventArgs e)
        {
            Conexion.llenarDisolvente(cbAcidez, "acidez");
            Conexion.llenarDisolvente(cbPresent, "presentacion");

        }

        private void btRegresar_Click(object sender, RoutedEventArgs e)
        {
            Alta w = new Alta();
            w.Show();
            this.Close();
        }

    }
}
