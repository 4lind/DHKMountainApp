using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DHKMontainApp.userControls
{
    public partial class Purchased : UserControl
    {
        private DataTable purchasesDataTable;
        private DataTable itemsDataTable;
        private string dhkReceiptFolder;      // folder where purchase Excel files are stored
        private string excelTemplatePath;      // template for purchase receipts (DONT-CHANGE-THIS-2.xlsx)

        public Purchased()
        {
            InitializeComponent();




            // Use the same folder and template as in Buy.cs
            dhkReceiptFolder = Properties.Settings.Default.SaveFolderPath2;
            excelTemplatePath = Path.Combine(Application.StartupPath, "DONT-CHANGE-THIS-2.xlsx");

            SetupDataGridViews();
            LoadPurchasesData();
            SetDefaultDates();
            EnsureFolderExists();
        }

        private void EnsureFolderExists()
        {
            try
            {
                if (!Directory.Exists(dhkReceiptFolder))
                {
                    MessageBox.Show($"المجلد غير موجود :\n{dhkReceiptFolder}",
                        "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في المجلد: {ex.Message}",
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetDefaultDates()
        {
            dtpToDate.Value = DateTime.Now;
            dtpFromDate.Value = DateTime.Now.AddDays(-30);
        }

        private void SetupDataGridViews()
        {
            // Purchases DataGridView
            dataGridViewPurchases.AutoGenerateColumns = false;
            dataGridViewPurchases.Columns.Clear();

            // Add row number column (always first)
            DataGridViewTextBoxColumn colNumber = new DataGridViewTextBoxColumn();
            colNumber.Name = "RowNumber";
            colNumber.HeaderText = "تسلسل";
            colNumber.Width = 60;                      // optional fixed width
            colNumber.ReadOnly = true;
            dataGridViewPurchases.Columns.Add(colNumber);

            // Then the rest of the columns
            dataGridViewPurchases.Columns.Add("PurchaseID", "رقم الفاتورة");
            dataGridViewPurchases.Columns.Add("SupplierName", "اسم الموارد");
            //dataGridViewPurchases.Columns.Add("SupplierID", "المعرف");
            dataGridViewPurchases.Columns.Add("PurchaseDate", "تاريخ الشراء");
            dataGridViewPurchases.Columns.Add("TotalCartons", "إجمالي الكراتين");
            dataGridViewPurchases.Columns.Add("TotalPairs", "إجمالي ازواج");
            dataGridViewPurchases.Columns.Add("TotalAmount", "المبلغ");
            dataGridViewPurchases.Columns.Add("Currency", "العملة");

            // Formatting
            dataGridViewPurchases.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
            dataGridViewPurchases.Columns["PurchaseDate"].DefaultCellStyle.Format = "yyyy/MM/dd";
            dataGridViewPurchases.Columns["RowNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Items DataGridView remains unchanged (no row numbers needed)
            dataGridViewItems.AutoGenerateColumns = false;
            dataGridViewItems.Columns.Clear();

            dataGridViewItems.Columns.Add("ProductName", "اسم المنتج");
            dataGridViewItems.Columns.Add("Cartons", "عـدد الكراتين");
            dataGridViewItems.Columns.Add("PairsPerCarton", "عـدد ازواج");
            dataGridViewItems.Columns.Add("UnitPrice", "سعر الزوج");
            dataGridViewItems.Columns.Add("ItemTotal", "المبلغ");
            dataGridViewItems.Columns.Add("RowNumber", "تسلسل");

            dataGridViewItems.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
            dataGridViewItems.Columns["ItemTotal"].DefaultCellStyle.Format = "N2";

            StyleDataGrid(dataGridViewPurchases);
            StyleDataGrid(dataGridViewItems);
            dataGridViewItems.Columns["RowNumber"].DisplayIndex = 0;
            dataGridViewItems.Columns["RowNumber"].Width = 60;

            dataGridViewPurchases.SelectionChanged += DataGridViewPurchases_SelectionChanged;
        }

        // Styling method identical to sales.cs
        public static void StyleDataGrid(DataGridView dgv)
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
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 11);

            dgv.RightToLeft = RightToLeft.Yes;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(28, 32, 52);
            dgv.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(23, 27, 44);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
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

        // Load all purchases initially
        public void LoadPurchasesData()
        {
            try
            {
                Database.Open();

                string query = @"
            SELECT 
                PurchaseID, 
                SupplierName, 
                PurchaseDate, 
                TotalCartons, 
                TotalPairs, 
                TotalAmount, 
                Currency, 
                FilePath
            FROM Purchases 
            ORDER BY PurchaseDate DESC";

                using (var adapter = new SQLiteDataAdapter(query, Database.con))
                {
                    purchasesDataTable = new DataTable();
                    adapter.Fill(purchasesDataTable);
                }

                dataGridViewPurchases.Rows.Clear();

                int rowIndex = 1;  // start numbering from 1
                foreach (DataRow row in purchasesDataTable.Rows)
                {
                    dataGridViewPurchases.Rows.Add(
                        rowIndex.ToString(),                   // Row number
                        row["PurchaseID"],
                        row["SupplierName"],
                        //row["SupplierID"] == DBNull.Value ? "NULL" : row["SupplierID"].ToString(),
                        Convert.ToDateTime(row["PurchaseDate"]).ToString("yyyy/MM/dd"),
                        row["TotalCartons"],
                        row["TotalPairs"],
                        Convert.ToDecimal(row["TotalAmount"]).ToString("N2"),
                        row["Currency"]
                    );
                    rowIndex++;
                }

                UpdateSummary();

                //label price sum 
                dataGridViewPurchases.ClearSelection();    // no row selected
                dataGridViewItems.Rows.Clear();            // clear details grid
                lblTotalAmount.Text = "0.00$";            // reset label
                if (itemsDataTable != null)
                    itemsDataTable.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل بيانات المشتريات: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.Close();
            }
        }

        // Search with filters
        private void PerformSearch()
        {
            try
            {
                Database.Open();

                StringBuilder query = new StringBuilder();
                query.Append(@"SELECT PurchaseID, SupplierName,
             PurchaseDate, TotalCartons, TotalPairs, 
             TotalAmount, Currency, FilePath 
             FROM Purchases 
             WHERE 1=1");

                List<SQLiteParameter> parameters = new List<SQLiteParameter>();

                if (!string.IsNullOrWhiteSpace(txtSupplierSearch.Text))
                {
                    query.Append(" AND (SupplierName LIKE @SupplierName OR PurchaseID = @PurchaseID)");
                    parameters.Add(new SQLiteParameter("@SupplierName", "%" + txtSupplierSearch.Text + "%"));
                    parameters.Add(new SQLiteParameter("@PurchaseID", txtSupplierSearch.Text));
                }

                query.Append(" AND PurchaseDate >= @FromDate");
                parameters.Add(new SQLiteParameter("@FromDate", dtpFromDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")));

                query.Append(" AND PurchaseDate <= @ToDate");
                parameters.Add(new SQLiteParameter("@ToDate", dtpToDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss")));

                query.Append(" ORDER BY PurchaseDate DESC");

                using (var cmd = new SQLiteCommand(query.ToString(), Database.con))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    using (var da = new SQLiteDataAdapter(cmd))
                    {
                        purchasesDataTable = new DataTable();
                        da.Fill(purchasesDataTable);
                    }
                }

                dataGridViewPurchases.Rows.Clear();

                int rowIndex = 1;
                foreach (DataRow row in purchasesDataTable.Rows)
                {

                    dataGridViewPurchases.Rows.Add(
                        rowIndex.ToString(),
                        row["PurchaseID"],
                        row["SupplierName"],
                        Convert.ToDateTime(row["PurchaseDate"]).ToString("yyyy/MM/dd"),
                        row["TotalCartons"],
                        row["TotalPairs"],
                        Convert.ToDecimal(row["TotalAmount"]).ToString("N2"),
                        row["Currency"]
                    );
                    rowIndex++;
                }

                UpdateSummary();

                dataGridViewPurchases.ClearSelection();
                dataGridViewItems.Rows.Clear();
                lblTotalAmount.Text = "0.00$";
                if (itemsDataTable != null)
                    itemsDataTable.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في البحث: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.Close();
            }
        }

        private void UpdateSummary()
        {
            if (purchasesDataTable != null && purchasesDataTable.Rows.Count > 0)
            {
                lblTotalPurchases.Text = purchasesDataTable.Rows.Count.ToString();
            }
            else
            {
                lblTotalPurchases.Text = "0";
            }
        }

        // Load items when a purchase row is selected
        private void DataGridViewPurchases_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewPurchases.SelectedRows.Count > 0)
            {
                string purchaseID = dataGridViewPurchases.SelectedRows[0].Cells["PurchaseID"].Value?.ToString();
                if (!string.IsNullOrEmpty(purchaseID))
                {
                    LoadPurchaseItems(purchaseID);          // this fills itemsDataTable and dataGridViewItems
                    UpdateSelectedTotal();                 // NEW: update lblTotalAmount from itemsDataTable
                }
            }
            else
            {
                // No row selected: clear items and reset label
                dataGridViewItems.Rows.Clear();
                lblTotalAmount.Text = "0.00$";
                if (itemsDataTable != null)
                    itemsDataTable.Clear();
            }
        }

        private void LoadPurchaseItems(string purchaseID)
        {
            try
            {
                Database.Open();

                string query = @"SELECT ProductName, Cartons, PairsPerCarton, 
                                UnitPrice, ItemTotal, RowNumber 
                                FROM PurchaseItems 
                                WHERE PurchaseID = @PurchaseID 
                                ORDER BY RowNumber";

                using (var cmd = new SQLiteCommand(query, Database.con))
                {
                    cmd.Parameters.AddWithValue("@PurchaseID", purchaseID);
                    using (var da = new SQLiteDataAdapter(cmd))
                    {
                        itemsDataTable = new DataTable();
                        da.Fill(itemsDataTable);
                    }
                }

                dataGridViewItems.Rows.Clear();

                foreach (DataRow row in itemsDataTable.Rows)
                {
                    dataGridViewItems.Rows.Add(
                        row["ProductName"],
                        row["Cartons"],
                        row["PairsPerCarton"],
                        Convert.ToDecimal(row["UnitPrice"]).ToString("N2"),
                        Convert.ToDecimal(row["ItemTotal"]).ToString("N2"),
                        row["RowNumber"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل تفاصيل عملية الشراء: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.Close();
            }
        }

        // Double-click to copy PurchaseID
        private void DataGridViewPurchases_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewPurchases.Rows.Count)
                return;

            var purchaseID = dataGridViewPurchases.Rows[e.RowIndex].Cells["PurchaseID"].Value?.ToString();

            if (!string.IsNullOrEmpty(purchaseID))
            {
                try
                {
                    Clipboard.SetText(purchaseID);
                    MessageBox.Show($"تم نسخ رقم الفاتورة: {purchaseID} إلى الحافظة",
                        "تم النسخ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في النسخ: {ex.Message}", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Timer events for search
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        private void TxtSupplierSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        // Preview (open Excel files)
        private void BtnPreview_Click(object sender, EventArgs e)
        {
            if (dataGridViewPurchases.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد عملية شراء للعرض", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string purchaseID = dataGridViewPurchases.SelectedRows[0].Cells["PurchaseID"].Value?.ToString();
            if (string.IsNullOrEmpty(purchaseID)) return;

            try
            {
                string[] possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{purchaseID}*.xlsx");
                Array.Sort(possibleFiles);
                string filePath = possibleFiles.FirstOrDefault();

                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    DialogResult result = MessageBox.Show(
                        $"ملف الإكسل لعملية الشراء رقم {purchaseID} غير موجود.\n\nهل تريد إنشاء ملف إكسل جديد؟",
                        "الملف غير موجود",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        filePath = RecreatePurchaseReceipt(purchaseID);
                        if (string.IsNullOrEmpty(filePath))
                        {
                            MessageBox.Show("تعذر إنشاء ملف الإكسل", "خطأ",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{purchaseID}*.xlsx");
                        Array.Sort(possibleFiles);
                    }
                    else
                    {
                        return;
                    }
                }

                int openedCount = 0;
                foreach (string file in possibleFiles)
                {
                    if (File.Exists(file))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(file);
                            openedCount++;
                        }
                        catch (Exception openEx)
                        {
                            MessageBox.Show($"تعذر فتح الملف: {file}\n{openEx.Message}",
                                "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                if (openedCount > 0)
                {
                    string message = openedCount > 1 ?
                        $"تم فتح {openedCount} صفحات لعملية الشراء {purchaseID}" :
                        $"تم فتح عملية الشراء رقم {purchaseID}";
                    MessageBox.Show(message, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("لم يتم فتح أي ملفات", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في فتح الملف: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Print the purchase receipt
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridViewPurchases.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد عملية شراء للطباعة", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string purchaseID = dataGridViewPurchases.SelectedRows[0].Cells["PurchaseID"].Value?.ToString();
            if (string.IsNullOrEmpty(purchaseID)) return;

            try
            {
                string[] possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{purchaseID}*.xlsx");
                Array.Sort(possibleFiles);

                bool filesExist = false;
                foreach (string file in possibleFiles)
                {
                    if (File.Exists(file))
                    {
                        filesExist = true;
                        break;
                    }
                }

                if (!filesExist)
                {
                    DialogResult createResult = MessageBox.Show(
                        $"ملف عملية الشراء رقم {purchaseID} غير موجود.\n\nهل تريد إنشاء الملف الآن؟",
                        "الملف غير موجود",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (createResult == DialogResult.Yes)
                    {
                        string newFilePath = RecreatePurchaseReceipt(purchaseID);
                        if (string.IsNullOrEmpty(newFilePath))
                        {
                            MessageBox.Show("تعذر إنشاء ملف عملية الشراء", "خطأ",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{purchaseID}*.xlsx");
                        Array.Sort(possibleFiles);
                    }
                    else
                    {
                        return;
                    }
                }

                DialogResult printResult = MessageBox.Show($"هل تريد طباعة عملية الشراء رقم: {purchaseID}؟",
                    "تأكيد الطباعة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (printResult != DialogResult.Yes) return;

                int printedCount = 0;
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                foreach (string file in possibleFiles)
                {
                    if (File.Exists(file))
                    {
                        try
                        {
                            ExcelFile workbook = ExcelFile.Load(file);
                            ExcelWorksheet worksheet = workbook.Worksheets[0];
                            var printOptions = worksheet.PrintOptions;
                            printOptions.NumberOfCopies = 1;
                            printOptions.Portrait = true;
                            printOptions.FitWorksheetWidthToPages = 1;
                            printOptions.FitWorksheetHeightToPages = 1;
                            workbook.Print();
                            printedCount++;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"خطأ في طباعة الملف {Path.GetFileName(file)}: {ex.Message}",
                                "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                string message = printedCount > 0
                    ? $"تم إرسال {printedCount} صفحة للطابعة بنجاح"
                    : "تعذر إرسال الملفات للطابعة";

                MessageBox.Show(message, "نتيجة الطباعة",
                    MessageBoxButtons.OK, printedCount > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الطباعة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete selected purchase(s)
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewPurchases.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار عملية شراء للحذف.", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("هل أنت متأكد من حذف عملية الشراء المحددة؟ سيتم حذف جميع بنودها والملفات المرتبطة بها.",
                "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            List<string> purchaseIDs = new List<string>();
            foreach (DataGridViewRow row in dataGridViewPurchases.SelectedRows)
            {
                var id = row.Cells["PurchaseID"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(id))
                    purchaseIDs.Add(id);
            }

            if (purchaseIDs.Count == 0) return;

            try
            {
                Database.Open();
                using (var tran = Database.con.BeginTransaction())
                {
                    foreach (string pid in purchaseIDs)
                    {
                        using (var cmd = new SQLiteCommand("DELETE FROM PurchaseItems WHERE PurchaseID = @id", Database.con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", pid);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd2 = new SQLiteCommand("DELETE FROM Purchases WHERE PurchaseID = @id", Database.con, tran))
                        {
                            cmd2.Parameters.AddWithValue("@id", pid);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    tran.Commit();
                }

                List<string> failedFiles = new List<string>();
                foreach (string pid in purchaseIDs)
                {
                    try
                    {
                        string[] files = Directory.GetFiles(dhkReceiptFolder, $"*{pid}*.xlsx");
                        foreach (string file in files)
                        {
                            try { File.Delete(file); }
                            catch (Exception ex) { failedFiles.Add($"{Path.GetFileName(file)} ({ex.Message})"); }
                        }
                    }
                    catch (Exception ex)
                    {
                        failedFiles.Add($"خطأ في البحث عن ملفات الشراء {pid}: {ex.Message}");
                    }
                }

                string message = "تم حذف عملية الشراء من قاعدة البيانات بنجاح.";
                if (failedFiles.Count > 0)
                {
                    message += "\n\nبعض الملفات لم يتم حذفها:\n" + string.Join("\n", failedFiles);
                    MessageBox.Show(message, "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(message, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                PerformSearch(); // refresh
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء الحذف: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.Close();
            }
        }

        // Recreate the purchase Excel receipt from database data
        private string RecreatePurchaseReceipt(string purchaseId)
        {
            try
            {
                if (!File.Exists(excelTemplatePath))
                {
                    MessageBox.Show($"ملف القالب غير موجود:\n{excelTemplatePath}",
                        "ملف غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                Database.Open();

                // Get purchase header
                string purchaseQuery = @"SELECT PurchaseID, SupplierName, 
                              PurchaseDate, TotalCartons, TotalPairs, 
                              TotalAmount, Currency, FilePath
                              FROM Purchases 
                              WHERE PurchaseID = @PurchaseID";
                using (var cmd = new SQLiteCommand(purchaseQuery, Database.con))
                {
                    cmd.Parameters.AddWithValue("@PurchaseID", purchaseId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("لم يتم العثور على بيانات عملية الشراء", "خطأ",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }

                        string supplierName = reader["SupplierName"]?.ToString() ?? "";
                        DateTime purchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);
                        decimal totalCartons = Convert.ToDecimal(reader["TotalCartons"]);
                        decimal totalPairs = Convert.ToDecimal(reader["TotalPairs"]);
                        decimal totalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                        string currency = reader["Currency"]?.ToString() ?? "";

                        // Get items
                        string itemsQuery = @"SELECT ProductName, Cartons, PairsPerCarton, 
                                    UnitPrice, ItemTotal, RowNumber
                                    FROM PurchaseItems 
                                    WHERE PurchaseID = @PurchaseID 
                                    ORDER BY RowNumber";
                        var itemsCmd = new SQLiteCommand(itemsQuery, Database.con);
                        itemsCmd.Parameters.AddWithValue("@PurchaseID", purchaseId);
                        var itemsReader = itemsCmd.ExecuteReader();

                        int itemsPerPage = 25;
                        int totalItems = 0;
                        var allItems = new List<Dictionary<string, object>>();
                        while (itemsReader.Read())
                        {
                            var item = new Dictionary<string, object>
                            {
                                ["ProductName"] = itemsReader["ProductName"]?.ToString() ?? "",
                                ["Cartons"] = Convert.ToDecimal(itemsReader["Cartons"]),
                                ["PairsPerCarton"] = Convert.ToDecimal(itemsReader["PairsPerCarton"]),
                                ["UnitPrice"] = Convert.ToDecimal(itemsReader["UnitPrice"]),
                                ["ItemTotal"] = Convert.ToDecimal(itemsReader["ItemTotal"])
                            };
                            allItems.Add(item);
                        }
                        totalItems = allItems.Count;
                        itemsReader.Close();

                        int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                        string outputPath = "";

                        if (totalPages == 1)
                        {
                            outputPath = CreateSinglePagePurchaseReceipt(purchaseId, supplierName, purchaseDate,
                                totalCartons, totalPairs, totalAmount, currency, allItems);
                        }
                        else
                        {
                            outputPath = CreateMultiPagePurchaseReceipt(purchaseId, supplierName, purchaseDate,
                                totalCartons, totalPairs, totalAmount, currency, allItems,
                                totalPages, itemsPerPage);
                        }

                        if (!string.IsNullOrEmpty(outputPath))
                            UpdatePurchaseFilePath(purchaseId, outputPath);

                        return outputPath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في إعادة إنشاء ملف الشراء: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                Database.Close();
            }
        }

        private string CreateSinglePagePurchaseReceipt(string purchaseId, string supplierName, DateTime purchaseDate,
            decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency,
            List<Dictionary<string, object>> items)
        {
            var workbook = ExcelFile.Load(excelTemplatePath);
            var worksheet = workbook.Worksheets[0];

            FillPurchaseHeaders(worksheet, purchaseId, supplierName, purchaseDate, 1, 1);
            int startRow = 10, lastRow = 34;
            ClearPageItems(worksheet, startRow, lastRow);
            FillPurchaseItemsSinglePage(worksheet, startRow, items);
            FillPurchaseSummary(worksheet, totalCartons, totalPairs, totalAmount, currency);
            AddPageNumbering(worksheet, 1, 1);

            string fileName = $"Purchase_{purchaseId}_{DateTime.Now:HHmmss}.xlsx";
            string outputPath = Path.Combine(dhkReceiptFolder, fileName);
            workbook.Save(outputPath);
            return outputPath;
        }

        private string CreateMultiPagePurchaseReceipt(string purchaseId, string supplierName, DateTime purchaseDate,
            decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency,
            List<Dictionary<string, object>> allItems, int totalPages, int itemsPerPage)
        {
            int startRow = 10, lastRow = 34;
            string firstPagePath = "";

            for (int page = 0; page < totalPages; page++)
            {
                var workbook = ExcelFile.Load(excelTemplatePath);
                var worksheet = workbook.Worksheets[0];

                FillPurchaseHeaders(worksheet, purchaseId, supplierName, purchaseDate, page + 1, totalPages);
                int startIndex = page * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, allItems.Count);
                ClearPageItems(worksheet, startRow, lastRow);
                FillPurchaseItemsMultiPage(worksheet, startRow, allItems, startIndex, endIndex);
                FillPurchaseSummary(worksheet, totalCartons, totalPairs, totalAmount, currency);
                AddPageNumbering(worksheet, page + 1, totalPages);

                string fileName = $"Purchase_{purchaseId}_Page_{page + 1}_of_{totalPages}.xlsx";
                string outputPath = Path.Combine(dhkReceiptFolder, fileName);
                workbook.Save(outputPath);

                if (page == 0) firstPagePath = outputPath;
            }
            return firstPagePath;
        }

        private void FillPurchaseHeaders(ExcelWorksheet worksheet, string purchaseId, string supplierName,
            DateTime purchaseDate, int currentPage, int totalPages)
        {
            int row, col;
            if (worksheet.Cells.FindText("costumerName", true, true, out row, out col))
                worksheet.Cells[row, col].Value = supplierName;
            if (worksheet.Cells.FindText("Date", true, true, out row, out col))
                worksheet.Cells[row, col].Value = purchaseDate.ToString("yyyy/MM/dd");
            if (worksheet.Cells.FindText("receiptID", true, true, out row, out col))
            {
                string idWithPage = totalPages > 1 ? $"{purchaseId}-{currentPage}" : purchaseId;
                worksheet.Cells[row, col].Value = idWithPage;
            }
        }

        private void ClearPageItems(ExcelWorksheet worksheet, int startRow, int endRow)
        {
            for (int i = startRow; i <= endRow; i++)
                for (int j = 0; j < 8; j++)
                    if (i < worksheet.Rows.Count)
                        worksheet.Cells[i, j].Value = "";
        }

        private void FillPurchaseItemsSinglePage(ExcelWorksheet worksheet, int startRow, List<Dictionary<string, object>> items)
        {
            int rowIndex = startRow;
            foreach (var item in items)
            {
                if (rowIndex >= worksheet.Rows.Count) worksheet.Rows.InsertEmpty(rowIndex, 1);
                string productName = item["ProductName"].ToString();
                decimal cartons = Convert.ToDecimal(item["Cartons"]);
                decimal pairs = Convert.ToDecimal(item["PairsPerCarton"]);
                decimal price = Convert.ToDecimal(item["UnitPrice"]);
                decimal total = Convert.ToDecimal(item["ItemTotal"]);

                worksheet.Cells[rowIndex, 1].Value = total.ToString("N2");
                worksheet.Cells[rowIndex, 2].Value = cartons.ToString("N0");
                worksheet.Cells[rowIndex, 3].Value = price.ToString("N2");
                worksheet.Cells[rowIndex, 4].Value = pairs.ToString("N0");
                worksheet.Cells[rowIndex, 5].Value = productName;
                var merge = worksheet.Cells.GetSubrangeAbsolute(rowIndex, 5, rowIndex, 7);
                merge.Merged = true;
                merge.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                ApplyBordersToRow(worksheet, rowIndex, 1, 7);
                rowIndex++;
            }
        }

        private void FillPurchaseItemsMultiPage(ExcelWorksheet worksheet, int startRow,
            List<Dictionary<string, object>> allItems, int startIndex, int endIndex)
        {
            int rowIndex = startRow;
            for (int i = startIndex; i < endIndex; i++)
            {
                if (rowIndex >= worksheet.Rows.Count) worksheet.Rows.InsertEmpty(rowIndex, 1);
                var item = allItems[i];
                string productName = item["ProductName"].ToString();
                decimal cartons = Convert.ToDecimal(item["Cartons"]);
                decimal pairs = Convert.ToDecimal(item["PairsPerCarton"]);
                decimal price = Convert.ToDecimal(item["UnitPrice"]);
                decimal total = Convert.ToDecimal(item["ItemTotal"]);

                worksheet.Cells[rowIndex, 1].Value = total.ToString("N2");
                worksheet.Cells[rowIndex, 2].Value = cartons.ToString("N0");
                worksheet.Cells[rowIndex, 3].Value = price.ToString("N2");
                worksheet.Cells[rowIndex, 4].Value = pairs.ToString("N0");
                worksheet.Cells[rowIndex, 5].Value = productName;
                var merge = worksheet.Cells.GetSubrangeAbsolute(rowIndex, 5, rowIndex, 7);
                merge.Merged = true;
                merge.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                ApplyBordersToRow(worksheet, rowIndex, 1, 7);
                rowIndex++;
            }
        }

        private void FillPurchaseSummary(ExcelWorksheet worksheet, decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency)
        {
            int summaryRow = 35;
            worksheet.Cells[summaryRow, 1].Value = totalCartons.ToString("N0");
            worksheet.Cells[summaryRow, 3].Value = totalPairs.ToString("N0");
            worksheet.Cells[summaryRow, 5].Value = currency;
            worksheet.Cells[summaryRow, 6].Value = totalAmount.ToString("N2");
        }

        private void AddPageNumbering(ExcelWorksheet worksheet, int currentPage, int totalPages)
        {
            worksheet.Cells[37, 4].Value = $"صفحة {currentPage}-{totalPages}";
        }

        private void ApplyBordersToRow(ExcelWorksheet worksheet, int rowIndex, int startCol, int endCol)
        {
            for (int col = startCol; col <= endCol; col++)
            {
                var cell = worksheet.Cells[rowIndex, col];
                cell.Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
            }
        }

        private void UpdatePurchaseFilePath(string purchaseId, string newFilePath)
        {
            try
            {
                Database.Open();
                string updateQuery = @"UPDATE Purchases SET FilePath = @FilePath WHERE PurchaseID = @PurchaseID";
                using (var cmd = new SQLiteCommand(updateQuery, Database.con))
                {
                    cmd.Parameters.AddWithValue("@FilePath", newFilePath);
                    cmd.Parameters.AddWithValue("@PurchaseID", purchaseId);
                    cmd.ExecuteNonQuery();
                }
                Database.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating purchase file path: {ex.Message}");
                Database.Close();
            }
        }


        private void UpdateSelectedTotal()
        {
            if (itemsDataTable != null && itemsDataTable.Rows.Count > 0)
            {
                decimal sum = 0;
                foreach (DataRow row in itemsDataTable.Rows)
                {
                    if (row["ItemTotal"] != DBNull.Value)
                        sum += Convert.ToDecimal(row["ItemTotal"]);
                }
                lblTotalAmount.Text = sum.ToString("N2") + "$";
            }
            else
            {
                lblTotalAmount.Text = "0.00$";
            }
        }


        private void Purchased_Load(object sender, EventArgs e)
        {
            // Any initialisation if needed
        }
    }
}