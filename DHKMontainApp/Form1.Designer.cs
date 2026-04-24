namespace DHKMontainApp
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_datetime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.homePanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_dashboard = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSetting = new System.Windows.Forms.Button();
            this.btn_paid = new System.Windows.Forms.Button();
            this.btn_sale = new System.Windows.Forms.Button();
            this.btn_payment = new System.Windows.Forms.Button();
            this.btn_buy = new System.Windows.Forms.Button();
            this.btn_item = new System.Windows.Forms.Button();
            this.btn_costumer = new System.Windows.Forms.Button();
            this.btn_Home = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panel2.Controls.Add(this.btnSetting);
            this.panel2.Controls.Add(this.btn_paid);
            this.panel2.Controls.Add(this.btn_sale);
            this.panel2.Controls.Add(this.btn_payment);
            this.panel2.Controls.Add(this.btn_buy);
            this.panel2.Controls.Add(this.lbl_datetime);
            this.panel2.Controls.Add(this.lblDate);
            this.panel2.Controls.Add(this.btn_item);
            this.panel2.Controls.Add(this.btn_costumer);
            this.panel2.Controls.Add(this.btn_Home);
            this.panel2.Controls.Add(this.panelLogo);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // lbl_datetime
            // 
            resources.ApplyResources(this.lbl_datetime, "lbl_datetime");
            this.lbl_datetime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.lbl_datetime.Name = "lbl_datetime";
            // 
            // lblDate
            // 
            resources.ApplyResources(this.lblDate, "lblDate");
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.lblDate.Name = "lblDate";
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panelLogo.Controls.Add(this.pictureBox1);
            this.panelLogo.Controls.Add(this.label1);
            resources.ApplyResources(this.panelLogo, "panelLogo");
            this.panelLogo.Name = "panelLogo";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Name = "label1";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.homePanel);
            this.panel8.Controls.Add(this.panel1);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // homePanel
            // 
            this.homePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(77)))));
            resources.ApplyResources(this.homePanel, "homePanel");
            this.homePanel.Name = "homePanel";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_dashboard);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lbl_dashboard
            // 
            this.lbl_dashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(77)))));
            resources.ApplyResources(this.lbl_dashboard, "lbl_dashboard");
            this.lbl_dashboard.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_dashboard.Name = "lbl_dashboard";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSetting
            // 
            resources.ApplyResources(this.btnSetting, "btnSetting");
            this.btnSetting.FlatAppearance.BorderSize = 0;
            this.btnSetting.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSetting.Image = global::DHKMontainApp.Properties.Resources.settings_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24;
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btn_paid
            // 
            resources.ApplyResources(this.btn_paid, "btn_paid");
            this.btn_paid.FlatAppearance.BorderSize = 0;
            this.btn_paid.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_paid.Image = global::DHKMontainApp.Properties.Resources.trolley_24dp_FFFFFF_FILL0_wght400_GRAD0_opsz24;
            this.btn_paid.Name = "btn_paid";
            this.btn_paid.UseVisualStyleBackColor = true;
            this.btn_paid.Click += new System.EventHandler(this.btn_paid_Click);
            // 
            // btn_sale
            // 
            resources.ApplyResources(this.btn_sale, "btn_sale");
            this.btn_sale.FlatAppearance.BorderSize = 0;
            this.btn_sale.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_sale.Image = global::DHKMontainApp.Properties.Resources.sales;
            this.btn_sale.Name = "btn_sale";
            this.btn_sale.UseVisualStyleBackColor = true;
            this.btn_sale.Click += new System.EventHandler(this.btn_sale_Click);
            // 
            // btn_payment
            // 
            resources.ApplyResources(this.btn_payment, "btn_payment");
            this.btn_payment.FlatAppearance.BorderSize = 0;
            this.btn_payment.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_payment.Image = global::DHKMontainApp.Properties.Resources.Sell;
            this.btn_payment.Name = "btn_payment";
            this.btn_payment.UseVisualStyleBackColor = true;
            this.btn_payment.Click += new System.EventHandler(this.btn_payment_Click_1);
            // 
            // btn_buy
            // 
            resources.ApplyResources(this.btn_buy, "btn_buy");
            this.btn_buy.FlatAppearance.BorderSize = 0;
            this.btn_buy.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_buy.Image = global::DHKMontainApp.Properties.Resources.payment;
            this.btn_buy.Name = "btn_buy";
            this.btn_buy.UseVisualStyleBackColor = true;
            this.btn_buy.Click += new System.EventHandler(this.btn_buy_Click);
            // 
            // btn_item
            // 
            resources.ApplyResources(this.btn_item, "btn_item");
            this.btn_item.FlatAppearance.BorderSize = 0;
            this.btn_item.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_item.Image = global::DHKMontainApp.Properties.Resources.items;
            this.btn_item.Name = "btn_item";
            this.btn_item.UseVisualStyleBackColor = true;
            this.btn_item.Click += new System.EventHandler(this.btn_item_Click);
            // 
            // btn_costumer
            // 
            resources.ApplyResources(this.btn_costumer, "btn_costumer");
            this.btn_costumer.FlatAppearance.BorderSize = 0;
            this.btn_costumer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_costumer.Image = global::DHKMontainApp.Properties.Resources.person;
            this.btn_costumer.Name = "btn_costumer";
            this.btn_costumer.UseVisualStyleBackColor = true;
            this.btn_costumer.Click += new System.EventHandler(this.btn_costumer_Click);
            // 
            // btn_Home
            // 
            resources.ApplyResources(this.btn_Home, "btn_Home");
            this.btn_Home.FlatAppearance.BorderSize = 0;
            this.btn_Home.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Home.Image = global::DHKMontainApp.Properties.Resources.Home;
            this.btn_Home.Name = "btn_Home";
            this.btn_Home.UseVisualStyleBackColor = true;
            this.btn_Home.Click += new System.EventHandler(this.btn_Home_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(95)))));
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.panel2.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Button btn_Home;
        private System.Windows.Forms.Button btn_item;
        private System.Windows.Forms.Button btn_costumer;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_dashboard;
        private System.Windows.Forms.Panel homePanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lbl_datetime;
        private System.Windows.Forms.Button btn_buy;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btn_paid;
        private System.Windows.Forms.Button btn_sale;
        private System.Windows.Forms.Button btn_payment;
    }
}

