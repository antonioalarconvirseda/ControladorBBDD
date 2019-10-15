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

        string db = "";
        MySqlConnection sqlConn = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 || textBox2.Text.Length > 0)
            {
                db = textBox1.Text;

                using (sqlConn = Conexion.ObtenerConexion(db))
                {
                    try
                    {
                        var result = Funciones.ObtenerListaQuery("show tables", db);
                        comboBox1.Items.Clear();
                        foreach (string nombre in result)
                        {
                            comboBox1.Items.Add(nombre);
                        }
                        if (Funciones.RellenarTablaDGV(comboBox1.Items[0].ToString(), db) != null)
                        {
                            dataGridView1.DataSource = new BindingSource(Funciones.RellenarTablaDGV(comboBox1.Items[0].ToString(), db), null);
                        }
                        sqlConn.Close();
                        comboBox1.Text = comboBox1.Items[0].ToString();
                        comboBox1.Enabled = true;
                        textBox2.Enabled = true;
                        button3.Enabled = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (sqlConn = Conexion.ObtenerConexion(db))
            {
                try
                {
                    dataGridView1.DataSource = new BindingSource(Funciones.RellenarTablaDGV(comboBox1.Text, db), null);
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
            // En Desarrollo ...
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
