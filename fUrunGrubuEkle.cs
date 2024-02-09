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
    public partial class fUrunGrubuEkle : Form
    {
        public fUrunGrubuEkle()
        {
            InitializeComponent();
        }
        BarkodDBEntities db=new BarkodDBEntities();
        private void fUrunGrubuEkle_Load(object sender, EventArgs e)
        {
            listUrunGrup.DisplayMember = "UrunGrupAd";
            listUrunGrup.ValueMember = "Id";
            listUrunGrup.DataSource = db.UrunGrup.OrderBy(a => a.UrunGrupAd).ToList() ;
        }

        private void bEkle_Click(object sender, EventArgs e)
        {
            if (tUrunGrupAd.Text != "")
            {
                UrunGrup ug=new UrunGrup();
                ug.UrunGrupAd = tUrunGrupAd.Text ;
                db.UrunGrup.Add(ug);
                db.SaveChanges();
                listUrunGrup.DisplayMember = "UrunGrupAd";
                listUrunGrup.ValueMember = "Id";
                listUrunGrup.DataSource = db.UrunGrup.OrderBy(a => a.UrunGrupAd).ToList();
                tUrunGrupAd.Clear();
                MessageBox.Show("Ürün Grubu Eklenmiştir");

                
            }
            else
            {
                MessageBox.Show("Grup Bilgisi Ekleyin");
            }
        }
    }
}
