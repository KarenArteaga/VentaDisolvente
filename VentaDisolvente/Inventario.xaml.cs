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
    /// Lógica de interacción para Inventario.xaml
    /// </summary>
    public partial class Inventario : Window
    {
        public Inventario()
        {
            InitializeComponent();
        }

        private void btBusqPer_Click(object sender, RoutedEventArgs e)
        {
            int acidez = cbAcidezInv.SelectedIndex + 1;
            float pres = Conexion.getPresentacion(cbPresenInv.SelectedIndex);
            Disolvente d = new Disolvente(acidez, pres);
            txtExistencia.Text= d.buscarCantidad().ToString();
            int ganancias = d.gananciasParciales();
            if(ganancias >= 0)
                txtGanPar.Text = ganancias + ".0 $";
            else
                MessageBox.Show("no se pudo actualizar el producto");
            

        }

        private void Inventario1_Loaded(object sender, RoutedEventArgs e)
        {
            Conexion.llenarDisolvente(cbAcidezInv, "acidez");
            Conexion.llenarDisolvente(cbPresenInv, "presentacion");
        }

        private void btRegresar_Click(object sender, RoutedEventArgs e)
        {
            Alta w = new Alta();
            w.Show();
            this.Close();
        }

        private void btActualizar_Click(object sender, RoutedEventArgs e)
        {
            Disolvente d = new Disolvente();
            Compra c = new Compra();
            int cuentaProductos = d.actualizarStock();
            if (cuentaProductos >= 0)
                txtStock.Text = cuentaProductos+ " productos";
            else
                MessageBox.Show("no se pudo actualizar Stock");

            int gananciasTot = c.gananciasTotales();

            if (gananciasTot >= 0)
                txtGanTot.Text = gananciasTot + ".0 $";
            else
                MessageBox.Show("no se pudieron actualizar las ganacias");

        }

    }
}
