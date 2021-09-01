using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoanCNPM.DAL;

namespace DoanCNPM
{
    public partial class QL_NhapHang : Form
    {
        public static QL_NhapHang instance;
        public TextBox tb;
        public QL_NhapHang()
        {
            InitializeComponent();
            instance = this;
            tb = txtmahdn;
        }
        DataClassesTestDataContext dt = new DataClassesTestDataContext();
        public int layMaNCC()
        {
            string a = txtTenNCC.Text;
            var ma = dt.NHACUNGCAPs.Where(r => r.TENNCC.Equals(a)).First();
            return ma.MANCC;

        }
        private void QL_NhapHang_Load(object sender, EventArgs e)
        {
            loadNCC();
            loadPNH();
            txtngaynhap.Value = DateTime.Parse(DateTime.Now.ToShortDateString());
        }
        public void loadPNH()
        {
            var rs = from a in dt.PHIEUNHAPHANGs select a;
            dgvhoadonnhap.DataSource = rs;
            dgvhoadonnhap.Columns[5].Visible = false;
            dgvhoadonnhap.Columns[6].Visible = false;
        }
        private void loadNCC()
        {
            var rs = from a in dt.NHACUNGCAPs select a;
            dgv_dsncc.DataSource = rs;
        }

        private void btThem_Click(object sender, EventArgs e)
        {

           
              
            
        }

        private void btSua_Click(object sender, EventArgs e)
        {
           
        }

        private void dgvhoadonnhap_Click(object sender, EventArgs e)
        {
            PHIEUNHAPHANG a = dgvhoadonnhap.CurrentRow.DataBoundItem as PHIEUNHAPHANG;
            var maNCC = dt.NHACUNGCAPs.Where(r => r.MANCC.Equals(a.MANCC)).SingleOrDefault();
            txtmahdn.Text = a.MANH.ToString();
            txtTenNCC.Text = maNCC.TENNCC;
            txtTongTien.Text = a.TONGTIEN.ToString();
            txtngaynhap.Value = (DateTime)a.NGAYNHAP;
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            
        }

        private void btTimkiem_Click_1(object sender, EventArgs e)
        {
            var qr = from d in dt.NHACUNGCAPs
                     where d.TENNCC.Contains(txtten.Text)
                     select d;
            if (qr.Count() == 0)
                MessageBox.Show("Không có dữ liệu", "Thông báo");
            else
                dgv_dsncc.DataSource = qr.ToList();
            txtten.Text = "";
        }

        private void btLapchitietdonhang_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                PHIEUNHAPHANG a = new PHIEUNHAPHANG();
                var rs = dt.NHACUNGCAPs.Where(r => r.TENNCC.Equals(txtTenNCC.Text)).First();
                a.MANV = Form1.nvdn.MANV;
                a.MANCC = rs.MANCC;
                a.NGAYNHAP = txtngaynhap.Value;
                a.TONGTIEN = 0;
                dt.PHIEUNHAPHANGs.InsertOnSubmit(a);
                dt.SubmitChanges();
                MessageBox.Show("Thêm thành công", "Thông báo");
                loadPNH();
            }
            catch (Exception) { }
        }

        private void btSua_Click_1(object sender, EventArgs e)
        {
          
        }

        private void btXoa_Click_1(object sender, EventArgs e)
        {
            if (dgvhoadonnhap.SelectedCells.Count > 0)
            {
                int a = dgvhoadonnhap.CurrentRow.Index;
                DataGridViewRow selectedRow = dgvhoadonnhap.Rows[a];
                string cellValue = Convert.ToString(selectedRow.Cells[0].Value);


                List<CTNHAPHANG> ctn = dt.CTNHAPHANGs.Where(ct => ct.MANH == int.Parse(cellValue)).ToList();
                foreach (var i in ctn)
                {
                    dt.CTNHAPHANGs.DeleteOnSubmit(i);
                    dt.SubmitChanges();
                }
                PHIEUNHAPHANG hd = dt.PHIEUNHAPHANGs.SingleOrDefault(t => t.MANH == int.Parse(cellValue));
                dt.PHIEUNHAPHANGs.DeleteOnSubmit(hd);
                dt.Refresh(RefreshMode.OverwriteCurrentValues, hd);
                dt.SubmitChanges();
                MessageBox.Show("Xóa Thành Công", "Thông báo");
                loadPNH();
            }
            else
            {
                MessageBox.Show("Hãy click vào một dòng trong bảng Hóa đơn để xóa hóa đơn", "Thông báo");
            }
        }

        private void dgv_dsncc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtmahdn.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn hóa đơn nhập hàng", "Thông Báo");
            }
            else
            {
                this.Hide();
                ChiTietNhap a = new ChiTietNhap();
                a.Show();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Chỉ được nhập số !!!!!", "chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_dsncc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NHACUNGCAP a = dgv_dsncc.CurrentRow.DataBoundItem as NHACUNGCAP;
            txtTenNCC.Text = a.TENNCC;
        }
    }
    }

