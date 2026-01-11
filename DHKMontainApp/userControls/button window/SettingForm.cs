using System;
using System.IO;
using System.Windows.Forms;

namespace DHKMontainApp.userControls.button_window
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            lblPath.Text = Properties.Settings.Default.SaveFolderPath;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "اختر المجلد لحفظ الملفات";

                // فتح المجلد الحالي إذا موجود
                if (!string.IsNullOrEmpty(Properties.Settings.Default.SaveFolderPath))
                {
                    folderDialog.SelectedPath = Properties.Settings.Default.SaveFolderPath;
                }

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderDialog.SelectedPath;

                    // سؤال التأكيد بالعربي
                    var result = MessageBox.Show(
                        "هل أنت متأكد أنك تريد حفظ هذا المجلد؟\nإذا قمت بالتأكيد سيتم إعادة تشغيل التطبيق.",
                        "تأكيد",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        // حفظ المجلد
                        Properties.Settings.Default.SaveFolderPath = selectedFolder;
                        Properties.Settings.Default.Save();

                        MessageBox.Show(
                            "تم حفظ المجلد بنجاح.\nسيتم إعادة تشغيل التطبيق.",
                            "تم",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        // إعادة تشغيل التطبيق
                        Application.Restart();
                        Environment.Exit(0); // لضمان إغلاق النسخة الحالية
                    }
                }
            }
        }
    }
}
