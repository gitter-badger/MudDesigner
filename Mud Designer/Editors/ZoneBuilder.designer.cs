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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstZones = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelectRealm = new System.Windows.Forms.Button();
            this.btnLoadRealm = new System.Windows.Forms.Button();
            this.btnSaveRealm = new System.Windows.Forms.Button();
            this.btnDeleteRealm = new System.Windows.Forms.Button();
            this.btnNewRealm = new System.Windows.Forms.Button();
            this.tabZoneBuilder = new System.Windows.Forms.TabControl();
            this.tabZone = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.propertyZone = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Help = new System.Windows.Forms.ToolTip(this.components);
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabZoneBuilder.SuspendLayout();
            this.tabZone.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.tabZoneBuilder);
            this.containerMain.Size = new System.Drawing.Size(650, 471);
            this.containerMain.SplitterDistance = 195;
            this.containerMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstZones);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 371);
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
            this.lstZones.Size = new System.Drawing.Size(189, 342);
            this.lstZones.Sorted = true;
            this.lstZones.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelectRealm);
            this.groupBox2.Controls.Add(this.btnLoadRealm);
            this.groupBox2.Controls.Add(this.btnSaveRealm);
            this.groupBox2.Controls.Add(this.btnDeleteRealm);
            this.groupBox2.Controls.Add(this.btnNewRealm);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 100);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zone Setup";
            // 
            // btnSelectRealm
            // 
            this.btnSelectRealm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSelectRealm.Location = new System.Drawing.Point(3, 74);
            this.btnSelectRealm.Name = "btnSelectRealm";
            this.btnSelectRealm.Size = new System.Drawing.Size(189, 23);
            this.btnSelectRealm.TabIndex = 12;
            this.btnSelectRealm.Text = "Select Realm";
            this.btnSelectRealm.UseVisualStyleBackColor = true;
            this.btnSelectRealm.Click += new System.EventHandler(this.btnSelectRealm_Click);
            // 
            // btnLoadRealm
            // 
            this.btnLoadRealm.Location = new System.Drawing.Point(106, 19);
            this.btnLoadRealm.Name = "btnLoadRealm";
            this.btnLoadRealm.Size = new System.Drawing.Size(85, 23);
            this.btnLoadRealm.TabIndex = 11;
            this.btnLoadRealm.Text = "Load Zone";
            this.btnLoadRealm.UseVisualStyleBackColor = true;
            this.btnLoadRealm.Click += new System.EventHandler(this.btnLoadRealm_Click);
            // 
            // btnSaveRealm
            // 
            this.btnSaveRealm.Location = new System.Drawing.Point(107, 48);
            this.btnSaveRealm.Name = "btnSaveRealm";
            this.btnSaveRealm.Size = new System.Drawing.Size(84, 23);
            this.btnSaveRealm.TabIndex = 10;
            this.btnSaveRealm.Text = "Save Zone";
            this.btnSaveRealm.UseVisualStyleBackColor = true;
            this.btnSaveRealm.Click += new System.EventHandler(this.btnSaveRealm_Click);
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
            this.btnNewRealm.Click += new System.EventHandler(this.btnNewRealm_Click);
            // 
            // tabZoneBuilder
            // 
            this.tabZoneBuilder.Controls.Add(this.tabZone);
            this.tabZoneBuilder.Controls.Add(this.tabPage2);
            this.tabZoneBuilder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabZoneBuilder.Location = new System.Drawing.Point(0, 0);
            this.tabZoneBuilder.Name = "tabZoneBuilder";
            this.tabZoneBuilder.SelectedIndex = 0;
            this.tabZoneBuilder.Size = new System.Drawing.Size(451, 471);
            this.tabZoneBuilder.TabIndex = 0;
            // 
            // tabZone
            // 
            this.tabZone.Controls.Add(this.groupBox3);
            this.tabZone.Location = new System.Drawing.Point(4, 22);
            this.tabZone.Name = "tabZone";
            this.tabZone.Padding = new System.Windows.Forms.Padding(3);
            this.tabZone.Size = new System.Drawing.Size(443, 445);
            this.tabZone.TabIndex = 0;
            this.tabZone.Text = "Zone Editing";
            this.tabZone.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.propertyZone);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 439);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zone Properties";
            // 
            // propertyZone
            // 
            this.propertyZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyZone.Location = new System.Drawing.Point(3, 16);
            this.propertyZone.Name = "propertyZone";
            this.propertyZone.Size = new System.Drawing.Size(247, 420);
            this.propertyZone.TabIndex = 0;
            this.propertyZone.ToolbarVisible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(443, 445);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Help
            // 
            this.Help.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Help.IsBalloon = true;
            this.Help.ToolTipTitle = "Zone Designer";
            // 
            // ZoneBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 471);
            this.Controls.Add(this.containerMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZoneBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zone Builder (No Realm Selected)";
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            this.containerMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabZoneBuilder.ResumeLayout(false);
            this.tabZone.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.ToolTip Help;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadRealm;
        private System.Windows.Forms.Button btnSaveRealm;
        private System.Windows.Forms.Button btnDeleteRealm;
        private System.Windows.Forms.Button btnNewRealm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstZones;
        private System.Windows.Forms.TabControl tabZoneBuilder;
        private System.Windows.Forms.TabPage tabZone;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PropertyGrid propertyZone;
        private System.Windows.Forms.Button btnSelectRealm;

    }
}