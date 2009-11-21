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
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Game Objects");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Game", new System.Windows.Forms.TreeNode[] {
            treeNode11});
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.containerProperties = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.page1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.containerLeftPane = new System.Windows.Forms.SplitContainer();
            this.treeProjectExplorer = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshObjectBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.containerProperties.Panel1.SuspendLayout();
            this.containerProperties.Panel2.SuspendLayout();
            this.containerProperties.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.containerLeftPane.Panel1.SuspendLayout();
            this.containerLeftPane.Panel2.SuspendLayout();
            this.containerLeftPane.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerMain
            // 
            this.containerMain.BackColor = System.Drawing.Color.Gray;
            this.containerMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.containerMain.Location = new System.Drawing.Point(0, 24);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.containerMain.Panel1.Controls.Add(this.panel1);
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.containerProperties);
            this.containerMain.Size = new System.Drawing.Size(1050, 589);
            this.containerMain.SplitterDistance = 133;
            this.containerMain.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(131, 29);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label1.Size = new System.Drawing.Size(129, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Object Browser";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.containerProperties.Panel1.Controls.Add(this.tabControl1);
            this.containerProperties.Panel1.Controls.Add(this.panel4);
            // 
            // containerProperties.Panel2
            // 
            this.containerProperties.Panel2.Controls.Add(this.containerLeftPane);
            this.containerProperties.Size = new System.Drawing.Size(913, 589);
            this.containerProperties.SplitterDistance = 667;
            this.containerProperties.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.page1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(665, 558);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // page1
            // 
            this.page1.AllowDrop = true;
            this.page1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.page1.Location = new System.Drawing.Point(4, 22);
            this.page1.Name = "page1";
            this.page1.Padding = new System.Windows.Forms.Padding(3);
            this.page1.Size = new System.Drawing.Size(657, 532);
            this.page1.TabIndex = 1;
            this.page1.Text = "Empty";
            this.page1.DragDrop += new System.Windows.Forms.DragEventHandler(this.page1_DragDrop);
            this.page1.DragEnter += new System.Windows.Forms.DragEventHandler(this.page1_DragEnter);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(665, 29);
            this.panel4.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label4.Size = new System.Drawing.Size(663, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "Visual Designer";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.containerLeftPane.Panel1.Controls.Add(this.treeProjectExplorer);
            this.containerLeftPane.Panel1.Controls.Add(this.panel2);
            // 
            // containerLeftPane.Panel2
            // 
            this.containerLeftPane.Panel2.Controls.Add(this.propertyGrid1);
            this.containerLeftPane.Panel2.Controls.Add(this.panel3);
            this.containerLeftPane.Size = new System.Drawing.Size(240, 587);
            this.containerLeftPane.SplitterDistance = 297;
            this.containerLeftPane.TabIndex = 0;
            // 
            // treeProjectExplorer
            // 
            this.treeProjectExplorer.BackColor = System.Drawing.Color.Gray;
            this.treeProjectExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProjectExplorer.Location = new System.Drawing.Point(0, 29);
            this.treeProjectExplorer.Name = "treeProjectExplorer";
            treeNode11.Name = "nodeGameObjects";
            treeNode11.Text = "Game Objects";
            treeNode12.Name = "nodeGame";
            treeNode12.Text = "Game";
            this.treeProjectExplorer.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12});
            this.treeProjectExplorer.Size = new System.Drawing.Size(240, 268);
            this.treeProjectExplorer.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 29);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label2.Size = new System.Drawing.Size(238, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Project Explorer";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 29);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(240, 257);
            this.propertyGrid1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 29);
            this.panel3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label3.Size = new System.Drawing.Size(238, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "Object Properties";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectToolStripMenuItem});
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
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewObjectToolStripMenuItem,
            this.toolStripMenuItem1,
            this.refreshObjectBrowserToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.projectToolStripMenuItem.Text = "Objects";
            // 
            // createNewObjectToolStripMenuItem
            // 
            this.createNewObjectToolStripMenuItem.Name = "createNewObjectToolStripMenuItem";
            this.createNewObjectToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.createNewObjectToolStripMenuItem.Text = "Managed Objects";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(193, 6);
            // 
            // refreshObjectBrowserToolStripMenuItem
            // 
            this.refreshObjectBrowserToolStripMenuItem.Name = "refreshObjectBrowserToolStripMenuItem";
            this.refreshObjectBrowserToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.refreshObjectBrowserToolStripMenuItem.Text = "Refresh Object Browser";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 29);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(131, 558);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 613);
            this.Controls.Add(this.containerMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visual Mud Designer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            this.containerMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.containerProperties.Panel1.ResumeLayout(false);
            this.containerProperties.Panel2.ResumeLayout(false);
            this.containerProperties.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.containerLeftPane.Panel1.ResumeLayout(false);
            this.containerLeftPane.Panel2.ResumeLayout(false);
            this.containerLeftPane.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.SplitContainer containerProperties;
        private System.Windows.Forms.SplitContainer containerLeftPane;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage page1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TreeView treeProjectExplorer;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem refreshObjectBrowserToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

