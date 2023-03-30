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
    public partial class frmGirisEkranı : Form
    {
        public frmGirisEkranı()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            frmAnaEkran frm = new frmAnaEkran();
            frm.Show();
            this.Hide();
        }
    }
}
