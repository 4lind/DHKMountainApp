namespace DHKMontainApp
{
    partial class costumers
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.txtEdite = new System.Windows.Forms.Button();
            this.btn_addCostumer = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.panel1.Controls.Add(this.btn_refresh);
            this.panel1.Controls.Add(this.btn_remove);
            this.panel1.Controls.Add(this.txtEdite);
            this.panel1.Controls.Add(this.btn_addCostumer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.panel1.Size = new System.Drawing.Size(984, 48);
            this.panel1.TabIndex = 0;
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
            this.btn_refresh.Size = new System.Drawing.Size(48, 47);
            this.btn_refresh.TabIndex = 17;
            this.btn_refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_refresh.UseVisualStyleBackColor = false;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click_1);
            // 
            // btn_remove
            // 
            this.btn_remove.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_remove.FlatAppearance.BorderSize = 0;
            this.btn_remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btn_remove.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_remove.Image = global::DHKMontainApp.Properties.Resources.delete_forever_24dp_FFFFFF_FILL0_wght400_GRAD0_opsz24;
            this.btn_remove.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_remove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_remove.Location = new System.Drawing.Point(646, 0);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btn_remove.Size = new System.Drawing.Size(89, 47);
            this.btn_remove.TabIndex = 16;
            this.btn_remove.Text = "مسح";
            this.btn_remove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_remove.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // txtEdite
            // 
            this.txtEdite.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtEdite.FlatAppearance.BorderSize = 0;
            this.txtEdite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtEdite.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtEdite.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEdite.Image = global::DHKMontainApp.Properties.Resources.edit_24dp_F3F3F3_FILL0_wght400_GRAD0_opsz24;
            this.txtEdite.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtEdite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtEdite.Location = new System.Drawing.Point(735, 0);
            this.txtEdite.Name = "txtEdite";
            this.txtEdite.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.txtEdite.Size = new System.Drawing.Size(100, 47);
            this.txtEdite.TabIndex = 14;
            this.txtEdite.Text = "تعديل ";
            this.txtEdite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtEdite.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.txtEdite.UseVisualStyleBackColor = true;
            this.txtEdite.Click += new System.EventHandler(this.txtEdite_Click);
            // 
            // btn_addCostumer
            // 
            this.btn_addCostumer.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_addCostumer.FlatAppearance.BorderSize = 0;
            this.btn_addCostumer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_addCostumer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btn_addCostumer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_addCostumer.Image = global::DHKMontainApp.Properties.Resources.addperson;
            this.btn_addCostumer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_addCostumer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_addCostumer.Location = new System.Drawing.Point(835, 0);
            this.btn_addCostumer.Name = "btn_addCostumer";
            this.btn_addCostumer.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btn_addCostumer.Size = new System.Drawing.Size(149, 47);
            this.btn_addCostumer.TabIndex = 2;
            this.btn_addCostumer.Text = "  إضافة الزبون";
            this.btn_addCostumer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_addCostumer.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_addCostumer.UseVisualStyleBackColor = true;
            this.btn_addCostumer.Click += new System.EventHandler(this.btn_addCostumer_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(26, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 1);
            this.panel2.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 1);
            this.panel3.TabIndex = 11;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(77)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.CausesValidation = false;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.SystemColors.Control;
            this.txtSearch.Location = new System.Drawing.Point(26, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch.Size = new System.Drawing.Size(250, 19);
            this.txtSearch.TabIndex = 12;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(282, 7);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "البحث عن الزبون :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(77)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(984, 511);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(77)))));
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(5, 51);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(984, 35);
            this.panel4.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.txtSearch);
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(594, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(390, 35);
            this.panel5.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dataGridView1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(5, 86);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(984, 511);
            this.panel6.TabIndex = 3;
            // 
            // costumers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "costumers";
            this.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Size = new System.Drawing.Size(994, 600);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_addCostumer;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button txtEdite;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
    }
}
