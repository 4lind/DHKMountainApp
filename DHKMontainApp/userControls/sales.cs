using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DHKMontainApp.userControls
{
    public partial class sales : UserControl
    {
        private DataTable salesDataTable;
        private DataTable itemsDataTable;
        private string dhkReceiptFolder;
        private string excelTemplatePath;
        //private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public sales()
        {
            InitializeComponent();

            // Initialize folder paths
            dhkReceiptFolder = Properties.Settings.Default.SaveFolderPath;
            //excelTemplatePath = Path.Combine(Application.StartupPath, "DONT-CHANGE-THIS.xlsx");
            excelTemplatePath = Path.Combine(Application.StartupPath, "DONT-CHANGE-THIS.xlsx");


            SetupDataGridViews();
            LoadSalesData();
            SetDefaultDates();

            // Ensure folder exists
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
                MessageBox.Show($"المجلد غير موجود :\n{ex.Message}",
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
            // Setup Sales DataGridView columns
            dataGridViewSales.AutoGenerateColumns = false;
            dataGridViewSales.Columns.Clear();

            // Add columns
            dataGridViewSales.Columns.Add("ReceiptID", "رقم الفاتورة");
            dataGridViewSales.Columns.Add("CustomerName", "اسم الزبون");
            dataGridViewSales.Columns.Add("CustomerID", "رقم الزبون");
            dataGridViewSales.Columns.Add("ReceiptDate", "تاريخ الفاتورة");
            dataGridViewSales.Columns.Add("TotalCartons", "إجمالي الكراتين");
            dataGridViewSales.Columns.Add("TotalPairs", "إجمالي الأزواج");
            dataGridViewSales.Columns.Add("TotalAmount", "المبلغ الإجمالي");
            dataGridViewSales.Columns.Add("Currency", "العملة");

            // Format columns
            dataGridViewSales.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
            dataGridViewSales.Columns["ReceiptDate"].DefaultCellStyle.Format = "yyyy/MM/dd";

            // Setup Items DataGridView columns
            dataGridViewItems.AutoGenerateColumns = false;
            dataGridViewItems.Columns.Clear();

            dataGridViewItems.Columns.Add("ProductName", "اسم المنتج");
            dataGridViewItems.Columns.Add("Cartons", "الكراتين");
            dataGridViewItems.Columns.Add("PairsPerCarton", "أزواج/كرتون");
            dataGridViewItems.Columns.Add("UnitPrice", "سعر الزوج");
            dataGridViewItems.Columns.Add("ItemTotal", "المبلغ الإجمالي");
            dataGridViewItems.Columns.Add("RowNumber", "رقم الصف");

            // Format columns
            dataGridViewItems.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
            dataGridViewItems.Columns["ItemTotal"].DefaultCellStyle.Format = "N2";

            // Apply styling
            StyleDataGrid(dataGridViewSales);
            StyleDataGrid(dataGridViewItems);

            // Set up selection changed event
            dataGridViewSales.SelectionChanged += DataGridViewSales_SelectionChanged;

        }


        private void dataGridViewSales_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the double-click is on a valid row (not header)
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewSales.Rows.Count)
                return;

            // Get the ReceiptID from the clicked row
            var receiptID = dataGridViewSales.Rows[e.RowIndex].Cells["ReceiptID"].Value?.ToString();

            if (!string.IsNullOrEmpty(receiptID))
            {
                try
                {
                    // Copy to clipboard
                    Clipboard.SetText(receiptID);

                    // Show a brief notification to the user
                    // You can use a ToolTip, StatusStrip, or a small message box
                    MessageBox.Show($"تم نسخ رقم الفاتورة: {receiptID} إلى الحافظة",
                        "تم النسخ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Alternative: You could use a ToolTip for a less intrusive notification
                    // toolTip.Show($"تم نسخ رقم الفاتورة: {receiptID}", dataGridViewSales, 2000);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في النسخ: {ex.Message}", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Search timer event handler
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        // Text changed event for customer search
        private void TxtCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            // Restart the timer when text changes
            searchTimer.Stop();
            searchTimer.Start();
        }

        // Date picker value changed event
        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            // Restart the timer when date changes
            searchTimer.Stop();
            searchTimer.Start();
        }

        // Perform the actual search
        private void PerformSearch()
        {
            try
            {
                Database.Open();

                StringBuilder query = new StringBuilder();
                query.Append(@"SELECT ReceiptID, CustomerName, CustomerID, 
                             ReceiptDate, TotalCartons, TotalPairs, 
                             TotalAmount, Currency, FilePath 
                             FROM Receipts 
                             WHERE 1=1");

                List<SqlParameter> parameters = new List<SqlParameter>();

                // Add customer name/ID filter
                if (!string.IsNullOrWhiteSpace(txtCustomerSearch.Text))
                {
                    query.Append(" AND (CustomerName LIKE @CustomerName OR CustomerID LIKE @CustomerID OR ReceiptID = @ReceiptID)");
                    parameters.Add(new SqlParameter("@CustomerName", "%" + txtCustomerSearch.Text + "%"));
                    parameters.Add(new SqlParameter("@CustomerID", "%" + txtCustomerSearch.Text + "%"));
                    parameters.Add(new SqlParameter("@ReceiptID", txtCustomerSearch.Text ));

                }

                // Add date range filters
                query.Append(" AND ReceiptDate >= @FromDate");
                parameters.Add(new SqlParameter("@FromDate", dtpFromDate.Value.Date));

                query.Append(" AND ReceiptDate <= @ToDate");
                parameters.Add(new SqlParameter("@ToDate", dtpToDate.Value.Date.AddDays(1).AddSeconds(-1)));

                query.Append(" ORDER BY ReceiptDate DESC");

                SqlDataAdapter adapter = new SqlDataAdapter(query.ToString(), Database.con);
                foreach (var param in parameters)
                {
                    adapter.SelectCommand.Parameters.Add(param);
                }

                salesDataTable = new DataTable();
                adapter.Fill(salesDataTable);

                // Clear and populate the DataGridView
                dataGridViewSales.Rows.Clear();

                foreach (DataRow row in salesDataTable.Rows)
                {
                    dataGridViewSales.Rows.Add(
                        row["ReceiptID"],
                        row["CustomerName"],
                        row["CustomerID"],
                        Convert.ToDateTime(row["ReceiptDate"]).ToString("yyyy/MM/dd"),
                        row["TotalCartons"],
                        row["TotalPairs"],
                        Convert.ToDecimal(row["TotalAmount"]).ToString("N2"),
                        row["Currency"]
                    );
                }

                UpdateSummary();


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

        // Original btnSearch_Click - now calls PerformSearch
        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchTimer.Stop(); // Stop timer first
            PerformSearch();
        }

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
            dgv.ColumnHeadersHeight = 45;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersVisible = false;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void LoadSalesData()
        {
            try
            {
                Database.Open();

                string query = @"SELECT ReceiptID, CustomerName, CustomerID, 
                                ReceiptDate, TotalCartons, TotalPairs, 
                                TotalAmount, Currency, FilePath
                                FROM Receipts 
                                ORDER BY ReceiptDate DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, Database.con);
                salesDataTable = new DataTable();
                adapter.Fill(salesDataTable);

                // Clear and populate the DataGridView
                dataGridViewSales.Rows.Clear();

                foreach (DataRow row in salesDataTable.Rows)
                {
                    dataGridViewSales.Rows.Add(
                        row["ReceiptID"],
                        row["CustomerName"],
                        row["CustomerID"],
                        Convert.ToDateTime(row["ReceiptDate"]).ToString("yyyy/MM/dd"),
                        row["TotalCartons"],
                        row["TotalPairs"],
                        Convert.ToDecimal(row["TotalAmount"]).ToString("N2"),
                        row["Currency"]
                    );
                }

                UpdateSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.Close();
            }
        }

        private void DataGridViewSales_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSales.SelectedRows.Count > 0)
            {
                string receiptID = dataGridViewSales.SelectedRows[0].Cells["ReceiptID"].Value?.ToString();
                if (!string.IsNullOrEmpty(receiptID))
                {
                    LoadReceiptItems(receiptID);
                }
            }
        }

        private void LoadReceiptItems(string receiptID)
        {
            try
            {
                Database.Open();

                string query = @"SELECT ProductName, Cartons, PairsPerCarton, 
                                UnitPrice, ItemTotal, RowNumber 
                                FROM ReceiptItems 
                                WHERE ReceiptID = @ReceiptID 
                                ORDER BY RowNumber";

                SqlDataAdapter adapter = new SqlDataAdapter(query, Database.con);
                adapter.SelectCommand.Parameters.AddWithValue("@ReceiptID", receiptID);

                itemsDataTable = new DataTable();
                adapter.Fill(itemsDataTable);

                // Clear and populate the DataGridView
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
                MessageBox.Show($"خطأ في تحميل تفاصيل الفاتورة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.Close();
            }
        }

        private void UpdateSummary()
        {
            if (salesDataTable != null && salesDataTable.Rows.Count > 0)
            {
                // Calculate total sales count
                lblTotalSales.Text = salesDataTable.Rows.Count.ToString();

                // Calculate total amount
                decimal totalAmount = 0;
                foreach (DataRow row in salesDataTable.Rows)
                {
                    if (row["TotalAmount"] != DBNull.Value)
                    {
                        totalAmount += Convert.ToDecimal(row["TotalAmount"]);
                    }
                }

                lblTotalAmount.Text = totalAmount.ToString("N2") + "$";
            }
            else
            {
                lblTotalSales.Text = "0";
                lblTotalAmount.Text = "0.00";
            }
        }

        private void FillTemplateHeaders(ExcelWorksheet worksheet, string receiptId, string customerName, DateTime receiptDate)
        {
            // Find and replace placeholders in template

            worksheet.Cells[7, 2].Value = receiptDate.ToString("dd/MM/yyyy"); // B8
            worksheet.Cells[7, 4].Value = customerName; // E8

            // Row 9: receiptID (cell D9)
            worksheet.Cells[8, 2].Value = receiptId; // B9
        }

        private void FillTemplateItems(ExcelWorksheet worksheet, SqlDataReader itemsReader)
        {
            // Items start from row 11 (index 10) to row 35 (index 34)
            int startRow = 10;
            int maxRows = 25; // From row 11 to 35 = 25 rows

            // Clear existing data from rows 11-35
            for (int row = startRow; row < startRow + maxRows; row++)
            {
                for (int col = 0; col < 8; col++) // Columns A-H
                {
                    worksheet.Cells[row, col].Value = "";
                    // Clear borders
                    worksheet.Cells[row, col].Style.Borders.SetBorders(MultipleBorders.None,
                        SpreadsheetColor.FromName(ColorName.Black), LineStyle.None);
                }
            }

            int currentRow = startRow;

            while (itemsReader.Read() && currentRow < startRow + maxRows)
            {
                // Get item data
                string productName = itemsReader["ProductName"]?.ToString() ?? "";
                decimal cartons = Convert.ToDecimal(itemsReader["Cartons"]);
                decimal pairsPerCarton = Convert.ToDecimal(itemsReader["PairsPerCarton"]);
                decimal unitPrice = Convert.ToDecimal(itemsReader["UnitPrice"]);
                decimal itemTotal = Convert.ToDecimal(itemsReader["ItemTotal"]);

                // Column B (index 1): المبلغ (Item Total)
                worksheet.Cells[currentRow, 1].Value = itemTotal.ToString("N2");

                // Column C (index 2): كارتون (Cartons)
                worksheet.Cells[currentRow, 2].Value = cartons.ToString("N0");

                // Column D (index 3): السعر الزوج (Unit Price)
                worksheet.Cells[currentRow, 3].Value = unitPrice.ToString("N2");

                // Column E (index 4): زوج (Pairs per Carton)
                worksheet.Cells[currentRow, 4].Value = pairsPerCarton.ToString("N0");

                // Column F (index 5): المادة (Product Name)
                worksheet.Cells[currentRow, 5].Value = productName;

                // Merge columns F, G, H for product name
                var mergeRange = worksheet.Cells.GetSubrangeAbsolute(currentRow, 5, currentRow, 7);
                mergeRange.Merged = true;
                mergeRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

                // Apply borders to the row (columns B to H)
                ApplyBordersToRow(worksheet, currentRow, 1, 7);

                currentRow++;
            }
        }

        private void ApplyBordersToRow(ExcelWorksheet worksheet, int rowIndex, int startCol, int endCol)
        {
            for (int col = startCol; col <= endCol; col++)
            {
                var cell = worksheet.Cells[rowIndex, col];
                cell.Style.Borders.SetBorders(
                    MultipleBorders.Top | MultipleBorders.Bottom |
                    MultipleBorders.Left | MultipleBorders.Right,
                    SpreadsheetColor.FromName(ColorName.Black),
                    LineStyle.Thin);
            }
        }

        private string RecreateReceiptFromTemplate(string receiptId)
        {
            try
            {
                // Check if template exists
                if (!File.Exists(excelTemplatePath))
                {
                    MessageBox.Show($"ملف القالب غير موجود في المجلد:\n{excelTemplatePath}",
                        "ملف غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                // Set GemBox license
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                // Get receipt data from database
                Database.Open();

                // 1. Get receipt header information
                string receiptQuery = @"SELECT ReceiptID, CustomerName, CustomerID, 
                              ReceiptDate, TotalCartons, TotalPairs, 
                              TotalAmount, Currency, FilePath
                              FROM Receipts 
                              WHERE ReceiptID = @ReceiptID";

                SqlCommand receiptCmd = new SqlCommand(receiptQuery, Database.con);
                receiptCmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                SqlDataReader receiptReader = receiptCmd.ExecuteReader();

                if (!receiptReader.Read())
                {
                    receiptReader.Close();
                    Database.Close();
                    MessageBox.Show("لم يتم العثور على بيانات الفاتورة", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                // Extract receipt data
                string customerName = receiptReader["CustomerName"]?.ToString() ?? "";
                DateTime receiptDate = Convert.ToDateTime(receiptReader["ReceiptDate"]);
                decimal totalCartons = Convert.ToDecimal(receiptReader["TotalCartons"]);
                decimal totalPairs = Convert.ToDecimal(receiptReader["TotalPairs"]);
                decimal totalAmount = Convert.ToDecimal(receiptReader["TotalAmount"]);
                string currency = receiptReader["Currency"]?.ToString() ?? "دينار";

                receiptReader.Close();

                // 2. Get receipt items count
                string countQuery = @"SELECT COUNT(*) as ItemCount FROM ReceiptItems 
                            WHERE ReceiptID = @ReceiptID";
                SqlCommand countCmd = new SqlCommand(countQuery, Database.con);
                countCmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                int totalItems = Convert.ToInt32(countCmd.ExecuteScalar());

                // 3. Get all receipt items
                string itemsQuery = @"SELECT ProductName, Cartons, PairsPerCarton, 
                            UnitPrice, ItemTotal, RowNumber
                            FROM ReceiptItems 
                            WHERE ReceiptID = @ReceiptID 
                            ORDER BY RowNumber";

                SqlCommand itemsCmd = new SqlCommand(itemsQuery, Database.con);
                itemsCmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                SqlDataReader itemsReader = itemsCmd.ExecuteReader();

                // 4. Determine if we need multiple pages
                int itemsPerPage = 25;
                int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                string outputPath = "";

                if (totalPages == 1)
                {
                    // Single page receipt
                    outputPath = CreateSinglePageReceipt(receiptId, customerName, receiptDate,
                        totalCartons, totalPairs, totalAmount, currency, itemsReader);
                }
                else
                {
                    // Multi-page receipt
                    outputPath = CreateMultiPageReceipt(receiptId, customerName, receiptDate,
                        totalCartons, totalPairs, totalAmount, currency, itemsReader,
                        totalItems, totalPages, itemsPerPage);
                }

                itemsReader.Close();
                Database.Close();

                // Update database with new file path
                if (!string.IsNullOrEmpty(outputPath))
                {
                    UpdateReceiptFilePath(receiptId, outputPath);
                    MessageBox.Show($"تم إعادة إنشاء الفاتورة بنجاح\n\nعدد الصفحات: {totalPages}\nالمسار: {Path.GetFileName(outputPath)}",
                        "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return outputPath;
            }
            catch (Exception ex)
            {
                Database.Close();
                MessageBox.Show($"خطأ في إعادة إنشاء الفاتورة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private string CreateSinglePageReceipt(string receiptId, string customerName, DateTime receiptDate,
            decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency,
            SqlDataReader itemsReader)
        {
            // Load template
            ExcelFile workbook = ExcelFile.Load(excelTemplatePath);
            ExcelWorksheet worksheet = workbook.Worksheets[0];

            // Fill headers
            FillTemplateHeaders(worksheet, receiptId, customerName, receiptDate);

            // Fill items
            FillTemplateItems(worksheet, itemsReader);

            // Fill summary
            FillTemplateSummary(worksheet, totalCartons, totalPairs, totalAmount, currency);

            // Add page numbering
            AddPageNumbering(worksheet, 1, 1);

            // Save the file
            string outputFileName = $"Receipt_{receiptId}.xlsx";
            string outputPath = Path.Combine(dhkReceiptFolder, outputFileName);
            workbook.Save(outputPath);

            return outputPath;
        }

        private string CreateMultiPageReceipt(string receiptId, string customerName, DateTime receiptDate,
            decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency,
            SqlDataReader itemsReader, int totalItems, int totalPages, int itemsPerPage)
        {
            // Store all items in a list first
            List<Dictionary<string, object>> allItems = new List<Dictionary<string, object>>();

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

            string firstPagePath = "";

            // Create each page
            for (int page = 0; page < totalPages; page++)
            {
                // Load template for each page
                ExcelFile workbook = ExcelFile.Load(excelTemplatePath);
                ExcelWorksheet worksheet = workbook.Worksheets[0];

                // Fill headers with page number
                FillTemplateHeadersMultiPage(worksheet, receiptId, customerName, receiptDate, page + 1, totalPages);

                // Calculate start and end indices for this page
                int startIndex = page * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, allItems.Count);

                // Fill items for this page
                FillTemplateItemsMultiPage(worksheet, allItems, startIndex, endIndex);

                // Fill summary (show totals on all pages)
                FillTemplateSummary(worksheet, totalCartons, totalPairs, totalAmount, currency);

                // Add page numbering
                AddPageNumbering(worksheet, page + 1, totalPages);

                // Save this page
                string pageSuffix = totalPages > 1 ? $"_Page_{page + 1}_of_{totalPages}" : "";
                string outputFileName = $"Receipt_{receiptId}{pageSuffix}.xlsx";
                string outputPath = Path.Combine(dhkReceiptFolder, outputFileName);
                workbook.Save(outputPath);

                // Save first page path for return value
                if (page == 0)
                    firstPagePath = outputPath;
            }

            return firstPagePath;
        }

        private void FillTemplateHeadersMultiPage(ExcelWorksheet worksheet, string receiptId,
            string customerName, DateTime receiptDate, int currentPage, int totalPages)
        {
            // Fill standard headers
            worksheet.Cells[7, 1].Value = receiptDate.ToString("dd/MM/yyyy"); // B8
            worksheet.Cells[7, 4].Value = customerName; // E8

            // Add page number to receipt ID
            string receiptIdWithPage = totalPages > 1 ? $"{receiptId}-{currentPage}" : receiptId;
            worksheet.Cells[8, 1].Value = receiptIdWithPage; // B9
        }

        private void FillTemplateItemsMultiPage(ExcelWorksheet worksheet, List<Dictionary<string, object>> allItems,
            int startIndex, int endIndex)
        {
            // Items start from row 11 (index 10) to row 35 (index 34)
            int startRow = 10;
            int maxRows = 25;

            // Clear existing data from rows 11-35
            for (int row = startRow; row < startRow + maxRows; row++)
            {
                for (int col = 0; col < 8; col++) // Columns A-H
                {
                    worksheet.Cells[row, col].Value = "";
                    // Clear borders
                    worksheet.Cells[row, col].Style.Borders.SetBorders(MultipleBorders.None,
                        SpreadsheetColor.FromName(ColorName.Black), LineStyle.None);
                }
            }

            int currentRow = startRow;

            for (int i = startIndex; i < endIndex && currentRow < startRow + maxRows; i++)
            {
                var item = allItems[i];

                string productName = item["ProductName"]?.ToString() ?? "";
                decimal cartons = Convert.ToDecimal(item["Cartons"]);
                decimal pairsPerCarton = Convert.ToDecimal(item["PairsPerCarton"]);
                decimal unitPrice = Convert.ToDecimal(item["UnitPrice"]);
                decimal itemTotal = Convert.ToDecimal(item["ItemTotal"]);

                // Column B (index 1): المبلغ (Item Total)
                worksheet.Cells[currentRow, 1].Value = itemTotal.ToString("N2");

                // Column C (index 2): كارتون (Cartons)
                worksheet.Cells[currentRow, 2].Value = cartons.ToString("N0");

                // Column D (index 3): السعر الزوج (Unit Price)
                worksheet.Cells[currentRow, 3].Value = unitPrice.ToString("N2");

                // Column E (index 4): زوج (Pairs per Carton)
                worksheet.Cells[currentRow, 4].Value = pairsPerCarton.ToString("N0");

                // Column F (index 5): المادة (Product Name)
                worksheet.Cells[currentRow, 5].Value = productName;

                // Merge columns F, G, H for product name
                var mergeRange = worksheet.Cells.GetSubrangeAbsolute(currentRow, 5, currentRow, 7);
                mergeRange.Merged = true;
                mergeRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

                // Apply borders to the row
                ApplyBordersToRow(worksheet, currentRow, 1, 7);

                currentRow++;
            }
        }

        private void AddPageNumbering(ExcelWorksheet worksheet, int currentPage, int totalPages)
        {
            // Add page number at row 37 (F37)
            int pageRow = 37; // Row 38 (0-based)

            string pageText = $"صفحة {currentPage}-{totalPages}";

            worksheet.Cells[pageRow, 4].Value = pageText; // F37
        }



        private void FillTemplateSummary(ExcelWorksheet worksheet, decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency)
        {
            // Fill the summary row at the bottom (row 36)
            int summaryRow = 35; // Row 36 (0-based index)

            // Fill summary data according to your template:
            // Column A: boxcount (total cartons)
            worksheet.Cells[summaryRow, 1].Value = totalCartons.ToString("N0");

            // Column C: couples (total pairs)
            worksheet.Cells[summaryRow, 3].Value = totalPairs.ToString("N0");

            // Column E: currency type
            worksheet.Cells[summaryRow, 5].Value = currency;

            // Column F: Price (total amount)
            worksheet.Cells[summaryRow, 6].Value = totalAmount.ToString("N2");
        }

        private void UpdateReceiptFilePath(string receiptId, string newFilePath)
        {
            try
            {
                Database.Open();

                string updateQuery = @"UPDATE Receipts SET FilePath = @FilePath WHERE ReceiptID = @ReceiptID";
                SqlCommand cmd = new SqlCommand(updateQuery, Database.con);
                cmd.Parameters.AddWithValue("@FilePath", newFilePath);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                cmd.ExecuteNonQuery();

                Database.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating file path: {ex.Message}");
                Database.Close();
            }
        }

        private void txtCustomerSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow Enter key to trigger search
            if (e.KeyChar == (char)Keys.Enter)
            {
                searchTimer.Stop();
                PerformSearch();
                e.Handled = true;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (dataGridViewSales.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد فاتورة للطباعة", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string receiptID = dataGridViewSales.SelectedRows[0].Cells["ReceiptID"].Value?.ToString();
            if (string.IsNullOrEmpty(receiptID)) return;

            try
            {
                // Check for any files with this receipt ID (for multi-page receipts)
                string[] possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{receiptID}*.xlsx");

                // Sort files to get the first page first
                Array.Sort(possibleFiles);
                string filePath = possibleFiles.FirstOrDefault();

                // If file doesn't exist, ask user if they want to create it
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    DialogResult result = MessageBox.Show(
                        $"ملف الإكسل للفاتورة رقم {receiptID} غير موجود.\n\nهل تريد إنشاء ملف إكسل جديد؟",
                        "الملف غير موجود",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Recreate the Excel file from template
                        filePath = RecreateReceiptFromTemplate(receiptID);

                        if (string.IsNullOrEmpty(filePath))
                        {
                            MessageBox.Show("تعذر إنشاء ملف الإكسل", "خطأ",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Update the possible files list after recreation
                        possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{receiptID}*.xlsx");
                        Array.Sort(possibleFiles);
                    }
                    else
                    {
                        // User chose not to create the file
                        return;
                    }
                }

                // Open all files first
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

                // Show brief message without requiring user interaction
                if (openedCount > 0)
                {
                    string message = openedCount > 1 ?
                        $"تم فتح {openedCount} صفحات للفاتورة {receiptID}" :
                        $"تم فتح الفاتورة رقم {receiptID}";

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
                MessageBox.Show($"خطأ في فتح الفاتورة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridViewSales.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد فاتورة للطباعة", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string receiptID = dataGridViewSales.SelectedRows[0].Cells["ReceiptID"].Value?.ToString();
            if (string.IsNullOrEmpty(receiptID))
            {
                return;
            }

            try
            {
                // Check for any files with this receipt ID (for multi-page receipts)
                string[] possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{receiptID}*.xlsx");

                // Sort files to get pages in correct order
                Array.Sort(possibleFiles);

                // Check if any files exist
                bool filesExist = false;
                foreach (string file in possibleFiles)
                {
                    if (File.Exists(file))
                    {
                        filesExist = true;
                        break;
                    }
                }

                // If no files exist, ask user if they want to create them
                if (!filesExist)
                {
                    DialogResult createResult = MessageBox.Show(
                        $"ملف الفاتورة رقم {receiptID} غير موجود.\n\nهل تريد إنشاء الملف الآن؟",
                        "الملف غير موجود",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (createResult == DialogResult.Yes)
                    {
                        // Recreate the Excel file from template
                        string newFilePath = RecreateReceiptFromTemplate(receiptID);

                        if (string.IsNullOrEmpty(newFilePath))
                        {
                            MessageBox.Show("تعذر إنشاء ملف الفاتورة", "خطأ",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Refresh the files list
                        possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{receiptID}*.xlsx");
                        Array.Sort(possibleFiles);
                    }
                    else
                    {
                        // User chose not to create the file
                        return;
                    }
                }

                // Show a confirmation dialog
                DialogResult printResult = MessageBox.Show($"هل تريد طباعة الفاتورة رقم: {receiptID}؟",
                    "تأكيد الطباعة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (printResult != DialogResult.Yes)
                {
                    return;
                }

                // Print all files
                int printedCount = 0;
                foreach (string file in possibleFiles)
                {
                    if (File.Exists(file))
                    {
                        if (PrintExcelFile(file))
                        {
                            printedCount++;
                        }
                    }
                }

                // Show result message
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
        private bool PrintExcelFile(string filePath)
        {
            try
            {
                // Load the Excel file
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                ExcelFile workbook = ExcelFile.Load(filePath);

                // Get the first worksheet
                ExcelWorksheet worksheet = workbook.Worksheets[0];

                // Configure print options
                var printOptions = worksheet.PrintOptions;
                printOptions.NumberOfCopies = 1;
                printOptions.Portrait = true;
                printOptions.FitWorksheetWidthToPages = 1;
                printOptions.FitWorksheetHeightToPages = 1;

                // Print the worksheet
                workbook.Print();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الطباعة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete selected receipts (and their items) from DB and refresh the grid
            if (dataGridViewSales.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار فاتورة للحذف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("هل أنت متأكد من حذف الفاتورة المحددة؟ سيتم حذف جميع بنودها.", "تأكيد الحذف",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                Database.Open();
                using (var tran = Database.con.BeginTransaction())
                {
                    foreach (DataGridViewRow row in dataGridViewSales.SelectedRows)
                    {
                        var receiptID = row.Cells["ReceiptID"].Value?.ToString();
                        if (string.IsNullOrWhiteSpace(receiptID)) continue;

                        // delete items first
                        using (var cmd = new SqlCommand("DELETE FROM ReceiptItems WHERE ReceiptID = @id", Database.con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", receiptID);
                            cmd.ExecuteNonQuery();
                        }

                        // delete receipt
                        using (var cmd2 = new SqlCommand("DELETE FROM Receipts WHERE ReceiptID = @id", Database.con, tran))
                        {
                            cmd2.Parameters.AddWithValue("@id", receiptID);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }

                MessageBox.Show("تم الحذف بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh view respecting current filters
                PerformSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء الحذف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.Close();
            }
        }
    }
}