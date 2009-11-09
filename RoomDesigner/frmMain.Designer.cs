namespace RoomDesigner
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
            this.containerSidebar = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tabObjects = new System.Windows.Forms.TabControl();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.containerSidebar.Panel1.SuspendLayout();
            this.containerSidebar.Panel2.SuspendLayout();
            this.containerSidebar.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerMain
            // 
            this.containerMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.Location = new System.Drawing.Point(0, 0);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.containerSidebar);
            this.containerMain.Size = new System.Drawing.Size(784, 564);
            this.containerMain.SplitterDistance = 225;
            this.containerMain.TabIndex = 0;
            // 
            // containerSidebar
            // 
            this.containerSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.containerSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerSidebar.Location = new System.Drawing.Point(0, 0);
            this.containerSidebar.Name = "containerSidebar";
            this.containerSidebar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerSidebar.Panel1
            // 
            this.containerSidebar.Panel1.Controls.Add(this.groupBox4);
            // 
            // containerSidebar.Panel2
            // 
            this.containerSidebar.Panel2.Controls.Add(this.tabObjects);
            this.containerSidebar.Size = new System.Drawing.Size(225, 564);
            this.containerSidebar.SplitterDistance = 294;
            this.containerSidebar.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.propertyGrid1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(223, 292);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Room Settings";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 16);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(217, 273);
            this.propertyGrid1.TabIndex = 0;
            // 
            // tabObjects
            // 
            this.tabObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabObjects.Location = new System.Drawing.Point(0, 0);
            this.tabObjects.Name = "tabObjects";
            this.tabObjects.SelectedIndex = 0;
            this.tabObjects.Size = new System.Drawing.Size(223, 264);
            this.tabObjects.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 564);
            this.Controls.Add(this.containerMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer: Room Designer";
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.ResumeLayout(false);
            this.containerSidebar.Panel1.ResumeLayout(false);
            this.containerSidebar.Panel2.ResumeLayout(false);
            this.containerSidebar.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.SplitContainer containerSidebar;
        private System.Windows.Forms.TabControl tabObjects;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}

