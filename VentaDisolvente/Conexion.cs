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
    class Conexion
    {

        /**
        * el método estático agregarConexion()  abre una conexion entre la aplicacion y la base de datos "bdDisolvente"
        * no tiene parámetros 
        * regresa la conexion abierta cuando el enlace se completó o una conexion de valor nulo cuando no se logró conectar
        */
        public static SqlConnection agregarConexion()
        {
            SqlConnection cnn;
            try
            {
                cnn = new SqlConnection("Data Source=Acer\\SQLEXPRESS;Initial Catalog=bdDisolvente ;Integrated Security=True");
                cnn.Open();
            }
            catch (Exception ex)
            {
                cnn = null;
            }
            return cnn;
        }

        /**
        * el método estático combrobarCon(String, String)  busca la contraseña de un usuario esecífico y la compara con la dada
        * los parámetros us y pw representan el usuario y la contraseña respectivamente  
        * regresa true en caso de que los dato sean correctos o false en caso contrario
        */
        public static bool comprobarCon(string us, string pw)
        {
            bool res;
            string query;

            query = String.Format("select contra from usuarios where nombreUsuario= '{0}'", us);
            SqlDataReader rd;
            SqlConnection con;

            try
            {
                con = agregarConexion();
                SqlCommand cmd = new SqlCommand(query, con);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    if (rd.GetString(0).Equals(pw))
                        res = true;
                    else
                        res = false;
                    con.Close(); 
                }
                else
                    res = false;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        /**
         * el método estático llenarDisolvente(ComboBox, String) llena una componente de tipo Combobox con los datos recuperados de la 
         * base de datos en la columna con el nombre dado de la tabla Disolvente 
         * el parámetro "cb" es el comboBox que se va a llenar
         * el parámetro "nombre" de tipo string representa el nombre de la columna en la que se buscarán los datos
         */
        public static void llenarDisolvente(ComboBox cb, String nombre)
        {
            try
            {
                SqlConnection con;
                SqlDataReader rd;
                con = Conexion.agregarConexion();
                SqlCommand cmd = new SqlCommand("select distinct " + nombre + " from Disolvente", con);
                rd = cmd.ExecuteReader();
                while (rd.Read()) //Con esta instrucción cambia de renglon a renglón
                {
                    cb.Items.Add(rd[nombre].ToString()); //Esta instruccion saca de cada renglon el nombre del programa 
                }
                cb.SelectedIndex = 0; //Para que aparezca la primera opción del ComboBox en la venta
                con.Close();
                rd.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo llenar el combo" + ex);
            }
        }


        /**
        * el método estático getUltimaCompra() busca el máximo entre las claves de todas las compras realizadas en la base de datos
        * no tiene parámetros 
        * regresa la clave máxima
        */
        public static int getUltimaCompra()
        {
            int res = 0;
            String query = String.Format("select max(clave) from Compra");
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
                MessageBox.Show("no se encontró la última compra" + ex);
            }
            return res;

        }

        /**
        * el método estático getIdDisolvente() busca el id del disolvete de acidez y presentacion dadas
        * los parametros aci y pres repsresentan la acidez y presentacion respectivamente 
        * regresa el id del disolvente 
        */
        public static int getIdDisolvente(int aci, float pres)
        {
            int res = 0;
            String query = String.Format("select idDisolvente from Disolvente where acidez= {0} and presentacion= {1}", aci, pres);
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
                MessageBox.Show("no se encontro id Disolvente" + ex);
            }
            return res;
        }

        /**
        * el método estático getUltimoCliente() busca el máximo entre los ids de todos los clientes existentes en la base de datos
        * no tiene parámetros
        * regresa el máximo de los id del cliente 
        */
        public static int getUltimoCliente()
        {
            int res = 0;
            String query = String.Format("select max(idCliente) from Cliente");
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
                MessageBox.Show("no se encontró el último cliente" + ex);
            }
            return res;

        }

        /**
        * el método estático getPresentacion() asigna un valor a los indices de un comboBox que fue llenado con presetaciones de los disolventes
        * el parámetro index representa el indice seleccionado de un ComboBox que contiene todas las presentaciones
        * regresa un dato tipo float que representa la presentacion 
        */
        public static float getPresentacion(int index)
        {
            float res;          
            switch (index+1)
            {
                case 1:
                    res = 0.25F;
                    break;
                case 2:
                    res = 0.5F;
                    break;
                case 3:
                    res = 1.0F;
                    break;
                case 4:
                    res = 2.0F;
                    break;
                default:
                    res = 10.0F;
                    break;

            }
            return res;
        }

    }
}

