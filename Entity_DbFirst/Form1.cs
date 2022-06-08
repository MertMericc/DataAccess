using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entity_DbFirst
{
    public partial class Form1 : Form
    {
        //EF'un bizim icin cıkardigi DbContext ten örnek aldim
        NorthwindEntities db;
        public Form1()
        {
            InitializeComponent();
            db = new NorthwindEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Customers.ToList();
        }
    }
}
