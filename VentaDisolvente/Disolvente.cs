using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VentaDisolvente
{
    class Disolvente
    {

        private int idDisolv;
        private float present;
        private int acidez;


        public Disolvente(int aci, float pres)
        {
            present = pres;
            acidez = aci;
            idDisolv = Conexion.getIdDisolvente(aci, pres);        

        }

        public Disolvente(int id)
        {
            idDisolv = id;
        }

        public Disolvente()
        {

        }

        public int getId()
        {
            return idDisolv;
        }

        /**
        * el método calcularPrecio() busca el precio de un disolvente específico y lo multiplica por la cantidad
        * el parámetro es la cantidad de productos que se requieren  
        * no lanza excepciones y regresa la multiplicacion de la cantidad por el precio o -1 en caso de error  
        */
        public int calcularPrecio(int cant)
        {
            int res = 0;
            String query = String.Format("select precio from Disolvente where idDisolvente={0}", idDisolv);
            SqlDataReader rd;
            try
            {
                SqlConnection con;
                con = Conexion.agregarConexion();
                SqlCommand cmd = new SqlCommand(query, con);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    res = rd.GetInt16(0) * cant;

                }
                con.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return res;
        }


        /**
        * el método buscarCantidad() calcula la cantidad de disolventes que se tienen en existencia de una clave específica
        * no tiene parámetros 
        * no lanza excepciones y regresa el numero de disolventes existentes o -1 en caso de error  
        */

        public int buscarCantidad()
        {
            int res = 0;
            String query = String.Format("select cantidad from Disolvente where idDisolvente= {0}", idDisolv);
            SqlDataReader rd;
            try
            {
                SqlConnection con;
                con = Conexion.agregarConexion();
                SqlCommand cmd = new SqlCommand(query, con);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    res = rd.GetInt16(0);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return res;
        }

        /**
        * el método actualizarCantidad() modifica la cantidad de disolventes existentes de cierta clave
        * el parámetro cant respresenta la cantidad a añadir a la suma
        * no lanza excepciones y regresa la nueva cantidad o -1 en caso de error
        */
        public int actualizarCantidad(int cant)
        {
            int cuantos = buscarCantidad();
            int res = 0;
            int nuevaCant = cuantos + cant;
            if (nuevaCant >= 0)
            {
                SqlCommand cmd;
                String query = String.Format("update Disolvente set cantidad= {0} where idDisolvente= {1}", nuevaCant, idDisolv);
                SqlConnection con;
                try
                {
                    con = Conexion.agregarConexion();
                    cmd = new SqlCommand(query, con);
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                    return nuevaCant;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            else
                return -1;

        }

        /**
        * el método actualizarStock() calcula la cantidad de productos totales que se tienen en existencia
        * no tiene parámetros 
        * no lanza excepciones y regresa la suma de la cantidad de disolventes totales 
        */
        public int actualizarStock()
        {
            int res = -1;
            String query = String.Format("select sum(cantidad) from Disolvente");
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


        /**
         * el método gananciasParciales() permite conocer todas las ganancias que ha generado un producto en específico
         * no tiene parámetros ya que utiliza los atributos de la clase
         * no lanza excepciones y regresa la suma que ha generado un Disolvente con una clave 
         */
        public int gananciasParciales()
        {
            int res = -1;
            String query = String.Format("select sum(totalCompra) from Compra where idDisolvente = {0}", idDisolv);
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
                res = 0;
            }

            return res;
        }
    }
}
