using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DHKMontainApp.userControls.button_window
{
    public partial class additems : Form
    {
        public additems()
        {
            InitializeComponent();
        }
        InputLanguage previousLanguage;
        private void btn_additem_Click(object sender, EventArgs e)
        {
            // 1. Check for empty fields
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txttype.Text) ||
                string.IsNullOrWhiteSpace(txtcount.Text) ||
                string.IsNullOrWhiteSpace(txtCouple.Text))
            {
                MessageBox.Show("يرجى ملء جميع الحقول.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validate that txtcount contains a valid integer
            if (!int.TryParse(txtcount.Text, out int count))
            {
                MessageBox.Show("حقل العدد يجب أن يحتوي على رقم صحيح.", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcount.Focus();
                txtcount.SelectAll();
                return;
            }

            // 3. Validate that txtCouple contains a valid integer
            if (!int.TryParse(txtCouple.Text, out int couple))
            {
                MessageBox.Show("حقل الزوج يجب أن يحتوي على رقم صحيح.", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCouple.Focus();
                txtCouple.SelectAll();
                return;
            }

            // 4. Proceed with database insertion
            try
            {
                Database.Open();

                string query = "INSERT INTO Product (productName, productCount, producttype, productCouple) VALUES (@name, @count, @type, @couple)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, Database.con))
                {
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@count", count);      // integer
                    cmd.Parameters.AddWithValue("@type", txttype.Text);
                    cmd.Parameters.AddWithValue("@couple", couple);    // integer

                    cmd.ExecuteNonQuery();
                }

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

        private void txtcount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCouple_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCouple_Click(object sender, EventArgs e)
        {
            previousLanguage = InputLanguage.CurrentInputLanguage;

            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.Name == "en-US")
                {
                    InputLanguage.CurrentInputLanguage = lang;
                    break;
                }
            }
        }

        private void txtCouple_Leave(object sender, EventArgs e)
        {
            if (previousLanguage != null)
            {
                InputLanguage.CurrentInputLanguage = previousLanguage;
            }
        }

        private void txtcount_Click(object sender, EventArgs e)
        {
            previousLanguage = InputLanguage.CurrentInputLanguage;

            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.Name == "en-US")
                {
                    InputLanguage.CurrentInputLanguage = lang;
                    break;
                }
            }
        }

        private void txtcount_Leave(object sender, EventArgs e)
        {
            if (previousLanguage != null)
            {
                InputLanguage.CurrentInputLanguage = previousLanguage;
            }
        }
    }
}
