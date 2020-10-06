using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VentaDisolvente
{
    class Compra
    {
        private static int clave=1000;
        private int idDisolv;
        private int idCliente;
        private int cantidad;
        private int totalC;

        public Compra(int id, int acidez, float pres, int cant)
        {
            clave++;
            idDisolv = Acciones.getIdDisolvente(acidez, pres);
            idCliente = id;
            cantidad = cant;
            totalC = Acciones.calcularPrecio(idDisolv, cantidad);

        }

        public int getClave()
        {
            return clave;
        }

        public int generarCompra()
        {
            
            int res = 0;
            int status = Acciones.actualizarCantidad(idDisolv, cantidad);
            if (status > 0)
            {

                SqlCommand cmd;
                SqlConnection con;              
                DateTime fecha = DateTime.Now;
                MessageBox.Show("compra realizada: " + totalC+".0 $ pesos" + fecha.ToString());
                String query = String.Format("insert into Compra (clave, RFC, idDisolvente, cantidad, totalCompra) values ({0}, '{1}', {2}, {3}, {4})",clave, idCliente.ToString(), idDisolv, cantidad, totalC);
                try
                {
                    con = Conexion.agregarConexion();
                    cmd = new SqlCommand(query, con);
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: " + ex);
                    res = -1;
                }
            }
            return res;
        }



    }
}
