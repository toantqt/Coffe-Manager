using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

        public int GetUncheckBillIDByTableID(int id) //Thành công: BillID, Thất bại: -1
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM Bill WHERE idTable = "+id+" AND status = 0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void InsertBill(int id)
        {
            string query = "insert into Bill(DateCheckIn,DateCheckOut,idTable,status,discount) values (GETDATE(),null," + id + ",0,0)";
            DataProvider.Instance.ExcuteNonQuery(query);
        }

        public void CheckOut(int id,int discount,float totalPrice)
        {
            string query = "UPDATE Bill SET DateCheckOut = GETDATE(), status = 1, discount = "+discount+", totalPrice = "+totalPrice+" WHERE id = "+id;
            DataProvider.Instance.ExcuteNonQuery(query);
        }

        public DataTable GetBillListByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            return DataProvider.Instance.ExcuteQuery("GetListBillDate @dateCheckIn , @dateCheckOut", new object[] { dateCheckIn, dateCheckOut});
        }

        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteScalar("SELECT MAX(id) FROM Bill");
            }
            catch {
                return 1;
            }
        }
    }
}























































