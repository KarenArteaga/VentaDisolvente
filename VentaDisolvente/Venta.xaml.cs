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
            float pres = Acciones.getPresentacion(cbPresent);
            int cantidad = Int16.Parse(txtCantidad.Text);
            int id= Int16.Parse(txtCliente.Text);         
            if (id == 0)
            {
                Cliente cl;
                cl = new Cliente();
                id = cl.getidCliente();
            }
            Compra comp = new Compra(id, acidez, pres, cantidad);
            MessageBox.Show("se genero la compra: " + comp.getClave());
            if (comp.generarCompra() > 0)
                MessageBox.Show("compra realizada");
            else
                MessageBox.Show("error en la compra");
        }

        private void btCalcular_Click(object sender, RoutedEventArgs e)
        {
            int acidez = cbAcidez.SelectedIndex + 1;
            float pres = Acciones.getPresentacion(cbPresent);
            int cantidad = Int16.Parse(txtCantidad.Text);
            int id = Acciones.getIdDisolvente(acidez, pres);
            txtTotal.Text= Acciones.calcularPrecio(id, cantidad)+".0 $";

        }

        private void Venta_Loaded(object sender, RoutedEventArgs e)
        {
            Acciones.llenarAcidez(cbAcidez);
            Acciones.llenarPresentacion(cbPresent);

        }
    }
}
