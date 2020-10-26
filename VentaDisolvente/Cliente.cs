using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VentaDisolvente
{
    class Cliente
    {
        private static int  idCliente= Conexion.getUltimoCliente();
        private string nombre;
        private string correo;
        private string direccion;
        private int numAnimales;

        public Cliente( string nombre, string correo, string direccion, int numAnimales)
        {
            idCliente++;
            this.nombre = nombre;
            this.correo = correo;
            this.direccion = direccion;
            this.numAnimales = numAnimales;
        }

        public Cliente()
        {
            idCliente++;
            nombre = "";
            correo = "";
            direccion = "";
            numAnimales = 0;
        }

        public Cliente(String cor)
        {
            correo = cor;
        }

        public int getidCliente()
        {
            return idCliente;
        }

        /**
        * el método crearClinete() inserta un cliente nuevo en la base de datos 
        * regresa -1 en caso de error o la clave del nuevo cliente en caso de éxito
        */
        public int crearCliente()
        {
            int res = 0;
            SqlCommand cmd;
            SqlConnection con;
            
            String query = String.Format("insert into Cliente (idCliente, nombre, direccion, correo) values ({0}, '{1}', '{2}', '{3}' )", idCliente, nombre, direccion, correo);
            try
            {
                con = Conexion.agregarConexion();
                cmd = new SqlCommand(query, con);
                res = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                res = -1;
            }
            
            return res;
        }


        /**
        * el método buscarCliente() busca los datos de un cliente específico por medio de su correo
        * no tiene parámetros 
        * no lanza excepciones y regresa una lista de cadenas que representan los datos del cliente 
        */
        public String[] buscarCliente()
        {
            String[] res = new String[4];
            String query;
            query = String.Format("select * from Cliente where correo like '%{0}%' ", correo);
            try
            {
                SqlConnection con;
                SqlDataReader rd;
                con = Conexion.agregarConexion();
                SqlCommand cmd = new SqlCommand(query, con);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    res[0] = rd.GetInt16(0).ToString(); //la clave es int
                    for (int i = 0; i < 4; i++)
                        res[i] = rd.GetString(i);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("no se encontró: " + correo + ex);
            }
            return res;

        }


    }
}
