using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fTableManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(LoginAccount.Type); }
        }
        public fTableManager(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            loadTableList();
            loadCategory();
            loadComboboxTable(cbChuyenBan);
        }

        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }

        void loadCategory() {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "name";
        }

        void loadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id); ;
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "name";
        }

        void loadTableList()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.loadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button();
                btn.Width = 80;
                btn.Height = 80;
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.DeepPink;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }

        void showBill(int id)
        {
            lvsBill.Items.Clear();
            List<QuanLyQuanCafe.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;

            foreach (QuanLyQuanCafe.DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lvsBill.Items.Add(lsvItem);
            }
            //CultureInfo culture = new CultureInfo("vn-VN");
            //txtTongTien.Text = totalPrice.ToString("c",culture);
            txtTongTien.Text = totalPrice.ToString();
        }

        void loadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.loadTableList();
            cb.DisplayMember = "name";
        }

        private void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lvsBill.Tag = (sender as Button).Tag;
            showBill(tableID);
        }





        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccountInfo += f_UpdateAccountInfo;
            f.ShowDialog();
        }

        void f_UpdateAccountInfo(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;
            f.ShowDialog();
        }

        private void f_UpdateFood(object sender, EventArgs e)
        {
            loadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lvsBill.Tag != null)
                showBill((lvsBill.Tag as Table).ID);
        }

        private void f_DeleteFood(object sender, EventArgs e)
        {
            loadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lvsBill.Tag != null)
                showBill((lvsBill.Tag as Table).ID);
            loadTableList();
        }

        private void f_InsertFood(object sender, EventArgs e)
        {
            loadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lvsBill.Tag != null)
                showBill((lvsBill.Tag as Table).ID);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return;
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            loadFoodListByCategoryID(id);
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            Table table = lvsBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int idFood = (cbFood.SelectedItem as Food).ID;
            int count = (int)numFoodCount.Value;
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), idFood, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }
            showBill(table.ID);
            loadTableList();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            Table table = lvsBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int discount = (int)numGiamGia.Value;
            double totalPrice = Convert.ToDouble(txtTongTien.Text.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;
            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\n Tổng tiền - (Tổng tiền / 100) x Giảm giá\n => {1} - ({1} / 100) x {2} = {3}", table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount,(float)finalTotalPrice);
                    showBill(table.ID);
                    loadTableList();
                }
            }
        }

        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            int id1 = (lvsBill.Tag as Table).ID;
            int id2 = (cbChuyenBan.SelectedItem as Table).ID;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}", (lvsBill.Tag as Table).Name, (cbChuyenBan.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);
                loadTableList();
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void lvsBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
