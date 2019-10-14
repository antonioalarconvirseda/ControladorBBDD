using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ControladorBBDD
{
    public partial class Form1 : Form
    {

        string connString = "";
        MySqlConnection sqlConn = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 || textBox2.Text.Length > 0)
            {
                connString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + textBox1.Text + ";";

                using (sqlConn = new MySqlConnection(connString))
                {
                    try
                    {
                        sqlConn.Open();
                        var result = MySqlCollectionQuery(sqlConn, "show tables");
                        comboBox1.Items.Clear();
                        foreach (string nombre in result)
                        {
                            comboBox1.Items.Add(nombre);
                        }
                        string sqlQuery = "SELECT * from " + comboBox1.Items[0];
                        MySqlCommand cmd = new MySqlCommand(sqlQuery, sqlConn);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        try
                        {
                            da.Fill(table);
                        }
                        catch (MySqlException exc)
                        {
                            MessageBox.Show("No se encuentra la tabla " + comboBox1.Items[0], "Error!");
                            return;
                        }
                        dataGridView1.DataSource = new BindingSource(table, null);
                        sqlConn.Close();
                        comboBox1.Text = comboBox1.Items[0].ToString();
                        comboBox1.Enabled = true;
                        textBox2.Enabled = true;
                        label4.Text = comboBox1.Items[0].ToString();
                        MessageBox.Show("Conectado con " + textBox1.Text, "Correcto!");
                    }
                    catch (MySqlException exc)
                    {
                        MessageBox.Show("Error al conectar con " + textBox1.Text, "Error!");
                    }
                }
            }
        }

        public List<string> MySqlCollectionQuery(MySqlConnection connection, string cmd)
        {
            List<string> QueryResult = new List<string>();
            MySqlCommand cmdName = new MySqlCommand(cmd, connection);
            MySqlDataReader reader = cmdName.ExecuteReader();
            while (reader.Read())
            {
                QueryResult.Add(reader.GetString(0));
            }
            reader.Close();
            return QueryResult;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (sqlConn = new MySqlConnection(connString))
            {
                try
                {
                    sqlConn.Open();
                    string sqlQuery = "SELECT * from " + comboBox1.Text;
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, sqlConn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    try
                    {
                        da.Fill(table);
                    }
                    catch (MySqlException exc)
                    {
                        MessageBox.Show("No se encuentra la tabla " + comboBox1.Text, "Error!");
                        return;
                    }
                    dataGridView1.DataSource = new BindingSource(table, null);
                    label4.Text = comboBox1.Text;
                    sqlConn.Close();
                }
                catch (MySqlException exc)
                {
                    MessageBox.Show("Ha ocurrido un problema con la conexión", "Error!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConn.Open();
                string sqlQuery = textBox2.Text;
                if (sqlQuery.Split(' ')[0].ToUpper().Equals("SELECT"))
                {
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, sqlConn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    try
                    {
                        da.Fill(table);
                    }
                    catch (MySqlException exc)
                    {
                        MessageBox.Show("No se ha podido ejecutar su query", "Error!");
                        return;
                    }
                    dataGridView1.DataSource = new BindingSource(table, null);
                    label4.Text = comboBox1.Text;
                }
                sqlConn.Close();
            }
            catch (MySqlException exc)
            {
                MessageBox.Show("Ha ocurrido un problema con la conexión", "Error!");
            }
        }
    }
}
