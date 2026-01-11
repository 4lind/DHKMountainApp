using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DHKMontainApp.Classes
{
    public class ReceiptManager
    {
        public void SaveReceiptToDatabase(string receiptId, string customerName, object customerId,
            decimal totalCartons, decimal totalPairs, decimal totalAmount, string currency,
            string filePath, DataGridView dataGridView)
        {
            try
            {
                Database.Open();

                // Start transaction for data consistency
                SqlTransaction transaction = Database.con.BeginTransaction();

                try
                {
                    // 1. Save receipt header
                    string insertReceiptQuery = @"
                        INSERT INTO Receipts 
                        (ReceiptID, CustomerName, CustomerID, ReceiptDate, TotalCartons, TotalPairs, TotalAmount, Currency, FilePath) 
                        VALUES 
                        (@ReceiptID, @CustomerName, @CustomerID, @ReceiptDate, @TotalCartons, @TotalPairs, @TotalAmount, @Currency, @FilePath)";

                    using (SqlCommand cmd = new SqlCommand(insertReceiptQuery, Database.con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ReceiptID", receiptId);
                        cmd.Parameters.AddWithValue("@CustomerName", customerName);

                        if (customerId != null && customerId is int && (int)customerId > 0)
                            cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        else
                            cmd.Parameters.AddWithValue("@CustomerID", DBNull.Value);

                        cmd.Parameters.AddWithValue("@ReceiptDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TotalCartons", totalCartons);
                        cmd.Parameters.AddWithValue("@TotalPairs", totalPairs);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@Currency", currency);
                        cmd.Parameters.AddWithValue("@FilePath", filePath);

                        cmd.ExecuteNonQuery();
                    }

                    // 2. Save receipt items and update product inventory
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

                            // 2a. Save receipt item
                            using (SqlCommand cmd = new SqlCommand(insertItemQuery, Database.con, transaction))
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

                            // 2b. Update product inventory (deduct sold cartons)
                            UpdateProductInventory(productName, cartonsSold, transaction);

                            rowNumber++;
                        }
                    }

                    // Commit transaction if everything succeeded
                    transaction.Commit();

                    MessageBox.Show("تم حفظ الفاتورة وتحديث المخزون بنجاح", "تم الحفظ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Rollback transaction if any error occurs
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

        // Method to update product inventory (deduct sold cartons)
        private void UpdateProductInventory(string productName, int cartonsSold, SqlTransaction transaction)
        {
            // First, check if product exists and has enough stock
            string checkStockQuery = @"
                SELECT productCount, productName 
                FROM Product 
                WHERE productName = @ProductName";

            using (SqlCommand checkCmd = new SqlCommand(checkStockQuery, Database.con, transaction))
            {
                checkCmd.Parameters.AddWithValue("@ProductName", productName);

                using (SqlDataReader reader = checkCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int currentStock = reader.GetInt32(reader.GetOrdinal("productCount"));

                        // Close reader before next query
                        reader.Close();

                        if (currentStock >= cartonsSold)
                        {
                            // Update product stock
                            string updateQuery = @"
                                UPDATE Product 
                                SET productCount = productCount - @CartonsSold 
                                WHERE productName = @ProductName";

                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, Database.con, transaction))
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
                        reader.Close();
                        throw new Exception($"المنتج غير موجود: {productName}");
                    }
                }
            }
        }

        // Method to check stock before selling (optional validation)
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

                        using (SqlCommand cmd = new SqlCommand(checkQuery, Database.con))
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

        // Get product stock information
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

                using (SqlCommand cmd = new SqlCommand(query, Database.con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
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

        // Helper method to calculate totals from DataGridView
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