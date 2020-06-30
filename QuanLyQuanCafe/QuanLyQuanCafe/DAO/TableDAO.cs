using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        private TableDAO() { }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExcuteQuery("SwitchTable @idTable1 , @idTable2",new object[]{id1,id2});
        }

        public List<Table> loadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM TableFood");
            foreach(DataRow item in data.Rows){
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;

        }

        public bool InsertTableFood(string name, string status)
        {
            string query = string.Format("insert into TableFood(name,status) values (N'{0}',N'{1}')", name, status);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateTableFood(string name, string status, int id)
        {
            string query = string.Format("update TableFood set name = N'{0}', status = N'{1}' where id = {2}", name, status,  id);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteTableFood(int id)
        {
            string query = string.Format("delete TableFood where id = {0}", id);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
    }
}
