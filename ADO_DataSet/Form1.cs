using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_DataSet
{

    public partial class Form1 : Form
    {
        string sqlconstr = "server=DESKTOP-NODMT1N;database=Northwind;integrated security=true;";
        SqlConnection connection;
        SqlCommand cmd;
        DataTable tbl;
        DataSet dsNorthwind;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "Select * from Customers";
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            try
            {
                connection.Open();
                tbl.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = tbl;
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(sqlconstr);
            cmd = new SqlCommand();
            dsNorthwind = new DataSet();
            tbl = new DataTable();
        }
    }
}
