using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entity_Sorgular
{
    public partial class Form1 : Form
    {
        NorthwindEntities dbContext;
        public Form1()
        {
            InitializeComponent();
            dbContext = new NorthwindEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Fiyati 20-50 arasinda olan urunlerin bilgisini bastırma
            //Sql sorgusu: Select ProductID,ProductName,UnitPrice,UnitsInStock,CategoryName from Product p inner join Cateogries c on p.categoryId=c.categoryUd
            //where p.UnitPrice>20 and p.UnitPrice<50

            //1.yöntem Linq metod
            var result = dbContext.Products
                .Where(x => x.UnitPrice > 20 && x.UnitPrice < 50)
                .OrderBy(x => x.UnitPrice)
                .Select(x => new
                {
                    x.ProductID,
                    x.ProductName,
                    x.UnitPrice,
                    x.UnitsInStock,
                    x.Categories.CategoryName

                });
            dataGridView1.DataSource = result.ToList();


            //2.yontem query yontem

            //var resultQuery = from p in dbContext.Products
            //                  where p.UnitPrice > 20 && p.UnitPrice < 50
            //                  orderby p.UnitPrice
            //                  select new
            //                  {
            //                      UrunId = p.ProductID,
            //                      UrunAdi = p.ProductName,
            //                      UrunFiyat = p.UnitPrice,
            //                      StokMiktari = p.UnitsInStock,
            //                      Kategorisi = p.Categories.CategoryName
            //                  };
            //dataGridView1.DataSource = resultQuery.ToList();

        }
    }
}
