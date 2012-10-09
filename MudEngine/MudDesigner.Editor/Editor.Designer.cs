namespace MudDesigner.Editor
{
    partial class Editor
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.objectBrowser = new System.Windows.Forms.ListBox();
            this.objectProperties = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.RoomEditor_Properties = new System.Windows.Forms.SplitContainer();
            this.North = new System.Windows.Forms.Button();
            this.East = new System.Windows.Forms.Button();
            this.West = new System.Windows.Forms.Button();
            this.South = new System.Windows.Forms.Button();
            this.RoomObjects = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorContainer)).BeginInit();
            this.editorContainer.Panel1.SuspendLayout();
            this.editorContainer.Panel2.SuspendLayout();
            this.editorContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoomEditor_Properties)).BeginInit();
            this.RoomEditor_Properties.Panel1.SuspendLayout();
            this.RoomEditor_Properties.SuspendLayout();
            this.RoomObjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editorContainer
            // 
            this.editorContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.editorContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.editorContainer.Location = new System.Drawing.Point(0, 24);
            this.editorContainer.Name = "editorContainer";
            // 
            // editorContainer.Panel1
            // 
            this.editorContainer.Panel1.Controls.Add(this.splitContainer1);
            this.editorContainer.Panel1.Controls.Add(this.panel1);
            // 
            // editorContainer.Panel2
            // 
            this.editorContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.editorContainer.Panel2.Controls.Add(this.RoomEditor_Properties);
            this.editorContainer.Size = new System.Drawing.Size(784, 516);
            this.editorContainer.SplitterDistance = 185;
            this.editorContainer.SplitterIncrement = 5;
            this.editorContainer.SplitterWidth = 5;
            this.editorContainer.TabIndex = 2;
            this.editorContainer.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 15);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.objectBrowser);
            this.splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.objectProperties);
            this.splitContainer1.Size = new System.Drawing.Size(181, 497);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 2;
            // 
            // objectBrowser
            // 
            this.objectBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectBrowser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.objectBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectBrowser.ForeColor = System.Drawing.Color.White;
            this.objectBrowser.FormattingEnabled = true;
            this.objectBrowser.Location = new System.Drawing.Point(0, 0);
            this.objectBrowser.Name = "objectBrowser";
            this.objectBrowser.Size = new System.Drawing.Size(181, 248);
            this.objectBrowser.TabIndex = 1;
            this.objectBrowser.SelectedIndexChanged += new System.EventHandler(this.objectBrowser_SelectedIndexChanged);
            // 
            // objectProperties
            // 
            this.objectProperties.CategorySplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.CommandsBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.CommandsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.objectProperties.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectProperties.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.objectProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectProperties.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.HelpForeColor = System.Drawing.Color.White;
            this.objectProperties.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.Location = new System.Drawing.Point(0, 0);
            this.objectProperties.Name = "objectProperties";
            this.objectProperties.SelectedItemWithFocusForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.Size = new System.Drawing.Size(181, 245);
            this.objectProperties.TabIndex = 0;
            this.objectProperties.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.ViewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.ViewForeColor = System.Drawing.Color.White;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 15);
            this.panel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.textBox1.Location = new System.Drawing.Point(81, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 13);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Search Toolbox";
            this.textBox1.WordWrap = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Toolbox";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // RoomEditor_Properties
            // 
            this.RoomEditor_Properties.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RoomEditor_Properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoomEditor_Properties.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.RoomEditor_Properties.Location = new System.Drawing.Point(0, 0);
            this.RoomEditor_Properties.Name = "RoomEditor_Properties";
            // 
            // RoomEditor_Properties.Panel1
            // 
            this.RoomEditor_Properties.Panel1.Controls.Add(this.RoomObjects);
            this.RoomEditor_Properties.Panel1.Controls.Add(this.South);
            this.RoomEditor_Properties.Panel1.Controls.Add(this.West);
            this.RoomEditor_Properties.Panel1.Controls.Add(this.East);
            this.RoomEditor_Properties.Panel1.Controls.Add(this.North);
            this.RoomEditor_Properties.Size = new System.Drawing.Size(594, 516);
            this.RoomEditor_Properties.SplitterDistance = 409;
            this.RoomEditor_Properties.SplitterIncrement = 2;
            this.RoomEditor_Properties.SplitterWidth = 5;
            this.RoomEditor_Properties.TabIndex = 0;
            // 
            // North
            // 
            this.North.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.North.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.North.FlatAppearance.BorderSize = 2;
            this.North.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.North.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.North.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.North.Location = new System.Drawing.Point(141, 15);
            this.North.Name = "North";
            this.North.Size = new System.Drawing.Size(125, 95);
            this.North.TabIndex = 0;
            this.North.Text = "North";
            this.North.UseVisualStyleBackColor = false;
            // 
            // East
            // 
            this.East.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.East.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.East.FlatAppearance.BorderSize = 2;
            this.East.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.East.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.East.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.East.Location = new System.Drawing.Point(276, 124);
            this.East.Name = "East";
            this.East.Size = new System.Drawing.Size(125, 95);
            this.East.TabIndex = 1;
            this.East.Text = "East";
            this.East.UseVisualStyleBackColor = false;
            // 
            // West
            // 
            this.West.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.West.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.West.FlatAppearance.BorderSize = 2;
            this.West.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.West.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.West.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.West.Location = new System.Drawing.Point(4, 124);
            this.West.Name = "West";
            this.West.Size = new System.Drawing.Size(125, 95);
            this.West.TabIndex = 2;
            this.West.Text = "West";
            this.West.UseVisualStyleBackColor = false;
            // 
            // South
            // 
            this.South.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.South.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.South.FlatAppearance.BorderSize = 2;
            this.South.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.South.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.South.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.South.Location = new System.Drawing.Point(141, 231);
            this.South.Name = "South";
            this.South.Size = new System.Drawing.Size(125, 95);
            this.South.TabIndex = 3;
            this.South.Text = "South";
            this.South.UseVisualStyleBackColor = false;
            // 
            // RoomObjects
            // 
            this.RoomObjects.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.RoomObjects.Controls.Add(this.tabPage1);
            this.RoomObjects.Controls.Add(this.tabPage2);
            this.RoomObjects.Controls.Add(this.tabPage3);
            this.RoomObjects.Controls.Add(this.tabPage4);
            this.RoomObjects.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RoomObjects.Location = new System.Drawing.Point(0, 332);
            this.RoomObjects.Name = "RoomObjects";
            this.RoomObjects.SelectedIndex = 0;
            this.RoomObjects.Size = new System.Drawing.Size(405, 180);
            this.RoomObjects.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabPage1.ForeColor = System.Drawing.Color.White;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(397, 151);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Details";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabPage2.ForeColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(401, 151);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Game Objects";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(401, 151);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Mobs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(401, 151);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Scripts";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.editorContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer Editor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.editorContainer.Panel1.ResumeLayout(false);
            this.editorContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorContainer)).EndInit();
            this.editorContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.RoomEditor_Properties.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoomEditor_Properties)).EndInit();
            this.RoomEditor_Properties.ResumeLayout(false);
            this.RoomObjects.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer editorContainer;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox objectBrowser;
        private System.Windows.Forms.PropertyGrid objectProperties;
        private System.Windows.Forms.SplitContainer RoomEditor_Properties;
        private System.Windows.Forms.Button North;
        private System.Windows.Forms.Button South;
        private System.Windows.Forms.Button West;
        private System.Windows.Forms.Button East;
        private System.Windows.Forms.TabControl RoomObjects;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
    }
}

