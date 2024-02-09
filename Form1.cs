using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DalhanSatisOtomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BarkodDBEntities db = new BarkodDBEntities();
        //Barkodda enter basınca
        private void tBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string barkod=tBarkod.Text.Trim();
                if (barkod.Length <= 2)
                {
                    //tMiktar.Text = barkod;
                    tBarkod.Clear();
                    tBarkod.Focus();
                }
                else
                {
                    if(db.Urun.Any(a=> a.Barkod == barkod))
                    {
                        var urun = db.Urun.Where(a => a.Barkod == barkod).FirstOrDefault();
                        UrunGetirListeye(urun, barkod,Convert.ToDouble( tMiktar.Text));
                    }
                    else
                    {
                        //eğer birim olarak kg olursa "27"
                        int onek =Convert.ToInt32( barkod.Substring(0, 2));
                        if (db.Terazi.Any(a => a.TeraziOnEk == onek))
                        {
                            string teraziUrunNo=barkod.Substring(2, 5);
                            if (db.Urun.Any(a => a.Barkod == teraziUrunNo))
                            {
                                var urunterazi = db.Urun.Where(a=> a.Barkod==teraziUrunNo).FirstOrDefault();
                                double miktarKg = Convert.ToDouble(barkod.Substring(7, 5))/1000;
                                UrunGetirListeye(urunterazi, teraziUrunNo, miktarKg);
                            }

                        }
                    }
                }
                gridSatisListesi.ClearSelection();
                gTop();
                tBarkod.Focus();



                if (tBarkod.Text == "8690632243726")
                {
                    Console.Beep(900, 1000);
                    fUrunGiris f=new fUrunGiris();
                    
                    f.ShowDialog();
                }
            }
        }
            //listeye ürün getirme (datagrid)
        private void UrunGetirListeye(Urun urun,string barkod ,double miktar)
        {
            
            int satirSayisi = gridSatisListesi.Rows.Count;
            //double miktar = Convert.ToDouble(tMiktar.Text);
            bool eklenmismi = false;
            if (satirSayisi > 0)
            {
                for (int i = 0; i < satirSayisi; i++)
                {
                    if (gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString() == barkod)
                    {
                        gridSatisListesi.Rows[i].Cells["Miktar"].Value = miktar + Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value);
                        gridSatisListesi.Rows[i].Cells["Toplam"].Value = Math.Round(Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value) * Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Fiyat"].Value), 2);
                        eklenmismi = true;
                    }
                }
            }
            if (!eklenmismi)
            {
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirSayisi].Cells["Barkod"].Value = barkod;
                gridSatisListesi.Rows[satirSayisi].Cells["UrunAdi"].Value = urun.UrunAd;
                gridSatisListesi.Rows[satirSayisi].Cells["UrunGrup"].Value = urun.UrunGrup;
                gridSatisListesi.Rows[satirSayisi].Cells["Birim"].Value = urun.Birim;
                gridSatisListesi.Rows[satirSayisi].Cells["Fiyat"].Value = urun.SatisFiyati;
                gridSatisListesi.Rows[satirSayisi].Cells["Miktar"].Value = miktar;
                gridSatisListesi.Rows[satirSayisi].Cells["Toplam"].Value = Math.Round(miktar * (double)urun.SatisFiyati, 2);
                gridSatisListesi.Rows[satirSayisi].Cells["AlisFiyat"].Value = urun.AlisFiyati;
                gridSatisListesi.Rows[satirSayisi].Cells["KdvTutari"].Value = urun.KdvTutari;
                tMiktar.Text = "1";

            }
        }
        private void gTop()
        {
            if(gridSatisListesi.Rows.Count > 0)
            {
                double top = 0;
                for(int i = 0 ; i<gridSatisListesi.Rows.Count ; i++ )
                {
                    top += Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Toplam"].Value);
                }
                tGenelToplam.Text=top.ToString("C2");
                tMiktar.Text = "1";
                tBarkod.Clear();
                tBarkod.Focus();
            }
        }

        //datagrid sil butonu clik
        private void gridSatisListesi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == gridSatisListesi.Columns["Sil"].Index)
            {
                gridSatisListesi.Rows.RemoveAt(e.RowIndex);
                tMiktar.Text = "1";
                gTop();
                if (gridSatisListesi.Rows.Count==0)
                {
                    tGenelToplam.Text = "";

                }
            }
        }


        // HizliButton
        private void HizliButtonn()
        {
            var hizliurun = db.HizliUrun.ToList();
            foreach(var item in hizliurun)
            {
                Button bH = this.Controls.Find("bH" + item.Id, true).FirstOrDefault() as Button;
                if(bH != null)
                {
                    
                    bH.Text = item.UrunAd + "\n" +("₺ ")+ item.fiyat;
                  
                }  
            }
        }


        private void HizliButtonClick(object nesne, EventArgs e)
        {
            Button b = (Button)nesne; 
            int buttonId=Convert.ToUInt16(b.Name.ToString().Substring(2,b.Name.ToString().Length-2));
            //bH kontrolü
            if (b.Text.ToString().StartsWith("-"))
            {
                fHizliButonUrunEkle f=new fHizliButonUrunEkle();
                f.lbuttonId.Text = buttonId.ToString();
                f.ShowDialog();
            }
            else
            {
            var urunBarkod=db.HizliUrun.Where(a=>a.Id==buttonId).Select(a=>a.Barkod).FirstOrDefault();
            
            var urun = db.Urun.Where(a=>a.Barkod==urunBarkod).FirstOrDefault();

            UrunGetirListeye(urun,urunBarkod,Convert.ToDouble(tMiktar.Text));gTop();}
        }

        private void bh_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                Button b = (Button)sender;
                if (!b.Text.StartsWith("-"))
                {
                    int butonid = Convert.ToInt32(b.Name.ToString().Substring(2, b.Name.Length - 2));
                    ContextMenuStrip s=new ContextMenuStrip();
                    ToolStripMenuItem delete=new ToolStripMenuItem();
                    delete.Text="Temizle - Buton No: "+ butonid.ToString();
                    delete.Click += Delete_Click;
                    s.Items.Add(delete);
                    this.ContextMenuStrip = s;
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int butonid = Convert.ToInt16(sender.ToString().Substring(19,sender.ToString().Length - 19));
            var guncelle = db.HizliUrun.Find(butonid);
            guncelle.Barkod = "-";
            guncelle.UrunAd="-";
            guncelle.fiyat = 0;
            db.SaveChanges();
            double fiyat=0;
            Button b = this.Controls.Find("bH"+butonid,true).FirstOrDefault() as Button;
            b.Text = "-"+"\n"+fiyat.ToString("C2");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gridSatisListesi.CellClick += new DataGridViewCellEventHandler(gridSatisListesi_CellContentClick);
            HizliButtonn();
            b5.Text=5.ToString("C2");
            b10.Text = 10.ToString("C2");
            b20.Text = 20.ToString("C2");
            b50.Text = 50.ToString("C2");
            b100.Text = 100.ToString("C2");
            b200.Text = 200.ToString("C2");
        }

        private void bNx_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            if (b.Text == ",")
            {
                int virgul = tNumarator.Text.Count(x => x == ',');
                if (virgul <1)
                {
                    tNumarator.Text += b.Text;
                }
            }else if (b.Text == "<") {
                if(tNumarator.Text.Length > 0)
                {
                    tNumarator.Text = tNumarator.Text.Substring(0,tNumarator.Text.Length-1);
                }
            }
            else
            {
                tNumarator.Text+= b.Text;
            }
        }

      
        //Adet hızlı
        private void bAdet_Click(object sender, EventArgs e)
        {
            if(tNumarator.Text != "")
            {
                tMiktar.Text=tNumarator.Text;
                tNumarator.Clear();
                tBarkod.Clear();
                tBarkod.Focus();
            }
        }
        //odenenHızlı
        private void bOdenen_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text != "")
            {
                double sonuc = Islemler.DoubleYap(tNumarator.Text)-Islemler.DoubleYap(tGenelToplam.Text);
                double odenen = Islemler.DoubleYap(tNumarator.Text);
                tOdenen.Text = odenen.ToString("C2");
                tParaustu.Text = sonuc.ToString("C2");
                tNumarator.Clear();
                tBarkod.Focus();
            }
        }
        //barkod hızlı
       


        private void ParaUstuHesapla_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            double sonuc = Islemler.DoubleYap(b.Text) - Islemler.DoubleYap(tGenelToplam.Text);
            double odenen = Islemler.DoubleYap(b.Text);
            tOdenen.Text = odenen.ToString("C2");
            tParaustu.Text = sonuc.ToString("C2");
        }

        private void bBarkod_Click_1(object sender, EventArgs e)
        {
            if (tNumarator.Text != "")
            {
                if (db.Urun.Any(a => a.Barkod == tNumarator.Text))
                {
                    var urun = db.Urun.Where(a=>a.Barkod==tNumarator.Text).FirstOrDefault();
                    UrunGetirListeye(urun,tNumarator.Text,Convert.ToDouble(tMiktar.Text));
                    tNumarator.Clear();
                    tBarkod.Focus();
                }
                else
                {
                    MessageBox.Show("Ürün Yok");
                }
            }
        }

        private void bDigerUrun_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text != "")
            {
                int satirsayisi = gridSatisListesi.Rows.Count;
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirsayisi].Cells["Barkod"].Value = "1111111111116";
                gridSatisListesi.Rows[satirsayisi].Cells["UrunAdi"].Value = "Barkodsuz Ürün";
                gridSatisListesi.Rows[satirsayisi].Cells["UrunGrup"].Value = "Barkodsuz Ürün";
                gridSatisListesi.Rows[satirsayisi].Cells["Birim"].Value = "Adet";
                gridSatisListesi.Rows[satirsayisi].Cells["Miktar"].Value = 1;
                gridSatisListesi.Rows[satirsayisi].Cells["Fiyat"].Value=Convert.ToDouble(tNumarator.Text);
                gridSatisListesi.Rows[satirsayisi].Cells["KdvTutari"].Value = 0;
                gridSatisListesi.Rows[satirsayisi].Cells["Toplam"].Value = Convert.ToDouble(tNumarator.Text);
                tNumarator.Text = "";
                gTop();
                tBarkod.Focus();
            }
        }


        private void bTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        public void Temizle()
        {
            tMiktar.Text="1";
            tBarkod.Clear();
            tOdenen.Clear();
            tParaustu.Clear();
            tGenelToplam.Text=0.ToString("C2");
            chSatisİadeİslemi.Checked = false;
            tNumarator.Clear();
            gridSatisListesi.Rows.Clear();
            tBarkod.Clear();
            tBarkod.Focus();
        }


        public void SatisYap(string odemesekli)
        {
            int satirsayisi = gridSatisListesi.Rows.Count;
            bool satisiade = chSatisİadeİslemi.Checked;
            double alisfiyattoplam = 0;
            if (satirsayisi > 0)
            {
                int? islemno = db.Islem.First().IslemNo;
                Satis satis=new Satis();   
                for(int i=0;i<satirsayisi;i++)
                {
                    satis.IslemNo = islemno;
                    satis.UrunAd = gridSatisListesi.Rows[i].Cells["UrunAdi"].Value.ToString();
                    satis.UrunGrup = gridSatisListesi.Rows[i].Cells["UrunGrup"].Value.ToString();
                    satis.Barkod = gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString();
                    satis.Birim = gridSatisListesi.Rows[i].Cells["Birim"].Value.ToString();
                    satis.AlisFiyat =Islemler.DoubleYap( gridSatisListesi.Rows[i].Cells["AlisFiyat"].Value.ToString());
                    satis.SatisFiyat = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Fiyat"].Value.ToString()) ;
                    satis.Miktar = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.Toplam = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Toplam"].Value.ToString());
                    satis.KdvTutari = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["KdvTutari"].Value.ToString()) * Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.OdemeSekli = odemesekli;
                    satis.Iade = satisiade;
                    satis.Tarih=DateTime.Now;
                    satis.Kullanici = lKullanici.Text;
                    
                    db.Satis.Add(satis);
                    db.SaveChanges();

                    if (satisiade)
                    {
                        Islemler.StokArtir(gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString()
                                                 , Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));
                    }
                    else
                    {
                        Islemler.StokAzalt(gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString()
                                               , Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));
                    }
                    alisfiyattoplam += Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["AlisFiyat"].Value.ToString());

                }
                IslemOzet io = new IslemOzet();
                io.IslemNo = islemno;
                io.Iade = satisiade;
                io.AlistFiyatToplam = alisfiyattoplam;
                io.Gelir = false;
                io.Gider = false;
                if (!satisiade)
                {
                   io.Aciklama = odemesekli + "Satış";
                }
                else
                {
                  io.Aciklama = "İade işlemi (" + odemesekli + ")";  
                }

                io.OdemeSekli = odemesekli;
                io.Kullanici = lKullanici.Text;
                io.Tarih = DateTime.Now;
                switch (odemesekli)
                {
                    case "Nakit":
                        io.Nakit = Islemler.DoubleYap(tGenelToplam.Text);
                        io.Kart = 0;break;
                    case "Kart":
                        io.Nakit = 0;
                        io.Kart = Islemler.DoubleYap(tGenelToplam.Text);
                        break;
                    case "Kart-Nakit":
                        io.Nakit= Islemler.DoubleYap(lNakit.Text);
                        io.Kart=Islemler.DoubleYap(lKart.Text);
                        break;
                }
                db.IslemOzet.Add(io);
                db.SaveChanges();

                var islemnoartir = db.Islem.First();
                islemnoartir.IslemNo += 1;
                db.SaveChanges();
                MessageBox.Show("Yazdırma İşlemi Yap");
            }
        }

        private void bNakit_Click(object sender, EventArgs e)
        {
            SatisYap("Nakit");
            Temizle();
        }

        private void bIade_Click(object sender, EventArgs e)
        {
            if (chSatisİadeİslemi.Checked)
            {
                chSatisİadeİslemi.Checked = false;
                chSatisİadeİslemi.Text = "Satış Yapılıyor";
            }
            else
            {
                chSatisİadeİslemi.Checked = true;
                chSatisİadeİslemi.Text = "İade İşlemi";

            }
        }

        private void bKart_Click(object sender, EventArgs e)
        {
            SatisYap("Kart");
            Temizle();
        }

        private void bNakitKart_Click(object sender, EventArgs e)
        {
            fNakitKart f=new fNakitKart();
            f.ShowDialog();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                SatisYap("Nakit");
                Temizle();

            }
            

            if(e.KeyCode == Keys.F2)
            {
                SatisYap("Kart");
                Temizle();
            }
            
               


            if (e.KeyCode == Keys.F3)
            {
                fNakitKart f = new fNakitKart();
                f.ShowDialog();
                Temizle();
            }
        }
    }
}
