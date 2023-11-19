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
            dataGridView1.CellClick += DataGridView1_CellClick;

        }
        private void BindTextBoxControls()
        {
            tbx = new TextBox[] { txtManv, txtHoten, txtSdt, txtMK };
        }
        private void ClearAllTextBoxes()
        {
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
                    throw new Exception("Trùng nhân viên");
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;


                if (selectedIndex >= 0)
                {
                    int id = int.Parse(dataGridView1.Rows[selectedIndex].Cells["MaNV"].Value.ToString());

                    NhanVien nvToUpdate = context.NhanViens.FirstOrDefault(nv => nv.MaNV == id);

                    if (nvToUpdate != null)
                    {
                        // Update the properties of the NhanVien entity
                        nvToUpdate.HoTen = txtHoten.Text.Trim();
                        nvToUpdate.NgaySinh = dateTimePicker1.Value.ToLocalTime();
                        nvToUpdate.Sdt = txtSdt.Text.Trim();
                        nvToUpdate.MK = txtMK.Text.Trim();
                        nvToUpdate.LoaiNguoiDung = comboBox1.SelectedItem.ToString();

                        // Save changes to the database
                        context.SaveChanges();

                        // Reload data in the DataGridView
                        loadData();

                        MessageBox.Show("Sửa thành công");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để sửa");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa trong danh sách");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;

                if (selectedIndex >= 0)
                {
                    int id = int.Parse(dataGridView1.Rows[selectedIndex].Cells["MaNV"].Value.ToString());

                    NhanVien nvToDelete = context.NhanViens.FirstOrDefault(nv => nv.MaNV == id);

                    if (nvToDelete != null)
                    {
                        context.NhanViens.Remove(nvToDelete);

                        context.SaveChanges();

                        loadData();

                        MessageBox.Show("Xóa thành công");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để xóa");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa trong danh sách");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int selectedIndex = e.RowIndex;

                int id = int.Parse(dataGridView1.Rows[selectedIndex].Cells["MaNV"].Value.ToString());

                NhanVien selectedNhanVien = context.NhanViens.FirstOrDefault(nv => nv.MaNV == id);

                if (selectedNhanVien != null)
                {
                    txtManv.Text = selectedNhanVien.MaNV.ToString();
                    txtHoten.Text = selectedNhanVien.HoTen;
                    dateTimePicker1.Value = selectedNhanVien.NgaySinh;

                    txtSdt.Text = selectedNhanVien.Sdt;
                    txtMK.Text = selectedNhanVien.MK;
                    comboBox1.SelectedItem = selectedNhanVien.LoaiNguoiDung;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string searchText = textBox8.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                List<NhanVien> matchingNhanViens = context.NhanViens
                    .Where(nv =>
                        nv.HoTen.Contains(searchText) ||
                        nv.MaNV.ToString().Contains(searchText) ||
                        nv.Sdt.Contains(searchText))
                    .ToList();

                if (matchingNhanViens.Any())
                {
                    dataGridView1.DataSource = matchingNhanViens;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả nào.");
                }
            }
            else
            {
                loadData();
            }
        }
    }
}
