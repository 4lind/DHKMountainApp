using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

        public sales()
        {
            InitializeComponent();

            dhkReceiptFolder = Properties.Settings.Default.SaveFolderPath;
            excelTemplatePath = Path.Combine(Application.StartupPath, "DONT-CHANGE-THIS.xlsx");

            SetupDataGridViews();
            LoadSalesData();
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
                MessageBox.Show($"المجلد غير موجود :\n{ex.Message}",
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetDefaultDates()
        {
            dtpToDate.Value = DateTime.Now;
            dtpFromDate.Value = DateTime.Now.AddDays(-30);
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
            dgv.ColumnHeadersHeight = 50;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersVisible = false;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void SetupDataGridViews()
        {
            // ========== dataGridViewSales ==========
            dataGridViewSales.AutoGenerateColumns = false;
            dataGridViewSales.Columns.Clear();

            // Add a new "رقم" column at the beginning (it will become the rightmost column because of RightToLeft)
            DataGridViewTextBoxColumn colRowNum = new DataGridViewTextBoxColumn();
            colRowNum.Name = "RowNum";
            colRowNum.HeaderText = "تسلسل";
            colRowNum.Width = 60;
            colRowNum.ReadOnly = true;
            dataGridViewSales.Columns.Add(colRowNum);

            // Existing columns
            dataGridViewSales.Columns.Add("ReceiptID", "رقم الفاتورة");
            dataGridViewSales.Columns.Add("CustomerName", "اسم الزبون");
            dataGridViewSales.Columns.Add("CustomerID", "المعرف");
            dataGridViewSales.Columns.Add("ReceiptDate", "تاريخ الفاتورة");
            dataGridViewSales.Columns.Add("TotalCartons", "إجمالي الكراتين");
            dataGridViewSales.Columns.Add("TotalPairs", "إجمالي الأزواج");
            dataGridViewSales.Columns.Add("TotalAmount", "المبلغ الإجمالي");
            dataGridViewSales.Columns.Add("Currency", "العملة");

            dataGridViewSales.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
            dataGridViewSales.Columns["ReceiptDate"].DefaultCellStyle.Format = "yyyy/MM/dd";
            dataGridViewSales.Columns["RowNum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // ========== dataGridViewItems ==========
            dataGridViewItems.AutoGenerateColumns = false;
            dataGridViewItems.Columns.Clear();

            dataGridViewItems.Columns.Add("ProductName", "اسم المنتج");
            dataGridViewItems.Columns.Add("Cartons", "عـدد الكراتين");
            dataGridViewItems.Columns.Add("PairsPerCarton", "عـدد ازواج");
            dataGridViewItems.Columns.Add("UnitPrice", "سعر الزوج");
            dataGridViewItems.Columns.Add("ItemTotal", "المبلغ الإجمالي");
            dataGridViewItems.Columns.Add("RowNumber", "تسلسل");

            dataGridViewItems.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
            dataGridViewItems.Columns["ItemTotal"].DefaultCellStyle.Format = "N2";

            // Make the "RowNumber" column the rightmost one
            dataGridViewItems.Columns["RowNumber"].DisplayIndex = 0;
            dataGridViewItems.Columns["RowNumber"].Width = 60;


            StyleDataGrid(dataGridViewSales);
            StyleDataGrid(dataGridViewItems);

            dataGridViewSales.SelectionChanged += DataGridViewSales_SelectionChanged;
        }

        private void dataGridViewSales_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewSales.Rows.Count)
                return;

            var receiptID = dataGridViewSales.Rows[e.RowIndex].Cells["ReceiptID"].Value?.ToString();

            if (!string.IsNullOrEmpty(receiptID))
            {
                try
                {
                    Clipboard.SetText(receiptID);
                    MessageBox.Show($"تم نسخ رقم الفاتورة: {receiptID} إلى الحافظة",
                        "تم النسخ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في النسخ: {ex.Message}", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        private void TxtCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }


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

                List<SQLiteParameter> parameters = new List<SQLiteParameter>();

                if (!string.IsNullOrWhiteSpace(txtCustomerSearch.Text))
                {
                    query.Append(" AND (CustomerName LIKE @CustomerName OR CustomerID LIKE @CustomerID OR ReceiptID = @ReceiptID)");
                    parameters.Add(new SQLiteParameter("@CustomerName", "%" + txtCustomerSearch.Text + "%"));
                    parameters.Add(new SQLiteParameter("@CustomerID", "%" + txtCustomerSearch.Text + "%"));
                    parameters.Add(new SQLiteParameter("@ReceiptID", txtCustomerSearch.Text));
                }

                query.Append(" AND ReceiptDate >= @FromDate");
                parameters.Add(new SQLiteParameter("@FromDate", dtpFromDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")));

                query.Append(" AND ReceiptDate <= @ToDate");
                parameters.Add(new SQLiteParameter("@ToDate", dtpToDate.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss")));

                query.Append(" ORDER BY ReceiptDate DESC");

                using (var cmd = new SQLiteCommand(query.ToString(), Database.con))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    using (var da = new SQLiteDataAdapter(cmd))
                    {
                        salesDataTable = new DataTable();
                        da.Fill(salesDataTable);
                    }
                }

                dataGridViewSales.Rows.Clear();

                int rowIndex = 1;   // start numbering from 1
                foreach (DataRow row in salesDataTable.Rows)
                {
                    string customerIdDisplay = row["CustomerID"] == DBNull.Value ? "-" : row["CustomerID"].ToString();

                    dataGridViewSales.Rows.Add(
                        rowIndex.ToString(),                        // Row number (تسلسل)
                        row["ReceiptID"],
                        row["CustomerName"],
                        customerIdDisplay,
                        Convert.ToDateTime(row["ReceiptDate"]).ToString("yyyy/MM/dd"),
                        row["TotalCartons"],
                        row["TotalPairs"],
                        Convert.ToDecimal(row["TotalAmount"]).ToString("N2"),
                        row["Currency"]
                    );
                    rowIndex++;
                }

                UpdateSummary();

                dataGridViewSales.ClearSelection();
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


        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }





        public void LoadSalesData()
        {
            try
            {
                Database.Open();

                string query = @"
            SELECT 
                ReceiptID, 
                CustomerName, 
                CustomerID,
                ReceiptDate, 
                TotalCartons, 
                TotalPairs, 
                TotalAmount, 
                Currency, 
                FilePath
            FROM Receipts 
            ORDER BY ReceiptDate DESC";

                using (var adapter = new SQLiteDataAdapter(query, Database.con))
                {
                    salesDataTable = new DataTable();
                    adapter.Fill(salesDataTable);
                }

                dataGridViewSales.Rows.Clear();

                int rowIndex = 1;
                foreach (DataRow row in salesDataTable.Rows)
                {
                    dataGridViewSales.Rows.Add(
                        rowIndex.ToString(),                               // Row number
                        row["ReceiptID"],
                        row["CustomerName"],
                        row["CustomerID"] == DBNull.Value ? "NULL" : row["CustomerID"].ToString(),
                        Convert.ToDateTime(row["ReceiptDate"]).ToString("yyyy/MM/dd"),
                        row["TotalCartons"],
                        row["TotalPairs"],
                        Convert.ToDecimal(row["TotalAmount"]).ToString("N2"),
                        row["Currency"]
                    );
                    rowIndex++;
                }

                UpdateSummary();


                // ---- NEW LINES ----
                dataGridViewSales.ClearSelection();
                dataGridViewItems.Rows.Clear();
                lblTotalAmount.Text = "0.00$";
                if (itemsDataTable != null)
                    itemsDataTable.Clear();
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
                    LoadReceiptItems(receiptID);          // fills itemsDataTable and dataGridViewItems
                    UpdateSelectedTotal();                 // NEW: update lblTotalAmount from items
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

                using (var cmd = new SQLiteCommand(query, Database.con))
                {
                    cmd.Parameters.AddWithValue("@ReceiptID", receiptID);
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
                lblTotalSales.Text = salesDataTable.Rows.Count.ToString();
            }
            else
            {
                lblTotalSales.Text = "0";
            }
        }


        private void FillTemplateHeaders(ExcelWorksheet worksheet, string receiptId, string customerName, DateTime receiptDate)
        {
            worksheet.Cells[7, 2].Value = receiptDate.ToString("yyyy/MM/dd");
            worksheet.Cells[7, 4].Value = customerName;
            worksheet.Cells[8, 2].Value = receiptId;
        }

        private void FillTemplateItems(ExcelWorksheet worksheet, SQLiteDataReader itemsReader)
        {
            int startRow = 10;
            int maxRows = 25;

            for (int row = startRow; row < startRow + maxRows; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    worksheet.Cells[row, col].Value = "";
                    worksheet.Cells[row, col].Style.Borders.SetBorders(MultipleBorders.None,
                        SpreadsheetColor.FromName(ColorName.Black), LineStyle.None);
                }
            }

            int currentRow = startRow;

            while (itemsReader.Read() && currentRow < startRow + maxRows)
            {
                string productName = itemsReader["ProductName"]?.ToString() ?? "";
                decimal cartons = Convert.ToDecimal(itemsReader["Cartons"]);
                decimal pairsPerCarton = Convert.ToDecimal(itemsReader["PairsPerCarton"]);
                decimal unitPrice = Convert.ToDecimal(itemsReader["UnitPrice"]);
                decimal itemTotal = Convert.ToDecimal(itemsReader["ItemTotal"]);

                worksheet.Cells[currentRow, 1].Value = itemTotal.ToString("N2");
                worksheet.Cells[currentRow, 2].Value = cartons.ToString("N0");
                worksheet.Cells[currentRow, 3].Value = unitPrice.ToString("N2");
                worksheet.Cells[currentRow, 4].Value = pairsPerCarton.ToString("N0");
                worksheet.Cells[currentRow, 5].Value = productName;

                var mergeRange = worksheet.Cells.GetSubrangeAbsolute(currentRow, 5, currentRow, 7);
                mergeRange.Merged = true;
                mergeRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

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
                if (!File.Exists(excelTemplatePath))
                {
                    MessageBox.Show($"ملف القالب غير موجود في المجلد:\n{excelTemplatePath}",
                        "ملف غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                Database.Open();

                string receiptQuery = @"SELECT ReceiptID, CustomerName, CustomerID, 
                              ReceiptDate, TotalCartons, TotalPairs, 
                              TotalAmount, Currency, FilePath
                              FROM Receipts 
                              WHERE ReceiptID = @ReceiptID";

                SQLiteCommand receiptCmd = new SQLiteCommand(receiptQuery, Database.con);
                receiptCmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                SQLiteDataReader receiptReader = receiptCmd.ExecuteReader();

                if (!receiptReader.Read())
                {
                    receiptReader.Close();
                    Database.Close();
                    MessageBox.Show("لم يتم العثور على بيانات الفاتورة", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                string customerName = receiptReader["CustomerName"]?.ToString() ?? "";
                DateTime receiptDate = Convert.ToDateTime(receiptReader["ReceiptDate"]);
                decimal totalCartons = Convert.ToDecimal(receiptReader["TotalCartons"]);
                decimal totalPairs = Convert.ToDecimal(receiptReader["TotalPairs"]);
                decimal totalAmount = Convert.ToDecimal(receiptReader["TotalAmount"]);
                string currency = receiptReader["Currency"]?.ToString() ?? "دينار";

                receiptReader.Close();

                string countQuery = @"SELECT COUNT(*) as ItemCount FROM ReceiptItems 
                            WHERE ReceiptID = @ReceiptID";
                SQLiteCommand countCmd = new SQLiteCommand(countQuery, Database.con);
                countCmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                int totalItems = Convert.ToInt32(countCmd.ExecuteScalar());

                string itemsQuery = @"SELECT ProductName, Cartons, PairsPerCarton, 
                            UnitPrice, ItemTotal, RowNumber
                            FROM ReceiptItems 
                            WHERE ReceiptID = @ReceiptID 
                            ORDER BY RowNumber";

                SQLiteCommand itemsCmd = new SQLiteCommand(itemsQuery, Database.con);
                itemsCmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                SQLiteDataReader itemsReader = itemsCmd.ExecuteReader();

                int itemsPerPage = 25;
                int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                string outputPath = "";

                if (totalPages == 1)
                {
                    outputPath = CreateSinglePageReceipt(receiptId, customerName, receiptDate,
                        totalCartons, totalPairs, totalAmount, currency, itemsReader);
                }
                else
                {
                    outputPath = CreateMultiPageReceipt(receiptId, customerName, receiptDate,
                        totalCartons, totalPairs, totalAmount, currency, itemsReader,
                        totalItems, totalPages, itemsPerPage);
                }

                itemsReader.Close();
                Database.Close();

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
            SQLiteDataReader itemsReader)
        {
            ExcelFile workbook = ExcelFile.Load(excelTemplatePath);
            ExcelWorksheet worksheet = workbook.Worksheets[0];

            FillTemplateHeaders(worksheet, receiptId, customerName, receiptDate);
            FillTemplateItems(worksheet, itemsReader);
            FillTemplateSummary(worksheet, totalCartons, totalPairs, totalAmount, currency);
            AddPageNumbering(worksheet, 1, 1);

            string outputFileName = $"Receipt_{receiptId}_{DateTime.Now:HHmmss}.xlsx";
            string outputPath = Path.Combine(dhkReceiptFolder, outputFileName);
            workbook.Save(outputPath);

            return outputPath;
        }

        private string CreateMultiPageReceipt(string receiptId, string customerName, DateTime receiptDate,
            decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency,
            SQLiteDataReader itemsReader, int totalItems, int totalPages, int itemsPerPage)
        {
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

            for (int page = 0; page < totalPages; page++)
            {
                ExcelFile workbook = ExcelFile.Load(excelTemplatePath);
                ExcelWorksheet worksheet = workbook.Worksheets[0];

                FillTemplateHeadersMultiPage(worksheet, receiptId, customerName, receiptDate, page + 1, totalPages);

                int startIndex = page * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, allItems.Count);

                FillTemplateItemsMultiPage(worksheet, allItems, startIndex, endIndex);

                FillTemplateSummary(worksheet, totalCartons, totalPairs, totalAmount, currency);
                AddPageNumbering(worksheet, page + 1, totalPages);

                string pageSuffix = totalPages > 1 ? $"_Page_{page + 1}_of_{totalPages}" : "";
                string outputFileName = $"Receipt_{receiptId}{pageSuffix}.xlsx";
                string outputPath = Path.Combine(dhkReceiptFolder, outputFileName);
                workbook.Save(outputPath);

                if (page == 0)
                    firstPagePath = outputPath;
            }

            return firstPagePath;
        }

        private void FillTemplateHeadersMultiPage(ExcelWorksheet worksheet, string receiptId,
            string customerName, DateTime receiptDate, int currentPage, int totalPages)
        {
            worksheet.Cells[7, 1].Value = receiptDate.ToString("dd/MM/yyyy");
            worksheet.Cells[7, 4].Value = customerName;

            string receiptIdWithPage = totalPages > 1 ? $"{receiptId}-{currentPage}" : receiptId;
            worksheet.Cells[8, 1].Value = receiptIdWithPage;
        }

        private void FillTemplateItemsMultiPage(ExcelWorksheet worksheet, List<Dictionary<string, object>> allItems,
            int startIndex, int endIndex)
        {
            int startRow = 10;
            int maxRows = 25;

            for (int row = startRow; row < startRow + maxRows; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    worksheet.Cells[row, col].Value = "";
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

                worksheet.Cells[currentRow, 1].Value = itemTotal.ToString("N2");
                worksheet.Cells[currentRow, 2].Value = cartons.ToString("N0");
                worksheet.Cells[currentRow, 3].Value = unitPrice.ToString("N2");
                worksheet.Cells[currentRow, 4].Value = pairsPerCarton.ToString("N0");
                worksheet.Cells[currentRow, 5].Value = productName;

                var mergeRange = worksheet.Cells.GetSubrangeAbsolute(currentRow, 5, currentRow, 7);
                mergeRange.Merged = true;
                mergeRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

                ApplyBordersToRow(worksheet, currentRow, 1, 7);

                currentRow++;
            }
        }

        private void AddPageNumbering(ExcelWorksheet worksheet, int currentPage, int totalPages)
        {
            int pageRow = 37;
            string pageText = $"صفحة {currentPage}-{totalPages}";
            worksheet.Cells[pageRow, 4].Value = pageText;
        }

        private void FillTemplateSummary(ExcelWorksheet worksheet, decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency)
        {
            int summaryRow = 35;

            worksheet.Cells[summaryRow, 1].Value = totalCartons.ToString("N0");
            worksheet.Cells[summaryRow, 3].Value = totalPairs.ToString("N0");
            worksheet.Cells[summaryRow, 5].Value = currency;
            worksheet.Cells[summaryRow, 6].Value = totalAmount.ToString("N2");
        }

        private void UpdateReceiptFilePath(string receiptId, string newFilePath)
        {
            try
            {
                Database.Open();

                string updateQuery = @"UPDATE Receipts SET FilePath = @FilePath WHERE ReceiptID = @ReceiptID";
                using (var cmd = new SQLiteCommand(updateQuery, Database.con))
                {
                    cmd.Parameters.AddWithValue("@FilePath", newFilePath);
                    cmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                    cmd.ExecuteNonQuery();
                }

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
                string[] possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{receiptID}*.xlsx");
                Array.Sort(possibleFiles);
                string filePath = possibleFiles.FirstOrDefault();

                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    DialogResult result = MessageBox.Show(
                        $"ملف الإكسل للفاتورة رقم {receiptID} غير موجود.\n\nهل تريد إنشاء ملف إكسل جديد؟",
                        "الملف غير موجود",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        filePath = RecreateReceiptFromTemplate(receiptID);
                        if (string.IsNullOrEmpty(filePath))
                        {
                            MessageBox.Show("تعذر إنشاء ملف الإكسل", "خطأ",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{receiptID}*.xlsx");
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
                string[] possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{receiptID}*.xlsx");
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
                        $"ملف الفاتورة رقم {receiptID} غير موجود.\n\nهل تريد إنشاء الملف الآن؟",
                        "الملف غير موجود",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (createResult == DialogResult.Yes)
                    {
                        string newFilePath = RecreateReceiptFromTemplate(receiptID);
                        if (string.IsNullOrEmpty(newFilePath))
                        {
                            MessageBox.Show("تعذر إنشاء ملف الفاتورة", "خطأ",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        possibleFiles = Directory.GetFiles(dhkReceiptFolder, $"*{receiptID}*.xlsx");
                        Array.Sort(possibleFiles);
                    }
                    else
                    {
                        return;
                    }
                }

                DialogResult printResult = MessageBox.Show($"هل تريد طباعة الفاتورة رقم: {receiptID}؟",
                    "تأكيد الطباعة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (printResult != DialogResult.Yes)
                {
                    return;
                }

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
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                ExcelFile workbook = ExcelFile.Load(filePath);
                ExcelWorksheet worksheet = workbook.Worksheets[0];

                var printOptions = worksheet.PrintOptions;
                printOptions.NumberOfCopies = 1;
                printOptions.Portrait = true;
                printOptions.FitWorksheetWidthToPages = 1;
                printOptions.FitWorksheetHeightToPages = 1;

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSales.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار فاتورة للحذف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("هل أنت متأكد من حذف الفاتورة المحددة؟ سيتم حذف جميع بنودها والملفات المرتبطة بها.",
                "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            List<string> receiptIDs = new List<string>();
            foreach (DataGridViewRow row in dataGridViewSales.SelectedRows)
            {
                var id = row.Cells["ReceiptID"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(id))
                    receiptIDs.Add(id);
            }

            if (receiptIDs.Count == 0) return;

            try
            {
                Database.Open();
                using (var tran = Database.con.BeginTransaction())
                {
                    foreach (string receiptID in receiptIDs)
                    {
                        using (var cmd = new SQLiteCommand("DELETE FROM ReceiptItems WHERE ReceiptID = @id", Database.con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", receiptID);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd2 = new SQLiteCommand("DELETE FROM Receipts WHERE ReceiptID = @id", Database.con, tran))
                        {
                            cmd2.Parameters.AddWithValue("@id", receiptID);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }

                List<string> failedFiles = new List<string>();
                foreach (string receiptID in receiptIDs)
                {
                    try
                    {
                        string[] files = Directory.GetFiles(dhkReceiptFolder, $"*{receiptID}*.xlsx");
                        foreach (string file in files)
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch (Exception ex)
                            {
                                failedFiles.Add($"{Path.GetFileName(file)} ({ex.Message})");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        failedFiles.Add($"خطأ في البحث عن ملفات الفاتورة {receiptID}: {ex.Message}");
                    }
                }

                string message = "تم حذف الفاتورة من قاعدة البيانات بنجاح.";
                if (failedFiles.Count > 0)
                {
                    message += "\n\nبعض الملفات لم يتم حذفها:\n" + string.Join("\n", failedFiles);
                    MessageBox.Show(message, "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(message, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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