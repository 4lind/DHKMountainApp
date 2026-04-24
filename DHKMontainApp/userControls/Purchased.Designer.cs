namespace DHKMontainApp.userControls
{
    partial class Purchased
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.panelSearchInputs = new System.Windows.Forms.Panel();
            this.txtSupplierSearch = new System.Windows.Forms.TextBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.labelSupplier = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.panelPurchasesGrid = new System.Windows.Forms.Panel();
            this.dataGridViewPurchases = new System.Windows.Forms.DataGridView();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.panelSummaryRight = new System.Windows.Forms.Panel();
            this.labelTotalPurchasesTitle = new System.Windows.Forms.Label();
            this.lblTotalPurchases = new System.Windows.Forms.Label();
            this.lblTotalAmountTitle = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.panelItemsGrid = new System.Windows.Forms.Panel();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.labelItemsTitle = new System.Windows.Forms.Label();
            this.searchTimer = new System.Windows.Forms.Timer(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.panelSearchInputs.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelPurchasesGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPurchases)).BeginInit();
            this.panelSummary.SuspendLayout();
            this.panelSummaryRight.SuspendLayout();
            this.panelItemsGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.panelTop.Controls.Add(this.groupBoxSearch);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(5, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 82);
            this.panelTop.TabIndex = 0;
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.panelSearchInputs);
            this.groupBoxSearch.Controls.Add(this.panelButtons);
            this.groupBoxSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.groupBoxSearch.ForeColor = System.Drawing.Color.White;
            this.groupBoxSearch.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBoxSearch.Size = new System.Drawing.Size(984, 68);
            this.groupBoxSearch.TabIndex = 0;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "بحث المشتريات";
            // 
            // panelSearchInputs
            // 
            this.panelSearchInputs.Controls.Add(this.txtSupplierSearch);
            this.panelSearchInputs.Controls.Add(this.dtpToDate);
            this.panelSearchInputs.Controls.Add(this.labelSupplier);
            this.panelSearchInputs.Controls.Add(this.labelTo);
            this.panelSearchInputs.Controls.Add(this.labelFrom);
            this.panelSearchInputs.Controls.Add(this.dtpFromDate);
            this.panelSearchInputs.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSearchInputs.Location = new System.Drawing.Point(407, 18);
            this.panelSearchInputs.Name = "panelSearchInputs";
            this.panelSearchInputs.Size = new System.Drawing.Size(574, 47);
            this.panelSearchInputs.TabIndex = 11;
            // 
            // txtSupplierSearch
            // 
            this.txtSupplierSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtSupplierSearch.Location = new System.Drawing.Point(361, 9);
            this.txtSupplierSearch.Name = "txtSupplierSearch";
            this.txtSupplierSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSupplierSearch.Size = new System.Drawing.Size(136, 22);
            this.txtSupplierSearch.TabIndex = 1;
            this.txtSupplierSearch.TextChanged += new System.EventHandler(this.TxtSupplierSearch_TextChanged);
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "yyyy/MM/dd";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(7, 7);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpToDate.Size = new System.Drawing.Size(102, 22);
            this.dtpToDate.TabIndex = 5;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // labelSupplier
            // 
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Location = new System.Drawing.Point(503, 12);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(62, 16);
            this.labelSupplier.TabIndex = 0;
            this.labelSupplier.Text = "اسم الموارد:";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(115, 12);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(58, 16);
            this.labelTo.TabIndex = 4;
            this.labelTo.Text = "إلى تاريخ:";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(301, 12);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(54, 16);
            this.labelFrom.TabIndex = 2;
            this.labelFrom.Text = "من تاريخ:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "yyyy/MM/dd";
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(193, 9);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpFromDate.Size = new System.Drawing.Size(102, 22);
            this.dtpFromDate.TabIndex = 3;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnSearch);
            this.panelButtons.Controls.Add(this.btnPrint);
            this.panelButtons.Controls.Add(this.btnPreview);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButtons.Location = new System.Drawing.Point(3, 18);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(324, 47);
            this.panelButtons.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(236, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 29);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(4, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 29);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "طـــبع";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Location = new System.Drawing.Point(98, 3);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(85, 29);
            this.btnPreview.TabIndex = 8;
            this.btnPreview.Text = "عرض التفاصيل";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // panelPurchasesGrid
            // 
            this.panelPurchasesGrid.Controls.Add(this.dataGridViewPurchases);
            this.panelPurchasesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPurchasesGrid.Location = new System.Drawing.Point(5, 85);
            this.panelPurchasesGrid.Name = "panelPurchasesGrid";
            this.panelPurchasesGrid.Size = new System.Drawing.Size(984, 212);
            this.panelPurchasesGrid.TabIndex = 1;
            // 
            // dataGridViewPurchases
            // 
            this.dataGridViewPurchases.AllowUserToAddRows = false;
            this.dataGridViewPurchases.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewPurchases.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPurchases.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPurchases.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.dataGridViewPurchases.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewPurchases.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewPurchases.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(27)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(27)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPurchases.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewPurchases.ColumnHeadersHeight = 40;
            this.dataGridViewPurchases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPurchases.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewPurchases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPurchases.EnableHeadersVisualStyles = false;
            this.dataGridViewPurchases.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(72)))));
            this.dataGridViewPurchases.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPurchases.MultiSelect = false;
            this.dataGridViewPurchases.Name = "dataGridViewPurchases";
            this.dataGridViewPurchases.ReadOnly = true;
            this.dataGridViewPurchases.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPurchases.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewPurchases.RowHeadersVisible = false;
            this.dataGridViewPurchases.RowHeadersWidth = 45;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewPurchases.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewPurchases.RowTemplate.Height = 35;
            this.dataGridViewPurchases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPurchases.Size = new System.Drawing.Size(984, 212);
            this.dataGridViewPurchases.TabIndex = 0;
            this.dataGridViewPurchases.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewPurchases_CellContentDoubleClick);
            this.dataGridViewPurchases.SelectionChanged += new System.EventHandler(this.DataGridViewPurchases_SelectionChanged);
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(27)))), ((int)(((byte)(44)))));
            this.panelSummary.Controls.Add(this.panelSummaryRight);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSummary.Location = new System.Drawing.Point(5, 547);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(984, 50);
            this.panelSummary.TabIndex = 2;
            // 
            // panelSummaryRight
            // 
            this.panelSummaryRight.Controls.Add(this.labelTotalPurchasesTitle);
            this.panelSummaryRight.Controls.Add(this.lblTotalPurchases);
            this.panelSummaryRight.Controls.Add(this.lblTotalAmountTitle);
            this.panelSummaryRight.Controls.Add(this.lblTotalAmount);
            this.panelSummaryRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSummaryRight.Location = new System.Drawing.Point(473, 0);
            this.panelSummaryRight.Name = "panelSummaryRight";
            this.panelSummaryRight.Size = new System.Drawing.Size(511, 50);
            this.panelSummaryRight.TabIndex = 4;
            // 
            // labelTotalPurchasesTitle
            // 
            this.labelTotalPurchasesTitle.AutoSize = true;
            this.labelTotalPurchasesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelTotalPurchasesTitle.ForeColor = System.Drawing.Color.White;
            this.labelTotalPurchasesTitle.Location = new System.Drawing.Point(158, 19);
            this.labelTotalPurchasesTitle.Name = "labelTotalPurchasesTitle";
            this.labelTotalPurchasesTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelTotalPurchasesTitle.Size = new System.Drawing.Size(98, 16);
            this.labelTotalPurchasesTitle.TabIndex = 2;
            this.labelTotalPurchasesTitle.Text = "إجمالي المشتريات:";
            this.labelTotalPurchasesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalPurchases
            // 
            this.lblTotalPurchases.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalPurchases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            this.lblTotalPurchases.Location = new System.Drawing.Point(51, 15);
            this.lblTotalPurchases.Name = "lblTotalPurchases";
            this.lblTotalPurchases.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotalPurchases.Size = new System.Drawing.Size(101, 20);
            this.lblTotalPurchases.TabIndex = 3;
            this.lblTotalPurchases.Text = "0";
            this.lblTotalPurchases.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalAmountTitle
            // 
            this.lblTotalAmountTitle.AutoSize = true;
            this.lblTotalAmountTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblTotalAmountTitle.ForeColor = System.Drawing.Color.White;
            this.lblTotalAmountTitle.Location = new System.Drawing.Point(424, 18);
            this.lblTotalAmountTitle.Name = "lblTotalAmountTitle";
            this.lblTotalAmountTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotalAmountTitle.Size = new System.Drawing.Size(76, 16);
            this.lblTotalAmountTitle.TabIndex = 0;
            this.lblTotalAmountTitle.Text = "إجمالي المبلغ:";
            this.lblTotalAmountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(253, 15);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotalAmount.Size = new System.Drawing.Size(165, 20);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "0.00$";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelItemsGrid
            // 
            this.panelItemsGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panelItemsGrid.Controls.Add(this.dataGridViewItems);
            this.panelItemsGrid.Controls.Add(this.labelItemsTitle);
            this.panelItemsGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelItemsGrid.Location = new System.Drawing.Point(5, 297);
            this.panelItemsGrid.Name = "panelItemsGrid";
            this.panelItemsGrid.Size = new System.Drawing.Size(984, 250);
            this.panelItemsGrid.TabIndex = 3;
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.AllowUserToAddRows = false;
            this.dataGridViewItems.AllowUserToDeleteRows = false;
            this.dataGridViewItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewItems.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.dataGridViewItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewItems.Location = new System.Drawing.Point(0, 34);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.ReadOnly = true;
            this.dataGridViewItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridViewItems.Size = new System.Drawing.Size(984, 216);
            this.dataGridViewItems.TabIndex = 1;
            // 
            // labelItemsTitle
            // 
            this.labelItemsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(83)))));
            this.labelItemsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelItemsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelItemsTitle.ForeColor = System.Drawing.Color.White;
            this.labelItemsTitle.Location = new System.Drawing.Point(0, 0);
            this.labelItemsTitle.Name = "labelItemsTitle";
            this.labelItemsTitle.Size = new System.Drawing.Size(984, 34);
            this.labelItemsTitle.TabIndex = 0;
            this.labelItemsTitle.Text = "تفاصيل عملية الشراء المحددة";
            this.labelItemsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // searchTimer
            // 
            this.searchTimer.Interval = 500;
            this.searchTimer.Tick += new System.EventHandler(this.SearchTimer_Tick);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = global::DHKMontainApp.Properties.Resources.delete_forever_24dp_FFFFFF_FILL0_wght400_GRAD0_opsz24;
            this.btnDelete.Location = new System.Drawing.Point(189, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(41, 29);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // Purchased
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.panelPurchasesGrid);
            this.Controls.Add(this.panelItemsGrid);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelTop);
            this.Name = "Purchased";
            this.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Size = new System.Drawing.Size(994, 600);
            this.Load += new System.EventHandler(this.Purchased_Load);
            this.panelTop.ResumeLayout(false);
            this.groupBoxSearch.ResumeLayout(false);
            this.panelSearchInputs.ResumeLayout(false);
            this.panelSearchInputs.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelPurchasesGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPurchases)).EndInit();
            this.panelSummary.ResumeLayout(false);
            this.panelSummaryRight.ResumeLayout(false);
            this.panelSummaryRight.PerformLayout();
            this.panelItemsGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.ResumeLayout(false);

        }

        // Control declarations
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.Panel panelSearchInputs;
        private System.Windows.Forms.TextBox txtSupplierSearch;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label labelSupplier;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Panel panelPurchasesGrid;
        private System.Windows.Forms.DataGridView dataGridViewPurchases;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Panel panelSummaryRight;
        private System.Windows.Forms.Label labelTotalPurchasesTitle;
        private System.Windows.Forms.Label lblTotalPurchases;
        private System.Windows.Forms.Label lblTotalAmountTitle;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Panel panelItemsGrid;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.Label labelItemsTitle;
        private System.Windows.Forms.Timer searchTimer;
    }
}