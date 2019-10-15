using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ControladorBBDD
{
    class Conexion
    {
        public static MySqlConnection ObtenerConexion(string db)
        {
            MySqlConnection conectar = new MySqlConnection("server=127.0.0.1; database="+db+"; Uid=root; pwd=;");

            conectar.Open();
            return conectar;
        }
    }
}
