namespace DHKMontainApp.userControls
{
    partial class homeviewPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblWelcome;

        private System.Windows.Forms.Panel panelRecentSales;
        private System.Windows.Forms.Label lblRecentSalesTitle;
        private System.Windows.Forms.DataGridView dgvRecentSales;

        private System.Windows.Forms.Panel panelQuickActions;
        private System.Windows.Forms.Label lblQuickActionsTitle;
        private System.Windows.Forms.Button btnNewInvoice;

        private System.Windows.Forms.Panel panelStockAlerts;
        private System.Windows.Forms.Label lblStockAlertsTitle;
        private System.Windows.Forms.ListBox listStockAlerts;
        private System.Windows.Forms.Timer timerRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(homeviewPage));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panelRecentSales = new System.Windows.Forms.Panel();
            this.listStockAlerts = new System.Windows.Forms.ListBox();
            this.lblStockAlertsTitle = new System.Windows.Forms.Label();
            this.dgvRecentSales = new System.Windows.Forms.DataGridView();
            this.lblRecentSalesTitle = new System.Windows.Forms.Label();
            this.panelQuickActions = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnViewReports = new System.Windows.Forms.Button();
            this.btnNewInvoice = new System.Windows.Forms.Button();
            this.lblQuickActionsTitle = new System.Windows.Forms.Label();
            this.panelStockAlerts = new System.Windows.Forms.Panel();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.btnPurchased = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelRecentSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentSales)).BeginInit();
            this.panelQuickActions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelStockAlerts.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(27)))), ((int)(((byte)(44)))));
            this.panelHeader.Controls.Add(this.lblWelcome);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelHeader.Size = new System.Drawing.Size(994, 74);
            this.panelHeader.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(20, 10);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(954, 54);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "أهلاً بكم في نظام إدارة شركة جبال دهوك";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelRecentSales
            // 
            this.panelRecentSales.Controls.Add(this.listStockAlerts);
            this.panelRecentSales.Controls.Add(this.lblStockAlertsTitle);
            this.panelRecentSales.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRecentSales.Location = new System.Drawing.Point(618, 209);
            this.panelRecentSales.Name = "panelRecentSales";
            this.panelRecentSales.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panelRecentSales.Size = new System.Drawing.Size(376, 391);
            this.panelRecentSales.TabIndex = 2;
            // 
            // listStockAlerts
            // 
            this.listStockAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.listStockAlerts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listStockAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listStockAlerts.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.listStockAlerts.ForeColor = System.Drawing.Color.White;
            this.listStockAlerts.FormattingEnabled = true;
            this.listStockAlerts.ItemHeight = 17;
            this.listStockAlerts.Location = new System.Drawing.Point(10, 42);
            this.listStockAlerts.Name = "listStockAlerts";
            this.listStockAlerts.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listStockAlerts.Size = new System.Drawing.Size(356, 339);
            this.listStockAlerts.TabIndex = 1;
            // 
            // lblStockAlertsTitle
            // 
            this.lblStockAlertsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStockAlertsTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblStockAlertsTitle.ForeColor = System.Drawing.Color.White;
            this.lblStockAlertsTitle.Location = new System.Drawing.Point(10, 0);
            this.lblStockAlertsTitle.Name = "lblStockAlertsTitle";
            this.lblStockAlertsTitle.Size = new System.Drawing.Size(356, 42);
            this.lblStockAlertsTitle.TabIndex = 0;
            this.lblStockAlertsTitle.Text = "تنبيهات المخزون";
            this.lblStockAlertsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvRecentSales
            // 
            this.dgvRecentSales.AllowUserToAddRows = false;
            this.dgvRecentSales.AllowUserToDeleteRows = false;
            this.dgvRecentSales.AllowUserToResizeRows = false;
            this.dgvRecentSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentSales.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.dgvRecentSales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecentSales.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecentSales.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(27)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(27)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecentSales.ColumnHeadersHeight = 50;
            this.dgvRecentSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecentSales.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRecentSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentSales.EnableHeadersVisualStyles = false;
            this.dgvRecentSales.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(72)))));
            this.dgvRecentSales.Location = new System.Drawing.Point(10, 45);
            this.dgvRecentSales.MultiSelect = false;
            this.dgvRecentSales.Name = "dgvRecentSales";
            this.dgvRecentSales.ReadOnly = true;
            this.dgvRecentSales.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgvRecentSales.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.dgvRecentSales.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvRecentSales.RowTemplate.Height = 45;
            this.dgvRecentSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentSales.Size = new System.Drawing.Size(598, 336);
            this.dgvRecentSales.TabIndex = 1;
            // 
            // lblRecentSalesTitle
            // 
            this.lblRecentSalesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecentSalesTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblRecentSalesTitle.ForeColor = System.Drawing.Color.White;
            this.lblRecentSalesTitle.Location = new System.Drawing.Point(10, 0);
            this.lblRecentSalesTitle.Name = "lblRecentSalesTitle";
            this.lblRecentSalesTitle.Size = new System.Drawing.Size(598, 45);
            this.lblRecentSalesTitle.TabIndex = 0;
            this.lblRecentSalesTitle.Text = "آخر الفواتير";
            this.lblRecentSalesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelQuickActions
            // 
            this.panelQuickActions.Controls.Add(this.panel1);
            this.panelQuickActions.Controls.Add(this.lblQuickActionsTitle);
            this.panelQuickActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQuickActions.Location = new System.Drawing.Point(0, 74);
            this.panelQuickActions.Name = "panelQuickActions";
            this.panelQuickActions.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelQuickActions.Size = new System.Drawing.Size(994, 135);
            this.panelQuickActions.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPurchased);
            this.panel1.Controls.Add(this.btnAddProduct);
            this.panel1.Controls.Add(this.btnAddCustomer);
            this.panel1.Controls.Add(this.btnViewReports);
            this.panel1.Controls.Add(this.btnNewInvoice);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(10, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel1.Size = new System.Drawing.Size(964, 66);
            this.panel1.TabIndex = 5;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.btnAddProduct.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddProduct.FlatAppearance.BorderSize = 0;
            this.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Image = global::DHKMontainApp.Properties.Resources.add_box_24dp_FFFFFF_FILL0_wght400_GRAD0_opsz24;
            this.btnAddProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddProduct.Location = new System.Drawing.Point(190, 5);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnAddProduct.Size = new System.Drawing.Size(179, 61);
            this.btnAddProduct.TabIndex = 5;
            this.btnAddProduct.Text = "إضافة منتج";
            this.btnAddProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.btnAddCustomer.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddCustomer.FlatAppearance.BorderSize = 0;
            this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCustomer.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAddCustomer.ForeColor = System.Drawing.Color.White;
            this.btnAddCustomer.Image = global::DHKMontainApp.Properties.Resources.addperson;
            this.btnAddCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCustomer.Location = new System.Drawing.Point(369, 5);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnAddCustomer.Size = new System.Drawing.Size(179, 61);
            this.btnAddCustomer.TabIndex = 2;
            this.btnAddCustomer.Text = "إضافة عميل";
            this.btnAddCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnAddCustomer.UseVisualStyleBackColor = false;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // btnViewReports
            // 
            this.btnViewReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.btnViewReports.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnViewReports.FlatAppearance.BorderSize = 0;
            this.btnViewReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewReports.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnViewReports.ForeColor = System.Drawing.Color.White;
            this.btnViewReports.Image = global::DHKMontainApp.Properties.Resources.sales;
            this.btnViewReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewReports.Location = new System.Drawing.Point(548, 5);
            this.btnViewReports.Name = "btnViewReports";
            this.btnViewReports.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnViewReports.Size = new System.Drawing.Size(178, 61);
            this.btnViewReports.TabIndex = 4;
            this.btnViewReports.Text = "عرض التقارير";
            this.btnViewReports.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnViewReports.UseVisualStyleBackColor = false;
            this.btnViewReports.Click += new System.EventHandler(this.btnViewReports_Click);
            // 
            // btnNewInvoice
            // 
            this.btnNewInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNewInvoice.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNewInvoice.FlatAppearance.BorderSize = 0;
            this.btnNewInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewInvoice.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNewInvoice.ForeColor = System.Drawing.Color.White;
            this.btnNewInvoice.Image = ((System.Drawing.Image)(resources.GetObject("btnNewInvoice.Image")));
            this.btnNewInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewInvoice.Location = new System.Drawing.Point(726, 5);
            this.btnNewInvoice.Name = "btnNewInvoice";
            this.btnNewInvoice.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnNewInvoice.Size = new System.Drawing.Size(238, 61);
            this.btnNewInvoice.TabIndex = 1;
            this.btnNewInvoice.Text = "فاتورة جديدة";
            this.btnNewInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNewInvoice.UseVisualStyleBackColor = false;
            this.btnNewInvoice.Click += new System.EventHandler(this.btnNewInvoice_Click);
            // 
            // lblQuickActionsTitle
            // 
            this.lblQuickActionsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblQuickActionsTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblQuickActionsTitle.ForeColor = System.Drawing.Color.White;
            this.lblQuickActionsTitle.Location = new System.Drawing.Point(20, 15);
            this.lblQuickActionsTitle.Name = "lblQuickActionsTitle";
            this.lblQuickActionsTitle.Size = new System.Drawing.Size(954, 39);
            this.lblQuickActionsTitle.TabIndex = 0;
            this.lblQuickActionsTitle.Text = "الإجراءات السريعة";
            this.lblQuickActionsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelStockAlerts
            // 
            this.panelStockAlerts.Controls.Add(this.dgvRecentSales);
            this.panelStockAlerts.Controls.Add(this.lblRecentSalesTitle);
            this.panelStockAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStockAlerts.Location = new System.Drawing.Point(0, 209);
            this.panelStockAlerts.Name = "panelStockAlerts";
            this.panelStockAlerts.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panelStockAlerts.Size = new System.Drawing.Size(618, 391);
            this.panelStockAlerts.TabIndex = 4;
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 10000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // btnPurchased
            // 
            this.btnPurchased.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.btnPurchased.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPurchased.FlatAppearance.BorderSize = 0;
            this.btnPurchased.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurchased.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnPurchased.ForeColor = System.Drawing.Color.White;
            this.btnPurchased.Image = global::DHKMontainApp.Properties.Resources.sales;
            this.btnPurchased.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPurchased.Location = new System.Drawing.Point(13, 5);
            this.btnPurchased.Name = "btnPurchased";
            this.btnPurchased.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnPurchased.Size = new System.Drawing.Size(177, 61);
            this.btnPurchased.TabIndex = 6;
            this.btnPurchased.Text = "عرض المشتريات";
            this.btnPurchased.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnPurchased.UseVisualStyleBackColor = false;
            this.btnPurchased.Click += new System.EventHandler(this.btnPurchased_Click);
            // 
            // homeviewPage
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.Controls.Add(this.panelStockAlerts);
            this.Controls.Add(this.panelRecentSales);
            this.Controls.Add(this.panelQuickActions);
            this.Controls.Add(this.panelHeader);
            this.Name = "homeviewPage";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(994, 600);
            this.Load += new System.EventHandler(this.homeviewPage_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelRecentSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentSales)).EndInit();
            this.panelQuickActions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelStockAlerts.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.Button btnViewReports;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnPurchased;
    }
}