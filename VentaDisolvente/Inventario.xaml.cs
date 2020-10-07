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
            float pres = Acciones.getPresentacion(cbPresenInv);
            int id= Acciones.getIdDisolvente(acidez, pres);
            txtExistencia.Text= Acciones.buscarCantidad(id).ToString();
            int ganancias = Acciones.gananciasParciales(id);
            if(ganancias >= 0)
                txtGanPar.Text = ganancias + ".0 $";
            else
                MessageBox.Show("no se pudo actualizar el producto");
            

        }

        private void Inventario1_Loaded(object sender, RoutedEventArgs e)
        {
            Acciones.llenarAcidez(cbAcidezInv);
            Acciones.llenarPresentacion(cbPresenInv);
        }

        private void btRegresar_Click(object sender, RoutedEventArgs e)
        {
            Alta w = new Alta();
            w.Show();
            this.Close();
        }

        private void btActualizar_Click(object sender, RoutedEventArgs e)
        {
            int cuentaProductos = Acciones.actualizarStock();
            if (cuentaProductos >= 0)
                txtStock.Text = cuentaProductos+ " productos";
            else
                MessageBox.Show("no se pudo actualizar Stock");

            int gananciasTot = Acciones.gananciasTotales();

            if (gananciasTot >= 0)
                txtGanTot.Text = gananciasTot + ".0 $";
            else
                MessageBox.Show("no se pudieron actualizar las ganacias");

        }

    }
}
