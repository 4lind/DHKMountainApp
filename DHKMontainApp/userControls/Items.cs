using System.Data.SQLite;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DHKMontainApp.userControls.button_window;

namespace DHKMontainApp.userControls
{
    public partial class items : UserControl
    {
        DataTable itemsTable;

        public items()
        {
            InitializeComponent();
            LoadItems();
            buttonR.MakeButtonRounded(btn_refresh, 20);
        }

        private void btn_addItems_Click(object sender, EventArgs e)
        {
            using (var add = new additems())
            {
                if (add.ShowDialog() == DialogResult.OK)
                    LoadItems();
            }
        }

        public void LoadItems()
        {
            Database.Close();
            Database.Open();

            string query = "SELECT id, productName, producttype, productCount, productCouple FROM Product";
            using (var da = new SQLiteDataAdapter(query, Database.con))
            {
                itemsTable = new DataTable();
                da.Fill(itemsTable);
                dataGridView1.DataSource = itemsTable;
            }

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


            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            
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

            if (dgv.Columns.Contains("productName"))
                dgv.Columns["productName"].HeaderText = "اسم منتج";

            if (dgv.Columns.Contains("producttype"))
                dgv.Columns["producttype"].HeaderText = "نوع";

            if (dgv.Columns.Contains("productCouple"))
                dgv.Columns["productCouple"].HeaderText = "زوج من كرتون";

            if (dgv.Columns.Contains("productCount"))
                dgv.Columns["productCount"].HeaderText = "عدد كرتون";



        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار صف للحذف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
            DialogResult confirm = MessageBox.Show("هل أنت متأكد من حذف هذا المنتج؟", "تأكيد الحذف",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    Database.Open();
                    string query = "DELETE FROM Product WHERE id = @id";
                    using (var cmd = new SQLiteCommand(query, Database.con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    Database.Close();
                    MessageBox.Show("تم حذف المنتج بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (itemsTable == null) return;
            string s = txtSearch.Text.Replace("'", "''");
            itemsTable.DefaultView.RowFilter = $"productName LIKE '%{s}%'";
        }

        private void btnedite_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("يرجى اختيار منتج أولاً!");
                    return;
                }

                string oldName = dataGridView1.CurrentRow.Cells["productName"].Value.ToString();
                string oldCount = dataGridView1.CurrentRow.Cells["productCount"].Value.ToString();
                string oldType = dataGridView1.CurrentRow.Cells["producttype"].Value.ToString();
                string oldcouple = dataGridView1.CurrentRow.Cells["productCouple"].Value.ToString();

                var editForm = new editeitem(oldName, oldCount, oldType, oldcouple);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    string newName = editForm.productName;
                    string newCount = editForm.productCount;
                    string newType = editForm.producttype;
                    string newcouple = editForm.productCouple;

                    if (!int.TryParse(newCount, out _) || !int.TryParse(newcouple, out _))
                    {
                        MessageBox.Show("العدد والزوجي يجب أن يكونا أرقاماً صحيحة.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Database.Open();
                    string updateQuery = @"
                        UPDATE Product
                        SET productName = @newName,
                            productCount = @newCount,
                            producttype = @newType,
                            productCouple = @newcouple
                        WHERE productName = @oldName 
                          AND productCount = @oldCount 
                          AND producttype = @oldType
                          AND productCouple = @oldcouple";
                    using (var cmd = new SQLiteCommand(updateQuery, Database.con))
                    {
                        cmd.Parameters.AddWithValue("@newName", newName);
                        cmd.Parameters.AddWithValue("@newCount", newCount);
                        cmd.Parameters.AddWithValue("@newType", newType);
                        cmd.Parameters.AddWithValue("@newcouple", newcouple);
                        cmd.Parameters.AddWithValue("@oldName", oldName);
                        cmd.Parameters.AddWithValue("@oldCount", oldCount);
                        cmd.Parameters.AddWithValue("@oldType", oldType);
                        cmd.Parameters.AddWithValue("@oldcouple", oldcouple);
                        cmd.ExecuteNonQuery();
                    }
                    Database.Close();
                    LoadItems();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء التعديل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e) => LoadItems();
    }
}