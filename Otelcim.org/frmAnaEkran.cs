using Otel.CommonClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Otelcim.org
{
    public partial class frmAnaEkran : Form
    {
        public frmAnaEkran()
        {
            InitializeComponent();
        }
   
        List<Musteri> musteriler = new List<Musteri>();
        List<Minibar> minibarlar = new List<Minibar>();
        List<OdaRezervasyon> odaRezervasyon = new List<OdaRezervasyon>();
        DateTime cikisTarihi;

        Button btnKral = new Button();
        Label labelYataksayı = new Label();
        CheckBox tiklananCheckBox = null;
        Label lblurunAd = new Label();
        Label lblFiyat = new Label();
        Button tiklananButton = null;
        decimal minibarFiyat;
        decimal otelF;


        private void ekMalzeme()
        {
            string[] malzemeAdlari = { "Alkollü İçecekler", "Atıştırmalıklar", "Gazlı İçecekler", "Şekerlemeler", "Bisküvi", "Kurabiye", "Kahve Çeşitleri", "Çay ve Çeşitleri" };

            int[] malzemeFiyatlari = { 200, 25, 10, 5, 10, 20, 50, 10 };

            for (int i = 0; i < malzemeAdlari.Length; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = malzemeAdlari[i];
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                checkBox.Tag = new Minibar()
                {
                    UrunAdı = malzemeAdlari[i],
                    UrunFiyati = malzemeFiyatlari[i],
                };
                flEkMalzemeler.Controls.Add(checkBox);
            }


        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            tiklananCheckBox = sender as CheckBox;
            Minibar minibar = tiklananCheckBox.Tag as Minibar;

            if (tiklananCheckBox.Checked == true)
            {
                lblurunAd.Text = minibar.UrunAdı;
                lblFiyat.Text = Convert.ToString(minibar.UrunFiyati);
            }

        }


        private void frmAnaEkran_Load(object sender, EventArgs e)
        {

            gBoxMusteri.Enabled = false;
            ekMalzeme();

            for (int i = 1; i <= 3; i++)
            {
                FlowLayoutPanel currentFlKat = (FlowLayoutPanel)this.Controls.Find("flKat" + (i).ToString(), true)[0]; 
                for (int j = 1; j <= 10; j++)
                {

                    Button btn = new Button()
                    {
                        Text = ((100 * i) + j).ToString(),
                        BackColor = Color.Green,
                        Tag = new OdaRezervasyon()
                        {
                            Oda = new Oda()
                            {
                                Fiyat = (100 * i) / 2,
                                OdaDurumu = OdaDurumu.Bos,
                                Numarasi = ((i * 100) + j)
                            }


                        }
                    };
                    btn.Click += Btn_Click;
                    currentFlKat.Controls.Add(btn);
                }

            }
            btnKral = new Button();
            btnKral.Text = "Kral Dairesi";
            btnKral.BackColor = Color.Yellow;
            btnKral.Tag = new OdaRezervasyon()
            {
                Oda = new Oda()
                {
                    Fiyat = 1000,
                    Numarasi = 401,
                    OdaDurumu = OdaDurumu.Bos,
                    YatakSayisi = 6
                },

            };
            btnKral.Width = 248;
            btnKral.Height = 50;
            btnKral.Click += Btn_Click;
            flKat4.Controls.Add(btnKral);

        }

       


        private void Btn_Click(object sender, EventArgs e)
        {

            tiklananButton = sender as Button;
            OdaRezervasyon rezervasyon = tiklananButton.Tag as OdaRezervasyon;
            gBoxMusteri.Enabled = true;
            lblSecOda.Text = rezervasyon.Oda.Numarasi.ToString();
            lbFiyat.Text = Convert.ToString(rezervasyon.Oda.Fiyat);
            labelYataksayı.Text = rezervasyon.Oda.YatakSayisi.ToString();
            lblCikisOda.Text = rezervasyon.Oda.Numarasi.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!VeriBosMu(txtBoxAd.Text, mtxBoxTC.Text, mtxtBoxTelefon.Text, nmKisi.Value))
            {
                MessageBox.Show("Bilgileri eksiksiz giriniz!");
                return;
            }

            OdaRezervasyon rezervasyon = tiklananButton.Tag as OdaRezervasyon;


            if (rezervasyon.Oda.OdaDurumu == OdaDurumu.Bos)
            {
                rezervasyon.Musteri = new Musteri()
                {
                    AdSoyad = txtBoxAd.Text,
                    GirisTarihi = dateTimePicker1.Value,
                    TC = mtxBoxTC.Text,
                    Tel = mtxtBoxTelefon.Text,
                    KisiSayısı = Convert.ToInt32(nmKisi.Value),
                };
                odaRezervasyon.Add(rezervasyon);
                musteriler.Add(rezervasyon.Musteri);
                rezervasyon.Oda.Numarasi = int.Parse(lblSecOda.Text);
                rezervasyon.Oda.Fiyat = Convert.ToDouble(lbFiyat.Text);
                rezervasyon.Oda.YatakSayisi = Convert.ToByte(labelYataksayı.Text);
                rezervasyon.Oda.OdaDurumu = OdaDurumu.Dolu;
                lblCikisFiyat.Text = Convert.ToString(rezervasyon.Oda.Fiyat);

                tiklananButton.BackColor = Color.Red;

                tabControl2.SelectedTab = tabControl2.TabPages[1];


                FormuTemizle();
            }
            else
            {
                MessageBox.Show("Oda dolu");
            }





        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 1)
            {
                cmbAdSoyad.Items.Clear();
                foreach (Musteri item in musteriler)
                {
                    cmbAdSoyad.Items.Add(item.AdSoyad);
                }
                KatEkleme();
                groupBox1.Enabled = false;


            }
        

        }


        private void KatEkleme()
        {
            if (lblCikisOda.Text.StartsWith("1"))
            {
                lblCikisKat.Text = "1.Kat";
            }
            else if (lblCikisOda.Text.StartsWith("2"))
            {
                lblCikisKat.Text = "2.Kat";
            }
            else if (lblCikisOda.Text.StartsWith("3"))
            {
                lblCikisKat.Text = "3.Kat";
            }
            else
            {
                lblCikisKat.Text = "4.Kat";
            }
        }

        
        private void buttonbarekle_Click(object sender, EventArgs e)
        {


            otelF = Convert.ToDecimal(lblCikisFiyat.Text);
            Minibar minibar = tiklananButton.Tag as Minibar;


            minibar = new Minibar()
            {
                UrunAdı = lblurunAd.Text,
                UrunFiyati = Convert.ToDecimal(lblFiyat.Text),

            };
            minibarlar.Add(minibar);
            minibarFiyat = minibar.UrunFiyati * nmMinibar.Value;

            lblBarFiyat.Text = minibarFiyat.ToString();

            lblCikisFiyat.Text = Convert.ToString(minibarFiyat + otelF);

        }

  

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            cikisTarihi = dateTimePicker2.Value;
            otelF = Convert.ToDecimal(lbFiyat.Text);

            TimeSpan gunFarki = dateTimePicker1.Value - dateTimePicker2.Value;

            int gunSayisi = (int)gunFarki.TotalDays;
            lblCikisFiyat.Text = (otelF * Math.Abs(gunSayisi)).ToString();
            foreach (Minibar item in minibarlar)
            {
                item.UrunFiyati = Convert.ToDecimal(lblCikisFiyat.Text);
            }
            groupBox1.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ödemeniz Yapılmıştır.Teşekkür ederiz.");
            FrmRapor frm = new FrmRapor(odaRezervasyon, cikisTarihi);
            frm.Show();
            this.Hide();

        }

        private bool VeriBosMu(string adSoyad, string tc, string telefon, decimal kisiSayisi)
        {
            char[] c = adSoyad.ToCharArray();
            bool isLetter = false;
            for (int i = 0; i < c.Length; i++)
            {
                if (char.IsNumber(c[i]))
                {
                    isLetter = true;
                }
            }


            return !string.IsNullOrEmpty(adSoyad) && (isLetter == false) && !string.IsNullOrEmpty(tc) && !string.IsNullOrEmpty(telefon) && kisiSayisi > 0;
        }

        private void FormuTemizle()
        {
            txtBoxAd.Text = mtxBoxTC.Text = mtxtBoxTelefon.Text = string.Empty;
        }

        private void txtBoxAd_TextChanged(object sender, EventArgs e)
        {

        }
    }

}




