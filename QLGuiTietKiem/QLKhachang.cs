using QLGuiTietKiem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLGuiTietKiem
{
    public partial class QLKhachang : Form
    {
        readonly QLGuiTietKiemEntities context = new QLGuiTietKiemEntities();
        private System.Windows.Forms.TextBox[] tbx;

        public QLKhachang()
        {
            InitializeComponent();
            BindTextBoxControls();
            dtDSKhachhang.CellClick += DtDSKhachhang_CellClick;
            btnTimkiem.Click += btnTimkiem_Click;
        }

        private void BindTextBoxControls()
        {
            tbx = new System.Windows.Forms.TextBox[] { txtMakh, txtTenkh, txtCM, txtDiachi, txtSDT };
        }

        private void ClearAllTextBoxes()
        {
            foreach (var textBox in tbx)
            {
                textBox.Text = string.Empty;
            }
        }

        private void loadData()
        {
            List<KhachHang> khs = context.KhachHangs.ToList();
            this.dtDSKhachhang.DataSource = khs;

            ClearAllTextBoxes();
        }

        private void QLKhachang_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtMakh.Text.Trim());
                bool isExist = context.KhachHangs.Any(kh => kh.MaKH == id);
                if (isExist)
                {
                    throw new Exception("Trùng khách hàng");
                }

                string name = txtTenkh.Text.Trim();
                string sochungminh = txtCM.Text.Trim();
                string diachi = txtDiachi.Text.Trim();
                string sdt = txtSDT.Text.Trim();

                KhachHang khNew = new KhachHang()
                {
                    MaKH = id,
                    HoTen = name,
                    SoCM = sochungminh,
                    DiaChi = diachi,
                    Sdt = sdt,
                };

                context.KhachHangs.Add(khNew);
                context.SaveChanges();
                MessageBox.Show("Thêm thành công");
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dtDSKhachhang.CurrentCell.RowIndex;

                if (selectedIndex >= 0)
                {
                    int id = int.Parse(dtDSKhachhang.Rows[selectedIndex].Cells["Column1"].Value.ToString());

                    KhachHang khToUpdate = context.KhachHangs.FirstOrDefault(kh => kh.MaKH == id);

                    if (khToUpdate != null)
                    {
                        khToUpdate.HoTen = txtTenkh.Text.Trim();
                        khToUpdate.SoCM = txtCM.Text.Trim();
                        khToUpdate.Sdt = txtSDT.Text.Trim();
                        khToUpdate.DiaChi = txtDiachi.Text.Trim();

                        context.SaveChanges();
                        loadData();
                        MessageBox.Show("Sửa thành công");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để sửa");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần sửa trong danh sách");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dtDSKhachhang.CurrentCell.RowIndex;

                if (selectedIndex >= 0)
                {
                    int id = int.Parse(dtDSKhachhang.Rows[selectedIndex].Cells["Column1"].Value.ToString());

                    KhachHang khToDelete = context.KhachHangs.FirstOrDefault(kh => kh.MaKH == id);

                    if (khToDelete != null)
                    {
                        context.KhachHangs.Remove(khToDelete);
                        context.SaveChanges();
                        loadData();
                        MessageBox.Show("Xóa thành công");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để xóa");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần xóa trong danh sách");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DtDSKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dtDSKhachhang.Rows[e.RowIndex];

                // Fill the textboxes with the selected row's data
                txtMakh.Text = selectedRow.Cells["Column1"].Value.ToString();
                txtTenkh.Text = selectedRow.Cells["Column2"].Value.ToString();
                txtCM.Text = selectedRow.Cells["Column3"].Value.ToString();
                txtDiachi.Text = selectedRow.Cells["Column4"].Value.ToString();
                txtSDT.Text = selectedRow.Cells["Column5"].Value.ToString();
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string searchText = txtTimkiem.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                List<KhachHang> matchingKhachHangs = context.KhachHangs
                    .Where(kh =>
                        kh.HoTen.Contains(searchText) ||
                        kh.Sdt.Contains(searchText) ||
                        kh.MaKH.ToString().Contains(searchText) ||
                        kh.SoCM.Contains(searchText))
                    .ToList();

                if (matchingKhachHangs.Any())
                {
                    dtDSKhachhang.DataSource = matchingKhachHangs;
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

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
