using System.Data.SQLite;
using System;
using System.Data;
using System.Windows.Forms;

namespace DHKMontainApp.Classes
{
    public class ReceiptManager
    {
        public void SaveReceiptToDatabase(string receiptId, string customerName, object customerId,
            decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency,
            string filePath, DataGridView dataGridView, DateTime receiptDate)
        {

            try
            {
                Database.Open();
                SQLiteTransaction transaction = Database.con.BeginTransaction();

                try
                {


                    string insertReceiptQuery = @"
                        INSERT INTO Receipts 
                        (ReceiptID, CustomerName, CustomerID, ReceiptDate, TotalCartons, TotalPairs, TotalAmount, Currency, FilePath) 
                        VALUES 
                        (@ReceiptID, @CustomerName, @CustomerID, @ReceiptDate, @TotalCartons, @TotalPairs, @TotalAmount, @Currency, @FilePath)";

                    using (var cmd = new SQLiteCommand(insertReceiptQuery, Database.con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                        cmd.Parameters.AddWithValue("@CustomerName", customerName);

                        if (customerId != null && customerId is int && (int)customerId > 0)
                            cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        else
                            cmd.Parameters.AddWithValue("@CustomerID", DBNull.Value);

                        cmd.Parameters.AddWithValue("@ReceiptDate", receiptDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@TotalCartons", totalCartons);
                        cmd.Parameters.AddWithValue("@TotalPairs", totalPairs);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@Currency", currency);
                        cmd.Parameters.AddWithValue("@FilePath", filePath);

                        cmd.ExecuteNonQuery();
                    }

                    string insertItemQuery = @"
                        INSERT INTO ReceiptItems 
                        (ReceiptID, ProductName, Cartons, PairsPerCarton, UnitPrice, ItemTotal, RowNumber) 
                        VALUES 
                        (@ReceiptID, @ProductName, @Cartons, @PairsPerCarton, @UnitPrice, @ItemTotal, @RowNumber)";

                    int rowNumber = 1;
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.Cells["ProductName"].Value != null &&
                            row.Cells["Cartons"].Value != null &&
                            row.Cells["PairsPerCarton"].Value != null &&
                            row.Cells["UnitPrice"].Value != null &&
                            row.Cells["Total"].Value != null)
                        {
                            string productName = row.Cells["ProductName"].Value.ToString();
                            int cartonsSold = Convert.ToInt32(row.Cells["Cartons"].Value.ToString().Replace(",", ""));
                            int pairsPerCarton = Convert.ToInt32(row.Cells["PairsPerCarton"].Value.ToString().Replace(",", ""));
                            decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value.ToString().Replace(",", ""));
                            decimal itemTotal = Convert.ToDecimal(row.Cells["Total"].Value.ToString().Replace(",", ""));

                            using (var cmd = new SQLiteCommand(insertItemQuery, Database.con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                                cmd.Parameters.AddWithValue("@ProductName", productName);
                                cmd.Parameters.AddWithValue("@Cartons", cartonsSold);
                                cmd.Parameters.AddWithValue("@PairsPerCarton", pairsPerCarton);
                                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                                cmd.Parameters.AddWithValue("@ItemTotal", itemTotal);
                                cmd.Parameters.AddWithValue("@RowNumber", rowNumber);

                                cmd.ExecuteNonQuery();
                            }

                            UpdateProductInventory(productName, cartonsSold, transaction);
                            rowNumber++;
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show("تم حفظ الفاتورة وتحديث المخزون بنجاح", "تم الحفظ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"فشل في حفظ الفاتورة: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في حفظ الفاتورة في قاعدة البيانات: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.Close();
            }

        }

        private void UpdateProductInventory(string productName, int cartonsSold, SQLiteTransaction transaction)
        {
            string checkStockQuery = @"
                SELECT productCount 
                FROM Product 
                WHERE productName = @ProductName";

            using (var checkCmd = new SQLiteCommand(checkStockQuery, Database.con, transaction))
            {
                checkCmd.Parameters.AddWithValue("@ProductName", productName);
                var result = checkCmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    int currentStock = Convert.ToInt32(result);

                    if (currentStock >= cartonsSold)
                    {
                        string updateQuery = @"
                            UPDATE Product 
                            SET productCount = productCount - @CartonsSold 
                            WHERE productName = @ProductName";

                        using (var updateCmd = new SQLiteCommand(updateQuery, Database.con, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@ProductName", productName);
                            updateCmd.Parameters.AddWithValue("@CartonsSold", cartonsSold);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        throw new Exception($"الكمية غير كافية للمنتج: {productName}\nالمتوفر: {currentStock} كرتون\nالمطلوب: {cartonsSold} كرتون");
                    }
                }
                else
                {
                    throw new Exception($"المنتج غير موجود: {productName}");
                }
            }
        }

        public bool CheckStockAvailability(DataGridView dataGridView)
        {
            try
            {
                Database.Open();

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["ProductName"].Value != null &&
                        row.Cells["Cartons"].Value != null)
                    {
                        string productName = row.Cells["ProductName"].Value.ToString();
                        int cartonsRequested = Convert.ToInt32(row.Cells["Cartons"].Value.ToString().Replace(",", ""));

                        string checkQuery = @"
                            SELECT productCount 
                            FROM Product 
                            WHERE productName = @ProductName";

                        using (var cmd = new SQLiteCommand(checkQuery, Database.con))
                        {
                            cmd.Parameters.AddWithValue("@ProductName", productName);
                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                int currentStock = Convert.ToInt32(result);
                                if (currentStock < cartonsRequested)
                                {
                                    MessageBox.Show($"الكمية غير كافية للمنتج: {productName}\nالمتوفر: {currentStock} كرتون\nالمطلوب: {cartonsRequested} كرتون",
                                        "خطأ في المخزون", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"المنتج غير موجود: {productName}",
                                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في التحقق من المخزون: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Database.Close();
            }
        }

        public DataTable GetProductStockInfo()
        {
            DataTable dt = new DataTable();
            try
            {
                Database.Open();
                string query = @"
                    SELECT 
                        id,
                        productName,
                        productCount as [الكمية المتوفرة],
                        producttype as [النوع],
                        productCouple as [الزوج في الكرتون]
                    FROM Product 
                    ORDER BY productName";

                using (var cmd = new SQLiteCommand(query, Database.con))
                using (var da = new System.Data.SQLite.SQLiteDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في جلب معلومات المخزون: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.Close();
            }
            return dt;
        }

        public (decimal totalCartons, decimal totalPairs, decimal totalAmount) CalculateTotals(DataGridView dataGridView)
        {
            decimal totalCartons = 0;
            decimal totalPairs = 0;
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["Cartons"].Value != null &&
                    row.Cells["PairsPerCarton"].Value != null &&
                    row.Cells["Total"].Value != null)
                {
                    decimal cartons = 0, pairsPerCarton = 0, rowTotal = 0;

                    if (decimal.TryParse(row.Cells["Cartons"].Value.ToString().Replace(",", ""), out cartons))
                        totalCartons += cartons;

                    if (decimal.TryParse(row.Cells["PairsPerCarton"].Value.ToString().Replace(",", ""), out pairsPerCarton))
                        totalPairs += cartons * pairsPerCarton;

                    if (decimal.TryParse(row.Cells["Total"].Value.ToString().Replace(",", ""), out rowTotal))
                        totalAmount += rowTotal;
                }
            }
            return (totalCartons, totalPairs, totalAmount);
        }
    }
}