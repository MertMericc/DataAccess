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
        int ShipperId = 0;
        public Form1()
        {
            InitializeComponent();
            db = new NorthwindEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //tüm müsterileri getirir
            //dataGridView1.DataSource = db.Customers.ToList();

            //musterilerden londrada olanlari listeleme
            //Linq kutuphanesi kullanilmali

            //1.yol metod ile sorgulama
            var result = db.Customers.Where(x => x.City == "london");


            //2.yol Query yontemi
            var sonuc = from musteri in db.Customers
                        where musteri.City == "london"
                        select musteri;





            dataGridView1.DataSource = result.ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Shippers.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Shippers shippers = new Shippers();
            shippers.CompanyName = txtFirma.Text;
            shippers.Phone = txtTelefon.Text;
            db.Shippers.Add(shippers);
            db.SaveChanges();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShipperId = (int)dataGridView1.CurrentRow.Cells[0].Value;
            txtFirma.Text= (string)dataGridView1.CurrentRow.Cells[1].Value;
            txtTelefon.Text= (string)dataGridView1.CurrentRow.Cells[2].Value;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Metod yonetimi
            Shippers sp = db.Shippers.Where(x => x.ShipperID == ShipperId).FirstOrDefault();

            //Query yontemi
            //var shipper = from ship in db.Shippers
            //              where ship.ShipperID == ShipperId
            //              select ship;

            sp.CompanyName = txtFirma.Text;
            sp.Phone = txtTelefon.Text;
            db.SaveChanges();
            dataGridView1.DataSource = db.Shippers.ToList();
        }
    }
}
