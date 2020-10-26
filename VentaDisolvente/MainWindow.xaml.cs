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

namespace VentaDisolvente
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {          
            bool res = Conexion.comprobarCon(txtUsuario.Text, txtContra.Text);
            Alta w = new Alta();
            if (res)
            {
                w.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("contraseña o usuario incorrectos");
            }

        }
    }
}
