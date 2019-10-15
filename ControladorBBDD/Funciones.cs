using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace ControladorBBDD
{
    public class Funciones
    {
        public static List<string> ObtenerListaQuery(string cmd, string db)
        {
            List<string> QueryResult = new List<string>();
            MySqlCommand cmdNombre = new MySqlCommand(cmd, Conexion.ObtenerConexion(db));
            MySqlDataReader reader = cmdNombre.ExecuteReader();
            while (reader.Read())
            {
                QueryResult.Add(reader.GetString(0));
            }
            reader.Close();
            return QueryResult;
        }

        public static DataTable RellenarTablaDGV(string nombre_tabla, string db)
        {
            string sqlQuery = "SELECT * from " + nombre_tabla;
            MySqlCommand cmd = new MySqlCommand(sqlQuery, Conexion.ObtenerConexion(db));
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            try
            {
                da.Fill(table);
            }
            catch (MySqlException exc)
            {
                MessageBox.Show("No se encuentra la tabla " + nombre_tabla, "Error!");
                return null;
            }
            return table;
        }
    }
}
