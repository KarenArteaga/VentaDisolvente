﻿using System;
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
    /// Lógica de interacción para Alta.xaml
    /// </summary>
    public partial class Alta : Window
    {
        public Alta()
        {
            InitializeComponent();
        }

        private void btVenta_Click(object sender, RoutedEventArgs e)
        {      
            Venta w = new Venta();
            w.Show();
            this.Close();
        }

        private void btPedido_Click(object sender, RoutedEventArgs e)
        {
            Pedido p = new Pedido();
            p.Show();
            this.Close();
        }

        private void btDevolucion_Click(object sender, RoutedEventArgs e)
        {
            Devolucion w = new Devolucion();
            w.Show();
            this.Close();

        }

        private void btInventario_Click(object sender, RoutedEventArgs e)
        {
            Inventario w = new Inventario();
            w.Show();
            this.Close();

        }

        private void btNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            NuevoC w = new NuevoC();
            w.Show();
            this.Close();
        }

        private void btSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
