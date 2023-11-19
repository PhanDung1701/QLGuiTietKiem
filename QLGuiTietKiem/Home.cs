using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLGuiTietKiem
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void gửiTiềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLGuitietkiem form2 = new QLGuitietkiem();
            form2.Show();
        }

        private void rútTiềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLRuttien form2 = new QLRuttien();
            form2.Show();
        }

        private void loạiSổToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLloaiso form2 = new QLloaiso();
            form2.Show();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dangnhap form2 = new Dangnhap();
            form2.Show();
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLNhanvien form2 = new QLNhanvien();
            form2.Show();
        }

        private void quảnLýKhácHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLKhachang form2 = new QLKhachang();
            form2.Show();
        }

        private void dịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
