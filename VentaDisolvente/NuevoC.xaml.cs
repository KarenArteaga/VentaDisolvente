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
    /// Lógica de interacción para NuevoC.xaml
    /// </summary>
    public partial class NuevoC : Window
    {
        public NuevoC()
        {
            InitializeComponent();
        }

        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {           
            Cliente cl;
            cl = new Cliente(txtNombre.Text, txtCorreo.Text, txtDireccion.Text, 0);
            int id = cl.getidCliente();
            txtCliente.Text = id.ToString();
            if (cl.crearCliente() > 0)
                MessageBox.Show("cliente creado exitosamente id: " + id);
            else
                MessageBox.Show("no se pudo agregar" );
        }

        private void btRegresar_Click(object sender, RoutedEventArgs e)
        {
            Alta w = new Alta();
            w.Show();
            this.Close();
        }

        private void btBuscarC_Click(object sender, RoutedEventArgs e)
        {
            String correo = txtCorreo.Text;
            Cliente c = new Cliente(correo);
            String [] str= c.buscarCliente();
            txtCliente.Text = str[0];
            txtNombre.Text = str[1];
            txtDireccion.Text = str[2];
        }
    }
}
