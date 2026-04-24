using System;
using System.Windows.Forms;
using System.Data.SQLite;


namespace DHKMontainApp.userControls.button_window
{
    public partial class addCostumer : Form
    {
        public addCostumer()
        {
            InitializeComponent();
            txtName.Focus();
            txtName.SelectAll();
        }

        private void btn_addcostumer_Click(object sender, EventArgs e)
        {
            AddCustomer();
        }

        private void AddCustomer()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("لا يمكن أن يكون حقل الاسم فارغًا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                txtName.SelectAll();
                return;
            }

            try
            {
                Database.Open();

                SQLiteCommand cmd = new SQLiteCommand(
                    "INSERT INTO customer (cName, cPhone, Address, Company) VALUES (@name, @phone, @address, @company)",
                    Database.con
                );

                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@company", txtCompany.Text.Trim());

                cmd.ExecuteNonQuery();

                Database.Close();
                MessageBox.Show("تمت إضافة بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Set DialogResult to OK so the main form knows to refresh
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Database.Close();
                MessageBox.Show("حدث خطأ أثناء إضافة الزبون: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtCompany.Focus();
                txtCompany.SelectAll();
            }
        }

        private void txtCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtPhone.Focus();
                txtPhone.SelectAll();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtName.Focus();
                txtName.SelectAll();
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtAddress.Focus();
                txtAddress.SelectAll();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtCompany.Focus();
                txtCompany.SelectAll();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                btn_addcostumer.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtPhone.Focus();
                txtPhone.SelectAll();
            }
        }

        private void btn_addcostumer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddCustomer();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtAddress.Focus();
                txtAddress.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtName.Focus();
                txtName.SelectAll();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


    }
}