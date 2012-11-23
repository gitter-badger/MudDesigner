namespace MudDesigner.Editor
{
    partial class frmZones
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.zonesGrpRealm = new System.Windows.Forms.GroupBox();
            this.zonesLstExistingZones = new System.Windows.Forms.ListBox();
            this.zonesBtnChangeRealm = new System.Windows.Forms.Button();
            this.zonessBtnDeleteZone = new System.Windows.Forms.Button();
            this.zonesBtnAddZone = new System.Windows.Forms.Button();
            this.grpZoneProperties = new System.Windows.Forms.GroupBox();
            this.zoneProperties = new System.Windows.Forms.PropertyGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comRealms = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.zonesGrpRealm.SuspendLayout();
            this.grpZoneProperties.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.zonesGrpRealm);
            this.groupBox1.Controls.Add(this.zonesBtnChangeRealm);
            this.groupBox1.Controls.Add(this.zonessBtnDeleteZone);
            this.groupBox1.Controls.Add(this.zonesBtnAddZone);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 513);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Existing Zones";
            // 
            // zonesGrpRealm
            // 
            this.zonesGrpRealm.Controls.Add(this.groupBox2);
            this.zonesGrpRealm.Controls.Add(this.zonesLstExistingZones);
            this.zonesGrpRealm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.zonesGrpRealm.ForeColor = System.Drawing.Color.White;
            this.zonesGrpRealm.Location = new System.Drawing.Point(3, 95);
            this.zonesGrpRealm.Name = "zonesGrpRealm";
            this.zonesGrpRealm.Size = new System.Drawing.Size(248, 415);
            this.zonesGrpRealm.TabIndex = 4;
            this.zonesGrpRealm.TabStop = false;
            this.zonesGrpRealm.Text = "No Realm loaded.";
            // 
            // zonesLstExistingZones
            // 
            this.zonesLstExistingZones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.zonesLstExistingZones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.zonesLstExistingZones.ForeColor = System.Drawing.Color.White;
            this.zonesLstExistingZones.FormattingEnabled = true;
            this.zonesLstExistingZones.Location = new System.Drawing.Point(3, 70);
            this.zonesLstExistingZones.Name = "zonesLstExistingZones";
            this.zonesLstExistingZones.Size = new System.Drawing.Size(242, 342);
            this.zonesLstExistingZones.Sorted = true;
            this.zonesLstExistingZones.TabIndex = 1;
            this.zonesLstExistingZones.SelectedIndexChanged += new System.EventHandler(this.zonesLstExistingZones_SelectedIndexChanged);
            this.zonesLstExistingZones.DoubleClick += new System.EventHandler(this.zonesLstExistingZones_DoubleClick_1);
            // 
            // zonesBtnChangeRealm
            // 
            this.zonesBtnChangeRealm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zonesBtnChangeRealm.Location = new System.Drawing.Point(6, 57);
            this.zonesBtnChangeRealm.Name = "zonesBtnChangeRealm";
            this.zonesBtnChangeRealm.Size = new System.Drawing.Size(242, 32);
            this.zonesBtnChangeRealm.TabIndex = 3;
            this.zonesBtnChangeRealm.Text = "Change Current Realm";
            this.zonesBtnChangeRealm.UseVisualStyleBackColor = true;
            this.zonesBtnChangeRealm.Click += new System.EventHandler(this.zonesBtnChangeRealm_Click);
            // 
            // zonessBtnDeleteZone
            // 
            this.zonessBtnDeleteZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zonessBtnDeleteZone.Location = new System.Drawing.Point(133, 19);
            this.zonessBtnDeleteZone.Name = "zonessBtnDeleteZone";
            this.zonessBtnDeleteZone.Size = new System.Drawing.Size(115, 32);
            this.zonessBtnDeleteZone.TabIndex = 2;
            this.zonessBtnDeleteZone.Text = "Delete Realm";
            this.zonessBtnDeleteZone.UseVisualStyleBackColor = true;
            this.zonessBtnDeleteZone.Click += new System.EventHandler(this.zonessBtnDeleteZone_Click);
            // 
            // zonesBtnAddZone
            // 
            this.zonesBtnAddZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zonesBtnAddZone.Location = new System.Drawing.Point(6, 19);
            this.zonesBtnAddZone.Name = "zonesBtnAddZone";
            this.zonesBtnAddZone.Size = new System.Drawing.Size(115, 32);
            this.zonesBtnAddZone.TabIndex = 1;
            this.zonesBtnAddZone.Text = "Create Zone";
            this.zonesBtnAddZone.UseVisualStyleBackColor = true;
            this.zonesBtnAddZone.Click += new System.EventHandler(this.zonesBtnAddZone_Click);
            // 
            // grpZoneProperties
            // 
            this.grpZoneProperties.Controls.Add(this.zoneProperties);
            this.grpZoneProperties.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpZoneProperties.ForeColor = System.Drawing.Color.White;
            this.grpZoneProperties.Location = new System.Drawing.Point(259, 0);
            this.grpZoneProperties.Name = "grpZoneProperties";
            this.grpZoneProperties.Size = new System.Drawing.Size(282, 513);
            this.grpZoneProperties.TabIndex = 2;
            this.grpZoneProperties.TabStop = false;
            this.grpZoneProperties.Text = "Selected Zone Properties";
            // 
            // zoneProperties
            // 
            this.zoneProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.zoneProperties.CanShowVisualStyleGlyphs = false;
            this.zoneProperties.CategorySplitterColor = System.Drawing.Color.White;
            this.zoneProperties.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.zoneProperties.CommandsForeColor = System.Drawing.Color.White;
            this.zoneProperties.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.zoneProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoneProperties.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.zoneProperties.HelpForeColor = System.Drawing.Color.White;
            this.zoneProperties.HelpVisible = false;
            this.zoneProperties.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.zoneProperties.Location = new System.Drawing.Point(3, 16);
            this.zoneProperties.Name = "zoneProperties";
            this.zoneProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.zoneProperties.Size = new System.Drawing.Size(276, 494);
            this.zoneProperties.TabIndex = 1;
            this.zoneProperties.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.zoneProperties.ViewForeColor = System.Drawing.Color.White;
            this.zoneProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.zoneProperties_PropertyValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comRealms);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(3, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 48);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Realms";
            // 
            // comRealms
            // 
            this.comRealms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.comRealms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comRealms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comRealms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comRealms.ForeColor = System.Drawing.Color.White;
            this.comRealms.FormattingEnabled = true;
            this.comRealms.Location = new System.Drawing.Point(3, 16);
            this.comRealms.Name = "comRealms";
            this.comRealms.Size = new System.Drawing.Size(233, 21);
            this.comRealms.TabIndex = 0;
            this.comRealms.SelectedIndexChanged += new System.EventHandler(this.comRealms_SelectedIndexChanged);
            // 
            // frmZones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(541, 513);
            this.Controls.Add(this.grpZoneProperties);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmZones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mud Designer Editor : Zones";
            this.Load += new System.EventHandler(this.frmZones_Load);
            this.groupBox1.ResumeLayout(false);
            this.zonesGrpRealm.ResumeLayout(false);
            this.grpZoneProperties.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button zonesBtnChangeRealm;
        private System.Windows.Forms.Button zonessBtnDeleteZone;
        private System.Windows.Forms.Button zonesBtnAddZone;
        private System.Windows.Forms.GroupBox grpZoneProperties;
        private System.Windows.Forms.PropertyGrid zoneProperties;
        private System.Windows.Forms.GroupBox zonesGrpRealm;
        private System.Windows.Forms.ListBox zonesLstExistingZones;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comRealms;
    }
}