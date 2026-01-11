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
    public partial class EditForm : Form
    {


        public string NewName = "";
        public string NewPhone = "";
        public string NewAddress = "";
        public string NewCompany = "";
 
        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(string name, string phone, string address, string company)
        {
            InitializeComponent();

            // Set old values
            txtName.Text = name;
            txtPhone.Text = phone;
            txtAddress.Text = address;
            txtCompany.Text = company;
        }

        private void btn_edite_Click(object sender, EventArgs e)
        {
            NewName = txtName.Text.Trim();
            NewPhone = txtPhone.Text.Trim();
            NewAddress = txtAddress.Text.Trim();
            NewCompany = txtCompany.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true; // Prevent beep sound
                txtCompany.Focus();
                txtCompany.SelectAll();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                btn_edite.Focus(); // Go to button from first field
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
                btn_edite.Focus(); // Go to button
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                txtPhone.Focus();
                txtPhone.SelectAll();
            }
        }

        private void btn_edite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_edite_Click(sender, e); // Trigger click event
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
                e.SuppressKeyPress = true;
                this.Close();
            }
        }
    }
}
