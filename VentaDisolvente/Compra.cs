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
        private int clave;
        private int idCliente;
        private int cantidad;
        private int totalC;
        Disolvente d;

        public Compra(int id, int acidez, float pres, int cant)
        {
            d = new Disolvente(acidez, pres);
            clave = Conexion.getUltimaCompra() + 1;
            idCliente = id;
            cantidad = cant;
            totalC = d.calcularPrecio(cantidad);
        }

        public Compra()
        {

        }

        public Compra(int c)
        {
            clave = c;
        }

        public int getClave()
        {
            return clave;
        }

        /**
        * el método generarCompra() inserta una nueva compra a la base de datos y modifica la cantidad existenete llamando al método 
        * actualizarCantidad() de la clase Disolvente
        * no tiene parámetros 
        * regresa el total de la compra o -1 en case de error  
        */
        public int generarCompra()
        {
            
            int status = d.actualizarCantidad(-cantidad);
            if (status > 0)
            {

                SqlCommand cmd;
                SqlConnection con;
                int res;              
                String query = String.Format("insert into Compra (clave, idCliente, idDisolvente, cantidad, totalCompra) values ({0}, {1}, {2}, {3}, {4})",clave, idCliente, d.getId(), cantidad, totalC);
                try
                {
                    con = Conexion.agregarConexion();
                    cmd = new SqlCommand(query, con);
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                    return totalC;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            return -1;
        }

        /**
        * el método devolucion() busca la cantidad de productos de una compra específica, elimina la compra y modifica la 
        * cantida de disolventes en existencia de la compra devuelta
        * no tiene parámetros 
        * regresa 1 o -1 en caso de éxito o error respectivamente  
        */
        public int devolucion()
        {

            int res = 0;
            SqlConnection con;
            SqlCommand cmd1, cmd2;
            SqlDataReader rd;
            int cantidadDev=0, idDisolv=0;

            String query1 = String.Format("select cantidad, idDisolvente from Compra where clave={0}", clave);
            String query2 = String.Format("delete from Compra where clave={0}", clave);

            try
            {               
                con = Conexion.agregarConexion();
                cmd1 = new SqlCommand(query1, con);
                rd = cmd1.ExecuteReader();
                if (rd.Read())
                {
                    cantidadDev = rd.GetInt16(0);
                    idDisolv = rd.GetInt16(1);
                }
                d = new Disolvente(idDisolv);
                d.actualizarCantidad(cantidadDev);
                rd.Close();
                cmd2 = new SqlCommand(query2, con);
                res = cmd2.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex);
                return -1;
            }
        }



        /**
        * el método gananciasTotales() calcula la suma de todas las ventas que se han hecho
        * no tiene parámetros 
        * no lanza excepciones y regresa el la suma total  
        */
        public int gananciasTotales()
        {
            int res = -1;
            String query = String.Format("select sum(totalCompra) from Compra");
            try
            {
                SqlConnection con;
                SqlDataReader rd;
                con = Conexion.agregarConexion();
                SqlCommand cmd = new SqlCommand(query, con);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    res = rd.GetInt32(0);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                res = -1;
            }

            return res;
        }


    }
}
