using System.Data.SQLite;
using System;
using System.Windows.Forms;

public static class DatabaseHelper
{
    public static bool IsConnected()
    {
        try
        {
            Database.Open();
            using (var cmd = new SQLiteCommand("SELECT 1", Database.con))
            {
                cmd.ExecuteScalar();
            }
            return true;
        }
        catch (SQLiteException ex)
        {
            HandleConnectionError(ex);
            return false;
        }
        finally
        {
            Database.Close();
        }
    }

    public static SQLiteDataReader ExecuteReaderWithRetry(string query, SQLiteParameter[] parameters = null, int maxRetries = 2)
    {
        int attempt = 0;
        while (attempt <= maxRetries)
        {
            try
            {
                Database.Open();
                var cmd = new SQLiteCommand(query, Database.con);
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (SQLiteException ex)
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

    private static void HandleConnectionError(SQLiteException ex)
    {
        string errorMessage = "Database connection error. " + ex.Message;
        MessageBox.Show(errorMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        LogError(ex);
    }

    private static void LogError(Exception ex)
    {
        string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {ex.GetType().Name}: {ex.Message}";
        System.Diagnostics.Debug.WriteLine(logMessage);
    }
}