using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }

        private BillInfoDAO() { }

        public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExcuteQuery("DELETE BillInfo WHERE idFood = " + id);
        }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM BillInfo WHERE idBill = " + id);
            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }
            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            //string query = "insert into BillInfo(idBill,idFood,count) values ("+idBill+","+idFood+","+count+")";

            DataProvider.Instance.ExcuteNonQuery("InsertBillInfo @idBill , @idFood , @count ",new object[] {idBill,idFood,count});
        }
    }
}
