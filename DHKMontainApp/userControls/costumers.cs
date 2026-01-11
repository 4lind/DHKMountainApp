using DHKMontainApp.userControls.button_window;
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

namespace DHKMontainApp
{
    public partial class costumers : UserControl
    {
        DataTable customerTable; // global variable

        public costumers()
        {
            InitializeComponent();
            StyleDataGrid(dataGridView1);
            LoadCustomerData();
            buttonR.MakeButtonRounded(btn_refresh, 20);

        }



        private void btn_addCostumer_Click(object sender, EventArgs e)
        {
            addCostumer addForm = new addCostumer();

            // Show as dialog and check if user clicked OK
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // Reload the DataGrid to show the new customer
                LoadCustomerData();
            }

        }



        public void LoadCustomerData()
        {
            Database.Close();
            Database.Open();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT id, cName, cPhone, Address, Company FROM customer",
                Database.con
            );

            customerTable = new DataTable();
            da.Fill(customerTable);

            dataGridView1.DataSource = customerTable;

            StyleDataGrid(dataGridView1);

            Database.Close();
        }

        public static void StyleDataGrid(DataGridView dgv)
        {

            dgv.ReadOnly = true;               // cannot edit cells
            dgv.AllowUserToAddRows = false;    // no extra empty row
            dgv.AllowUserToDeleteRows = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.FromArgb(28, 32, 52);
            dgv.GridColor = Color.FromArgb(45, 50, 72);

            dgv.RowTemplate.Height = 40;
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(33, 38, 62);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 62, 95);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding = new Padding(5);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 11);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(28, 32, 52);

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(23, 27, 44);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersVisible = false;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgv.Columns.Contains("id"))
                dgv.Columns["id"].HeaderText = "المعرف";

            if (dgv.Columns.Contains("cName"))
                dgv.Columns["cName"].HeaderText = "الاسم";

            if (dgv.Columns.Contains("cPhone"))
                dgv.Columns["cPhone"].HeaderText = "رقم الهاتف";
            if (dgv.Columns.Contains("Address"))
                dgv.Columns["Address"].HeaderText = "العنوان";

            if (dgv.Columns.Contains("Company"))
                dgv.Columns["Company"].HeaderText = "الشركة";


        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (customerTable == null) return;

            string s = txtSearch.Text.Replace("'", "''");

            customerTable.DefaultView.RowFilter =
                $"cName LIKE '%{s}%'";
        }

        private void txtEdite_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a row first!");
                return;
            }

            // Get old values
            string oldName = dataGridView1.CurrentRow.Cells["cName"].Value.ToString();
            string oldPhone = dataGridView1.CurrentRow.Cells["cPhone"].Value.ToString();
            string oldAddress = dataGridView1.CurrentRow.Cells["Address"].Value.ToString();
            string oldCompany = dataGridView1.CurrentRow.Cells["Company"].Value.ToString();

            // Open EditForm
            EditForm editForm = new EditForm(oldName, oldPhone, oldAddress, oldCompany);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Save updates to database
                Database.Open();

                SqlCommand cmd = new SqlCommand(@"
            UPDATE customer 
            SET cName = @newName, 
                cPhone = @newPhone, 
                Address = @newAddress, 
                Company = @newCompany
            WHERE cName = @oldName AND cPhone = @oldPhone
        ", Database.con);

                cmd.Parameters.AddWithValue("@newName", editForm.NewName);
                cmd.Parameters.AddWithValue("@newPhone", editForm.NewPhone);
                cmd.Parameters.AddWithValue("@newAddress", editForm.NewAddress);
                cmd.Parameters.AddWithValue("@newCompany", editForm.NewCompany);

                cmd.Parameters.AddWithValue("@oldName", oldName);
                cmd.Parameters.AddWithValue("@oldPhone", oldPhone);

                cmd.ExecuteNonQuery();
                Database.Close();

                // Reload DataGrid
                LoadCustomerData();
            }
        }



        private void btn_refresh_Click_1(object sender, EventArgs e)
        {
            LoadCustomerData();

        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار صف للحذف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

            DialogResult confirm = MessageBox.Show(
                "هل أنت متأكد من حذف هذا الزبون؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    Database.Open();

                    string query = "DELETE FROM customer WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, Database.con);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    Database.Close();

                    MessageBox.Show("تم حذف الزبون بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadCustomerData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}

