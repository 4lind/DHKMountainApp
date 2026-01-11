using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DHKMontainApp.userControls
{
    public partial class homeviewPage : UserControl
    {
        // Add this delegate and event for navigation
        public delegate void NavigationEventHandler(object sender, string pageName);
        public event NavigationEventHandler NavigateRequested;

        public homeviewPage()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        private void homeviewPage_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
            SetupTimers();
        }

        private void SetupTimers()
        {
            timerRefresh.Start();
        }





        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void SetupDataGridView()
        {
            dgvRecentSales.Columns.Clear();
            dgvRecentSales.Columns.Add("ReceiptID", "رقم الفاتورة");
            dgvRecentSales.Columns.Add("CustomerName", "اسم العميل");
            dgvRecentSales.Columns.Add("Date", "التاريخ");
            dgvRecentSales.Columns.Add("Amount", "المبلغ");
            dgvRecentSales.Columns["Amount"].DefaultCellStyle.Format = "N2";
            dgvRecentSales.Columns["Date"].DefaultCellStyle.Format = "yyyy/MM/dd";
            StyleDataGrid(dgvRecentSales);
        }

        private void StyleDataGrid(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
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
            dgv.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(28, 32, 52);
            dgv.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(23, 27, 44);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(5, 2, 5, 2);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 50;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadDashboardData()
        {
            try
            {
                Database.Open();
                LoadRecentSales();
                LoadStockAlerts();
                Database.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dashboard: {ex.Message}");
                listStockAlerts.Items.Clear();
                listStockAlerts.Items.Add($"⚠️ خطأ في تحميل البيانات: {ex.Message}");
            }
        }


        private void LoadRecentSales()
        {
            try
            {
                dgvRecentSales.Rows.Clear();
                string query = @"SELECT TOP 20 ReceiptID, CustomerName, ReceiptDate, TotalAmount FROM Receipts ORDER BY ReceiptDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, Database.con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvRecentSales.Rows.Add(
                            reader["ReceiptID"],
                            reader["CustomerName"],
                            Convert.ToDateTime(reader["ReceiptDate"]).ToString("yyyy/MM/dd"),
                            Convert.ToDecimal(reader["TotalAmount"]).ToString("N2")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading recent sales: {ex.Message}");
                dgvRecentSales.Rows.Clear();
                dgvRecentSales.Rows.Add("لا توجد بيانات", "-", "-", "0.00");
            }
        }

        private void LoadStockAlerts()
        {
            try
            {
                listStockAlerts.Items.Clear();
                string query = @"SELECT productName, productCount FROM Product ORDER BY productCount ASC";
                using (SqlCommand cmd = new SqlCommand(query, Database.con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int alertCount = 0;
                    int finishedCount = 0;

                    while (reader.Read())
                    {
                        string productName = reader["productName"].ToString();
                        int stock = Convert.ToInt32(reader["productCount"]);

                        if (stock == 0)
                        {
                            // Item is finished
                            listStockAlerts.Items.Add($"❌ {productName} - نفذت الكمية (انتهت)");
                            finishedCount++;
                        }
                        else if (stock <= 100)
                        {
                            // Item is low stock
                            string alert = stock <= 50
                                ? $"⚠️ {productName} - متبقي {stock} فقط! (منخفض جداً)"
                                : $"⚠️ {productName} - متبقي {stock} (منخفض)";
                            listStockAlerts.Items.Add(alert);
                            alertCount++;
                        }
                    }

                    // Show summary message
                    string summary = "";
                    if (finishedCount > 0)
                    {
                        summary += $"❌ انتهت {finishedCount} منتج\n";
                    }
                    if (alertCount > 0)
                    {
                        summary += $"⚠️ {alertCount} منتج يحتاج تعبئة\n";
                    }
                    if (finishedCount == 0 && alertCount == 0)
                    {
                        summary = "✅ لا توجد تنبيهات للمخزون - كل المنتجات متوفرة";
                    }

                    // Add separator and summary
                    if (listStockAlerts.Items.Count > 0)
                    {
                        listStockAlerts.Items.Add("────────────────────");
                        listStockAlerts.Items.Add(summary);
                    }
                    else
                    {
                        listStockAlerts.Items.Add(summary);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading stock alerts: {ex.Message}");
                listStockAlerts.Items.Clear();
                listStockAlerts.Items.Add("⚠️ خطأ في تحميل تنبيهات المخزون");
            }
        }

        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            NavigateToPage("payment");
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            NavigateToPage("customers");
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            NavigateToPage("items");
        }

        private void btnViewReports_Click(object sender, EventArgs e)
        {
            NavigateToPage("sales");
        }

        private void NavigateToPage(string pageName)
        {
            NavigateRequested?.Invoke(this, pageName);
        }

        private void panelQuickActions_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblRecentSalesTitle_Click(object sender, EventArgs e)
        {

        }

        private void dgvRecentSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelStatistics_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalProductsValue_Click(object sender, EventArgs e)
        {

        }

        private void cardTotalProducts_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddProduct_Click_1(object sender, EventArgs e)
        {

        }

        private void panelStockAlerts_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}