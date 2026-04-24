// payment.Designer.cs
namespace DHKMontainApp.userControls
{
    partial class Receipt
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnSavePrint = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.groupBoxAdd = new System.Windows.Forms.GroupBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtprice = new System.Windows.Forms.TextBox();
            this.lblCartons = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPairs = new System.Windows.Forms.Label();
            this.txtcouple = new System.Windows.Forms.TextBox();
            this.txtCartons = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboProducts = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.datetime_Checkbox = new System.Windows.Forms.CheckBox();
            this.dateTimePicker_Payment = new System.Windows.Forms.DateTimePicker();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dHKDBDataSet = new DHKMontainApp.DHKDBDataSet();
            this.dHKDBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelBottom.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxAdd.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dHKDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dHKDBDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(46)))), ((int)(((byte)(71)))));
            this.panelBottom.Controls.Add(this.panel1);
            this.panelBottom.Controls.Add(this.btnSavePrint);
            this.panelBottom.Controls.Add(this.btnRemoveItem);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(5, 526);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panelBottom.Size = new System.Drawing.Size(984, 71);
            this.panelBottom.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.txtCurrency);
            this.panel1.Controls.Add(this.lblCurrency);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(571, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 71);
            this.panel1.TabIndex = 12;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTotal.ForeColor = System.Drawing.Color.White;
            this.txtTotal.Location = new System.Drawing.Point(195, 23);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotal.Size = new System.Drawing.Size(110, 27);
            this.txtTotal.TabIndex = 7;
            this.txtTotal.Text = "0.00";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCurrency
            // 
            this.txtCurrency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.txtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrency.ForeColor = System.Drawing.Color.White;
            this.txtCurrency.Location = new System.Drawing.Point(53, 24);
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCurrency.Size = new System.Drawing.Size(50, 23);
            this.txtCurrency.TabIndex = 9;
            this.txtCurrency.Text = "USD";
            this.txtCurrency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCurrency
            // 
            this.lblCurrency.ForeColor = System.Drawing.Color.White;
            this.lblCurrency.Location = new System.Drawing.Point(113, 24);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(46, 24);
            this.lblCurrency.TabIndex = 8;
            this.lblCurrency.Text = "العملة :";
            this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(315, 23);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(95, 24);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "مجموع القائمة :";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSavePrint
            // 
            this.btnSavePrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(150)))), ((int)(((byte)(100)))));
            this.btnSavePrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePrint.ForeColor = System.Drawing.Color.White;
            this.btnSavePrint.Location = new System.Drawing.Point(40, 18);
            this.btnSavePrint.Name = "btnSavePrint";
            this.btnSavePrint.Size = new System.Drawing.Size(110, 32);
            this.btnSavePrint.TabIndex = 11;
            this.btnSavePrint.Text = "حفظ / طباعة";
            this.btnSavePrint.UseVisualStyleBackColor = false;
            this.btnSavePrint.Click += new System.EventHandler(this.btnSavePrint_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveItem.Location = new System.Drawing.Point(171, 18);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(90, 32);
            this.btnRemoveItem.TabIndex = 10;
            this.btnRemoveItem.Text = "حذف صنف";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // groupBoxAdd
            // 
            this.groupBoxAdd.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxAdd.Controls.Add(this.panel8);
            this.groupBoxAdd.Controls.Add(this.panel7);
            this.groupBoxAdd.Controls.Add(this.panel6);
            this.groupBoxAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxAdd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxAdd.ForeColor = System.Drawing.Color.White;
            this.groupBoxAdd.Location = new System.Drawing.Point(0, 48);
            this.groupBoxAdd.Name = "groupBoxAdd";
            this.groupBoxAdd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBoxAdd.Size = new System.Drawing.Size(984, 73);
            this.groupBoxAdd.TabIndex = 2;
            this.groupBoxAdd.TabStop = false;
            this.groupBoxAdd.Text = "إضافة صنف";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnAddItem);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(3, 19);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(147, 51);
            this.panel8.TabIndex = 20;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(46)))), ((int)(((byte)(71)))));
            this.btnAddItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Image = global::DHKMontainApp.Properties.Resources.Add;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddItem.Location = new System.Drawing.Point(3, 3);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(127, 42);
            this.btnAddItem.TabIndex = 3;
            this.btnAddItem.Text = "إضافة";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txtprice);
            this.panel7.Controls.Add(this.lblCartons);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.lblPairs);
            this.panel7.Controls.Add(this.txtcouple);
            this.panel7.Controls.Add(this.txtCartons);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(203, 19);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(403, 51);
            this.panel7.TabIndex = 19;
            // 
            // txtprice
            // 
            this.txtprice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(78)))));
            this.txtprice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtprice.ForeColor = System.Drawing.Color.White;
            this.txtprice.Location = new System.Drawing.Point(17, 10);
            this.txtprice.Name = "txtprice";
            this.txtprice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtprice.Size = new System.Drawing.Size(101, 23);
            this.txtprice.TabIndex = 1;
            this.txtprice.Text = "0.00";
            this.txtprice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtprice.Click += new System.EventHandler(this.txtprice_Click);
            this.txtprice.Leave += new System.EventHandler(this.TxtPrice_Leave);
            // 
            // lblCartons
            // 
            this.lblCartons.ForeColor = System.Drawing.Color.White;
            this.lblCartons.Location = new System.Drawing.Point(231, 7);
            this.lblCartons.Name = "lblCartons";
            this.lblCartons.Size = new System.Drawing.Size(57, 24);
            this.lblCartons.TabIndex = 13;
            this.lblCartons.Text = "كارتون :";
            this.lblCartons.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(124, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 24);
            this.label1.TabIndex = 17;
            this.label1.Text = "سعر :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPairs
            // 
            this.lblPairs.ForeColor = System.Drawing.Color.White;
            this.lblPairs.Location = new System.Drawing.Point(346, 9);
            this.lblPairs.Name = "lblPairs";
            this.lblPairs.Size = new System.Drawing.Size(32, 24);
            this.lblPairs.TabIndex = 15;
            this.lblPairs.Text = "زوج :";
            this.lblPairs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtcouple
            // 
            this.txtcouple.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.txtcouple.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcouple.ForeColor = System.Drawing.Color.White;
            this.txtcouple.Location = new System.Drawing.Point(300, 11);
            this.txtcouple.Name = "txtcouple";
            this.txtcouple.ReadOnly = true;
            this.txtcouple.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtcouple.Size = new System.Drawing.Size(40, 23);
            this.txtcouple.TabIndex = 16;
            this.txtcouple.Text = "0";
            this.txtcouple.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtcouple.Leave += new System.EventHandler(this.TxtCouple_Leave);
            // 
            // txtCartons
            // 
            this.txtCartons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.txtCartons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCartons.ForeColor = System.Drawing.Color.White;
            this.txtCartons.Location = new System.Drawing.Point(185, 10);
            this.txtCartons.Name = "txtCartons";
            this.txtCartons.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCartons.Size = new System.Drawing.Size(40, 23);
            this.txtCartons.TabIndex = 14;
            this.txtCartons.Text = "0";
            this.txtCartons.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCartons.Click += new System.EventHandler(this.txtCartons_Click);
            this.txtCartons.Leave += new System.EventHandler(this.TxtCartons_Leave);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.comboProducts);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(606, 19);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(375, 51);
            this.panel6.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(254, 5);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(100, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = " اسم المادة :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboProducts
            // 
            this.comboProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.comboProducts.ForeColor = System.Drawing.Color.White;
            this.comboProducts.FormattingEnabled = true;
            this.comboProducts.Location = new System.Drawing.Point(18, 10);
            this.comboProducts.Name = "comboProducts";
            this.comboProducts.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboProducts.Size = new System.Drawing.Size(230, 23);
            this.comboProducts.TabIndex = 0;
            this.comboProducts.SelectedIndexChanged += new System.EventHandler(this.comboProducts_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel9);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(984, 48);
            this.panel3.TabIndex = 3;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.datetime_Checkbox);
            this.panel9.Controls.Add(this.dateTimePicker_Payment);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(258, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(348, 48);
            this.panel9.TabIndex = 23;
            // 
            // datetime_Checkbox
            // 
            this.datetime_Checkbox.AutoSize = true;
            this.datetime_Checkbox.Checked = true;
            this.datetime_Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.datetime_Checkbox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.datetime_Checkbox.ForeColor = System.Drawing.Color.White;
            this.datetime_Checkbox.Location = new System.Drawing.Point(233, 12);
            this.datetime_Checkbox.Name = "datetime_Checkbox";
            this.datetime_Checkbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.datetime_Checkbox.Size = new System.Drawing.Size(104, 24);
            this.datetime_Checkbox.TabIndex = 22;
            this.datetime_Checkbox.Text = ": تاريخ اليوم ";
            this.datetime_Checkbox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.datetime_Checkbox.UseVisualStyleBackColor = true;
            this.datetime_Checkbox.CheckedChanged += new System.EventHandler(this.datetime_Checkbox_CheckedChanged);
            // 
            // dateTimePicker_Payment
            // 
            this.dateTimePicker_Payment.CalendarMonthBackground = System.Drawing.SystemColors.MenuHighlight;
            this.dateTimePicker_Payment.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dateTimePicker_Payment.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dateTimePicker_Payment.Enabled = false;
            this.dateTimePicker_Payment.Location = new System.Drawing.Point(14, 13);
            this.dateTimePicker_Payment.Name = "dateTimePicker_Payment";
            this.dateTimePicker_Payment.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker_Payment.TabIndex = 21;
            this.dateTimePicker_Payment.Value = new System.DateTime(2026, 2, 23, 23, 9, 48, 0);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_refresh);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(58, 48);
            this.panel5.TabIndex = 19;
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(78)))));
            this.btn_refresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btn_refresh.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_refresh.Image = global::DHKMontainApp.Properties.Resources.refresh_24dp_FFFFFF_FILL0_wght400_GRAD0_opsz24;
            this.btn_refresh.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_refresh.Location = new System.Drawing.Point(0, 0);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_refresh.Size = new System.Drawing.Size(48, 48);
            this.btn_refresh.TabIndex = 18;
            this.btn_refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_refresh.UseVisualStyleBackColor = false;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.comboBoxCustomer);
            this.panel4.Controls.Add(this.labelSearch);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(606, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(378, 48);
            this.panel4.TabIndex = 0;
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.comboBoxCustomer.ForeColor = System.Drawing.Color.White;
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Location = new System.Drawing.Point(18, 12);
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboBoxCustomer.Size = new System.Drawing.Size(230, 23);
            this.comboBoxCustomer.TabIndex = 1;
            // 
            // labelSearch
            // 
            this.labelSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelSearch.ForeColor = System.Drawing.Color.White;
            this.labelSearch.Location = new System.Drawing.Point(254, 7);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelSearch.Size = new System.Drawing.Size(98, 28);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = " اسم الزبون :";
            this.labelSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBoxAdd);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel2.Size = new System.Drawing.Size(984, 131);
            this.panel2.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(5, 134);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(984, 392);
            this.dataGridView1.TabIndex = 7;
            // 
            // dHKDBDataSet
            // 
            this.dHKDBDataSet.DataSetName = "DHKDBDataSet";
            this.dHKDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dHKDBDataSetBindingSource
            // 
            this.dHKDBDataSetBindingSource.DataSource = this.dHKDBDataSet;
            this.dHKDBDataSetBindingSource.Position = 0;
            // 
            // Receipt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelBottom);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Receipt";
            this.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(994, 600);
            this.panelBottom.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxAdd.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dHKDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dHKDBDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnSavePrint;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.TextBox txtCurrency;
        private System.Windows.Forms.BindingSource dHKDBDataSetBindingSource;
        private DHKDBDataSet dHKDBDataSet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.TextBox txtprice;
        private System.Windows.Forms.ComboBox comboProducts;
        private System.Windows.Forms.TextBox txtCartons;
        private System.Windows.Forms.Label lblPairs;
        private System.Windows.Forms.Label lblCartons;
        private System.Windows.Forms.TextBox txtcouple;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Payment;
        private System.Windows.Forms.CheckBox datetime_Checkbox;
        private System.Windows.Forms.Panel panel9;
    }
}
