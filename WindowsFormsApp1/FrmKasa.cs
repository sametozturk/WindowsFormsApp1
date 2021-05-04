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
using DevExpress.Charts;

namespace WindowsFormsApp1
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {

        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
                    }

        void firmahareket()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute FirmaHareketler",bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;
        }
        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;

            firmahareket();

            musterihareket();

            //toplam tutar hesap
            SqlCommand komut1 = new SqlCommand("Select sum(Tutar) From TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblKasaToplam.Text = dr1[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
            //son ayın faturaları
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ) from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString() + " TL";

            }
            bgl.baglanti().Close();

            //son ayın personel maasları
            SqlCommand komut3 = new SqlCommand("Select MAASLAR From TBL_GIDERLER order by ID asc",bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaaslari.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //toplam müsteri sayısı
            SqlCommand komut4 = new SqlCommand("Select Count(*) From TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam firma sayısı
            SqlCommand komut5 = new SqlCommand("Select Count(*) From TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam firma şehir sayısı
            SqlCommand komut6 = new SqlCommand("Select Count(Distinct(IL)) From TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam müşteri şehir sayısı
            SqlCommand komut7 = new SqlCommand("Select Count(Distinct(IL)) From TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblSehirSayisi2.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam personel sayısı
            SqlCommand komut8 = new SqlCommand("Select Count(*) From TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblPersonelSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();


            //toplam ürün sayısı
            SqlCommand komut9 = new SqlCommand("Select Count(Adet) From TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                LblStokSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();

            //1.chart  elektrik son 4 ay liste
            SqlCommand komut10 = new SqlCommand("Select top 4 Ay,Elektrık from TBL_GIDERLER order by ID Desc", bgl.baglanti());
            SqlDataReader dr10 = komut10.ExecuteReader();
            while (dr10.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
            }
            bgl.baglanti().Close();
            //2.chart su son 4 ay
            SqlCommand komut11 = new SqlCommand("Select top 4 Ay,Su from TBL_GIDERLER order by ID Desc", bgl.baglanti());
            SqlDataReader dr11 = komut11.ExecuteReader();
            while (dr11.Read())
            {
                chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
            }
            bgl.baglanti().Close();




        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
