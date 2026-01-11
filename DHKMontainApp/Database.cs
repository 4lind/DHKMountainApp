using System.Data.SqlClient;

public static class Database
{
    // single connection for whole app
    public static SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=DHKDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
    //public static SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=DHKDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");


    // helper methods
    public static void Open()
    {
        if (con.State != System.Data.ConnectionState.Open)
            con.Open();
    }

    public static void Close()
    {
        if (con.State != System.Data.ConnectionState.Closed)
            con.Close();
    }

    // ADD JUST THIS METHOD for connection testing
    public static bool TestConnection()
    {
        try
        {
            Open();
            using (var cmd = new SqlCommand("SELECT 1", con))
            {
                cmd.ExecuteScalar();
            }
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            Close();
        }
    }
}