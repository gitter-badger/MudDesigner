namespace MudDesigner.Editor
{
    partial class frmRealms
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
            this.realmsBtnDeleteRealm = new System.Windows.Forms.Button();
            this.realmsBtnAddRealm = new System.Windows.Forms.Button();
            this.realmsLstExistingRealms = new System.Windows.Forms.ListBox();
            this.grpRealmProperties = new System.Windows.Forms.GroupBox();
            this.realmsProperties = new System.Windows.Forms.PropertyGrid();
            this.groupBox1.SuspendLayout();
            this.grpRealmProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.realmsBtnDeleteRealm);
            this.groupBox1.Controls.Add(this.realmsBtnAddRealm);
            this.groupBox1.Controls.Add(this.realmsLstExistingRealms);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 410);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Existing Realms";
            // 
            // realmsBtnDeleteRealm
            // 
            this.realmsBtnDeleteRealm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.realmsBtnDeleteRealm.Location = new System.Drawing.Point(133, 19);
            this.realmsBtnDeleteRealm.Name = "realmsBtnDeleteRealm";
            this.realmsBtnDeleteRealm.Size = new System.Drawing.Size(115, 32);
            this.realmsBtnDeleteRealm.TabIndex = 2;
            this.realmsBtnDeleteRealm.Text = "Delete Realm";
            this.realmsBtnDeleteRealm.UseVisualStyleBackColor = true;
            this.realmsBtnDeleteRealm.Click += new System.EventHandler(this.realmsBtnDeleteRealm_Click);
            // 
            // realmsBtnAddRealm
            // 
            this.realmsBtnAddRealm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.realmsBtnAddRealm.Location = new System.Drawing.Point(6, 19);
            this.realmsBtnAddRealm.Name = "realmsBtnAddRealm";
            this.realmsBtnAddRealm.Size = new System.Drawing.Size(115, 32);
            this.realmsBtnAddRealm.TabIndex = 1;
            this.realmsBtnAddRealm.Text = "Create Realm";
            this.realmsBtnAddRealm.UseVisualStyleBackColor = true;
            this.realmsBtnAddRealm.Click += new System.EventHandler(this.realmsBtnAddRealm_Click);
            // 
            // realmsLstExistingRealms
            // 
            this.realmsLstExistingRealms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.realmsLstExistingRealms.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.realmsLstExistingRealms.ForeColor = System.Drawing.Color.White;
            this.realmsLstExistingRealms.FormattingEnabled = true;
            this.realmsLstExistingRealms.Location = new System.Drawing.Point(3, 65);
            this.realmsLstExistingRealms.Name = "realmsLstExistingRealms";
            this.realmsLstExistingRealms.Size = new System.Drawing.Size(248, 342);
            this.realmsLstExistingRealms.Sorted = true;
            this.realmsLstExistingRealms.TabIndex = 0;
            this.realmsLstExistingRealms.SelectedIndexChanged += new System.EventHandler(this.realmsLstExistingRealms_SelectedIndexChanged);
            this.realmsLstExistingRealms.DoubleClick += new System.EventHandler(this.realmsLstExistingRealms_DoubleClick);
            // 
            // grpRealmProperties
            // 
            this.grpRealmProperties.Controls.Add(this.realmsProperties);
            this.grpRealmProperties.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpRealmProperties.ForeColor = System.Drawing.Color.White;
            this.grpRealmProperties.Location = new System.Drawing.Point(260, 0);
            this.grpRealmProperties.Name = "grpRealmProperties";
            this.grpRealmProperties.Size = new System.Drawing.Size(282, 410);
            this.grpRealmProperties.TabIndex = 1;
            this.grpRealmProperties.TabStop = false;
            this.grpRealmProperties.Text = "Realm Properties";
            // 
            // realmsProperties
            // 
            this.realmsProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.realmsProperties.CanShowVisualStyleGlyphs = false;
            this.realmsProperties.CategorySplitterColor = System.Drawing.Color.White;
            this.realmsProperties.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.realmsProperties.CommandsForeColor = System.Drawing.Color.White;
            this.realmsProperties.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.realmsProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.realmsProperties.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.realmsProperties.HelpForeColor = System.Drawing.Color.White;
            this.realmsProperties.HelpVisible = false;
            this.realmsProperties.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.realmsProperties.Location = new System.Drawing.Point(3, 16);
            this.realmsProperties.Name = "realmsProperties";
            this.realmsProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.realmsProperties.Size = new System.Drawing.Size(276, 391);
            this.realmsProperties.TabIndex = 1;
            this.realmsProperties.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.realmsProperties.ViewForeColor = System.Drawing.Color.White;
            this.realmsProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.realmsProperties_PropertyValueChanged);
            // 
            // frmRealms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(542, 410);
            this.Controls.Add(this.grpRealmProperties);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRealms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mud Designer Editor : Realms";
            this.Load += new System.EventHandler(this.frmRealms_Load);
            this.groupBox1.ResumeLayout(false);
            this.grpRealmProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox realmsLstExistingRealms;
        private System.Windows.Forms.Button realmsBtnDeleteRealm;
        private System.Windows.Forms.Button realmsBtnAddRealm;
        private System.Windows.Forms.GroupBox grpRealmProperties;
        private System.Windows.Forms.PropertyGrid realmsProperties;
    }
}