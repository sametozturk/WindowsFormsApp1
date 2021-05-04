﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            TxtKullaniciAd.Text = "";
            TxtSifre.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Btnİslem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_ADMIN values (@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin sisteme kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (Btnİslem.Text == "Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("update TBL_ADMIN set Sifre=@p2 where KullaniciAd=@p1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
                komut1.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt yenilendi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
           
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr != null)
            {
                TxtKullaniciAd.Text = dr["KullaniciAd"].ToString();
                TxtSifre.Text = dr["Sifre"].ToString();
            }
        }

        private void TxtKullaniciAd_TextChanged(object sender, EventArgs e)
        {
            if(TxtKullaniciAd.Text != "")
            {
                Btnİslem.Text = "Güncelle";
                Btnİslem.BackColor = Color.Yellow;
            }
            else
            {
                Btnİslem.Text = "Kaydet";
                Btnİslem.BackColor = Color.MediumTurquoise;
            }
        }
    }
}
