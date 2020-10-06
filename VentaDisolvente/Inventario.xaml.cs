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

        }

        private void Inventario1_Loaded(object sender, RoutedEventArgs e)
        {
            Acciones.llenarAcidez(cbAcidezInv);
            Acciones.llenarPresentacion(cbPresenInv);
        }
    }
}
