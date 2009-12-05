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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyZone = new System.Windows.Forms.PropertyGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnValidateScript = new System.Windows.Forms.Button();
            this.btnSaveZone = new System.Windows.Forms.Button();
            this.btnNewZone = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabZoneCreation = new System.Windows.Forms.TabPage();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstRooms = new System.Windows.Forms.ListBox();
            this.btnRoomEditor = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabScript.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyZone);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(794, 574);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // propertyZone
            // 
            this.propertyZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyZone.Location = new System.Drawing.Point(0, 71);
            this.propertyZone.Name = "propertyZone";
            this.propertyZone.Size = new System.Drawing.Size(210, 503);
            this.propertyZone.TabIndex = 1;
            this.propertyZone.ToolbarVisible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnValidateScript);
            this.groupBox1.Controls.Add(this.btnSaveZone);
            this.groupBox1.Controls.Add(this.btnNewZone);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zone Setup";
            // 
            // btnValidateScript
            // 
            this.btnValidateScript.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnValidateScript.Location = new System.Drawing.Point(3, 45);
            this.btnValidateScript.Name = "btnValidateScript";
            this.btnValidateScript.Size = new System.Drawing.Size(204, 23);
            this.btnValidateScript.TabIndex = 13;
            this.btnValidateScript.Text = "Validate Script";
            this.btnValidateScript.UseVisualStyleBackColor = true;
            this.btnValidateScript.Click += new System.EventHandler(this.btnValidateScript_Click);
            // 
            // btnSaveZone
            // 
            this.btnSaveZone.Location = new System.Drawing.Point(110, 19);
            this.btnSaveZone.Name = "btnSaveZone";
            this.btnSaveZone.Size = new System.Drawing.Size(97, 23);
            this.btnSaveZone.TabIndex = 11;
            this.btnSaveZone.Text = "Save Zone";
            this.btnSaveZone.UseVisualStyleBackColor = true;
            this.btnSaveZone.Click += new System.EventHandler(this.btnSaveZone_Click);
            // 
            // btnNewZone
            // 
            this.btnNewZone.Location = new System.Drawing.Point(3, 19);
            this.btnNewZone.Name = "btnNewZone";
            this.btnNewZone.Size = new System.Drawing.Size(102, 23);
            this.btnNewZone.TabIndex = 9;
            this.btnNewZone.Text = "New Zone";
            this.btnNewZone.UseVisualStyleBackColor = true;
            this.btnNewZone.Click += new System.EventHandler(this.btnNewZone_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(580, 574);
            this.splitContainer2.SplitterDistance = 365;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabZoneCreation);
            this.tabControl1.Controls.Add(this.tabScript);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(365, 574);
            this.tabControl1.TabIndex = 0;
            // 
            // tabZoneCreation
            // 
            this.tabZoneCreation.Location = new System.Drawing.Point(4, 22);
            this.tabZoneCreation.Name = "tabZoneCreation";
            this.tabZoneCreation.Padding = new System.Windows.Forms.Padding(3);
            this.tabZoneCreation.Size = new System.Drawing.Size(357, 548);
            this.tabZoneCreation.TabIndex = 0;
            this.tabZoneCreation.Text = "Zone Creation";
            this.tabZoneCreation.UseVisualStyleBackColor = true;
            // 
            // tabScript
            // 
            this.tabScript.Controls.Add(this.txtScript);
            this.tabScript.Location = new System.Drawing.Point(4, 22);
            this.tabScript.Name = "tabScript";
            this.tabScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabScript.Size = new System.Drawing.Size(357, 548);
            this.tabScript.TabIndex = 1;
            this.tabScript.Text = "Script";
            this.tabScript.UseVisualStyleBackColor = true;
            // 
            // txtScript
            // 
            this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScript.Location = new System.Drawing.Point(3, 3);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(351, 542);
            this.txtScript.TabIndex = 0;
            this.txtScript.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstRooms);
            this.groupBox2.Controls.Add(this.btnRoomEditor);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 168);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Rooms";
            // 
            // lstRooms
            // 
            this.lstRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRooms.FormattingEnabled = true;
            this.lstRooms.Location = new System.Drawing.Point(3, 16);
            this.lstRooms.Name = "lstRooms";
            this.lstRooms.Size = new System.Drawing.Size(205, 121);
            this.lstRooms.TabIndex = 1;
            // 
            // btnRoomEditor
            // 
            this.btnRoomEditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRoomEditor.Location = new System.Drawing.Point(3, 142);
            this.btnRoomEditor.Name = "btnRoomEditor";
            this.btnRoomEditor.Size = new System.Drawing.Size(205, 23);
            this.btnRoomEditor.TabIndex = 0;
            this.btnRoomEditor.Text = "Build-A-Room";
            this.btnRoomEditor.UseVisualStyleBackColor = true;
            this.btnRoomEditor.Click += new System.EventHandler(this.btnRoomEditor_Click);
            // 
            // ZoneBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 574);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZoneBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zone Builder";
            this.Load += new System.EventHandler(this.ZoneBuilder_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabScript.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnValidateScript;
        private System.Windows.Forms.Button btnSaveZone;
        private System.Windows.Forms.Button btnNewZone;
        private System.Windows.Forms.PropertyGrid propertyZone;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstRooms;
        private System.Windows.Forms.Button btnRoomEditor;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabZoneCreation;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.RichTextBox txtScript;

    }
}