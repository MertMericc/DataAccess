using System;
using System.Data.SqlClient;

namespace Ado_Giris
{
    class Program
    {
        static void Main(string[] args)
        {
            //SqlConnection veritabanina baglanmamizdan sorumlu class

            string constrEv = "server=DESKTOP-NODMT1N;database=Northwind;integrated security=true;";
            SqlConnection connection = new SqlConnection(constrEv);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Select * from Shippers";

            //2.yöntem
            //SqlConnection con2 = new();
            //con2.ConnectionString = constrEv;


            try
            {
                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine($"{rdr.GetInt32(0)}\t{rdr.GetString(1)}\t{rdr.GetString(2)}");
                }

                //2.yöntem
                //while (rdr.Read())
                //{
                //    Console.WriteLine($"{rdr["ShipperId"]}\t{rdr["CompanyName"]}\t{rdr["Phone"]}");
                //}


            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.WriteLine("Hello World!");
        }
    }
}
