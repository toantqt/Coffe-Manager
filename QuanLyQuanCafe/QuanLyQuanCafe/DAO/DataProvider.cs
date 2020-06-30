using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        //private string connSTR = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
         private string connSTR = @"Data Source=DESKTOP-2IU4QFO\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
        public DataTable ExcuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connSTR)) //using: chạy xong sẽ tự giải phóng
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                conn.Close();
            }
            return data;
        }

        public int ExcuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection conn = new SqlConnection(connSTR)) //using: chạy xong sẽ tự giải phóng
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                if (parameter != null) {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                conn.Close();
            }
            return data;
        }

        public object ExcuteScalar(string query)
        {
            object data = 0;
            using (SqlConnection conn = new SqlConnection(connSTR)) //using: chạy xong sẽ tự giải phóng
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                data = command.ExecuteScalar();
                conn.Close();
            }
            return data;
        }
    }
}
