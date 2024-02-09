using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DalhanSatisOtomasyon
{
    public partial class fUrunGiris : Form
    {
        public fUrunGiris()
        {
            InitializeComponent();
        }
        BarkodDBEntities db =new BarkodDBEntities();
        private void tBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) {
                string barkod =tBarkod.Text.Trim();
                if(db.Urun.Any(a=>a.Barkod==barkod))
                {
                    var urun=db.Urun.Where(a=>a.Barkod==barkod).SingleOrDefault();
                    tUrunAdi.Text = urun.UrunAd;
                    tAciklama.Text=urun.Aciklama;
                    cmbUrunGrubu.Text = urun.UrunGrup;
                    tAlisFiyati.Text = urun.AlisFiyati.ToString();
                    tSatisFiyati.Text = urun.SatisFiyati.ToString();
                    tMiktar.Text=urun.Miktar.ToString();
                    tKdvOrani.Text=urun.KdvOrani.ToString();
                }
                else
                {
                    MessageBox.Show("Ürün Kayıtlı Değil, kaydedebilirsiniz");
                }
            }
        }

        private void bKaydet_Click(object sender, EventArgs e)
        {
            if (tBarkod.Text != "" && tUrunAdi.Text != "" && cmbUrunGrubu.Text != "" && tAlisFiyati.Text != "" && tSatisFiyati.Text != "" && tKdvOrani.Text != ""&&tMiktar.Text!="") 
            {
                if (db.Urun.Any(a => a.Barkod == tBarkod.Text))
                {
                    var guncelle=db.Urun.Where(a=>a.Barkod==tBarkod.Text).SingleOrDefault();
                    guncelle.Barkod = tBarkod.Text;
                    guncelle.UrunAd = tUrunAdi.Text;
                    guncelle.Aciklama = tAciklama.Text;
                    guncelle.UrunGrup = cmbUrunGrubu.Text;
                    guncelle.AlisFiyati = Islemler.DoubleYap(tAlisFiyati.Text);
                    guncelle.SatisFiyati = Islemler.DoubleYap(tSatisFiyati.Text);
                    guncelle.KdvOrani = Convert.ToInt32(tKdvOrani.Text);
                    guncelle.KdvTutari = Math.Round(Islemler.DoubleYap(tSatisFiyati.Text) * Convert.ToInt32(tKdvOrani.Text) / 100, 2);
                    guncelle.Miktar += Islemler.DoubleYap(tMiktar.Text);
                    guncelle.Birim = "Adet";
                    guncelle.Tarih = DateTime.Now;
                    db.SaveChanges();
                    MessageBox.Show("Ürün Güncellenmiştir");

                }
                else
                {
                    Urun urun = new Urun();
                    urun.Barkod = tBarkod.Text;
                    urun.UrunAd=tUrunAdi.Text;
                    urun.Aciklama = tAciklama.Text;
                    urun.UrunGrup = cmbUrunGrubu.Text;
                    urun.AlisFiyati = Islemler.DoubleYap(tAlisFiyati.Text);
                    urun.SatisFiyati=Islemler.DoubleYap(tSatisFiyati.Text) ;
                    urun.KdvOrani = Convert.ToInt32(tKdvOrani.Text);
                    urun.KdvTutari=Math.Round(Islemler.DoubleYap(tSatisFiyati.Text) * Convert.ToInt32(tKdvOrani.Text)/100,2);
                    urun.Miktar=Islemler.DoubleYap(tMiktar.Text) ;
                    urun.Birim = "Adet";
                    urun.Tarih = DateTime.Now;
                    db.Urun.Add(urun);
                    db.SaveChanges();
                    if (tBarkod.Text.Length==8)
                    {
                        var ozelbarkod = db.Barkod.First();
                        ozelbarkod.BarkodNo += 1;
                        db.SaveChanges();
                    }
                    tBarkod.Clear();
                    tUrunAdi.Clear();
                    tAciklama.Clear();
                    tAlisFiyati.Text = "0";
                    tSatisFiyati.Text= "0";
                    tMiktar.Text = "0";
                    tKdvOrani.Text = "8";
                    gridUrunler.DataSource=db.Urun.OrderByDescending(a=>a.Urunid).Take(10).ToList();
                }
            }
            else
            {
                MessageBox.Show("Bilgi girişlerini Kontrol Edin");
            }

        }

        private void tUrunAra_TextChanged(object sender, EventArgs e)
        {
            if (tUrunAra.Text.Length>=2)
            {
                string urunad = tUrunAra.Text;
                gridUrunler.DataSource=db.Urun.Where(a=>a.UrunAd.Contains(urunad)).ToList();   
            }
        }

        private void bIptal_Click(object sender, EventArgs e)
        {
            tBarkod.Clear();
            tUrunAdi.Clear();
            tAciklama.Clear();
            tAlisFiyati.Text = "0";
            tSatisFiyati.Text = "0";
            tMiktar.Text = "0";
            tKdvOrani.Text = "8";
        }

        private void bUrunGrubuEkle_Click(object sender, EventArgs e)
        {
            fUrunGrubuEkle f=new fUrunGrubuEkle();
            f.ShowDialog();
        }

        private void fUrunGiris_Load(object sender, EventArgs e)
        {
            tUrunSayisi.Text=db.Urun.Count().ToString();
            cmbUrunGrubu.DisplayMember = "UrunGrupAd";
            cmbUrunGrubu.ValueMember = "Id";
            cmbUrunGrubu.DataSource = db.UrunGrup.OrderBy(a => a.UrunGrupAd).ToList();
        }

        private void bBarkod_Click(object sender, EventArgs e)
        {
            var barkodno = db.Barkod.First();
            int karakter = barkodno.BarkodNo.ToString().Length;
            string sifirlar = string.Empty;
            for(int  i = 0; i < 8 - karakter; i++)
            {
                sifirlar = sifirlar + "0";
            }
            string olusanbarkod = sifirlar +barkodno.BarkodNo.ToString();
            tBarkod.Text = olusanbarkod;
            tUrunAdi.Focus();
        }
    }
}
