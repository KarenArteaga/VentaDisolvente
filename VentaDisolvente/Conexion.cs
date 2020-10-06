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

    }
}

