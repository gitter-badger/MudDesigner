namespace VisualDesigner
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
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.ObjectList = new VisualComponents.VisualContainer();
            this.containerProperties = new System.Windows.Forms.SplitContainer();
            this.VisualEditor = new VisualComponents.VisualContainer();
            this.tabDesigner = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.containerLeftPane = new System.Windows.Forms.SplitContainer();
            this.ObjectExplorer = new VisualComponents.VisualContainer();
            this.tabExplorer = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.Properties = new VisualComponents.VisualContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.ObjectList.SuspendLayout();
            this.containerProperties.Panel1.SuspendLayout();
            this.containerProperties.Panel2.SuspendLayout();
            this.containerProperties.SuspendLayout();
            this.VisualEditor.SuspendLayout();
            this.tabDesigner.SuspendLayout();
            this.containerLeftPane.Panel1.SuspendLayout();
            this.containerLeftPane.Panel2.SuspendLayout();
            this.containerLeftPane.SuspendLayout();
            this.ObjectExplorer.SuspendLayout();
            this.tabExplorer.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Properties.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerMain
            // 
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.Location = new System.Drawing.Point(0, 24);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.ObjectList);
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.containerProperties);
            this.containerMain.Size = new System.Drawing.Size(1050, 589);
            this.containerMain.SplitterDistance = 112;
            this.containerMain.TabIndex = 0;
            // 
            // ObjectList
            // 
            this.ObjectList.BackColor = System.Drawing.Color.Gray;
            this.ObjectList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ObjectList.Controls.Add(this.flowLayoutPanel1);
            this.ObjectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectList.IsClosable = false;
            this.ObjectList.Location = new System.Drawing.Point(0, 0);
            this.ObjectList.Name = "ObjectList";
            this.ObjectList.Size = new System.Drawing.Size(112, 589);
            this.ObjectList.TabIndex = 0;
            this.ObjectList.Title = "Object Toolbox";
            // 
            // containerProperties
            // 
            this.containerProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.containerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerProperties.Location = new System.Drawing.Point(0, 0);
            this.containerProperties.Name = "containerProperties";
            // 
            // containerProperties.Panel1
            // 
            this.containerProperties.Panel1.Controls.Add(this.VisualEditor);
            // 
            // containerProperties.Panel2
            // 
            this.containerProperties.Panel2.Controls.Add(this.containerLeftPane);
            this.containerProperties.Size = new System.Drawing.Size(934, 589);
            this.containerProperties.SplitterDistance = 683;
            this.containerProperties.TabIndex = 0;
            // 
            // VisualEditor
            // 
            this.VisualEditor.BackColor = System.Drawing.Color.Gray;
            this.VisualEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VisualEditor.Controls.Add(this.tabDesigner);
            this.VisualEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VisualEditor.IsClosable = false;
            this.VisualEditor.Location = new System.Drawing.Point(0, 0);
            this.VisualEditor.Name = "VisualEditor";
            this.VisualEditor.Size = new System.Drawing.Size(681, 587);
            this.VisualEditor.TabIndex = 1;
            this.VisualEditor.Title = "Visual Editor";
            // 
            // tabDesigner
            // 
            this.tabDesigner.Controls.Add(this.tabPage2);
            this.tabDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDesigner.Location = new System.Drawing.Point(0, 0);
            this.tabDesigner.Name = "tabDesigner";
            this.tabDesigner.SelectedIndex = 0;
            this.tabDesigner.Size = new System.Drawing.Size(679, 585);
            this.tabDesigner.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Silver;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(671, 559);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "tabPage2";
            // 
            // containerLeftPane
            // 
            this.containerLeftPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerLeftPane.Location = new System.Drawing.Point(0, 0);
            this.containerLeftPane.Name = "containerLeftPane";
            this.containerLeftPane.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerLeftPane.Panel1
            // 
            this.containerLeftPane.Panel1.Controls.Add(this.ObjectExplorer);
            // 
            // containerLeftPane.Panel2
            // 
            this.containerLeftPane.Panel2.Controls.Add(this.Properties);
            this.containerLeftPane.Size = new System.Drawing.Size(245, 587);
            this.containerLeftPane.SplitterDistance = 297;
            this.containerLeftPane.TabIndex = 0;
            // 
            // ObjectExplorer
            // 
            this.ObjectExplorer.BackColor = System.Drawing.Color.Gray;
            this.ObjectExplorer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ObjectExplorer.Controls.Add(this.tabExplorer);
            this.ObjectExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectExplorer.IsClosable = false;
            this.ObjectExplorer.Location = new System.Drawing.Point(0, 0);
            this.ObjectExplorer.Name = "ObjectExplorer";
            this.ObjectExplorer.Size = new System.Drawing.Size(245, 297);
            this.ObjectExplorer.TabIndex = 0;
            this.ObjectExplorer.Title = "Project Explorer";
            // 
            // tabExplorer
            // 
            this.tabExplorer.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabExplorer.Controls.Add(this.tabPage1);
            this.tabExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabExplorer.Location = new System.Drawing.Point(0, 0);
            this.tabExplorer.Name = "tabExplorer";
            this.tabExplorer.SelectedIndex = 0;
            this.tabExplorer.Size = new System.Drawing.Size(243, 295);
            this.tabExplorer.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Silver;
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(235, 269);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Project Objects";
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.Silver;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(229, 263);
            this.treeView1.TabIndex = 3;
            // 
            // Properties
            // 
            this.Properties.BackColor = System.Drawing.Color.Gray;
            this.Properties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Properties.Controls.Add(this.propertyGrid1);
            this.Properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Properties.IsClosable = false;
            this.Properties.Location = new System.Drawing.Point(0, 0);
            this.Properties.Name = "Properties";
            this.Properties.Size = new System.Drawing.Size(245, 286);
            this.Properties.TabIndex = 1;
            this.Properties.Title = "Properties";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(243, 284);
            this.propertyGrid1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1050, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.BackColor = System.Drawing.Color.Silver;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(110, 562);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 613);
            this.Controls.Add(this.containerMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visual Mud Designer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            this.containerMain.ResumeLayout(false);
            this.ObjectList.ResumeLayout(false);
            this.containerProperties.Panel1.ResumeLayout(false);
            this.containerProperties.Panel2.ResumeLayout(false);
            this.containerProperties.ResumeLayout(false);
            this.VisualEditor.ResumeLayout(false);
            this.tabDesigner.ResumeLayout(false);
            this.containerLeftPane.Panel1.ResumeLayout(false);
            this.containerLeftPane.Panel2.ResumeLayout(false);
            this.containerLeftPane.ResumeLayout(false);
            this.ObjectExplorer.ResumeLayout(false);
            this.tabExplorer.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.Properties.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.SplitContainer containerProperties;
        private System.Windows.Forms.SplitContainer containerLeftPane;
        private VisualComponents.VisualContainer ObjectExplorer;
        private VisualComponents.VisualContainer Properties;
        private VisualComponents.VisualContainer VisualEditor;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private VisualComponents.VisualContainer ObjectList;
        private System.Windows.Forms.TabControl tabExplorer;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabDesigner;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

