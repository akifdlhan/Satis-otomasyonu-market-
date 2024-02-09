using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalhanSatisOtomasyon
{
    static class Islemler
    {
        public static double DoubleYap(string deger)
        {
            double sonuc;
            double.TryParse(deger,NumberStyles.Currency,CultureInfo.CurrentUICulture.NumberFormat, out sonuc);
            return sonuc;
        }

        public static void StokAzalt(string barkod,double miktar)
        {
            using(var db =new BarkodDBEntities())
            {
                var urunbilgi = db.Urun.SingleOrDefault(x => x.Barkod == barkod);
                urunbilgi.Miktar -= miktar;
                db.SaveChanges();
            }
        }
        public static void StokArtir(string barkod, double miktar)
        {
            using (var db = new BarkodDBEntities())
            {
                var urunbilgi = db.Urun.SingleOrDefault(x => x.Barkod == barkod);
                urunbilgi.Miktar += miktar;
                db.SaveChanges();
            }
        }
    }
}
