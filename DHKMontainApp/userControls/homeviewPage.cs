using System.Data.SQLite;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DHKMontainApp.userControls
{
    public partial class homeviewPage : UserControl
    {
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

        private void SetupTimers() { timerRefresh.Start(); }
        private void timerRefresh_Tick(object sender, EventArgs e) { LoadDashboardData(); }

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

        private void StyleDataGrid(DataGridView dgv) { /* unchanged */ }

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
                string query = @"SELECT ReceiptID, CustomerName, ReceiptDate, TotalAmount 
                                 FROM Receipts 
                                 ORDER BY ReceiptDate DESC 
                                 LIMIT 20";
                using (var cmd = new SQLiteCommand(query, Database.con))
                using (var reader = cmd.ExecuteReader())
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
                using (var cmd = new SQLiteCommand(query, Database.con))
                using (var reader = cmd.ExecuteReader())
                {
                    int alertCount = 0, finishedCount = 0;
                    while (reader.Read())
                    {
                        string productName = reader["productName"].ToString();
                        int stock = Convert.ToInt32(reader["productCount"]);

                        if (stock == 0)
                        {
                            listStockAlerts.Items.Add($"❌ {productName} - نفذت الكمية (انتهت)");
                            finishedCount++;
                        }
                        else if (stock <= 100)
                        {
                            string alert = stock <= 50
                                ? $"⚠️ {productName} - متبقي {stock} فقط! (منخفض جداً)"
                                : $"⚠️ {productName} - متبقي {stock} (منخفض)";
                            listStockAlerts.Items.Add(alert);
                            alertCount++;
                        }
                    }

                    string summary = "";
                    if (finishedCount > 0) summary += $"❌ انتهت {finishedCount} منتج\n";
                    if (alertCount > 0) summary += $"⚠️ {alertCount} منتج يحتاج تعبئة\n";
                    if (finishedCount == 0 && alertCount == 0) summary = "✅ لا توجد تنبيهات للمخزون - كل المنتجات متوفرة";

                    if (listStockAlerts.Items.Count > 0)
                    {
                        listStockAlerts.Items.Add("────────────────────");
                        listStockAlerts.Items.Add(summary);
                    }
                    else listStockAlerts.Items.Add(summary);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading stock alerts: {ex.Message}");
                listStockAlerts.Items.Clear();
                listStockAlerts.Items.Add("⚠️ خطأ في تحميل تنبيهات المخزون");
            }
        }


        // send to Form1 to navigate to the requested page
        private void NavigateToPage(string pageName) => NavigateRequested?.Invoke(this, pageName);
        private void btnNewInvoice_Click(object sender, EventArgs e) => NavigateToPage("payment");
        private void btnAddCustomer_Click(object sender, EventArgs e) => NavigateToPage("customers");
        private void btnAddProduct_Click(object sender, EventArgs e) => NavigateToPage("items");
        private void btnViewReports_Click(object sender, EventArgs e) => NavigateToPage("sales");

        private void btnPurchased_Click(object sender, EventArgs e) => NavigateToPage("purchased");

    }
}