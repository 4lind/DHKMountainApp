using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace DHKMontainApp.userControls
{
    public partial class Buy : UserControl
    {
        private string dhkReceiptFolder;
        private string excelTemplatePath;
        private InputLanguage previousLanguage;
        private Dictionary<string, DataRow> productLookup = new Dictionary<string, DataRow>();

        public Buy()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            InitializeComponent();

            dhkReceiptFolder = Properties.Settings.Default.SaveFolderPath2;
            excelTemplatePath = Path.Combine(Application.StartupPath, "DONT-CHANGE-THIS-2.xlsx");

            EnsureFolderExists();
            LoadProducts();
            SetupDataGridView();

            comboProducts.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboProducts.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboProducts.DropDownStyle = ComboBoxStyle.DropDown;


            buttonR.MakeButtonRounded(btn_refresh, 20);

        }

        private void EnsureFolderExists()
        {
            try
            {
                if (!Directory.Exists(dhkReceiptFolder))
                    MessageBox.Show($"المجلد غير موجود :\n{dhkReceiptFolder}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!File.Exists(excelTemplatePath))
                    MessageBox.Show($"ملف القالب غير موجود:\n{excelTemplatePath}", "ملف غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في المجلد: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("ProductName", "اسم المادة");
            dataGridView1.Columns.Add("Cartons", "عدد الكراتين");
            dataGridView1.Columns.Add("PairsPerCarton", "عدد ازواج");
            dataGridView1.Columns.Add("UnitPrice", "سعر الزوج");
            dataGridView1.Columns.Add("Total", "المبلغ");

            StyleDataGrid(dataGridView1);
            SetColumnAlignments();
        }

        private void SetColumnAlignments()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dataGridView1.Columns.Contains("Cartons"))
                dataGridView1.Columns["Cartons"].DefaultCellStyle.Format = "N0";
            if (dataGridView1.Columns.Contains("PairsPerCarton"))
                dataGridView1.Columns["PairsPerCarton"].DefaultCellStyle.Format = "N0";
            if (dataGridView1.Columns.Contains("UnitPrice"))
                dataGridView1.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
            if (dataGridView1.Columns.Contains("Total"))
                dataGridView1.Columns["Total"].DefaultCellStyle.Format = "N2";
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
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersVisible = false;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void LoadProducts()
        {
            try
            {
                Database.Open();
                string query = "SELECT id, productName, productCount, productCouple, producttype FROM Product";
                using (var da = new SQLiteDataAdapter(query, Database.con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboProducts.Items.Clear();
                    productLookup.Clear();
                    AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                    foreach (DataRow r in dt.Rows)
                    {
                        string name = r["productName"].ToString();
                        comboProducts.Items.Add(new ComboBoxItem
                        {
                            Text = $"{name} (المتوفر: {r["productCount"]})",
                            Value = r
                        });
                        productLookup[name] = r;
                        auto.Add(name);
                    }
                    comboProducts.AutoCompleteCustomSource = auto;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { Database.Close(); }
        }

        private class ComboBoxItem
        {
            public string Text;
            public object Value;
            public override string ToString() => Text;
        }

        private void comboProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProducts.SelectedItem is ComboBoxItem item)
            {
                DataRow row = (DataRow)item.Value;
                txtcouple.Text = row["productCouple"].ToString();
                txtCartons.Text = "1";
                txtprice.Text = "0.00";
                txtProductType.Text = "";
                txtCartons.Focus();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboProducts.Text))
            {
                MessageBox.Show("يرجى كتابة اسم المنتج", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCartons.Text, out int cartons) || cartons <= 0)
            {
                MessageBox.Show("يرجى إدخال عدد كراتين صحيح", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtcouple.Text, out int pairsPerCarton) || pairsPerCarton <= 0)
            {
                MessageBox.Show("يرجى إدخال عدد أزواج في الكرتون صحيح", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtprice.Text, out decimal unitPrice) || unitPrice <= 0)
            {
                MessageBox.Show("يرجى إدخال سعر صحيح", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName;
            bool isNewProduct = false;

            if (comboProducts.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)comboProducts.SelectedItem;
                DataRow row = (DataRow)selectedItem.Value;
                productName = row["productName"].ToString();
            }
            else
            {
                productName = comboProducts.Text.Trim();
                isNewProduct = true;

                if (string.IsNullOrWhiteSpace(txtProductType.Text))
                {
                    MessageBox.Show("يرجى إدخال نوع المنتج للمنتج الجديد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            decimal itemTotal = cartons * pairsPerCarton * unitPrice;

            DataGridViewRow newRow = dataGridView1.Rows[dataGridView1.Rows.Add()];
            newRow.Cells["ProductName"].Value = productName;
            newRow.Cells["Cartons"].Value = cartons.ToString("N0");
            newRow.Cells["PairsPerCarton"].Value = pairsPerCarton.ToString("N0");
            newRow.Cells["UnitPrice"].Value = unitPrice.ToString("N2");
            newRow.Cells["Total"].Value = itemTotal.ToString("N2");
            if (isNewProduct)
                newRow.Tag = txtProductType.Text.Trim();

            UpdateTotal();
            ClearInputFields();
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (row.Cells["Total"].Value != null && decimal.TryParse(row.Cells["Total"].Value.ToString().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal val))
                    total += val;
            txtTotal.Text = total.ToString("N2");
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار صف لحذفه", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("هل أنت متأكد من حذف العنصر المحدد؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    dataGridView1.Rows.Remove(row);
                UpdateTotal();
            }
        }

        private void ClearInputFields()
        {
            if (comboProducts.SelectedItem is ComboBoxItem item)
            {
                DataRow row = (DataRow)item.Value;
                txtcouple.Text = row["productCouple"].ToString();
            }
            else
            {
                txtcouple.Text = "";
            }
            txtCartons.Text = "1";
            txtprice.Text = "0.00";
            txtProductType.Text = "";
        }

        private void datetime_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker_Purchase.Enabled = !datetime_Checkbox.Checked;
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
            dateTimePicker_Purchase.Value= DateTime.Now;
        }

        private void txtCartons_Click(object sender, EventArgs e)
        {
            previousLanguage = InputLanguage.CurrentInputLanguage;
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
                if (lang.Culture.Name == "en-US") InputLanguage.CurrentInputLanguage = lang;
        }

        private void txtprice_Click(object sender, EventArgs e)
        {
            previousLanguage = InputLanguage.CurrentInputLanguage;
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
                if (lang.Culture.Name == "en-US") InputLanguage.CurrentInputLanguage = lang;
        }

        private void TxtCartons_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCartons.Text, out int val) || val < 0) txtCartons.Text = "0";
            if (previousLanguage != null) InputLanguage.CurrentInputLanguage = previousLanguage;
        }

        private void TxtCouple_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(txtcouple.Text, out int val) || val < 0) txtcouple.Text = "0";
        }

        private void TxtPrice_Leave(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtprice.Text, out decimal val) || val < 0) txtprice.Text = "0.00";
            if (previousLanguage != null) InputLanguage.CurrentInputLanguage = previousLanguage;
        }

        // ===================== MULTI‑PAGE SUPPORT (like Receipt.cs) =====================

        private void ClearBordersFromRow(ExcelWorksheet worksheet, int rowIndex, int startCol, int endCol)
        {
            for (int col = startCol; col <= endCol; col++)
            {
                var cell = worksheet.Cells[rowIndex, col];
                cell.Style.Borders.SetBorders(MultipleBorders.None, SpreadsheetColor.FromName(ColorName.Black), LineStyle.None);
            }
        }

        private void FillHeadersWithPage(ExcelWorksheet worksheet, string purchaseId, int currentPage, int totalPages, DateTime purchaseDate)
        {
            int row, col;
            //if (worksheet.Cells.FindText("costumerName", true, true, out row, out col))   /// hata comment krn chnki na vet nave mwardi diar katn 
            //    worksheet.Cells[row, col].Value = txtName.Text;
            if (worksheet.Cells.FindText("Date", true, true, out row, out col))
                worksheet.Cells[row, col].Value = purchaseDate.ToString("yyyy/MM/dd");
            if (worksheet.Cells.FindText("receiptID", true, true, out row, out col))
            {
                string receiptIdWithPage = totalPages > 1 ? $"{purchaseId}-{currentPage}" : purchaseId;
                worksheet.Cells[row, col].Value = receiptIdWithPage;
            }
        }

        private void ClearPageItems(ExcelWorksheet worksheet, int startRow, int endRow)
        {
            for (int i = startRow; i <= endRow; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i < worksheet.Rows.Count)
                    {
                        worksheet.Cells[i, j].Value = "";
                        ClearBordersFromRow(worksheet, i, 0, 7);
                        if (j >= 5 && j <= 7)
                        {
                            var cellRange = worksheet.Cells.GetSubrangeAbsolute(i, j, i, j);
                            if (cellRange.Merged)
                                cellRange.Merged = false;
                        }
                    }
                }
            }
        }

        private void FillPageItems(ExcelWorksheet worksheet, int startRow, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                int currentRow = startRow + (i - startIndex);
                if (currentRow >= worksheet.Rows.Count)
                    worksheet.Rows.InsertEmpty(currentRow, 1);

                string productName = dataGridView1.Rows[i].Cells["ProductName"].Value?.ToString() ?? "";
                string cartons = dataGridView1.Rows[i].Cells["Cartons"].Value?.ToString() ?? "0";
                string pairsPerCarton = dataGridView1.Rows[i].Cells["PairsPerCarton"].Value?.ToString() ?? "0";
                string unitPrice = dataGridView1.Rows[i].Cells["UnitPrice"].Value?.ToString() ?? "0.00";
                string total = dataGridView1.Rows[i].Cells["Total"].Value?.ToString() ?? "0.00";

                worksheet.Cells[currentRow, 1].Value = total;
                worksheet.Cells[currentRow, 2].Value = cartons;
                worksheet.Cells[currentRow, 3].Value = unitPrice;
                worksheet.Cells[currentRow, 4].Value = pairsPerCarton;
                worksheet.Cells[currentRow, 5].Value = productName;

                var mergeRange = worksheet.Cells.GetSubrangeAbsolute(currentRow, 5, currentRow, 7);
                mergeRange.Merged = true;
                worksheet.Cells[currentRow, 5].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                ApplyBordersToRow(worksheet, currentRow, 1, 7);
            }
        }

        private void FillSummaryRow(ExcelWorksheet worksheet, decimal totalCartons, decimal totalPairs, decimal totalAmount)
        {
            int summaryRow = 35;
            worksheet.Cells[summaryRow, 1].Value = totalCartons.ToString("N0");
            worksheet.Cells[summaryRow, 3].Value = totalPairs.ToString("N0");
            worksheet.Cells[summaryRow, 5].Value = txtCurrency.Text;
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

        // ===================== SAVE & PRINT (with multi‑page) =====================

        private void btnSavePrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات في الجدول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult printConfirmation = MessageBox.Show("هل تريد طباعة إيصال الشراء بعد الحفظ؟", "طباعة الإيصال", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                bool shouldPrint = (printConfirmation == DialogResult.Yes);

                DateTime purchaseDate = datetime_Checkbox.Checked ? DateTime.Now : dateTimePicker_Purchase.Value;
                string purchaseId = purchaseDate.ToString("yyyyMMddHHmmss") + DateTime.Now.ToString("fff");

                decimal totalCartons = 0, totalPairs = 0, totalAmount = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Cartons"].Value != null && row.Cells["PairsPerCarton"].Value != null && row.Cells["Total"].Value != null)
                    {
                        if (decimal.TryParse(row.Cells["Cartons"].Value.ToString().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cartons) &&
                            decimal.TryParse(row.Cells["PairsPerCarton"].Value.ToString().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal pairsPerCarton) &&
                            decimal.TryParse(row.Cells["Total"].Value.ToString().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rowTotal))
                        {
                            totalCartons += cartons;
                            totalPairs += cartons * pairsPerCarton;
                            totalAmount += rowTotal;
                        }
                    }
                }

                bool dbSaved = SavePurchaseToDatabase(purchaseId, totalCartons, totalPairs, totalAmount, purchaseDate);
                if (!dbSaved)
                {
                    MessageBox.Show("فشل حفظ بيانات الشراء في قاعدة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // --- Multi‑page Excel creation ---
                int itemsPerPage = 25;
                int totalItems = dataGridView1.Rows.Count;
                int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                string filePath = "";

                if (totalPages == 1)
                {
                    filePath = CreateSinglePageReceipt(purchaseId, totalCartons, totalPairs, totalAmount, shouldPrint, purchaseDate);
                }
                else
                {
                    filePath = CreateMultiPageReceipt(purchaseId, totalCartons, totalPairs, totalAmount, totalPages, itemsPerPage, shouldPrint, purchaseDate);
                }

                MessageBox.Show($"تم حفظ فاتورة الشراء رقم {purchaseId}\nالمسار: {Path.GetFileName(filePath)}", "تمت العملية بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFormAfterSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في إنشاء الفاتورة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CreateSinglePageReceipt(string purchaseId, decimal totalCartons, decimal totalPairs, decimal totalAmount, bool shouldPrint, DateTime purchaseDate)
        {
            if (!File.Exists(excelTemplatePath))
                throw new FileNotFoundException($"ملف القالب غير موجود: {excelTemplatePath}");

            var workbook = ExcelFile.Load(excelTemplatePath);
            var worksheet = workbook.Worksheets[0];

            FillHeadersWithPage(worksheet, purchaseId, 1, 1, purchaseDate);
            int startRow = 10, lastRow = 34;
            ClearPageItems(worksheet, startRow, lastRow);
            FillPageItems(worksheet, startRow, 0, dataGridView1.Rows.Count);
            FillSummaryRow(worksheet, totalCartons, totalPairs, totalAmount);
            AddPageNumbering(worksheet, 1, 1);

            string fileName = $"Purchase_{purchaseId}_{DateTime.Now:HHmmss}.xlsx";
            string outputFile = Path.Combine(dhkReceiptFolder, fileName);
            workbook.Save(outputFile);

            if (shouldPrint)
            {
                worksheet.PrintOptions.NumberOfCopies = 1;
                workbook.Print();
            }
            return outputFile;
        }

        private string CreateMultiPageReceipt(string purchaseId, decimal totalCartons, decimal totalPairs, decimal totalAmount,
            int totalPages, int itemsPerPage, bool shouldPrint, DateTime purchaseDate)
        {
            if (!File.Exists(excelTemplatePath))
                throw new FileNotFoundException($"ملف القالب غير موجود: {excelTemplatePath}");

            int startRow = 10, lastRow = 34;
            string firstPagePath = "";

            for (int page = 0; page < totalPages; page++)
            {
                var workbook = ExcelFile.Load(excelTemplatePath);
                var worksheet = workbook.Worksheets[0];

                FillHeadersWithPage(worksheet, purchaseId, page + 1, totalPages, purchaseDate);
                int startIndex = page * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, dataGridView1.Rows.Count);
                ClearPageItems(worksheet, startRow, lastRow);
                FillPageItems(worksheet, startRow, startIndex, endIndex);
                FillSummaryRow(worksheet, totalCartons, totalPairs, totalAmount);
                AddPageNumbering(worksheet, page + 1, totalPages);

                string fileName = $"Purchase_{purchaseId}_Page_{page + 1}_of_{totalPages}.xlsx";
                string outputFile = Path.Combine(dhkReceiptFolder, fileName);
                workbook.Save(outputFile);

                if (shouldPrint)
                {
                    worksheet.PrintOptions.NumberOfCopies = 1;
                    workbook.Print();
                }

                if (page == 0)
                    firstPagePath = outputFile;
            }
            return firstPagePath;
        }

        // ===================== DATABASE METHODS (unchanged) =====================

        private bool SavePurchaseToDatabase(string purchaseId, decimal totalCartons, decimal totalPairs, decimal totalAmount, DateTime purchaseDate)
        {
            try
            {
                Database.Open();
                using (var tran = Database.con.BeginTransaction())
                {
                    string insertPurchase = @"INSERT INTO Purchases (PurchaseID, SupplierName, SupplierID, PurchaseDate, TotalCartons, TotalPairs, TotalAmount, Currency)
                                      VALUES (@id, @name, @sid, @date, @cartons, @pairs, @amount, @curr)";
                    using (var cmd = new SQLiteCommand(insertPurchase, Database.con, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", purchaseId);
                        cmd.Parameters.AddWithValue("@name", string.IsNullOrWhiteSpace(txtName.Text) ? "غير محدد" : txtName.Text);
                        cmd.Parameters.AddWithValue("@sid", DBNull.Value);
                        cmd.Parameters.AddWithValue("@date", purchaseDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@cartons", totalCartons);
                        cmd.Parameters.AddWithValue("@pairs", totalPairs);
                        cmd.Parameters.AddWithValue("@amount", totalAmount);
                        cmd.Parameters.AddWithValue("@curr", txtCurrency.Text);
                        cmd.ExecuteNonQuery();
                    }

                    int rowNumber = 1;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        string productName = row.Cells["ProductName"].Value.ToString();
                        string productType = row.Tag?.ToString() ?? "";
                        if (!int.TryParse(row.Cells["Cartons"].Value.ToString().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out int cartons))
                            cartons = 0;
                        if (!int.TryParse(row.Cells["PairsPerCarton"].Value.ToString().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out int pairsPerCarton))
                            pairsPerCarton = 0;
                        if (!decimal.TryParse(row.Cells["UnitPrice"].Value.ToString().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal unitPrice))
                            unitPrice = 0;
                        if (!decimal.TryParse(row.Cells["Total"].Value.ToString().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal itemTotal))
                            itemTotal = 0;

                        string insertItem = @"INSERT INTO PurchaseItems (PurchaseID, ProductName, Cartons, PairsPerCarton, UnitPrice, ItemTotal, RowNumber)
                                      VALUES (@pid, @pname, @cart, @ppc, @price, @total, @row)";
                        using (var cmd = new SQLiteCommand(insertItem, Database.con, tran))
                        {
                            cmd.Parameters.AddWithValue("@pid", purchaseId);
                            cmd.Parameters.AddWithValue("@pname", productName);
                            cmd.Parameters.AddWithValue("@cart", cartons);
                            cmd.Parameters.AddWithValue("@ppc", pairsPerCarton);
                            cmd.Parameters.AddWithValue("@price", unitPrice);
                            cmd.Parameters.AddWithValue("@total", itemTotal);
                            cmd.Parameters.AddWithValue("@row", rowNumber++);
                            cmd.ExecuteNonQuery();
                        }

                        string checkProduct = "SELECT COUNT(*) FROM Product WHERE productName = @pname";
                        using (var checkCmd = new SQLiteCommand(checkProduct, Database.con, tran))
                        {
                            checkCmd.Parameters.AddWithValue("@pname", productName);
                            long count = (long)checkCmd.ExecuteScalar();
                            if (count > 0)
                            {
                                string updateStock = "UPDATE Product SET productCount = productCount + @cart WHERE productName = @pname";
                                using (var updCmd = new SQLiteCommand(updateStock, Database.con, tran))
                                {
                                    updCmd.Parameters.AddWithValue("@cart", cartons);
                                    updCmd.Parameters.AddWithValue("@pname", productName);
                                    updCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(productType))
                                    productType = "عام";
                                string insertProduct = @"INSERT INTO Product (productName, productCount, producttype, productCouple)
                                                         VALUES (@name, @count, @type, @couple)";
                                using (var insCmd = new SQLiteCommand(insertProduct, Database.con, tran))
                                {
                                    insCmd.Parameters.AddWithValue("@name", productName);
                                    insCmd.Parameters.AddWithValue("@count", cartons);
                                    insCmd.Parameters.AddWithValue("@type", productType);
                                    insCmd.Parameters.AddWithValue("@couple", pairsPerCarton);
                                    insCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    tran.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في حفظ البيانات: {ex.Message}\n\n{ex.StackTrace}", "خطأ في قاعدة البيانات", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally { Database.Close(); }
        }

        private void ClearFormAfterSave()
        {
            dataGridView1.Rows.Clear();
            txtTotal.Text = "0.00";
            txtCartons.Text = "1";
            txtcouple.Text = "0";
            txtprice.Text = "0.00";
            txtProductType.Text = "";
            comboProducts.SelectedIndex = -1;
            comboProducts.Text = "";
            LoadProducts();
        }

        private void Paid_Load(object sender, EventArgs e) { }
        private void txtcouple_TextChanged(object sender, EventArgs e) { }
    }
}