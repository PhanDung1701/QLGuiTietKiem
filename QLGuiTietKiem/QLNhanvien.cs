using QLGuiTietKiem.Data;
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
    public partial class QLNhanvien : Form
    {
        public QLNhanvien()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void QLNhanvien_Load(object sender, EventArgs e)
        {
            QLGuiTietKiemEntities context = new QLGuiTietKiemEntities();

            List<NhanVien> nvs = context.NhanViens.ToList();
            this.dataGridView1.DataSource = nvs;
        }
    }
}
