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
                SqlCommand cmd = new SqlCommand("select distinct acidez from Disolvente", con);
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
            cb.Items.Add("250 ml");
            cb.Items.Add("500 ml");
            cb.Items.Add("1.0 l");
            cb.Items.Add("2.0 l");
            cb.Items.Add("10.0 l");
            cb.SelectedIndex = 0;

        }

        public static float getPresentacion(ComboBox cb)
        {
            float res;
            int seleccion = cb.SelectedIndex + 1;
            switch (seleccion)
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

        public static int buscarCantidad(int idDisolv)
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
                MessageBox.Show("no se puede buscar" + ex);
            }
            return res;
        }

        public static int calcularPrecio(int idDisolv, int cant)
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
                    res = rd.GetInt16(0)*cant;
                    
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("no se puede buscar" + ex);
            }
            return res;
        }

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

        public static int actualizarCantidad(int idDisolv, int cant)
        {
            int cuantos = buscarCantidad(idDisolv);
            int res = 0;
            int nuevaCant = cuantos - cant;
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("no se pudo actualizar la cantidad de disolventes " + ex);
                }
            }
            else
                MessageBox.Show("no hay suficientes");
            return res;

        }

        public static int devolucion(int claveComp)
        {

            int res = 0;
            SqlConnection con;
            SqlCommand cmd1, cmd2;
            SqlDataReader rd;
            int cantidadDev, idDisolv;
            
            String query1 = String.Format("select cantidad, idDisolvente from Compra where clave={0}", claveComp) ;
            String query2 = String.Format("delete from Compra where clave={0}", claveComp); 
            
            try
            {
                con = Conexion.agregarConexion();
                cmd1 = new SqlCommand(query1, con);
                rd = cmd1.ExecuteReader();
                cantidadDev = rd.GetInt16(0);
                idDisolv = rd.GetInt16(1);
                int cantidadNueva = buscarCantidad(idDisolv)+cantidadDev;
                actualizarCantidad(idDisolv, cantidadNueva);
                cmd2 = new SqlCommand(query2, con);
                res = cmd2.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo dar de baja" + ex);
            }

            return res;
        }

    }
}

