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
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(LoginAccount); }
        }

        public fAccountProfile(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        void ChangeAccount(Account acc)
        {
            txtTenDangNhap.Text = LoginAccount.UserName;
            txtTenHienThi.Text = loginAccount.DisplayName;
            
        }

        void UpdateAccount()
        {
            string displayName = txtTenHienThi.Text;
            string passWord = txtMatKhau.Text;
            string newPass = txtMatKhauMoi.Text;
            string reEnterPass = txtNhapLaiMatKhauMoi.Text;
            string userName = txtTenDangNhap.Text;
            if (!newPass.Equals(reEnterPass))
            {
                MessageBox.Show("Nhập lại mật khẩu mới chưa khớp!");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, passWord, newPass))
                {
                    MessageBox.Show("Cập nhật thành công");
                    if (updateAccountInfo != null)
                        updateAccountInfo(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else{
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu");
                }
            }
        }

        private event EventHandler<AccountEvent> updateAccountInfo;
        public event EventHandler<AccountEvent> UpdateAccountInfo
        {
            add { updateAccountInfo += value; }
            remove { updateAccountInfo -= value; }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        public AccountEvent(Account acc){
            this.Acc = acc;
        }
    }
}
