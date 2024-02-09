namespace DalhanSatisOtomasyon
{
    partial class fUrunGrubuEkle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fUrunGrubuEkle));
            this.lNakit = new System.Windows.Forms.Label();
            this.tUrunGrupAd = new System.Windows.Forms.TextBox();
            this.listUrunGrup = new System.Windows.Forms.ListBox();
            this.bEkle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lNakit
            // 
            this.lNakit.AutoSize = true;
            this.lNakit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lNakit.ForeColor = System.Drawing.Color.DarkCyan;
            this.lNakit.Location = new System.Drawing.Point(12, 23);
            this.lNakit.Name = "lNakit";
            this.lNakit.Size = new System.Drawing.Size(134, 20);
            this.lNakit.TabIndex = 5;
            this.lNakit.Text = "Ürün Grubu Adı";
            // 
            // tUrunGrupAd
            // 
            this.tUrunGrupAd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tUrunGrupAd.Location = new System.Drawing.Point(12, 56);
            this.tUrunGrupAd.Name = "tUrunGrupAd";
            this.tUrunGrupAd.Size = new System.Drawing.Size(257, 26);
            this.tUrunGrupAd.TabIndex = 6;
            // 
            // listUrunGrup
            // 
            this.listUrunGrup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listUrunGrup.FormattingEnabled = true;
            this.listUrunGrup.ItemHeight = 20;
            this.listUrunGrup.Location = new System.Drawing.Point(12, 117);
            this.listUrunGrup.Name = "listUrunGrup";
            this.listUrunGrup.Size = new System.Drawing.Size(257, 144);
            this.listUrunGrup.TabIndex = 7;
            // 
            // bEkle
            // 
            this.bEkle.BackColor = System.Drawing.Color.Orange;
            this.bEkle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bEkle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.bEkle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bEkle.Image = ((System.Drawing.Image)(resources.GetObject("bEkle.Image")));
            this.bEkle.Location = new System.Drawing.Point(12, 289);
            this.bEkle.Name = "bEkle";
            this.bEkle.Size = new System.Drawing.Size(257, 92);
            this.bEkle.TabIndex = 22;
            this.bEkle.Text = "Ekle";
            this.bEkle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bEkle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bEkle.UseVisualStyleBackColor = false;
            this.bEkle.Click += new System.EventHandler(this.bEkle_Click);
            // 
            // fUrunGrubuEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(287, 399);
            this.Controls.Add(this.bEkle);
            this.Controls.Add(this.listUrunGrup);
            this.Controls.Add(this.tUrunGrupAd);
            this.Controls.Add(this.lNakit);
            this.Name = "fUrunGrubuEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fUrunGrubuEkle";
            this.Load += new System.EventHandler(this.fUrunGrubuEkle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lNakit;
        private System.Windows.Forms.TextBox tUrunGrupAd;
        private System.Windows.Forms.ListBox listUrunGrup;
        private System.Windows.Forms.Button bEkle;
    }
}