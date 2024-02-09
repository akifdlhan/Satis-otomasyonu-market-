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
    public partial class fNakitKart : Form
    {
        public fNakitKart()
        {
            InitializeComponent();
        }
        private void hesapla()
        { Form1 form = (Form1)Application.OpenForms["Form1"];
                    double nakit = Islemler.DoubleYap(tNakit.Text);
                    double geneltoplam = Islemler.DoubleYap(form.tGenelToplam.Text);
                    double kart = geneltoplam - nakit;
                    form.lNakit.Text = nakit.ToString("C2");
                    form.lKart.Text = kart.ToString("C2");
                    form.SatisYap("Kart-Nakit");
                    this.Hide();
                    form.Temizle();

        }

        private void tNakit_KeyDown(object sender, KeyEventArgs e)
        {if (tNakit.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                     hesapla();  
                }
            }
           
        }
        private void bNx_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == ",")
            {
                int virgul = tNakit.Text.Count(x => x == ',');
                if (virgul < 1)
                {
                    tNakit.Text += b.Text;
                }
            }
            else if (b.Text == "<")
            {
                if (tNakit.Text.Length > 0)
                {
                    tNakit.Text = tNakit.Text.Substring(0, tNakit.Text.Length - 1);
                }
            }
            else
            {
                tNakit.Text += b.Text;
            }
        }


        private void bEnter_Click(object sender, EventArgs e)
        {
            if (tNakit.Text != "") { hesapla();}
            
        }
    }
}
