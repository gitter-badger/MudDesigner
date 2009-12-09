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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabRealm = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstRealms = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoadRealm = new System.Windows.Forms.Button();
            this.btnSaveRealm = new System.Windows.Forms.Button();
            this.btnDeleteRealm = new System.Windows.Forms.Button();
            this.btnNewRealm = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.propertyRealm = new System.Windows.Forms.PropertyGrid();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabRealm.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Load Realm";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(127, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Save Realm";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Delete Realm";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "New Realm";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabRealm);
            this.tabControl1.Controls.Add(this.tabScript);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(470, 464);
            this.tabControl1.TabIndex = 0;
            // 
            // tabRealm
            // 
            this.tabRealm.Controls.Add(this.splitContainer1);
            this.tabRealm.Location = new System.Drawing.Point(4, 22);
            this.tabRealm.Name = "tabRealm";
            this.tabRealm.Padding = new System.Windows.Forms.Padding(3);
            this.tabRealm.Size = new System.Drawing.Size(462, 438);
            this.tabRealm.TabIndex = 0;
            this.tabRealm.Text = "Realm Setup";
            this.tabRealm.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(456, 432);
            this.splitContainer1.SplitterDistance = 196;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstRealms);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 356);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Realms List";
            // 
            // lstRealms
            // 
            this.lstRealms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRealms.FormattingEnabled = true;
            this.lstRealms.Location = new System.Drawing.Point(3, 16);
            this.lstRealms.Name = "lstRealms";
            this.lstRealms.Size = new System.Drawing.Size(190, 329);
            this.lstRealms.Sorted = true;
            this.lstRealms.TabIndex = 17;
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
            this.groupBox2.Size = new System.Drawing.Size(196, 76);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Realm Setup";
            // 
            // btnLoadRealm
            // 
            this.btnLoadRealm.Location = new System.Drawing.Point(106, 19);
            this.btnLoadRealm.Name = "btnLoadRealm";
            this.btnLoadRealm.Size = new System.Drawing.Size(85, 23);
            this.btnLoadRealm.TabIndex = 11;
            this.btnLoadRealm.Text = "Load Realm";
            this.btnLoadRealm.UseVisualStyleBackColor = true;
            this.btnLoadRealm.Click += new System.EventHandler(this.btnLoadRealm_Click);
            // 
            // btnSaveRealm
            // 
            this.btnSaveRealm.Location = new System.Drawing.Point(107, 48);
            this.btnSaveRealm.Name = "btnSaveRealm";
            this.btnSaveRealm.Size = new System.Drawing.Size(84, 23);
            this.btnSaveRealm.TabIndex = 10;
            this.btnSaveRealm.Text = "Save Realm";
            this.btnSaveRealm.UseVisualStyleBackColor = true;
            this.btnSaveRealm.Click += new System.EventHandler(this.btnSaveRealm_Click);
            // 
            // btnDeleteRealm
            // 
            this.btnDeleteRealm.Location = new System.Drawing.Point(6, 48);
            this.btnDeleteRealm.Name = "btnDeleteRealm";
            this.btnDeleteRealm.Size = new System.Drawing.Size(85, 23);
            this.btnDeleteRealm.TabIndex = 9;
            this.btnDeleteRealm.Text = "Delete Realm";
            this.btnDeleteRealm.UseVisualStyleBackColor = true;
            this.btnDeleteRealm.Click += new System.EventHandler(this.btnDeleteRealm_Click);
            // 
            // btnNewRealm
            // 
            this.btnNewRealm.Location = new System.Drawing.Point(6, 19);
            this.btnNewRealm.Name = "btnNewRealm";
            this.btnNewRealm.Size = new System.Drawing.Size(85, 23);
            this.btnNewRealm.TabIndex = 8;
            this.btnNewRealm.Text = "New Realm";
            this.btnNewRealm.UseVisualStyleBackColor = true;
            this.btnNewRealm.Click += new System.EventHandler(this.btnNewRealm_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.propertyRealm);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 432);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Realm Properties";
            // 
            // propertyRealm
            // 
            this.propertyRealm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyRealm.Location = new System.Drawing.Point(3, 16);
            this.propertyRealm.Name = "propertyRealm";
            this.propertyRealm.Size = new System.Drawing.Size(250, 413);
            this.propertyRealm.TabIndex = 18;
            this.propertyRealm.ToolbarVisible = false;
            // 
            // tabScript
            // 
            this.tabScript.Controls.Add(this.txtScript);
            this.tabScript.Location = new System.Drawing.Point(4, 22);
            this.tabScript.Name = "tabScript";
            this.tabScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabScript.Size = new System.Drawing.Size(462, 438);
            this.tabScript.TabIndex = 1;
            this.tabScript.Text = "Realm Script";
            this.tabScript.UseVisualStyleBackColor = true;
            // 
            // txtScript
            // 
            this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScript.Location = new System.Drawing.Point(3, 3);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(456, 432);
            this.txtScript.TabIndex = 0;
            this.txtScript.Text = "";
            this.txtScript.TextChanged += new System.EventHandler(this.txtScript_TextChanged);
            // 
            // RealmExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 464);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RealmExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Realm Explorer";
            this.tabControl1.ResumeLayout(false);
            this.tabRealm.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabScript.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabRealm;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstRealms;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadRealm;
        private System.Windows.Forms.Button btnSaveRealm;
        private System.Windows.Forms.Button btnDeleteRealm;
        private System.Windows.Forms.Button btnNewRealm;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PropertyGrid propertyRealm;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.RichTextBox txtScript;



    }
}

