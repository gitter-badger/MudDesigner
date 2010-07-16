namespace MudDesigner.Engine.UITypeEditors
{
    partial class UIRealmControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.lstRealmMembers = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstAvailableZones = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(-1, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Realm Members";
            // 
            // lstRealmMembers
            // 
            this.lstRealmMembers.ContextMenuStrip = this.contextMenuStrip1;
            this.lstRealmMembers.FormattingEnabled = true;
            this.lstRealmMembers.Location = new System.Drawing.Point(2, 21);
            this.lstRealmMembers.Name = "lstRealmMembers";
            this.lstRealmMembers.Size = new System.Drawing.Size(160, 238);
            this.lstRealmMembers.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewPropertiesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 26);
            // 
            // viewPropertiesToolStripMenuItem
            // 
            this.viewPropertiesToolStripMenuItem.Name = "viewPropertiesToolStripMenuItem";
            this.viewPropertiesToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.viewPropertiesToolStripMenuItem.Text = "View Properties";
            // 
            // lstAvailableZones
            // 
            this.lstAvailableZones.ContextMenuStrip = this.contextMenuStrip1;
            this.lstAvailableZones.FormattingEnabled = true;
            this.lstAvailableZones.Location = new System.Drawing.Point(237, 21);
            this.lstAvailableZones.Name = "lstAvailableZones";
            this.lstAvailableZones.Size = new System.Drawing.Size(160, 238);
            this.lstAvailableZones.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(234, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Available Zones";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(165, 31);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(66, 35);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "<<<";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(165, 72);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(66, 35);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = ">>>";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // UIRealmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(403, 268);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstAvailableZones);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstRealmMembers);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIRealmControl";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zone Collection Editor";
            this.Load += new System.EventHandler(this.UIRealmControl_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UIRealmControl_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstRealmMembers;
        private System.Windows.Forms.ListBox lstAvailableZones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewPropertiesToolStripMenuItem;
    }
}