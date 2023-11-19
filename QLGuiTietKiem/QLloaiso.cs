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
    public partial class QLloaiso : Form
    {
        readonly QLGuiTietKiemEntities context = new QLGuiTietKiemEntities();
        private System.Windows.Forms.TextBox[] tbx;
        public QLloaiso()
        {
            InitializeComponent();
            BindTextBoxControls();
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }
        private void BindTextBoxControls()
        {
            tbx = new System.Windows.Forms.TextBox[] { txtMals, txtTenls, txtLaixuat,};
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
            List<LoaiSTK> lss = context.LoaiSTKs.ToList();
            this.dataGridView1.DataSource = lss;

            ClearAllTextBoxes();
        }
        private void QLloaiso_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtMals.Text.Trim());
                bool isExist = context.LoaiSTKs.Any(ls => ls.MaLoaiSo == id);
                if (isExist)
                {
                    throw new Exception("Trùng loại sổ");
                }

                string name = txtTenls.Text.Trim();
                float laiSuat = float.Parse(txtLaixuat.Text.Trim());
                int soThang = (int)nbSothang.Value;

                LoaiSTK lsNew = new LoaiSTK()
                {
                    MaLoaiSo = id,
                    TenLoaiSo = name,
                    LaiSuat = laiSuat,
                    SoThang = soThang,
                };

                context.LoaiSTKs.Add(lsNew);
                context.SaveChanges();
                MessageBox.Show("Thêm thành công");
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;

                if (selectedIndex >= 0)
                {
                    int id = int.Parse(dataGridView1.Rows[selectedIndex].Cells["Column1"].Value.ToString());

                    LoaiSTK lsToUpdate = context.LoaiSTKs.FirstOrDefault(ls => ls.MaLoaiSo == id);

                    if (lsToUpdate != null)
                    {
                        lsToUpdate.TenLoaiSo = txtTenls.Text.Trim();
                        lsToUpdate.LaiSuat = float.Parse(txtLaixuat.Text.Trim());
                        lsToUpdate.SoThang = int.Parse(nbSothang.Text.Trim());
                        context.SaveChanges();
                        loadData();
                        MessageBox.Show("Sửa thành công");
                    }
                    else
                    {
                        MessageBox.Show("lsông tìm thấy lsách hàng để sửa");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại sổ cần sửa trong danh sách");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;

                if (selectedIndex >= 0)
                {
                    int id = int.Parse(dataGridView1.Rows[selectedIndex].Cells["Column1"].Value.ToString());

                    LoaiSTK lsToDelete = context.LoaiSTKs.FirstOrDefault(ls => ls.MaLoaiSo == id);

                    if (lsToDelete != null)
                    {
                        context.LoaiSTKs.Remove(lsToDelete);
                        context.SaveChanges();
                        loadData();
                        MessageBox.Show("Xóa thành công");
                    }
                    else
                    {
                        MessageBox.Show("lsông tìm thấy hàng để xóa");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn lsách hàng cần xóa trong danh sách");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                txtMals.Text = selectedRow.Cells["Column1"].Value.ToString();
                txtTenls.Text = selectedRow.Cells["Column2"].Value.ToString();
                txtLaixuat.Text = selectedRow.Cells["Column3"].Value.ToString();
                nbSothang.Value = Convert.ToInt32(selectedRow.Cells["Column4"].Value);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string searchText = textBox5.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                List<LoaiSTK> matchingLoaiSTKs = context.LoaiSTKs
                    .Where(ls =>
                        ls.TenLoaiSo.Contains(searchText) ||
                        ls.MaLoaiSo.ToString().Contains(searchText))
                    .ToList();

                if (matchingLoaiSTKs.Any())
                {
                    dataGridView1.DataSource = matchingLoaiSTKs;
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
