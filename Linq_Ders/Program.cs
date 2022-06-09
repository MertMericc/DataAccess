using System;
using System.Linq;

namespace Linq_Ders
{
    class Program
    {
        static void Main(string[] args)
        {
           
            LinqAggregateFunc();
        }

        private static void LinqAggregateFunc()
        {
            int[] sayilar = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //Linq kullanim sekli 2 ye ayrilir metod kullanimi ve Query kullanimi


            //dizi toplami metod kullanimi
            int total = sayilar.Sum();
            Console.WriteLine("Toplam= " + total);

            //Query kullanimi
            total = (from toplam in sayilar select toplam).Sum();




            int max = sayilar.Max();
            Console.WriteLine("Maximum= " + max);

            double avg = sayilar.Average();
            Console.WriteLine("Ortlama= " + avg);
            avg = (from ortalama in sayilar select ortalama).Average();

            int adet = sayilar.Count();
            Console.WriteLine("ToplamAdet= " + adet);


            //string dizilerde kullanimi
            //aggregate string islemlerinde birlestirme icin kullanilir
            string[] ogrenciler = { "Ali", "Veli", "Ayse", "Fatma" };
            var result = ogrenciler.Aggregate((x, y) => x + "," + y);
            Console.WriteLine(result);
        }
    }
}
