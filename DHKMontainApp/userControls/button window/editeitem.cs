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
    }
}
