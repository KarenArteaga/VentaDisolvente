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
            this.Hide();
            Venta w = new Venta();
            w.Show();
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btPedido_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDevolucion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btInventario_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Inventario w = new Inventario();
            w.Show();

        }
    }
}