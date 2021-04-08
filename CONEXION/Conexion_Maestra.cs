using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Punto_de_Venta.CONEXION
{
    class Conexion_Maestra
    {
        public static string conexion = @"Data source=LAPTOP-F9VT67M5\SQLSERVER2019;" +
                                        "Initial Catalog=bd_Punto_de_Venta;" +
                                        "Integrated Security=True";//Convert.ToString(CONEXION.Desencryptacion.checkServer());
  
        public static SqlConnection conectar = new SqlConnection(conexion);
        public static void abrir()
        {
            if (conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }
        public static void cerrar()
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }

    }
}
