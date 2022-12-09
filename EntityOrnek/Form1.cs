using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSinavEntities db = new DbSinavEntities();
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string aranan = TxtAd.Text;
            var degerler = from item in db.TBLOGRENCİ
                           where item.AD.Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void BtnDersListesi_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-92MVUQ37;Initial Catalog=DbSinav;Integrated Security=True");
            SqlCommand komut = new SqlCommand("Select * From tbldersler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCİ.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {
            var query = from item in db.TBLNOTLAR
                        select new { item.NOTID, item.TBLOGRENCİ.AD,item.TBLOGRENCİ.SOYAD,item.TBLDERSLER.DERSAD, item.DERS, item.SINAV1, item.SINAV2,item.SINAV3,item.ORTALAMA,item.DURUM };
              dataGridView1.DataSource = query.ToList();
            //dataGridView1.DataSource = db.TBLNOTLAR.ToList();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCİ t = new TBLOGRENCİ();
            t.AD = TxtAd.Text;
            t.SOYAD = TxtSoyad.Text;
            db.TBLOGRENCİ.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Sisteme Kayıt Edildi");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciId.Text);
            var x = db.TBLOGRENCİ.Find(id);
            db.TBLOGRENCİ.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Sistemden Silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciId.Text);
            var x = db.TBLOGRENCİ.Find(id);
            x.AD = TxtAd.Text;
            x.SOYAD = TxtSoyad.Text;
            x.FOTOGRAF = TxtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Bilgileri Başarıyla Güncellendi.");
        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();

        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCİ.Where(x => x.AD == TxtAd.Text | x.SOYAD == TxtSoyad.Text).ToList();
        }

        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                //Asc-Ascending
                List<TBLOGRENCİ> liste1 = db.TBLOGRENCİ.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }
            if (radioButton2.Checked == true)
            {
                //Des-Descending
                List<TBLOGRENCİ> liste2 = db.TBLOGRENCİ.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked == true)
            {
                List<TBLOGRENCİ> liste3 = db.TBLOGRENCİ.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            if (radioButton4.Checked == true)
            {
                List<TBLOGRENCİ> liste4 = db.TBLOGRENCİ.Where(p => p.ID == 5).ToList();
                dataGridView1.DataSource = liste4;
            }
            if (radioButton5.Checked == true)
            {
                List<TBLOGRENCİ> liste5 = db.TBLOGRENCİ.Where(p => p.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }
            if (radioButton6.Checked == true)
            {
                List<TBLOGRENCİ> liste6 = db.TBLOGRENCİ.Where(p => p.AD.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }
            if (radioButton7.Checked == true)
            {
                bool deger = db.TBLKULUP.Any();
                MessageBox.Show(deger.ToString(),"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            if (radioButton8.Checked == true)
            {
                int toplam = db.TBLOGRENCİ.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton9.Checked == true)
            {
                var toplam = db.TBLNOTLAR.Sum(p => p.SINAV1);
                MessageBox.Show("Sınav 1 Toplam Puan: " + toplam.ToString());
            }
            if(radioButton10.Checked == true)
            {
                var ortalama = db.TBLNOTLAR.Average(p => p.SINAV1);
                MessageBox.Show("Sınav 1 Ortalama Puanı: " + ortalama.ToString());
            }
            if(radioButton11.Checked==true)
            {
                var enyuksek = db.TBLNOTLAR.Max(p => p.SINAV1);
                MessageBox.Show("1. Sınavın en yüksek notu: " + enyuksek.ToString());
            }
            if (radioButton12.Checked == true)
            {
                var endusuk = db.TBLNOTLAR.Min(p => p.SINAV1);
                MessageBox.Show("1. Sınavın en düşük notu: " + endusuk.ToString());
            }
            if (radioButton13.Checked == true)
            {
                var enyuksekisim = db.TBLNOTLAR.Max(p => p.SINAV1);
                dataGridView1.DataSource = db.NOTLISTESI().Where(p => p.SINAV1 == enyuksekisim).ToList();
            }
            if (radioButton14.Checked == true)
            {
                var ortyuksek = db.TBLNOTLAR.Average(p => p.SINAV1);
                List<NOTLISTESI_Result> liste = db.NOTLISTESI().Where(p => p.SINAV1 > ortyuksek).ToList();
                dataGridView1.DataSource = liste;
            }
            if (radioButton15.Checked == true)
            {
                var ortyuksek = db.TBLNOTLAR.Average(p => p.SINAV2);
                List<NOTLISTESI_Result> liste = db.NOTLISTESI().Where(p => p.SINAV1 > ortyuksek).ToList();
                dataGridView1.DataSource = liste;
            }

        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.TBLNOTLAR
                        join d2 in db.TBLOGRENCİ
                         on d1.OGR equals d2.ID
                        join d3 in db.TBLDERSLER
                        on d1.DERS equals d3.DERSID
                        select new
                        {
                            ÖĞRENCİ=d2.AD+" "+d2.SOYAD,
                            DERS=d3.DERSAD,
                            SINAV1=d1.SINAV1,
                            SINAV2=d1.SINAV2,
                            SINAV3=d1.SINAV3,
                            ORTALAMA=d1.ORTALAMA,

                        };
            dataGridView1.DataSource = sorgu.ToList();
        }
    }
}
