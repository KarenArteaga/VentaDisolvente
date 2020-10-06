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
        public static SqlConnection agregarConexion()
        {
            SqlConnection cnn;
            try
            {
                cnn = new SqlConnection("Data Source=Acer\\SQLEXPRESS;Initial Catalog=VentaDisolvente;Integrated Security=True");
                cnn.Open();

                //MessageBox.Show("conectado");
            }
            catch (Exception ex)
            {
                cnn = null;
                MessageBox.Show("no se pudo conectar" + ex);
            }
            return cnn;
        }

        //Función para comprobar la contraseña
        public static String comprobarCon(string us, string pw)
        {
            string res = "", query;
            //Una manera de hacer el query
            //query = "select contra from usuarios where nombreUsuario =" + us;
            //En esta manera, se remplaza lo que esta en los corchetes con lo que va después de la coma
            query = String.Format("select contra from usuarios where nombreUsuario= '{0}'", us);
            SqlDataReader rd;
            SqlConnection con;

            try
            {
                //conectarme
                con = agregarConexion();
                SqlCommand cmd = new SqlCommand(query, con);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    if (rd.GetString(0).Equals(pw))
                        res = "contraseña correcta";
                    else
                        res = "contraseña incorrecta";
                    con.Close(); //Se cierra conexion con la base, la contraseña es incorrecta 
                }
                else
                    res = "usuario incorrecto";
            }
            catch (Exception ex)
            {
                res = "error" + ex;
            }
            return res;
        }

        public static void llenarAcidez(ComboBox cb)
        {
            try
            {
                SqlConnection con;
                SqlDataReader rd;
                con = agregarConexion();
                SqlCommand cmd = new SqlCommand("select acidez from Disolvente", con);
                rd = cmd.ExecuteReader();
                while (rd.Read()) //Con esta instrucción cambia de renflon a renglón
                {
                    cb.Items.Add(rd["acidez"].ToString()); //Esta instruccion saca de cada renglon el nombre del programa 
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

        public static void llenarPresentacion(ComboBox cb)
        {
            try
            {
                SqlConnection con;
                SqlDataReader rd;
                con = agregarConexion();
                SqlCommand cmd = new SqlCommand("select presentacion from Disolvente where acidez=1", con);
                rd = cmd.ExecuteReader();
                while (rd.Read()) //Con esta instrucción cambia de renflon a renglón
                {
                    cb.Items.Add(rd["presentacion"].ToString()); //Esta instruccion saca de cada renglon el nombre del programa 
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

        public static int buscarCantidad(int aci, float pres)
        {
            int res = 0;
            String query = String.Format("select cantidad from Disolvente where acidez= '{0}' and presentacion= '{1}'", aci, pres);
            SqlDataReader rd;
            try
            {
                SqlConnection con;
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
                MessageBox.Show("no se puede buscar" + ex);
            }
            return res;
        }

        public static float calcularPrecio(int aci, float pres, int cant)
        {
            float res = 0;
            String query = String.Format("select precio from Disolvente where acidez= '{0}' and presentacion= '{1}'", aci, pres);
            SqlDataReader rd;
            try
            {
                SqlConnection con;
                con = Conexion.agregarConexion();
                SqlCommand cmd = new SqlCommand(query, con);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    res = rd.GetFloat(0);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("no se puede buscar" + ex);
            }
            return res * cant;
        }

        public static int getIdDisolvente(int aci, float pres)
        {
            int res = 0;
            String query = String.Format("select idDisolvente from Disolvente where acidez= '{0}' and presentacion= '{1}'", aci, pres);
            SqlDataReader rd;
            try
            {
                SqlConnection con;
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
                MessageBox.Show("no se encontro id Disolvente" + ex);
            }
            return res;
        }

        public static bool hacerCompra(int id, int aci, float pres, int cant)
        {
            bool res = false;
            int cuantos = Conexion.buscarCantidad(aci, pres);
            String query = String.Format("update cantidad set cantidad= '{0}' where acidez = '{1}' and presentacion = '{2}'", cuantos - cant, aci, pres);
            SqlConnection con;
            try
            {
                con = Conexion.agregarConexion();
                SqlCommand cmd1, cmd2;
                if (cuantos - cant > 0)
                {
                    cmd1 = new SqlCommand(query, con);
                    int idDisolv = Conexion.getIdDisolvente(aci, pres);
                    String fecha = DateTime.Today.ToString();
                    String query2 = String.Format("insert into Compra (RFC, idDisolvente, fecha, cantidad) values ('{0}', '{1}', '{2}', '{3}')", id, idDisolv, fecha, cant);
                    cmd2 = new SqlCommand(query2, con);
                    res = true;
                }
                else
                    MessageBox.Show("no hay suficientes");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("no se pudo hacer la compra "+ ex);
            }
            return res;
        }
    }
}

