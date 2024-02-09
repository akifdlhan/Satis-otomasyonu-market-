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
    public partial class fHizliButonUrunEkle : Form
    {
        public fHizliButonUrunEkle()
        {
            InitializeComponent();
        }
        BarkodDBEntities db=new BarkodDBEntities();
        private void tUrunAra_TextChanged(object sender, EventArgs e)
        {
            if (tUrunAra.Text != "")
            {
                string urunad = tUrunAra.Text;
                var urunler = db.Urun.Where(a => a.UrunAd.Contains(urunad)).ToList();
                gridUrunler.DataSource = urunler;
            }
        }
      

        private void gridUrunler_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (gridUrunler.Rows.Count > 0)
            {
                string barkod = gridUrunler.CurrentRow.Cells["Barkod"].Value.ToString();
                string urunad = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                double fiyat = Convert.ToDouble(gridUrunler.CurrentRow.Cells["SatisFiyati"].Value.ToString());
                int id = Convert.ToInt32(lbuttonId.Text);
                var guncellenecek = db.HizliUrun.Find(id);
                guncellenecek.Barkod = barkod;
                guncellenecek.UrunAd = urunad;
                guncellenecek.fiyat = fiyat;
                db.SaveChanges();
                MessageBox.Show("Ürün Tanımlanmıştır");

                Form1 f = (Form1)Application.OpenForms["Form1"];
                if (f != null)
                {
                    Button b = f.Controls.Find("bH" + id, true).FirstOrDefault() as Button;
                    b.Text = urunad + "\n" + fiyat.ToString("C2");
                }
            }
        }

        private void chbTumunuGoster_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTumunuGoster.Checked)
            {
                gridUrunler.DataSource = db.Urun.ToList();

            }
            else
            {
                gridUrunler.DataSource = null;
            }
        }

        private void fHizliButonUrunEkle_Load(object sender, EventArgs e)
        {

        }
    }
}
