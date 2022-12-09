using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        DbSinavEntities db = new DbSinavEntities();
        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLNOTLAR.Where(x => x.SINAV1 < 50);
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCİ.Where(x => x.AD =="Tuba");
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCİ.Where(x => x.AD == textBox1.Text || x.SOYAD == textBox1.Text);
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCİ.Select(x => new { soyadı = x.SOYAD });
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCİ.Select(x => new 
            { 
                Ad = x.AD.ToUpper(), 
                Soyadı = x.SOYAD.ToLower() 
            });
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCİ.Select(x => new
            {
                Ad = x.AD.ToUpper(),
                Soyadı = x.SOYAD.ToLower()
            }).Where(x => x.Ad != "Tuba");
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLNOTLAR.Select(x => 
            new
            {
                OgrenciAd=x.OGR,
                OgrenciOrtalamasi=x.ORTALAMA,
                Durumu=x.DURUM==false ?"Kaldı": "Geçti"
            });
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCİ.Where(y => y.ID == x.OGR), (x, y) => new
            {
                y.AD,
                x.ORTALAMA,
                Durum = x.ORTALAMA >= 50 ? "Geçti " : "Kaldı"
            });
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCİ.OrderBy(x => x.ID).Take(3);
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCİ.OrderByDescending(x => x.ID).Take(3);
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCİ.OrderBy(x => x.AD);
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            var degerler = db.TBLOGRENCİ.OrderBy(x => x.ID).Skip(5);
            dataGridView1.DataSource = degerler.ToList();
        }
    }
}
