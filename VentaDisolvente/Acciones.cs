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
    class Acciones //esta clase va a ser en la que vamos a poner todos los métodos estáticos 
    {

        public static void llenarAcidez(ComboBox cb)
        {
            try
            {
                SqlConnection con;
                SqlDataReader rd;
                con = Conexion.agregarConexion();
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
                con = Conexion.agregarConexion();
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
            int cuantos = buscarCantidad(aci, pres);
            String query = String.Format("update Disolvente set cantidad= '{0}' where acidez = '{1}' and presentacion = '{2}'", cuantos - cant, aci, pres);
            SqlConnection con;
            try
            {
                con = Conexion.agregarConexion();
                SqlCommand cmd1, cmd2;
                if (cuantos - cant > 0)
                {
                    cmd1 = new SqlCommand(query, con);
                    int idDisolv = getIdDisolvente(aci, pres);
                    float total = calcularPrecio(aci, pres, cant);
                    String fecha = DateTime.Today.ToString();
                    String query2 = String.Format("insert into Compra (RFC, idDisolvente, fecha, cantidad, totalCompra) values ('{0}', '{1}', '{2}', '{3}', '{4}')", id, idDisolv, fecha, cant, total);
                    cmd2 = new SqlCommand(query2, con);
                    res = true;
                }
                else
                    MessageBox.Show("no hay suficientes");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("no se pudo hacer la compra " + ex);
            }
            return res;
        }
    }
}

