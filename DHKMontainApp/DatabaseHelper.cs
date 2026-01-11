using System;
using System.Data.SqlClient;
using System.Windows.Forms;

public static class DatabaseHelper
{
    public static bool IsConnected()
    {
        try
        {
            Database.Open();
            using (var cmd = new SqlCommand("SELECT 1", Database.con))
            {
                cmd.ExecuteScalar();
            }
            return true;
        }
        catch (SqlException ex)
        {
            HandleConnectionError(ex);
            return false;
        }
        finally
        {
            Database.Close();
        }
    }

    public static SqlDataReader ExecuteReaderWithRetry(string query, SqlParameter[] parameters = null, int maxRetries = 2)
    {
        int attempt = 0;
        while (attempt <= maxRetries)
        {
            try
            {
                Database.Open();
                using (var cmd = new SqlCommand(query, Database.con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
            }
            catch (SqlException ex)
            {
                attempt++;
                if (attempt > maxRetries)
                {
                    HandleConnectionError(ex);
                    throw;
                }
                System.Threading.Thread.Sleep(1000 * attempt);
            }
        }
        return null;
    }

    private static void HandleConnectionError(SqlException ex)
    {
        string errorMessage = "Database connection error. ";

        switch (ex.Number)
        {
            case -1: // Connection Error
                errorMessage += "Cannot connect to server.";
                break;
            case 2: // Timeout
                errorMessage += "Connection timeout. Server may be busy.";
                break;
            case 53: // Network Error
                errorMessage += "Network error. Please check your connection.";
                break;
            case 4060: // Database Not Found
                errorMessage += "Database not found.";
                break;
            case 18456: // Login Failed
                errorMessage += "Login failed. Check credentials.";
                break;
            default:
                errorMessage += ex.Message;
                break;
        }

        // Show message box
        MessageBox.Show(errorMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        // Log error
        LogError(ex);
    }

    private static void LogError(Exception ex)
    {
        string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {ex.GetType().Name}: {ex.Message}";
        // Write to file or debug output
        System.Diagnostics.Debug.WriteLine(logMessage);
        // File.AppendAllText("app_errors.log", logMessage + Environment.NewLine);
    }
}