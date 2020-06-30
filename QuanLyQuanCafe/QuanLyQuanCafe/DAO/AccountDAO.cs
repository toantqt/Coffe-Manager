using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            private set { AccountDAO.instance = value; }
        }


        private AccountDAO() { }

        public bool Login(string TenDangNhap, string MatKhau)
        {
            string query = "SELECT * FROM Account WHERE UserName = N'"+TenDangNhap+"' AND PassWord = N'"+MatKhau+"'";
            DataTable result = DataProvider.Instance.ExcuteQuery(query);
            return result.Rows.Count > 0;
        }

        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExcuteNonQuery("UpdateAccount @userName , @displayName , @passWord , @newPassWord ", new object[] { userName, displayName, pass, newPass });
            return result > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM Account WHERE UserName = '" + userName + "'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public bool InsertAccount(string userName, string displayName, int type)
        {
            string query = string.Format("insert into Account(UserName,DisplayName,PassWord,Type) values (N'{0}',N'{1}',123,{2})", userName, displayName, type);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateAccount(string userName, string displayName, int type, int id)
        {
            string query = string.Format("update Account set UserName = N'{0}', DisplayName = N'{1}', Type = {2} where id = {3}",userName, displayName, type, id);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteAccount(int id)
        {
            string query = string.Format("delete Account where id = {0}", id);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }


    }
}
