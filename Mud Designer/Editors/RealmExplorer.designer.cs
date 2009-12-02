namespace MudDesigner.Editors
{
    partial class RealmExplorer
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lstRealms = new System.Windows.Forms.ListBox();
            this.propertyRealm = new System.Windows.Forms.PropertyGrid();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRemoveZone = new System.Windows.Forms.Button();
            this.btnPlaceZone = new System.Windows.Forms.Button();
            this.btnBuildZone = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstZonesInRealm = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstAvailableZones = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.btnValidateScript = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSaveRealm = new System.Windows.Forms.Button();
            this.btnDeleteRealm = new System.Windows.Forms.Button();
            this.btnNewRealm = new System.Windows.Forms.Button();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(465, 488);
            this.splitContainer1.SplitterDistance = 193;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lstRealms);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyRealm);
            this.splitContainer2.Size = new System.Drawing.Size(193, 488);
            this.splitContainer2.SplitterDistance = 220;
            this.splitContainer2.TabIndex = 0;
            // 
            // lstRealms
            // 
            this.lstRealms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRealms.FormattingEnabled = true;
            this.lstRealms.Location = new System.Drawing.Point(0, 0);
            this.lstRealms.Name = "lstRealms";
            this.lstRealms.Size = new System.Drawing.Size(193, 212);
            this.lstRealms.TabIndex = 0;
            this.lstRealms.SelectedIndexChanged += new System.EventHandler(this.lstRealms_SelectedIndexChanged);
            // 
            // propertyRealm
            // 
            this.propertyRealm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyRealm.Location = new System.Drawing.Point(0, 0);
            this.propertyRealm.Name = "propertyRealm";
            this.propertyRealm.Size = new System.Drawing.Size(193, 264);
            this.propertyRealm.TabIndex = 0;
            this.propertyRealm.ToolbarVisible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnRemoveZone);
            this.groupBox4.Controls.Add(this.btnPlaceZone);
            this.groupBox4.Controls.Add(this.btnBuildZone);
            this.groupBox4.Location = new System.Drawing.Point(2, 256);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 88);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Realm Zone Setup";
            // 
            // btnRemoveZone
            // 
            this.btnRemoveZone.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRemoveZone.Location = new System.Drawing.Point(3, 62);
            this.btnRemoveZone.Name = "btnRemoveZone";
            this.btnRemoveZone.Size = new System.Drawing.Size(254, 23);
            this.btnRemoveZone.TabIndex = 3;
            this.btnRemoveZone.Text = "Remove Zone From Realm";
            this.btnRemoveZone.UseVisualStyleBackColor = true;
            // 
            // btnPlaceZone
            // 
            this.btnPlaceZone.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPlaceZone.Location = new System.Drawing.Point(3, 39);
            this.btnPlaceZone.Name = "btnPlaceZone";
            this.btnPlaceZone.Size = new System.Drawing.Size(254, 23);
            this.btnPlaceZone.TabIndex = 2;
            this.btnPlaceZone.Text = "Place Zone In Realm";
            this.btnPlaceZone.UseVisualStyleBackColor = true;
            this.btnPlaceZone.Click += new System.EventHandler(this.btnPlaceZone_Click);
            // 
            // btnBuildZone
            // 
            this.btnBuildZone.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBuildZone.Location = new System.Drawing.Point(3, 16);
            this.btnBuildZone.Name = "btnBuildZone";
            this.btnBuildZone.Size = new System.Drawing.Size(254, 23);
            this.btnBuildZone.TabIndex = 1;
            this.btnBuildZone.Text = "Build A Zone";
            this.btnBuildZone.UseVisualStyleBackColor = true;
            this.btnBuildZone.Click += new System.EventHandler(this.btnBuildZone_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstZonesInRealm);
            this.groupBox3.Location = new System.Drawing.Point(2, 350);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 135);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zones contained within Realm";
            // 
            // lstZonesInRealm
            // 
            this.lstZonesInRealm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstZonesInRealm.FormattingEnabled = true;
            this.lstZonesInRealm.Location = new System.Drawing.Point(3, 16);
            this.lstZonesInRealm.Name = "lstZonesInRealm";
            this.lstZonesInRealm.Size = new System.Drawing.Size(254, 108);
            this.lstZonesInRealm.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstAvailableZones);
            this.groupBox2.Location = new System.Drawing.Point(2, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 102);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Zones";
            // 
            // lstAvailableZones
            // 
            this.lstAvailableZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAvailableZones.FormattingEnabled = true;
            this.lstAvailableZones.Location = new System.Drawing.Point(3, 16);
            this.lstAvailableZones.Name = "lstAvailableZones";
            this.lstAvailableZones.Size = new System.Drawing.Size(254, 82);
            this.lstAvailableZones.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Realm Setup";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabOptions);
            this.tabControl1.Controls.Add(this.tabScript);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(259, 123);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.btnValidateScript);
            this.tabOptions.Controls.Add(this.btnClose);
            this.tabOptions.Controls.Add(this.btnSaveRealm);
            this.tabOptions.Controls.Add(this.btnDeleteRealm);
            this.tabOptions.Controls.Add(this.btnNewRealm);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(251, 97);
            this.tabOptions.TabIndex = 0;
            this.tabOptions.Text = "Explorer Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // btnValidateScript
            // 
            this.btnValidateScript.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnValidateScript.Location = new System.Drawing.Point(3, 71);
            this.btnValidateScript.Name = "btnValidateScript";
            this.btnValidateScript.Size = new System.Drawing.Size(245, 23);
            this.btnValidateScript.TabIndex = 8;
            this.btnValidateScript.Text = "Validate Script";
            this.btnValidateScript.UseVisualStyleBackColor = true;
            this.btnValidateScript.Click += new System.EventHandler(this.btnValidateScript_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(139, 35);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close Explorer";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSaveRealm
            // 
            this.btnSaveRealm.Location = new System.Drawing.Point(139, 6);
            this.btnSaveRealm.Name = "btnSaveRealm";
            this.btnSaveRealm.Size = new System.Drawing.Size(109, 23);
            this.btnSaveRealm.TabIndex = 6;
            this.btnSaveRealm.Text = "Save Realm";
            this.btnSaveRealm.UseVisualStyleBackColor = true;
            this.btnSaveRealm.Click += new System.EventHandler(this.btnSaveRealm_Click);
            // 
            // btnDeleteRealm
            // 
            this.btnDeleteRealm.Location = new System.Drawing.Point(3, 35);
            this.btnDeleteRealm.Name = "btnDeleteRealm";
            this.btnDeleteRealm.Size = new System.Drawing.Size(114, 23);
            this.btnDeleteRealm.TabIndex = 5;
            this.btnDeleteRealm.Text = "Delete Realm";
            this.btnDeleteRealm.UseVisualStyleBackColor = true;
            this.btnDeleteRealm.Click += new System.EventHandler(this.btnDeleteRealm_Click);
            // 
            // btnNewRealm
            // 
            this.btnNewRealm.Location = new System.Drawing.Point(3, 6);
            this.btnNewRealm.Name = "btnNewRealm";
            this.btnNewRealm.Size = new System.Drawing.Size(114, 23);
            this.btnNewRealm.TabIndex = 4;
            this.btnNewRealm.Text = "New Realm";
            this.btnNewRealm.UseVisualStyleBackColor = true;
            this.btnNewRealm.Click += new System.EventHandler(this.btnNewRealm_Click);
            // 
            // tabScript
            // 
            this.tabScript.Controls.Add(this.txtScript);
            this.tabScript.Location = new System.Drawing.Point(4, 22);
            this.tabScript.Name = "tabScript";
            this.tabScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabScript.Size = new System.Drawing.Size(251, 97);
            this.tabScript.TabIndex = 1;
            this.tabScript.Text = "Script";
            this.tabScript.UseVisualStyleBackColor = true;
            // 
            // txtScript
            // 
            this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScript.Location = new System.Drawing.Point(3, 3);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(245, 91);
            this.txtScript.TabIndex = 0;
            this.txtScript.Text = "";
            // 
            // RealmExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 488);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RealmExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Realm Explorer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabOptions.ResumeLayout(false);
            this.tabScript.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox lstRealms;
        private System.Windows.Forms.PropertyGrid propertyRealm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstAvailableZones;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstZonesInRealm;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPlaceZone;
        private System.Windows.Forms.Button btnBuildZone;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSaveRealm;
        private System.Windows.Forms.Button btnDeleteRealm;
        private System.Windows.Forms.Button btnNewRealm;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.RichTextBox txtScript;
        private System.Windows.Forms.Button btnValidateScript;
        private System.Windows.Forms.Button btnRemoveZone;


    }
}

