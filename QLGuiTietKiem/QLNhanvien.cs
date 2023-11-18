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
        readonly QLGuiTietKiemEntities context = new QLGuiTietKiemEntities();
        string[] loainguoidungs = { "Nhân viên", "Admin" };
        private TextBox[] tbx;

        public QLNhanvien()
        {
            InitializeComponent();
            BindTextBoxControls();

        }
        private void BindTextBoxControls()
        {
            // Find all TextBox controls on the form and store them in the array
            tbx = new TextBox[] { txtManv, txtHoten, txtSdt, txtMK };
        }
        private void ClearAllTextBoxes()
        {
            // Iterate through the TextBox array and clear the text
            foreach (var textBox in tbx)
            {
                textBox.Text = string.Empty;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void QLNhanvien_Load(object sender, EventArgs e)
        {

            loadData();
            comboBox1.Items.AddRange(loainguoidungs);
        }

        void loadData()
        {
            List<NhanVien> nvs = context.NhanViens.ToList();
            this.dataGridView1.DataSource = nvs;

            ClearAllTextBoxes();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtManv.Text.Trim());
                bool isExist = context.NhanViens.Any(nv => nv.MaNV == id);
                if (isExist)
                {
                    throw new Exception("Trùng nhana viên");
                }

                string name = txtHoten.Text.Trim();
                DateTime namsinh = dateTimePicker1.Value.ToLocalTime();
                string sdt = (txtSdt.Text.Trim());
                string mk = txtMK.Text.Trim();
                string loaiNguoiDung = comboBox1.SelectedItem.ToString();

                NhanVien nvNew = new NhanVien()
                {
                    MaNV = id,
                    HoTen = name,
                    NgaySinh = namsinh,
                    Sdt = sdt,
                    LoaiNguoiDung=loaiNguoiDung,
                    MK = mk,
                };

                context.NhanViens.Add(nvNew);
                context.SaveChanges();
                MessageBox.Show("Thêm thành công");
                loadData();


            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
