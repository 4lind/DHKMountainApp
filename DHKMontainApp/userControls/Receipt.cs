using GemBox.Spreadsheet;
using System.Data.SQLite;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace DHKMontainApp.userControls
{
    public partial class Receipt : UserControl
    {
        private string dhkReceiptFolder;
        private string excelTemplatePath;
        InputLanguage previousLanguage;

        public Receipt()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            InitializeComponent();

            dhkReceiptFolder = Properties.Settings.Default.SaveFolderPath;
            excelTemplatePath = Path.Combine(Application.StartupPath, "DONT-CHANGE-THIS.xlsx");

            EnsureFolderExists();
            LoadCustomers();
            LoadProducts();
            SetupDataGridView();
            buttonR.MakeButtonRounded(btn_refresh, 20);

            comboProducts.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboProducts.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBoxCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;


            //datetime_Checkbox.CheckedChanged += datetime_Checkbox_CheckedChanged;
            //dateTimePicker_Payment.Enabled = !datetime_Checkbox.Checked;
        }

        private void datetime_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker_Payment.Enabled = !datetime_Checkbox.Checked;
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

                if (!File.Exists(excelTemplatePath))
                {
                    MessageBox.Show($"ملف القالب غير موجود في المجلد:\n{excelTemplatePath}\n",
                        "ملف غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في المجلد: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "ProductName";
            col1.HeaderText = "اسم المادة";
            dataGridView1.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "Cartons";
            col2.HeaderText = "عدد الكراتين";
            dataGridView1.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "PairsPerCarton";
            col3.HeaderText = "عدد ازواج";
            dataGridView1.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "UnitPrice";
            col4.HeaderText = "سعر الزوج";
            dataGridView1.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "Total";
            col5.HeaderText = "المبلغ";
            dataGridView1.Columns.Add(col5);

            StyleDataGrid(dataGridView1);
            SetColumnAlignments();
        }

        private void SetColumnAlignments()
        {
            if (dataGridView1.Columns.Contains("ProductName"))
            {
                dataGridView1.Columns["ProductName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dataGridView1.Columns.Contains("Cartons"))
            {
                dataGridView1.Columns["Cartons"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Cartons"].DefaultCellStyle.Format = "N0";
            }

            if (dataGridView1.Columns.Contains("PairsPerCarton"))
            {
                dataGridView1.Columns["PairsPerCarton"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["PairsPerCarton"].DefaultCellStyle.Format = "N0";
            }

            if (dataGridView1.Columns.Contains("UnitPrice"))
            {
                dataGridView1.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
            }

            if (dataGridView1.Columns.Contains("Total"))
            {
                dataGridView1.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Total"].DefaultCellStyle.Format = "N2";
            }

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
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

        private void LoadCustomers()
        {
            try
            {
                Database.Close();
                Database.Open();
                string query = "SELECT id, cName FROM customer";
                using (var da = new SQLiteDataAdapter(query, Database.con))
                {
                    var dt = new DataTable();
                    da.Fill(dt);

                    comboBoxCustomer.DisplayMember = "cName";
                    comboBoxCustomer.ValueMember = "id";
                    comboBoxCustomer.DataSource = dt;

                    AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                    foreach (DataRow row in dt.Rows)
                        auto.Add(row["cName"].ToString());

                    comboBoxCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBoxCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    comboBoxCustomer.AutoCompleteCustomSource = auto;

                    comboBoxCustomer.SelectedIndex = -1;
                    comboBoxCustomer.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ بجلب الزبائن: " + ex.Message);
            }
            finally
            {
                Database.Close();
            }
        }

        private void LoadProducts()
        {
            try
            {
                Database.Open();
                string query = "SELECT id, productName, productCount, productCouple FROM Product";
                using (var da = new SQLiteDataAdapter(query, Database.con))
                {
                    var dt = new DataTable();
                    da.Fill(dt);

                    comboProducts.Items.Clear();

                    AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

                    foreach (DataRow r in dt.Rows)
                    {
                        comboProducts.Items.Add(new ComboBoxItem
                        {
                            Text = $"{r["productName"]} (المتوفر: {r["productCount"]})",
                            Value = r
                        });

                        autoComplete.Add(r["productName"].ToString());
                    }

                    comboProducts.AutoCompleteCustomSource = autoComplete;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Database.Close();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (comboProducts.SelectedItem == null)
            {
                MessageBox.Show("يرجى اختيار منتج", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            ComboBoxItem selectedItem = (ComboBoxItem)comboProducts.SelectedItem;
            DataRow row = (DataRow)selectedItem.Value;
            string productName = row["productName"].ToString();
            int availableStock = Convert.ToInt32(row["productCount"]);

            int alreadyAdded = 0;
            foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
            {
                if (dgvRow.Cells[0].Value?.ToString() == productName)
                {
                    if (int.TryParse(dgvRow.Cells[1].Value?.ToString(), out int existingCartons))
                    {
                        alreadyAdded += existingCartons;
                    }
                }
            }

            if (cartons + alreadyAdded > availableStock)
            {
                MessageBox.Show($"الكمية غير كافية!\nالمتوفر: {availableStock} كرتون\nالمطلوب الآن: {cartons} كرتون",
                    "خطأ في المخزون", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal itemTotal = cartons * pairsPerCarton * unitPrice;

            dataGridView1.Rows.Add(
                productName,
                cartons.ToString("N0"),
                pairsPerCarton.ToString("N0"),
                unitPrice.ToString("N2"),
                itemTotal.ToString("N2")
            );

            UpdateTotal();
            ClearInputFields();
        }

        private void UpdateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    string totalValue = row.Cells["Total"].Value.ToString().Replace(",", "");
                    if (decimal.TryParse(totalValue, out decimal rowTotal))
                    {
                        total += rowTotal;
                    }
                }
            }

            txtTotal.Text = total.ToString("N2");
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار صف لحذفه", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("هل أنت متأكد من حذف العنصر المحدد؟", "تأكيد الحذف",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }

                UpdateTotal();
            }
        }

        private void TxtCartons_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCartons.Text, out int value) || value < 0)
                txtCartons.Text = "0";

            if (previousLanguage != null)
            {
                InputLanguage.CurrentInputLanguage = previousLanguage;
            }
        }

        private void TxtCouple_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(txtcouple.Text, out int value) || value < 0)
                txtcouple.Text = "0";
        }

        private void txtprice_Click(object sender, EventArgs e)
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

        private void TxtPrice_Leave(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtprice.Text, out decimal value) || value < 0)
                txtprice.Text = "0.00";

            if (previousLanguage != null)
            {
                InputLanguage.CurrentInputLanguage = previousLanguage;
            }
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
                txtCartons.Focus();
            }
        }

        private void ClearInputFields()
        {
            if (comboProducts.SelectedItem is ComboBoxItem item)
            {
                DataRow row = (DataRow)item.Value;
                txtcouple.Text = row["productCouple"].ToString();
            }
            txtCartons.Text = "1";
            txtprice.Text = "0.00";
        }

        private void TxtCartons_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtCouple_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void ClearBordersFromRow(ExcelWorksheet worksheet, int rowIndex, int startCol, int endCol)
        {
            for (int col = startCol; col <= endCol; col++)
            {
                var cell = worksheet.Cells[rowIndex, col];
                cell.Style.Borders.SetBorders(MultipleBorders.None, SpreadsheetColor.FromName(ColorName.Black), LineStyle.None);
            }
        }

        private void FillHeaders(ExcelWorksheet worksheet, string receiptId, int currentPage, int totalPages, DateTime receiptDate)
        {
            int row, column;

            if (worksheet.Cells.FindText("costumerName", true, true, out row, out column))
            {
                worksheet.Cells[row, column].Value = comboBoxCustomer.Text;
            }

            if (worksheet.Cells.FindText("Date", true, true, out row, out column))
            {
                worksheet.Cells[row, column].Value = receiptDate.ToString("yyyy/MM/dd");
            }

            if (worksheet.Cells.FindText("receiptID", true, true, out row, out column))
            {
                worksheet.Cells[row, column].Value = $"{receiptId}-{currentPage}";
            }

            if (worksheet.Cells.FindText("costumerName:", true, true, out row, out column))
            {
                worksheet.Cells[row, column].Value = comboBoxCustomer.Text;
            }
        }

        private void AddPageNumberingAtF37(ExcelWorksheet worksheet, int currentPage, int totalPages)
        {
            int pageNumberRow = 37;
            string pageText = $"صفحة {currentPage}-{totalPages}";
            worksheet.Cells[pageNumberRow, 4].Value = pageText;
        }

        private void FillSummaryRow(ExcelWorksheet worksheet, decimal totalCartons, decimal totalPairs, decimal totalAmount)
        {
            int summaryRow = 35;

            if (summaryRow >= worksheet.Rows.Count)
            {
                worksheet.Rows.InsertEmpty(summaryRow, 1);
            }

            worksheet.Cells[summaryRow, 1].Value = totalCartons.ToString("N0");
            worksheet.Cells[summaryRow, 3].Value = totalPairs.ToString("N0");
            worksheet.Cells[summaryRow, 5].Value = txtCurrency.Text;
            worksheet.Cells[summaryRow, 6].Value = totalAmount.ToString("N2");

            if (worksheet.Cells.FindText("boxcount", true, true, out int row, out int column))
            {
                if (column + 1 < worksheet.Columns.Count)
                    worksheet.Cells[row, column + 1].Value = totalCartons.ToString("N0");
            }

            if (worksheet.Cells.FindText("couples", true, true, out row, out column))
            {
                if (column + 1 < worksheet.Columns.Count)
                    worksheet.Cells[row, column + 1].Value = totalPairs.ToString("N0");
            }

            if (worksheet.Cells.FindText("Price", true, true, out row, out column))
            {
                if (column + 1 < worksheet.Columns.Count)
                    worksheet.Cells[row, column + 1].Value = totalAmount.ToString("N2");
            }

            if (worksheet.Cells.FindText("currency type", true, true, out row, out column))
            {
                if (column + 1 < worksheet.Columns.Count)
                    worksheet.Cells[row, column + 1].Value = txtCurrency.Text;
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
                            {
                                cellRange.Merged = false;
                            }
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
                {
                    worksheet.Rows.InsertEmpty(currentRow, 1);
                }

                string unitPrice = dataGridView1.Rows[i].Cells["UnitPrice"].Value?.ToString() ?? "0.00";
                string cartons = dataGridView1.Rows[i].Cells["Cartons"].Value?.ToString() ?? "0";
                string pairsPerCarton = dataGridView1.Rows[i].Cells["PairsPerCarton"].Value?.ToString() ?? "0";
                string productName = dataGridView1.Rows[i].Cells["ProductName"].Value?.ToString() ?? "";
                string total = dataGridView1.Rows[i].Cells["Total"].Value?.ToString() ?? "0.00";

                decimal decCartons = decimal.Parse(cartons.Replace(",", ""));
                decimal decPairsPerCarton = decimal.Parse(pairsPerCarton.Replace(",", ""));
                decimal decUnitPrice = decimal.Parse(unitPrice.Replace(",", ""));

                worksheet.Cells[currentRow, 1].Value = (decCartons * decPairsPerCarton * decUnitPrice).ToString("N2");
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

        private bool SaveReceiptToDatabase(string receiptId, string filePath, decimal totalCartons,
            decimal totalPairs, decimal totalAmount, DateTime receiptDate)
        {
            try
            {
                var receiptManager = new DHKMontainApp.Classes.ReceiptManager();

                int? customerId = null;  // use nullable int

                // Get the selected customer ID safely
                if (comboBoxCustomer.SelectedValue != null)
                {
                    // Convert to int (handles both int and string)
                    if (int.TryParse(comboBoxCustomer.SelectedValue.ToString(), out int id))
                    {
                        customerId = id;
                    }
                }

                receiptManager.SaveReceiptToDatabase(
                    receiptId: receiptId,
                    customerName: comboBoxCustomer.Text,
                    customerId: customerId,  // pass as int? (nullable)
                    totalCartons: totalCartons,
                    totalPairs: totalPairs,
                    totalAmount: totalAmount,
                    currency: txtCurrency.Text,
                    filePath: filePath,
                    dataGridView: dataGridView1,
                    receiptDate: receiptDate
                );

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database save error: {ex.Message}");
                return false;
            }
        }
        private void btnCheckStock_Click(object sender, EventArgs e)
        {

        }

        private void ShowStockInfo(DataTable stockInfo)
        {
            string stockMessage = "معلومات المخزون:\n\n";

            foreach (DataRow row in stockInfo.Rows)
            {
                stockMessage += $"{row["productName"]}: {row["الكمية المتوفرة"]} كرتون\n";
            }

            MessageBox.Show(stockMessage, "المخزون المتوفر",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearFormAfterSave()
        {
            try
            {
                dataGridView1.Rows.Clear();
                txtTotal.Text = "0.00";
                txtCartons.Text = "1";
                txtcouple.Text = "0";
                txtprice.Text = "0.00";
                comboProducts.SelectedIndex = -1;
                comboProducts.Text = "";
                LoadProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing form: {ex.Message}");
            }
        }

        private void btnSavePrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comboBoxCustomer.Text))
                {
                    MessageBox.Show("يرجى اختيار الزبون", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات في الجدول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var receiptManager = new DHKMontainApp.Classes.ReceiptManager();
                bool stockAvailable = receiptManager.CheckStockAvailability(dataGridView1);

                if (!stockAvailable)
                {
                    return;
                }

                DialogResult printConfirmation = MessageBox.Show(
                    "هل تريد طباعة الإيصال بعد الحفظ؟",
                    "طباعة الإيصال",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                bool shouldPrint = (printConfirmation == DialogResult.Yes);

                DateTime receiptDate = datetime_Checkbox.Checked ? DateTime.Now : dateTimePicker_Payment.Value;

                string receiptId;
                if (datetime_Checkbox.Checked)
                {
                    receiptId = DateTime.Now.ToString("yyyyMMddHHmmss");
                }
                else
                {
                    receiptId = dateTimePicker_Payment.Value.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss");
                }

                decimal totalCartons = 0;
                decimal totalPairs = 0;
                decimal totalAmount = 0;

                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (dgvRow.Cells["Cartons"].Value != null &&
                        dgvRow.Cells["PairsPerCarton"].Value != null &&
                        dgvRow.Cells["Total"].Value != null)
                    {
                        decimal cartons = 0, pairsPerCarton = 0, rowTotal = 0;

                        if (decimal.TryParse(dgvRow.Cells["Cartons"].Value.ToString().Replace(",", ""), out cartons))
                            totalCartons += cartons;

                        if (decimal.TryParse(dgvRow.Cells["PairsPerCarton"].Value.ToString().Replace(",", ""), out pairsPerCarton))
                            totalPairs += cartons * pairsPerCarton;

                        if (decimal.TryParse(dgvRow.Cells["Total"].Value.ToString().Replace(",", ""), out rowTotal))
                            totalAmount += rowTotal;
                    }
                }

                int itemsPerPage = 25;
                int totalItems = dataGridView1.Rows.Count;
                int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                string filePath = "";
                string resultMessage = "";

                if (totalPages == 1)
                {
                    filePath = CreateSinglePageReceipt(receiptId, totalCartons, totalPairs, totalAmount, shouldPrint, receiptDate);
                    resultMessage = $"تم حفظ الفاتورة في:\n{Path.GetFileName(filePath)}";

                    if (shouldPrint)
                        resultMessage += "\n\nتم طباعة الإيصال بنجاح";
                }
                else
                {
                    filePath = CreateMultiPageReceipt(receiptId, totalCartons, totalPairs, totalAmount, totalPages, itemsPerPage, shouldPrint, receiptDate);
                    resultMessage = $"تم حفظ {totalPages} صفحات للفاتورة\nرقم الفاتورة: {receiptId}";

                    if (shouldPrint)
                        resultMessage += "\n\nتم طباعة جميع الصفحات بنجاح";
                }

                bool dbSaved = SaveReceiptToDatabase(receiptId, filePath, totalCartons, totalPairs, totalAmount, receiptDate);

                MessageBox.Show(resultMessage, "تمت العملية بنجاح",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFormAfterSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في إنشاء الفاتورة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CreateSinglePageReceipt(string receiptId, decimal totalCartons, decimal totalPairs, decimal totalAmount, bool shouldPrint, DateTime receiptDate)
        {
            try
            {
                if (!File.Exists(excelTemplatePath))
                {
                    throw new FileNotFoundException($"ملف القالب غير موجود: {excelTemplatePath}");
                }

                var workbook = ExcelFile.Load(excelTemplatePath);
                var worksheet = workbook.Worksheets[0];

                FillHeaders(worksheet, receiptId, 1, 1, receiptDate);

                int startRow = 10;
                int lastRow = 34;

                ClearPageItems(worksheet, startRow, lastRow);

                int itemsToFill = Math.Min(dataGridView1.Rows.Count, 25);
                FillPageItems(worksheet, startRow, 0, itemsToFill);

                FillSummaryRow(worksheet, totalCartons, totalPairs, totalAmount);
                AddPageNumberingAtF37(worksheet, 1, 1);

                string fileName = $"Receipt_{receiptId}_{DateTime.Now:HHmmss}.xlsx";
                string outputFile = Path.Combine(dhkReceiptFolder, fileName);

                workbook.Save(outputFile);

                if (shouldPrint)
                {
                    var printOptions = worksheet.PrintOptions;
                    printOptions.NumberOfCopies = 1;
                    printOptions.PrintHeadings = false;
                    printOptions.PrintGridlines = false;

                    workbook.Print();
                }

                return outputFile;
            }
            catch (Exception ex)
            {
                string errorMessage = $"خطأ في إنشاء ملف الإكسل:\n\n{ex.Message}";

                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nالتفاصيل:\n{ex.InnerException.Message}";
                }

                MessageBox.Show(errorMessage, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception($"خطأ في إنشاء ملف الإكسل: {ex.Message}", ex);
            }
        }

        private string CreateMultiPageReceipt(string receiptId, decimal totalCartons, decimal totalPairs, decimal totalAmount,
            int totalPages, int itemsPerPage, bool shouldPrint, DateTime receiptDate)
        {
            try
            {
                if (!File.Exists(excelTemplatePath))
                {
                    throw new FileNotFoundException($"ملف القالب غير موجود: {excelTemplatePath}");
                }

                int startRow = 10;
                int lastRow = 34;

                string firstPagePath = "";

                for (int page = 0; page < totalPages; page++)
                {
                    var workbook = ExcelFile.Load(excelTemplatePath);
                    var worksheet = workbook.Worksheets[0];

                    FillHeaders(worksheet, receiptId, page + 1, totalPages, receiptDate);

                    int startIndex = page * itemsPerPage;
                    int endIndex = Math.Min(startIndex + itemsPerPage, dataGridView1.Rows.Count);

                    ClearPageItems(worksheet, startRow, lastRow);
                    FillPageItems(worksheet, startRow, startIndex, endIndex);

                    FillSummaryRow(worksheet, totalCartons, totalPairs, totalAmount);
                    AddPageNumberingAtF37(worksheet, page + 1, totalPages);

                    string fileName = $"Receipt_{receiptId}_Page_{page + 1}_of_{totalPages}.xlsx";
                    string outputFile = Path.Combine(dhkReceiptFolder, fileName);

                    Directory.CreateDirectory(dhkReceiptFolder);
                    workbook.Save(outputFile);

                    if (shouldPrint)
                    {
                        var printOptions = worksheet.PrintOptions;
                        printOptions.NumberOfCopies = 1;
                        workbook.Print();
                    }

                    if (page == 0)
                        firstPagePath = outputFile;
                }

                return firstPagePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في إنشاء ملفات الإكسل المتعددة: {ex.Message}");
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadProducts();
            dateTimePicker_Payment.Value = DateTime.Now;
        }

        private void txtCartons_Click(object sender, EventArgs e)
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
    }
}