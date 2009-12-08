namespace MudDesigner.Editors
{
    partial class ZoneBuilder
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
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoadRealm = new System.Windows.Forms.Button();
            this.btnSaveRealm = new System.Windows.Forms.Button();
            this.btnDeleteRealm = new System.Windows.Forms.Button();
            this.btnNewRealm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstZones = new System.Windows.Forms.ListBox();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerMain
            // 
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.Location = new System.Drawing.Point(0, 0);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.groupBox1);
            this.containerMain.Panel1.Controls.Add(this.groupBox2);
            this.containerMain.Size = new System.Drawing.Size(758, 471);
            this.containerMain.SplitterDistance = 203;
            this.containerMain.TabIndex = 0;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Zone Designer";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoadRealm);
            this.groupBox2.Controls.Add(this.btnSaveRealm);
            this.groupBox2.Controls.Add(this.btnDeleteRealm);
            this.groupBox2.Controls.Add(this.btnNewRealm);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(203, 76);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zone Setup";
            // 
            // btnLoadRealm
            // 
            this.btnLoadRealm.Location = new System.Drawing.Point(106, 19);
            this.btnLoadRealm.Name = "btnLoadRealm";
            this.btnLoadRealm.Size = new System.Drawing.Size(85, 23);
            this.btnLoadRealm.TabIndex = 11;
            this.btnLoadRealm.Text = "Load Zone";
            this.btnLoadRealm.UseVisualStyleBackColor = true;
            // 
            // btnSaveRealm
            // 
            this.btnSaveRealm.Location = new System.Drawing.Point(107, 48);
            this.btnSaveRealm.Name = "btnSaveRealm";
            this.btnSaveRealm.Size = new System.Drawing.Size(84, 23);
            this.btnSaveRealm.TabIndex = 10;
            this.btnSaveRealm.Text = "Save Zone";
            this.btnSaveRealm.UseVisualStyleBackColor = true;
            // 
            // btnDeleteRealm
            // 
            this.btnDeleteRealm.Location = new System.Drawing.Point(6, 48);
            this.btnDeleteRealm.Name = "btnDeleteRealm";
            this.btnDeleteRealm.Size = new System.Drawing.Size(85, 23);
            this.btnDeleteRealm.TabIndex = 9;
            this.btnDeleteRealm.Text = "Delete Zone";
            this.btnDeleteRealm.UseVisualStyleBackColor = true;
            // 
            // btnNewRealm
            // 
            this.btnNewRealm.Location = new System.Drawing.Point(6, 19);
            this.btnNewRealm.Name = "btnNewRealm";
            this.btnNewRealm.Size = new System.Drawing.Size(85, 23);
            this.btnNewRealm.TabIndex = 8;
            this.btnNewRealm.Text = "New Zone";
            this.btnNewRealm.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstZones);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 395);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zone List";
            // 
            // lstZones
            // 
            this.lstZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstZones.FormattingEnabled = true;
            this.lstZones.Location = new System.Drawing.Point(3, 16);
            this.lstZones.Name = "lstZones";
            this.lstZones.Size = new System.Drawing.Size(197, 368);
            this.lstZones.Sorted = true;
            this.lstZones.TabIndex = 17;
            // 
            // ZoneBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 471);
            this.Controls.Add(this.containerMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZoneBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zone Builder";
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadRealm;
        private System.Windows.Forms.Button btnSaveRealm;
        private System.Windows.Forms.Button btnDeleteRealm;
        private System.Windows.Forms.Button btnNewRealm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstZones;

    }
}