using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DHKMontainApp
{
    internal static class buttonR
    {
        // Make a button appear rounded. Call after button has been sized (e.g., in Form.Load or control's Resize).

        public static void MakeButtonRounded(Button button, int radius)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(button.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(button.Width - radius, button.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, button.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            button.Region = new System.Drawing.Region(path);
        }
    }
}