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
        DataTable tblProduct;
        DataTable tblSupplier;
        DataSet dsNorthwind;
        SqlDataAdapter adap;
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
            cmd.Connection = connection;
            dsNorthwind = new DataSet();
            tbl = new DataTable();
            adap = new SqlDataAdapter();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Products";

            tblProduct = new DataTable("Products");
                tblProduct.Load(cmd.ExecuteReader());

           if(dsNorthwind.Tables["Products"]==null)
            dsNorthwind.Tables.Add(tblProduct);
            dataGridView1.DataSource = dsNorthwind.Tables["Products"];

            connection.Close();

            //try
            //{

            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = "Select * from Products";

            //    connection.Open();
            //    tbl.Load(cmd.ExecuteReader());
            //    dataGridView1.DataSource = tbl;
            //}
            //catch (SqlException ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    connection.Close();
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            tblSupplier = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Suppliers";
            DataTable table = new DataTable("Suppliers");
            table.Load(cmd.ExecuteReader());

            if (dsNorthwind.Tables["Suppliers"] == null)
                dsNorthwind.Tables.Add(table);

            //tblSupplier = new();
            //tblSupplier.Load(cmd.ExecuteReader());
            //dataGridView1.DataSource = tblSupplier;
          

                dataGridView1.DataSource = dsNorthwind.Tables["Suppliers"];

          
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            adap.SelectCommand = new SqlCommand("Select * from Orders", connection);
            adap.Fill(dsNorthwind, "Orders");

            dataGridView1.DataSource = dsNorthwind.Tables["Orders"];
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SalesByCategory";
            cmd.Parameters.AddWithValue("@CategoryName", "Produce");
            DataTable table = new DataTable("Suppliers");
            table.Load(cmd.ExecuteReader());

            dataGridView1.DataSource = table;
            connection.Close();
        }
    }
}
