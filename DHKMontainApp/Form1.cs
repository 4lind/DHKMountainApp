using DHKMontainApp.userControls;
using DHKMontainApp.userControls.button_window;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DHKMontainApp
{
    public partial class Form1 : Form
    {
        // Cached UserControls (created once)
        private homeviewPage homeUC;
        private UserControl costumerUC;
        private Receipt ReceiptUC;
        private UserControl itemsUC;
        private sales salesUC;
        private Buy BuyUC;
        private Purchased PurchasedUC;


        // Track current page
        private UserControl currentPage;

        public Form1()
        {
            InitializeComponent();

            // Simple connection test
            if (!Database.TestConnection())
            {
                MessageBox.Show("Cannot connect to database. Some features may be unavailable.",
                               "Connection Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }

            // Rest of your code remains the same...
            homeUC = new homeviewPage();
            costumerUC = new costumers();
            ReceiptUC = new Receipt();
            itemsUC = new items();
            salesUC = new sales();
            BuyUC = new Buy();
            PurchasedUC= new Purchased();

            homeUC.NavigateRequested += HomePage_NavigateRequested;
            LoadPage(homeUC);
            lbl_dashboard.Text = btn_Home.Text;
            btn_Home.BackColor = Color.FromArgb(50, 52, 120);
        }

        /* ===================== TIME ===================== */

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            var culture = new CultureInfo("ar-IQ");
            lblDate.Text = DateTime.Now.ToString("dddd، dd MMMM yyyy", culture);
            lbl_datetime.Text = DateTime.Now.ToString("tt hh:mm:ss", culture);
        }

        /* ===================== NAVIGATION ===================== */

        private void LoadPage(UserControl page)
        {
            // Prevent reloading same page
            if (currentPage == page)
                return;

            homePanel.SuspendLayout();

            // Hide current page
            if (currentPage != null)
                currentPage.Visible = false;

            // Add page once only
            if (!homePanel.Controls.Contains(page))
            {
                page.Dock = DockStyle.Fill;
                page.Margin = new Padding(0);
                homePanel.Controls.Add(page);
            }

            // Show selected page
            page.Visible = true;
            page.BringToFront();
            currentPage = page;

            homePanel.ResumeLayout();
        }

        private void ResetButtons()
        {
            btn_Home.BackColor = default;
            btn_costumer.BackColor = default;
            btn_buy.BackColor = default;
            btn_item.BackColor = default;
            btn_sale.BackColor = default;
            btn_paid.BackColor = default;
            btn_payment.BackColor = default;

        }

        /* ===================== BUTTON EVENTS ===================== */

        private void btn_Home_Click(object sender, EventArgs e)
        {
            ResetButtons();
            btn_Home.BackColor = Color.FromArgb(50, 52, 120);
            lbl_dashboard.Text = btn_Home.Text;
            LoadPage(homeUC);
        }

        private void btn_costumer_Click(object sender, EventArgs e)
        {
            ResetButtons();
            btn_costumer.BackColor = Color.FromArgb(50, 52, 120);
            lbl_dashboard.Text = btn_costumer.Text;
            LoadPage(costumerUC);
        }



        private void btn_item_Click(object sender, EventArgs e)
        {
            ResetButtons();
            btn_item.BackColor = Color.FromArgb(50, 52, 120);
            lbl_dashboard.Text = btn_item.Text;
            LoadPage(itemsUC);
        }

        private void btn_sale_Click(object sender, EventArgs e)
        {
            ResetButtons();
            btn_sale.BackColor = Color.FromArgb(50, 52, 120);
            lbl_dashboard.Text = btn_sale.Text;

            // Refresh data safely
            salesUC.LoadSalesData();

            LoadPage(salesUC);
        }

        private void btn_payment_Click_1(object sender, EventArgs e)
        {
            ResetButtons();
            btn_payment.BackColor = Color.FromArgb(50, 52, 120);
            lbl_dashboard.Text = btn_payment.Text;

            LoadPage(ReceiptUC);
        }

        private void btn_buy_Click(object sender, EventArgs e)
        {
            ResetButtons();
            btn_buy.BackColor = Color.FromArgb(50, 52, 120);
            lbl_dashboard.Text = btn_buy.Text;

            LoadPage(BuyUC);
        }

        private void btn_paid_Click(object sender, EventArgs e)
        {
            ResetButtons();

            btn_paid.BackColor = Color.FromArgb(50, 52, 120);
            lbl_dashboard.Text = btn_paid.Text;
            LoadPage(PurchasedUC);

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
        }


        /* ===================== HOME SHORTCUTS ===================== */

        private void HomePage_NavigateRequested(object sender, string pageName)
        {
            switch (pageName)
            {
                case "payment":
                    btn_payment.PerformClick();
                    break;

                case "customers":
                    using (var f = new addCostumer())
                    {
                        f.ShowDialog();
                    }
                    break;

                case "sales":
                    btn_sale.PerformClick();
                    break;

                case "items":
                    btn_buy.PerformClick();

                    break;
                case "purchased":
                    btn_paid.PerformClick();

                    break;


            }
        }

        /* ===================== CLEANUP ===================== */

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            homeUC?.Dispose();
            costumerUC?.Dispose();
            ReceiptUC?.Dispose();
            itemsUC?.Dispose();
            salesUC?.Dispose();
            BuyUC?.Dispose();
            PurchasedUC?.Dispose();
        }






    }
}
