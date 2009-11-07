namespace CurrencyEditor
{
    partial class frmMain
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstCurrencies = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnNewCurrency = new System.Windows.Forms.Button();
            this.btnSaveCurrency = new System.Windows.Forms.Button();
            this.btnDeleteCurrency = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.propertyGrid1);
            this.groupBox2.Location = new System.Drawing.Point(1, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 214);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Currency Setup";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 16);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(317, 195);
            this.propertyGrid1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstCurrencies);
            this.groupBox1.Location = new System.Drawing.Point(1, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 130);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Currencies";
            // 
            // lstCurrencies
            // 
            this.lstCurrencies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCurrencies.FormattingEnabled = true;
            this.lstCurrencies.Location = new System.Drawing.Point(3, 16);
            this.lstCurrencies.Name = "lstCurrencies";
            this.lstCurrencies.Size = new System.Drawing.Size(317, 108);
            this.lstCurrencies.TabIndex = 0;
            this.lstCurrencies.SelectedIndexChanged += new System.EventHandler(this.lstCurrencies_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDeleteCurrency);
            this.groupBox3.Controls.Add(this.btnSaveCurrency);
            this.groupBox3.Controls.Add(this.btnNewCurrency);
            this.groupBox3.Location = new System.Drawing.Point(1, 353);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(323, 54);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // btnNewCurrency
            // 
            this.btnNewCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewCurrency.Location = new System.Drawing.Point(3, 16);
            this.btnNewCurrency.Name = "btnNewCurrency";
            this.btnNewCurrency.Size = new System.Drawing.Size(100, 31);
            this.btnNewCurrency.TabIndex = 0;
            this.btnNewCurrency.Text = "New Currency";
            this.btnNewCurrency.UseVisualStyleBackColor = true;
            this.btnNewCurrency.Click += new System.EventHandler(this.btnNewCurrency_Click);
            // 
            // btnSaveCurrency
            // 
            this.btnSaveCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCurrency.Location = new System.Drawing.Point(109, 16);
            this.btnSaveCurrency.Name = "btnSaveCurrency";
            this.btnSaveCurrency.Size = new System.Drawing.Size(100, 31);
            this.btnSaveCurrency.TabIndex = 1;
            this.btnSaveCurrency.Text = "Save Currency";
            this.btnSaveCurrency.UseVisualStyleBackColor = true;
            this.btnSaveCurrency.Click += new System.EventHandler(this.btnSaveCurrency_Click);
            // 
            // btnDeleteCurrency
            // 
            this.btnDeleteCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCurrency.Location = new System.Drawing.Point(215, 16);
            this.btnDeleteCurrency.Name = "btnDeleteCurrency";
            this.btnDeleteCurrency.Size = new System.Drawing.Size(100, 31);
            this.btnDeleteCurrency.TabIndex = 2;
            this.btnDeleteCurrency.Text = "Delete Currency";
            this.btnDeleteCurrency.UseVisualStyleBackColor = true;
            this.btnDeleteCurrency.Click += new System.EventHandler(this.btnDeleteCurrency_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 410);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Currency Editor";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstCurrencies;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnNewCurrency;
        private System.Windows.Forms.Button btnSaveCurrency;
        private System.Windows.Forms.Button btnDeleteCurrency;

    }
}

