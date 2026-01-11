using GemBox.Spreadsheet;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace DHKMontainApp.userControls
{
    public partial class payment : UserControl
    {
        //private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private string dhkReceiptFolder;
        private string excelTemplatePath;
        InputLanguage previousLanguage;

        public payment()
        {
            // SET THE FREE LICENSE KEY HERE IN CONSTRUCTOR - VERY IMPORTANT!
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            InitializeComponent();

            // Initialize folder paths

            //dhkReceiptFolder = Path.Combine(desktopPath, "DHKReceipt");
            dhkReceiptFolder = Properties.Settings.Default.SaveFolderPath;
            //excelTemplatePath = Path.Combine(dhkReceiptFolder, "DONT-CHANGE-THIS.xlsx");
            excelTemplatePath = Path.Combine(Application.StartupPath, "DONT-CHANGE-THIS.xlsx");


            // Ensure folder exists
            EnsureFolderExists();

            LoadCustomers();
            LoadProducts();
            SetupDataGridView();

            buttonR.MakeButtonRounded(btn_refresh, 20);


            //combo box search type
            comboProducts.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboProducts.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBoxCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void EnsureFolderExists()
        {
            try
            {
                // Create DHKReceipt folder if it doesn't exist
                if (!Directory.Exists(dhkReceiptFolder))
                {
                    MessageBox.Show($"المجلد غير موجود :\n{dhkReceiptFolder}",
                        "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Check if template exists
                if (!File.Exists(excelTemplatePath))
                {
                    MessageBox.Show($"ملف القالب غير موجود في المجلد:\nexcelTemplatePath\n",
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
            // Clear existing columns
            dataGridView1.Columns.Clear();

            // Add columns
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "ProductName";
            col1.HeaderText = "اسم المنتج";
            dataGridView1.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "Cartons";
            col2.HeaderText = "عدد الكراتين";
            dataGridView1.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "PairsPerCarton";
            col3.HeaderText = "الزوج في الكرتون";
            dataGridView1.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "UnitPrice";
            col4.HeaderText = "سعر الزوج";
            dataGridView1.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "Total";
            col5.HeaderText = "المجموع";
            dataGridView1.Columns.Add(col5);

            // Apply styling
            StyleDataGrid(dataGridView1);

            // Set specific column alignments AFTER styling
            SetColumnAlignments();
        }

        private void SetColumnAlignments()
        {
            // Set CENTER alignment for all columns
            if (dataGridView1.Columns.Contains("ProductName"))
            {
                dataGridView1.Columns["ProductName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dataGridView1.Columns.Contains("Cartons"))
            {
                dataGridView1.Columns["Cartons"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Cartons"].DefaultCellStyle.Format = "N0"; // No decimals for count
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

            // Set header alignment to CENTER
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // Your styling method - Updated for center alignment
        public static void StyleDataGrid(DataGridView dgv)
        {
            dgv.ReadOnly = true;               // cannot edit cells
            dgv.AllowUserToAddRows = false;    // no extra empty row
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
            dgv.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2); // Top, Left, Bottom, Right
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 11);

            // Set RTL for Arabic text but keep CENTER alignment
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
                var da = new SqlDataAdapter("SELECT id, cName FROM customer", Database.con);
                var dt = new DataTable();
                da.Fill(dt);

                // Bind normally
                comboBoxCustomer.DisplayMember = "cName";
                comboBoxCustomer.ValueMember = "id";
                comboBoxCustomer.DataSource = dt;

                // --- AutoComplete Support ---
                AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                foreach (DataRow row in dt.Rows)
                    auto.Add(row["cName"].ToString());

                comboBoxCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBoxCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBoxCustomer.AutoCompleteCustomSource = auto;

                // ------ FIX AUTO SELECT ------
                comboBoxCustomer.SelectedIndex = -1;     // stops auto-filling first item
                comboBoxCustomer.Text = "";              // clear text box
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
                var da = new SqlDataAdapter("SELECT id, productName, productCount, productCouple FROM Product", Database.con);
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

                // Set autocomplete source
                comboProducts.AutoCompleteCustomSource = autoComplete;
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
            // Validate input
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

            // Get product data
            ComboBoxItem selectedItem = (ComboBoxItem)comboProducts.SelectedItem;
            DataRow row = (DataRow)selectedItem.Value;
            string productName = row["productName"].ToString();
            int availableStock = Convert.ToInt32(row["productCount"]);

            // Calculate how many cartons are already added to the DataGridView for this product
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

            // Check stock availability
            if (cartons + alreadyAdded > availableStock)
            {
                MessageBox.Show($"الكمية غير كافية!\nالمتوفر: {availableStock} كرتون\nالمطلوب الآن: {cartons} كرتون",
                    "خطأ في المخزون", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate total for this item
            decimal itemTotal = cartons * pairsPerCarton * unitPrice;

            // Add to DataGridView
            dataGridView1.Rows.Add(
                productName,
                cartons.ToString("N0"),
                pairsPerCarton.ToString("N0"),
                unitPrice.ToString("N2"),
                itemTotal.ToString("N2")
            );

            // Update overall total
            UpdateTotal();

            // Clear input fields
            ClearInputFields();
        }

        private void UpdateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    // Remove any formatting characters like commas
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

            // Confirm deletion
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

        // simple ComboBox item wrapper
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
                txtCartons.Text = "1"; // Default to 1 carton
                txtprice.Text = "0.00";
                txtCartons.Focus();
            }
        }

        // Clear input fields after adding
        private void ClearInputFields()
        {
            // Reset to default values
            if (comboProducts.SelectedItem is ComboBoxItem item)
            {
                DataRow row = (DataRow)item.Value;
                txtcouple.Text = row["productCouple"].ToString();
            }
            txtCartons.Text = "1";
            txtprice.Text = "0.00";
        }

        // Add these event handlers for better user experience
        private void TxtCartons_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers and backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtCouple_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers and backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow numbers, decimal point, and backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        // Helper method to clear borders from a row
        private void ClearBordersFromRow(ExcelWorksheet worksheet, int rowIndex, int startCol, int endCol)
        {
            // Clear borders by setting them to None
            for (int col = startCol; col <= endCol; col++)
            {
                var cell = worksheet.Cells[rowIndex, col];
                // Clear borders by setting to None (no borders)
                cell.Style.Borders.SetBorders(MultipleBorders.None, SpreadsheetColor.FromName(ColorName.Black), LineStyle.None);
            }
        }


        private void FillHeaders(ExcelWorksheet worksheet, string receiptId, int currentPage, int totalPages)
        {
            int row, column;

            // Fill customer name
            if (worksheet.Cells.FindText("costumerName", true, true, out row, out column))
            {
                worksheet.Cells[row, column].Value = comboBoxCustomer.Text;
            }

            // Fill date
            if (worksheet.Cells.FindText("Date", true, true, out row, out column))
            {
                worksheet.Cells[row, column].Value = DateTime.Now.ToString("dd/MM/yyyy");
            }

            // Fill receipt ID
            if (worksheet.Cells.FindText("receiptID", true, true, out row, out column))
            {
                worksheet.Cells[row, column].Value = $"{receiptId}-{currentPage}";
            }

            // Fill customer name next to "الســـيد:"
            if (worksheet.Cells.FindText("costumerName:", true, true, out row, out column))
            {
                    worksheet.Cells[row, column].Value = comboBoxCustomer.Text;
            }
        }

        private void AddPageNumberingAtF37(ExcelWorksheet worksheet, int currentPage, int totalPages)
        {
            int pageNumberRow = 37; // F37 (0-based: row 36, column 5)

            string pageText = $"صفحة {currentPage}-{totalPages}";
            worksheet.Cells[pageNumberRow, 4].Value = pageText;
        }

        private void FillSummaryRow(ExcelWorksheet worksheet, decimal totalCartons, decimal totalPairs, decimal totalAmount)
        {
            int summaryRow = 35; // Row 36

            if (summaryRow >= worksheet.Rows.Count)
            {
                worksheet.Rows.InsertEmpty(summaryRow, 1);
            }

            // Show totals on ALL pages
            worksheet.Cells[summaryRow, 1].Value = totalCartons.ToString("N0");  // B36
            worksheet.Cells[summaryRow, 3].Value = totalPairs.ToString("N0");    // D36
            worksheet.Cells[summaryRow, 5].Value = txtCurrency.Text;             // F36
            worksheet.Cells[summaryRow, 6].Value = totalAmount.ToString("N2");   // G36

            // Also fill the template summary section at the very bottom
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
            // Clear rows from startRow to endRow
            for (int i = startRow; i <= endRow; i++)
            {
                for (int j = 0; j < 8; j++) // Columns A to H
                {
                    if (i < worksheet.Rows.Count)
                    {
                        worksheet.Cells[i, j].Value = "";
                        ClearBordersFromRow(worksheet, i, 0, 7);

                        // Unmerge cells in columns F,G,H
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

                // Get values
                string unitPrice = dataGridView1.Rows[i].Cells["UnitPrice"].Value?.ToString() ?? "0.00";
                string cartons = dataGridView1.Rows[i].Cells["Cartons"].Value?.ToString() ?? "0";
                string pairsPerCarton = dataGridView1.Rows[i].Cells["PairsPerCarton"].Value?.ToString() ?? "0";
                string productName = dataGridView1.Rows[i].Cells["ProductName"].Value?.ToString() ?? "";
                string total = dataGridView1.Rows[i].Cells["Total"].Value?.ToString() ?? "0.00";

                // Parse for calculations
                decimal decCartons = decimal.Parse(cartons.Replace(",", ""));
                decimal decPairsPerCarton = decimal.Parse(pairsPerCarton.Replace(",", ""));
                decimal decUnitPrice = decimal.Parse(unitPrice.Replace(",", ""));

                // Column B: Total for item
                worksheet.Cells[currentRow, 1].Value = (decCartons * decPairsPerCarton * decUnitPrice).ToString("N2");

                // Column C: Cartons
                worksheet.Cells[currentRow, 2].Value = cartons;

                // Column D: Price per pair
                worksheet.Cells[currentRow, 3].Value = unitPrice;

                // Column E: Pairs per carton
                worksheet.Cells[currentRow, 4].Value = pairsPerCarton;

                // Columns F,G,H: Product name (merged)
                worksheet.Cells[currentRow, 5].Value = productName;
                var mergeRange = worksheet.Cells.GetSubrangeAbsolute(currentRow, 5, currentRow, 7);
                mergeRange.Merged = true;
                worksheet.Cells[currentRow, 5].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

                // Apply borders from B to H
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

        // Database save function - returns true if successful
        private bool SaveReceiptToDatabase(string receiptId, string filePath, decimal totalCartons,
            decimal totalPairs, decimal totalAmount)
        {
            try
            {
                // Create ReceiptManager instance
                var receiptManager = new DHKMontainApp.Classes.ReceiptManager();

                // Get customer ID
                object customerId = null;
                if (comboBoxCustomer.SelectedValue != null)
                {
                    customerId = comboBoxCustomer.SelectedValue;
                }

                // Save to database
                receiptManager.SaveReceiptToDatabase(
                    receiptId: receiptId,
                    customerName: comboBoxCustomer.Text,
                    customerId: customerId,
                    totalCartons: totalCartons,
                    totalPairs: totalPairs,
                    totalAmount: totalAmount,
                    currency: txtCurrency.Text,
                    filePath: filePath,
                    dataGridView: dataGridView1
                );

                return true;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Database save error: {ex.Message}");
                return false;
            }
        }

        private void btnCheckStock_Click(object sender, EventArgs e)
        {
            try
            {
                var receiptManager = new DHKMontainApp.Classes.ReceiptManager();
                DataTable stockInfo = receiptManager.GetProductStockInfo();

                // Create and show stock form (you can create a simple form to display this)
                ShowStockInfo(stockInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في جلب معلومات المخزون: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowStockInfo(DataTable stockInfo)
        {
            // Simple way to show stock in MessageBox
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
                // Clear DataGridView
                dataGridView1.Rows.Clear();

                // Reset totals
                txtTotal.Text = "0.00";

                // Clear input fields
                txtCartons.Text = "1";
                txtcouple.Text = "0";
                txtprice.Text = "0.00";

                // Reset product selection
                comboProducts.SelectedIndex = -1;
                comboProducts.Text = "";

                // Reload products to update stock counts in dropdown
                LoadProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing form: {ex.Message}");
            }
        }





        // masalet save u printe 

        private void btnSavePrint_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate customer selection
                if (string.IsNullOrWhiteSpace(comboBoxCustomer.Text))
                {
                    MessageBox.Show("يرجى اختيار الزبون", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if dataGridView1 has any data or rows
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات في الجدول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check stock availability before proceeding
                var receiptManager = new DHKMontainApp.Classes.ReceiptManager();
                bool stockAvailable = receiptManager.CheckStockAvailability(dataGridView1);

                // If method returns true when stock is OK, false when there's a problem
                if (!stockAvailable)
                {
                    // The stock check method should already show its own error message
                    return;
                }

                // Ask user if they want to print BEFORE processing
                DialogResult printConfirmation = MessageBox.Show(
                    "هل تريد طباعة الإيصال بعد الحفظ؟",
                    "طباعة الإيصال",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                bool shouldPrint = (printConfirmation == DialogResult.Yes);

                // Calculate totals
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

                // Generate receipt ID
                string receiptId = DateTime.Now.ToString("yyyyMMddHHmmss");

                // Items per page: from row 11 to row 35 = 25 rows
                int itemsPerPage = 25;
                int totalItems = dataGridView1.Rows.Count;
                int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                // File path variable
                string filePath = "";
                string resultMessage = "";

                if (totalPages == 1)
                {
                    // Single page - pass the print preference
                    filePath = CreateSinglePageReceipt(receiptId, totalCartons, totalPairs, totalAmount, shouldPrint);
                    resultMessage = $"تم حفظ الفاتورة في:\n{System.IO.Path.GetFileName(filePath)}";

                    if (shouldPrint)
                        resultMessage += "\n\nتم طباعة الإيصال بنجاح";
                }
                else
                {
                    // Multiple pages - pass the print preference
                    filePath = CreateMultiPageReceipt(receiptId, totalCartons, totalPairs, totalAmount, totalPages, itemsPerPage, shouldPrint);
                    resultMessage = $"تم حفظ {totalPages} صفحات للفاتورة\nرقم الفاتورة: {receiptId}";

                    if (shouldPrint)
                        resultMessage += "\n\nتم طباعة جميع الصفحات بنجاح";
                }

                // Save to database (includes inventory update)
                bool dbSaved = SaveReceiptToDatabase(receiptId, filePath, totalCartons, totalPairs, totalAmount);

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
        private string CreateSinglePageReceipt(string receiptId, decimal totalCartons, decimal totalPairs, decimal totalAmount, bool shouldPrint)
        {
            try
            {
                // Check if template exists
                if (!File.Exists(excelTemplatePath))
                {
                    throw new FileNotFoundException($"ملف القالب غير موجود: {excelTemplatePath}");
                }

                // Load workbook
                var workbook = ExcelFile.Load(excelTemplatePath);
                var worksheet = workbook.Worksheets[0];

                // Fill header information
                FillHeaders(worksheet, receiptId, 1, 1);

                // Clear and fill items
                int startRow = 10; // Row 11
                int lastRow = 34;  // Row 35

                ClearPageItems(worksheet, startRow, lastRow);

                // Determine how many items to fill (max 25 per page)
                int itemsToFill = Math.Min(dataGridView1.Rows.Count, 25);
                FillPageItems(worksheet, startRow, 0, itemsToFill);

                // Fill summary at row 36
                FillSummaryRow(worksheet, totalCartons, totalPairs, totalAmount);

                // Add page numbering
                AddPageNumberingAtF37(worksheet, 1, 1);

                // Save the file
                string fileName = $"Receipt_{receiptId}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                string outputFile = Path.Combine(dhkReceiptFolder, fileName);

                workbook.Save(outputFile);

                // Print only if user selected Yes
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
            int totalPages, int itemsPerPage, bool shouldPrint)
        {
            try
            {
                // Check if template exists
                if (!File.Exists(excelTemplatePath))
                {
                    throw new FileNotFoundException($"ملف القالب غير موجود: {excelTemplatePath}");
                }

                int startRow = 10; // Row 11
                int lastRow = 34;  // Row 35

                // For multi-page, we'll save the first page and return its path
                string firstPagePath = "";

                for (int page = 0; page < totalPages; page++)
                {
                    var workbook = ExcelFile.Load(excelTemplatePath);
                    var worksheet = workbook.Worksheets[0];

                    // Fill headers with page number
                    FillHeaders(worksheet, receiptId, page + 1, totalPages);

                    // Calculate start and end indices for this page
                    int startIndex = page * itemsPerPage;
                    int endIndex = Math.Min(startIndex + itemsPerPage, dataGridView1.Rows.Count);

                    // Clear and fill items for this page
                    ClearPageItems(worksheet, startRow, lastRow);
                    FillPageItems(worksheet, startRow, startIndex, endIndex);

                    // Fill summary row (row 36) - show totals on ALL pages
                    FillSummaryRow(worksheet, totalCartons, totalPairs, totalAmount);

                    // Add page numbering at F37: "صفحة 1-2" etc.
                    AddPageNumberingAtF37(worksheet, page + 1, totalPages);

                    // Save this page to DHKReceipt folder
                    string fileName = $"Receipt_{receiptId}_Page_{page + 1}_of_{totalPages}.xlsx";
                    string outputFile = Path.Combine(dhkReceiptFolder, fileName);

                    // Ensure directory exists
                    Directory.CreateDirectory(dhkReceiptFolder);
                    workbook.Save(outputFile);

                    // Print only if user selected Yes
                    if (shouldPrint)
                    {
                        var printOptions = worksheet.PrintOptions;
                        printOptions.NumberOfCopies = 1;
                        workbook.Print();
                    }

                    // Save first page path for database
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
        }
    }
}