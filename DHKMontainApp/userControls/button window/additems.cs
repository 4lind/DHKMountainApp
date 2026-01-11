using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DHKMontainApp.userControls.button_window
{
    public partial class additems : Form
    {
        public additems()
        {
            InitializeComponent();
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            // Validate input
            if (txtName.Text == "" || txttype.Text == "" || txtcount.Text == "")
            {
                MessageBox.Show("يرجى ملء جميع الحقول.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Database.Open();

                string query = "INSERT INTO Product (productName, productCount, producttype , productCouple) VALUES (@name, @count, @type,@couple )";
                SqlCommand cmd = new SqlCommand(query, Database.con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@count", txtcount.Text);
                cmd.Parameters.AddWithValue("@type", txttype.Text);
                cmd.Parameters.AddWithValue("@couple", txtCouple.Text);


                cmd.ExecuteNonQuery();

                Database.Close();

                MessageBox.Show("تمت إضافة المنتج بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent beep sound
                txttype.Focus();
                txttype.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txttype.Focus();
                txttype.SelectAll();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                btn_additem.Focus(); // Go to button from first field
            }
        }

        private void txttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtcount.Focus();
                txtcount.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtcount.Focus();
                txtcount.SelectAll();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtName.Focus();
                txtName.SelectAll();
            }
        }

        private void txtcount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtCouple.Focus();
                txtCouple.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtCouple.Focus();
                txtCouple.SelectAll();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txttype.Focus();
                txttype.SelectAll();
            }
        }

        private void txtCouple_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btn_additem.Focus(); // Go to button
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                btn_additem.Focus(); // Go to button
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtcount.Focus();
                txtcount.SelectAll();
            }
        }

        private void btn_additem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_additem_Click(sender, e); // Trigger click event
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtCouple.Focus();
                txtCouple.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtName.Focus();
                txtName.SelectAll();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                this.Close();

            }
        }
    }
}
