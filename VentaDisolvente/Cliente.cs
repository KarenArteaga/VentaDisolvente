﻿using System;
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
        private static int  idCliente= Acciones.getUltimoCliente();
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

        public int getidCliente()
        {
            return idCliente;
        }

        public int crearCliente()
        {
            int res = 0;
            SqlCommand cmd;
            SqlConnection con;
            
            String query = String.Format("insert into Cliente (RFC, nombre, direccion, correo) values ('{0}', '{1}', '{2}', '{3}' )", idCliente.ToString(), nombre, direccion, correo);
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
            
            return res;
        }


    }
}
