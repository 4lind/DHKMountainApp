using System;
using System.Windows.Forms;
using System.IO;

namespace DHKMontainApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Database.EnsureDatabaseCreated();
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                // Write to a file in case MessageBox doesn't show
                File.WriteAllText(Path.Combine(Application.StartupPath, "error.txt"), ex.ToString());
                MessageBox.Show($"Startup error:\n\n{ex.Message}\n\nSee error.txt for details.",
                                "Fatal Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}