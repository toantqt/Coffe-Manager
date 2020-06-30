using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> listFood = new List<Food>();
            string query = "SELECT * FROM Food WHERE idCategory = "+id;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }

        public bool InsertFood(string name, int idCategory, float price)
        {
            string query = string.Format("insert into Food(name,idCategory,price) values (N'{0}',{1},{2})", name, idCategory, price);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateFood(string name, int idCategory, float price, int idFood)
        {
            string query = string.Format("update Food set name = N'{0}', idCategory = {1}, price = {2} where id = {3}", name, idCategory, price, idFood);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);
            string query = string.Format("delete Food where id = {0}", idFood);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }

    }
}
