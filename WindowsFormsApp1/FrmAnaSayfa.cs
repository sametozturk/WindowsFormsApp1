using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;


namespace WindowsFormsApp1
{
    public partial class FrmAnaSayfa : Form 
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Urunad,sum(Adet) as 'Adet' from TBL_URUNLER group by Urunad having Sum(adet) <= 30 order by sum(adet)", bgl.baglanti());
            da.Fill(dt);
            gridControlStoklar.DataSource = dt;
        }
        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select top 10 tarıh,saat,baslık from TBL_NOTLAR order by ID desc", bgl.baglanti());
            da.Fill(dt);
            gridControlAjanda.DataSource = dt;
        }

        void FirmaHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHareketler2", bgl.baglanti());
            da.Fill(dt);
            gridControlHareket.DataSource = dt;
        }

        void firhist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Ad,Telefon1 from TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            gridControlFirma.DataSource = dt;
        }

        void haberler()
        {
            
        }
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            
            stoklar();
            ajanda();
            FirmaHareketleri();
            firhist();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/kurlar_tr.html");
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
