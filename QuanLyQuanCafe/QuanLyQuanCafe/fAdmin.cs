using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource tableFoodList = new BindingSource();
        BindingSource foodCategoryList = new BindingSource();

        public fAdmin()
        {
            InitializeComponent();
            dtgvThucAn.DataSource = foodList;
            dtgvDanhMuc.DataSource = foodCategoryList;
            dtgvTaiKhoan.DataSource = accountList;
            dtgvBanAn.DataSource = tableFoodList;
            loadListFood();
            addFoodBinding();
            loadFoodCategory();
            addFoodCategoryBinfing();
            loadTableFood();
            addTableFoodBinding();
            loadCategoryIntoCombobox(cbDanhMucThucAn);
            loadAccountList();
            addAccountBinding();
            loadDatetimePickerBill();
            loadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        void addFoodBinding()
        {
            txtIDThucAn.DataBindings.Add(new Binding("Text", dtgvThucAn.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtTenThucAn.DataBindings.Add(new Binding("Text", dtgvThucAn.DataSource, "Tên", true, DataSourceUpdateMode.Never));
            numGiaThucAn.DataBindings.Add(new Binding("Value", dtgvThucAn.DataSource, "Giá", true, DataSourceUpdateMode.Never));
            cbDanhMucThucAn.DataBindings.Add(new Binding("Text", dtgvThucAn.DataSource, "Loại"));
        }

        void addAccountBinding()
        {
            txtIDTaiKhoan.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtTenTaiKhoan.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "Tài Khoản", true, DataSourceUpdateMode.Never));
            txtTenHienThiTaiKhoan.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "Tên nhân viên", true, DataSourceUpdateMode.Never));
            numLoaiTaiKhoan.DataBindings.Add(new Binding("Value", dtgvTaiKhoan.DataSource, "Loại tài khoản", true, DataSourceUpdateMode.Never));
        }

        void addTableFoodBinding()
        {
            txtIDBanAn.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtTenBanAn.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "Tên bàn", true, DataSourceUpdateMode.Never));
            txtTrangThaiBanAn.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "Trạng thái", true, DataSourceUpdateMode.Never));
        }

        void addFoodCategoryBinfing()
        {
            txtIDDanhMuc.DataBindings.Add(new Binding("Text", dtgvDanhMuc.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtTenDanhMuc.DataBindings.Add(new Binding("Text", dtgvDanhMuc.DataSource, "Tên", true, DataSourceUpdateMode.Never));
            
        }

        void loadFoodCategory()
        {
            string query = "select id, name as [Tên] from FoodCategory";
            foodCategoryList.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }

        void loadTableFood()
        {
            string query = "select id, name as [Tên bàn], status as [Trạng thái] from TableFood";
            tableFoodList.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }

        void loadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "name";
        }


        void loadDatetimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year,today.Month,1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        void loadAccountList()
        {
            string query = "SELECT id, DisplayName as [Tên nhân viên], UserName as [Tài khoản], Type as [Loại tài khoản] FROM Account";
            accountList.DataSource = DataProvider.Instance.ExcuteQuery(query);
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabDanhMuc_Click(object sender, EventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        void loadListBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            dtgvDoanhThu.DataSource =  BillDAO.Instance.GetBillListByDate(dateCheckIn , dateCheckOut);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void dtgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        void loadListFood()
        {
            string query = "SELECT a.id, a.name as [Tên], a.price as [Giá], b.name as [Loại] FROM Food a, FoodCategory b WHERE a.idCategory = b.id";
            foodList.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        private void btnXemThucAn_Click(object sender, EventArgs e)
        {
            loadListFood();
        }

        private void btnThemThucAn_Click(object sender, EventArgs e)
        {
            string name = txtTenThucAn.Text;
            int idCategory = (cbDanhMucThucAn.SelectedItem as Category).ID;
            float price = (float)numGiaThucAn.Value;
            if (FoodDAO.Instance.InsertFood(name, idCategory, price))
            {
                MessageBox.Show("Thêm món thành công");
                loadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm món");
            }
        }

        private void btnSuaThucAn_Click(object sender, EventArgs e)
        {
            string name = txtTenThucAn.Text;
            int idCategory = (cbDanhMucThucAn.SelectedItem as Category).ID;
            float price = (float)numGiaThucAn.Value;
            int id = Convert.ToInt32(txtIDThucAn.Text);
            if (FoodDAO.Instance.UpdateFood(name, idCategory, price, id))
            {
                MessageBox.Show("Sửa món thành công");
                loadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa món");
            }
        }

        private void btnXoaThucAn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDThucAn.Text);
            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công");
                loadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa món");
            }
        }


        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }



        private void btnTimThucAn_Click_1(object sender, EventArgs e)
        {
            string query = "SELECT a.id, a.name as [Tên], a.price as [Giá], b.name as [Loại] FROM Food a, FoodCategory b WHERE a.idCategory = b.id and a.name like N'%" + txtTimThucAn.Text + "%'";
            foodList.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }

        private void btnXemTaiKhoan_Click(object sender, EventArgs e)
        {
            loadAccountList();
        }

        private void btnXemBanAn_Click(object sender, EventArgs e)
        {
            loadTableFood();
        }

        private void btnXemDanhMuc_Click(object sender, EventArgs e)
        {
            loadFoodCategory();
        }

        

        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            string userName = txtTenTaiKhoan.Text;
            string displayName = txtTenHienThiTaiKhoan.Text;
            int type = (int)numLoaiTaiKhoan.Value;
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công với Mật khẩu mặc định là: 123");
                loadAccountList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm tài khoản");
            }
        }

        private void btnSuaTaiKhoan_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDTaiKhoan.Text);
            string userName = txtTenTaiKhoan.Text;
            string displayName = txtTenHienThiTaiKhoan.Text;
            int type = (int)numLoaiTaiKhoan.Value;
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type, id))
            {
                MessageBox.Show("Sửa tài khoản thành công");
                loadAccountList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa tài khoản");
            }
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDTaiKhoan.Text);
            if (AccountDAO.Instance.DeleteAccount(id))
            {
                MessageBox.Show("Xóa tài khoản thành công");
                loadAccountList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa tài khoản");
            }
        }

        private void btnThemBanAn_Click(object sender, EventArgs e)
        {
            string name = txtTenBanAn.Text;
            string status = txtTrangThaiBanAn.Text;
            if (TableDAO.Instance.InsertTableFood(name, status))
            {
                MessageBox.Show("Thêm bàn thành công");
                loadTableFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm bàn");
            }
        }

        private void btnSuaBanAn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDBanAn.Text);
            string name = txtTenBanAn.Text;
            string status = txtTrangThaiBanAn.Text;
            if (TableDAO.Instance.UpdateTableFood(name, status, id))
            {
                MessageBox.Show("Sửa bàn thành công");
                loadTableFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa bàn");
            }
        }

        private void btnXoaBanAn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDBanAn.Text);
            if (TableDAO.Instance.DeleteTableFood(id))
            {
                MessageBox.Show("Xóa bàn thành công");
                loadTableFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa bàn");
            }
        }

        private void btnThemDanhMuc_Click(object sender, EventArgs e)
        {
            string name = txtTenDanhMuc.Text;
            if (CategoryDAO.Instance.InsertFoodCategory(name))
            {
                MessageBox.Show("Thêm danh mục thức ăn thành công");
                loadFoodCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục thức ăn");
            }
        }

        private void btnSuaDanhMuc_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDDanhMuc.Text);
            string name = txtTenDanhMuc.Text;
            if (CategoryDAO.Instance.UpdateFoodCategory(name,id))
            {
                MessageBox.Show("Sửa danh mục thức ăn thành công");
                loadFoodCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa danh mục thức ăn");
            }
        }

        private void btnXoaDanhMuc_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDDanhMuc.Text);
            if (CategoryDAO.Instance.DeleteFoodCategory(id))
            {
                MessageBox.Show("Xóa danh mục thức ăn thành công");
                loadFoodCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục thức ăn");
            }
        }


    }
}
