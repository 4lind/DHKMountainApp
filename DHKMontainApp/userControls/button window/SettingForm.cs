using System;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DHKMontainApp.userControls.button_window
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            lblPath.Text = Properties.Settings.Default.SaveFolderPath;
            lblPath2.Text = Properties.Settings.Default.SaveFolderPath2;
        }

        private void btnFolderReceipt_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "اختر المجلد لحفظ الملفات";

                if (!string.IsNullOrEmpty(Properties.Settings.Default.SaveFolderPath))
                {
                    folderDialog.SelectedPath = Properties.Settings.Default.SaveFolderPath;
                }

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderDialog.SelectedPath;

                    var result = MessageBox.Show(
                        "هل أنت متأكد أنك تريد حفظ هذا المجلد؟\nإذا قمت بالتأكيد سيتم إعادة تشغيل التطبيق.",
                        "تأكيد",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        Properties.Settings.Default.SaveFolderPath = selectedFolder;
                        Properties.Settings.Default.Save();

                        MessageBox.Show(
                            "تم حفظ المجلد بنجاح.\nسيتم إعادة تشغيل التطبيق.",
                            "تم",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        Application.Restart();
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "SQLite Database|*.db|All files|*.*";
            sfd.DefaultExt = "db";
            sfd.FileName = $"DHKDB_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var sourceConn = new SQLiteConnection($"Data Source={Database.DatabasePath}"))
                    {
                        sourceConn.Open();
                        using (var destConn = new SQLiteConnection($"Data Source={sfd.FileName}"))
                        {
                            destConn.Open();
                            sourceConn.BackupDatabase(destConn, "main", "main", -1, null, 0);
                        }
                    }

                    MessageBox.Show($"✅ تم إنشاء النسخة الاحتياطية بنجاح!\nالموقع: {sfd.FileName}",
                                    "نجاح",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ فشل النسخ الاحتياطي: {ex.Message}",
                                    "خطأ",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFolderBuy_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "اختر المجلد لحفظ الملفات";

                if (!string.IsNullOrEmpty(Properties.Settings.Default.SaveFolderPath))
                {
                    folderDialog.SelectedPath = Properties.Settings.Default.SaveFolderPath;
                }

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderDialog.SelectedPath;

                    var result = MessageBox.Show(
                        "هل أنت متأكد أنك تريد حفظ هذا المجلد؟\nإذا قمت بالتأكيد سيتم إعادة تشغيل التطبيق.",
                        "تأكيد",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        Properties.Settings.Default.SaveFolderPath2 = selectedFolder;
                        Properties.Settings.Default.Save();

                        MessageBox.Show(
                            "تم حفظ المجلد بنجاح.\nسيتم إعادة تشغيل التطبيق.",
                            "تم",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        Application.Restart();
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}