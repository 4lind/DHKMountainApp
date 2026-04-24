using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DHKMontainApp.userControls.button_window
{
    public partial class editeitem : Form
    {

        public string productName = "";
        public string productCount = "";
        public string producttype = "";
        public string productCouple = "";
        InputLanguage previousLanguage;


        public editeitem(string name, string count, string type,string couple)
        {
            InitializeComponent();
            txtName.Text = name;
            txtcount.Text = count;
            txttype.Text = type;
            txtcouple.Text = couple;
        }


  



        private void btn_editeitem_Click(object sender, EventArgs e)
        {
            productName = txtName.Text.Trim();
            productCount = txtcount.Text.Trim();
            producttype = txttype.Text.Trim();
            productCouple = txtcouple.Text.Trim();


            DialogResult = DialogResult.OK;
            Close();
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
                btn_editeitem.Focus(); // Go to button from first field
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
                txtcouple.Focus();
                txtcouple.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtcouple.Focus();
                txtcouple.SelectAll();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txttype.Focus();
                txttype.SelectAll();
            }
        }

        private void txtcouple_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btn_editeitem.Focus(); // Go to button
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                btn_editeitem.Focus(); // Go to button
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtcount.Focus();
                txtcount.SelectAll();
            }
        }

        private void btn_editeitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_editeitem_Click(sender, e); // Trigger click event
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtcouple.Focus();
                txtcouple.SelectAll();
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
            // Optional: Force English every time text changes
            InputLanguage english = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US"));
            if (english != null && InputLanguage.CurrentInputLanguage != english)
            {
                InputLanguage.CurrentInputLanguage = english;
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

        private void txtcouple_Click(object sender, EventArgs e)
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

        private void txtcouple_Leave(object sender, EventArgs e)
        {
            if (previousLanguage != null)
            {
                InputLanguage.CurrentInputLanguage = previousLanguage;
            }
        }
    }
}
