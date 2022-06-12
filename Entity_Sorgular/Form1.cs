using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
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

        private void button2_Click(object sender, EventArgs e)
        {
            #region soru
            //siparisler tablosundan musteri sirket adi, calisanadisoyadii
            //siparisid , siparistarihi, ve kargosirket adi getiren sorgu
            #endregion

            #region Sql
            /*selec C.companyname, e.firstname+ ''+
             * e.Lastname,o.orderId,o.OrderDate,s.Companyname
              from orders o
              inner join customers c on o.customerId=c.customerId
            inner join employees e on e.employeesId=o.employeesId
            inner join shippers s on s.shipperId=o.Shipvia

             */
            #endregion

            #region Linq Query
            var resultQuery = from x in dbContext.Orders
                              select new
                              {
                                  MusteriAdi=x.Customers.CompanyName,
                                  Calisan=(x.Employees.FirstName+""+x.Employees.LastName),
                                  SiparisId=x.OrderID,
                                  SiparisTarihi=x.OrderDate,
                                  KargoFirması=x.Shippers.CompanyName
                              };
            #endregion

            #region Linq Metod
            var resultMetod = dbContext.Orders.Select(x => new {

                MusteriAdi = x.Customers.CompanyName,
                Calisan = (x.Employees.FirstName + "" + x.Employees.LastName),
                SiparisId = x.OrderID,
                SiparisTarihi = x.OrderDate,
                KargoFirması = x.Shippers.CompanyName
            });
            dataGridView1.DataSource = resultQuery.ToList();
            #endregion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //CompanyName icerisinde restorant gecen musteriler hangileridir(Customer)
            #region Sql
            //Select * from Customers where CompanyName like '%rest%'
            #endregion

            #region Query
            var resultQuery = from c in dbContext.Customers
                              where c.CompanyName.Contains("rest")
                              select c;
            #endregion

            #region Metod
            var resultMetod = dbContext.Customers.Where(x => x.CompanyName.Contains("rest"));
            #endregion
            dataGridView1.DataSource = resultQuery.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region Soru

            //Calisanlarin adini,soyadini ,dogum tarihini ve yaşını getiren sorgu
            #endregion



            #region Linq Metod
            var result = dbContext.Employees.Select(x => new
            {
                AdiSoyadi = x.FirstName + " " + x.LastName,
                DogumTarihi = x.BirthDate,
                Yas = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)
            });
            #endregion
            #region Linq Query


            var resultQuery = from x in dbContext.Employees
                              select new
                              {
                                  AdiSoyadi = x.FirstName + " " + x.LastName,
                                  DogumTarihi = x.BirthDate,
                                  Yas = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)
                              };
            #endregion
        }

        private void button5_Click(object sender, EventArgs e)
        {
            #region Soru
            // Kategorilerine stoktaki ürün sayısını veren sorgu 
            #endregion
            #region Linq Query

            var resultQuery = from p in dbContext.Products
                              group p by p.Categories.CategoryName into g
                              select new
                              {
                                  KategoriAdi = g.Key,
                                  ToplamStok = g.Sum(p => p.UnitsInStock)
                              };

            #endregion

            #region Linq Metod
            var resultMetod = dbContext.Products.GroupBy(p => p.Categories.CategoryName).Select(g => new
            {
                KategoriAdi = g.Key,
                ToplamStok = g.Sum(x => x.UnitsInStock)
            });


            #endregion
        }

        private void button6_Click(object sender, EventArgs e)
        {
            #region Soru

            // Kategorisi Beverages olan ve UrunAdi:Kola, Fiyati:5.00, StokAdet:500 olan ürün ekleyiniz.
            #endregion

            #region 1.Yol

            // Once CategoryId bulunur 
            int catId = dbContext.Categories.FirstOrDefault(x => x.CategoryName == "Beverages").CategoryID;
            int cat2Id = dbContext.Categories.Where(x => x.CategoryName == "Beverages").FirstOrDefault().CategoryID;

            Products p = new Products();
            p.CategoryID = catId;
            p.ProductName = "Kola";
            p.UnitPrice = 5;
            p.UnitsInStock = 500;

            dbContext.Products.Add(p);


            #endregion

            #region 2.Yol
            dbContext.Products.Add(new Products
            {
                ProductName = "Kola",
                UnitPrice = 5,
                UnitsInStock = 500,
                CategoryID = dbContext.Categories.Where(x => x.CategoryName == "Beverages").FirstOrDefault().CategoryID
            });


            #endregion

            #region 3.Yol
            dbContext.Categories.FirstOrDefault(x => x.CategoryName == "Beverages").Products.Add(new Products
            {
                ProductName = "Kola",
                UnitPrice = 5,
                UnitsInStock = 500
            });
            #endregion
            dbContext.SaveChanges();
            dataGridView1.DataSource = dbContext.Products.ToList();
        }
    }
}
