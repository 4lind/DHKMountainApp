using System;
using System.IO;
using System.Data.SQLite;

public static class Database
{
    private static readonly string DbPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "DHK Co",
        "DHKDB.db"
    );

    // Public property to expose the path
    public static string DatabasePath => DbPath;

    public static SQLiteConnection con = new SQLiteConnection($"Data Source={DbPath}");

    static Database()
    {
        var dir = Path.GetDirectoryName(DbPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    public static void Open()
    {
        if (con.State != System.Data.ConnectionState.Open)
        {
            con.Open();
            using (var cmd = new SQLiteCommand("PRAGMA foreign_keys = ON;", con))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void Close()
    {
        if (con.State != System.Data.ConnectionState.Closed)
            con.Close();
    }

    public static bool TestConnection()
    {
        try
        {
            Open();
            using (var cmd = new SQLiteCommand("SELECT 1", con))
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

    public static void EnsureDatabaseCreated()
    {
        using (var con = new SQLiteConnection($"Data Source={DbPath}"))
        {
            con.Open();
            using (var cmd = new SQLiteCommand("PRAGMA foreign_keys = ON;", con))
                cmd.ExecuteNonQuery();

            string checkTable = "SELECT name FROM sqlite_master WHERE type='table' AND name='customer';";
            using (var cmd = new SQLiteCommand(checkTable, con))
            {
                var result = cmd.ExecuteScalar();
                if (result == null)
                {
                    string createTables = @"
                    CREATE TABLE customer (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        cName TEXT NOT NULL,
                        cPhone TEXT,
                        Address TEXT,
                        Company TEXT NOT NULL
                    );

                    CREATE TABLE Product (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        productName TEXT NOT NULL,
                        productCount INTEGER NOT NULL,
                        producttype TEXT NOT NULL,
                        productCouple INTEGER
                    );

                    CREATE TABLE Receipts (
                        ReceiptID TEXT PRIMARY KEY,
                        CustomerName TEXT NOT NULL,
                        CustomerID INTEGER,
                        ReceiptDate TEXT NOT NULL,
                        TotalCartons NUMERIC NOT NULL,
                        TotalPairs NUMERIC NOT NULL,
                        TotalAmount NUMERIC NOT NULL,
                        Currency TEXT,
                        FilePath TEXT,
                        CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (CustomerID) REFERENCES customer(id)
                    );

                    CREATE TABLE ReceiptItems (
                        ItemID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ReceiptID TEXT NOT NULL,
                        ProductName TEXT NOT NULL,
                        Cartons INTEGER NOT NULL,
                        PairsPerCarton INTEGER NOT NULL,
                        UnitPrice NUMERIC NOT NULL,
                        ItemTotal NUMERIC NOT NULL,
                        RowNumber INTEGER NOT NULL,
                        FOREIGN KEY (ReceiptID) REFERENCES Receipts(ReceiptID)
                     );
                    CREATE TABLE IF NOT EXISTS Supplier (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        sName TEXT NOT NULL,
                        sPhone TEXT,
                        Address TEXT,
                        Company TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Purchases (
                        PurchaseID TEXT PRIMARY KEY,
                        SupplierName TEXT NOT NULL,
                        SupplierID INTEGER,
                        PurchaseDate TEXT NOT NULL,
                        TotalCartons NUMERIC NOT NULL,
                        TotalPairs NUMERIC NOT NULL,
                        TotalAmount NUMERIC NOT NULL,
                        Currency TEXT,
                        FilePath TEXT,
                        CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (SupplierID) REFERENCES Supplier(id)
                    );


                    CREATE TABLE IF NOT EXISTS PurchaseItems (
                        ItemID INTEGER PRIMARY KEY AUTOINCREMENT,
                        PurchaseID TEXT NOT NULL,
                        ProductName TEXT NOT NULL,
                        Cartons INTEGER NOT NULL,
                        PairsPerCarton INTEGER NOT NULL,
                        UnitPrice NUMERIC NOT NULL,
                        ItemTotal NUMERIC NOT NULL,
                        RowNumber INTEGER NOT NULL,
                        FOREIGN KEY (PurchaseID) REFERENCES Purchases(PurchaseID)
                    ); ";
                    using (var cmd2 = new SQLiteCommand(createTables, con))
                        cmd2.ExecuteNonQuery();
                }
            }
        }
    }
}