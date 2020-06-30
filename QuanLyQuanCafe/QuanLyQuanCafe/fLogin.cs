using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string TenDangNhap = txtTenDangNhap.Text;
            string MatKhau = txtMatKhau.Text;
            if (Login(TenDangNhap,MatKhau))
            {
                Account loginAccount = AccountDAO.Instance.GetAccountByUserName(TenDangNhap);
                fTableManager f = new fTableManager(loginAccount);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else if(TenDangNhap=="" || MatKhau==""){
                MessageBox.Show("Bạn phải điền đầy đủ thông tin để đăng nhập");
            }
            else
            {
                MessageBox.Show("Tài đăng nhập hoặc Mật khẩu không đúng");
            }
        }
        bool Login(string TenDangNhap, string MatKhau)
        {
            return AccountDAO.Instance.Login(TenDangNhap, MatKhau);
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
