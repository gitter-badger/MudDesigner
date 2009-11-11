namespace MudDesigner
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnLogo = new System.Windows.Forms.Button();
            this.tabEditors = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnProjectManager = new System.Windows.Forms.Button();
            this.btnCurrencyEditor = new System.Windows.Forms.Button();
            this.btnRoomDesigner = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabEditors.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnLogo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabEditors);
            this.splitContainer1.Size = new System.Drawing.Size(615, 383);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnLogo
            // 
            this.btnLogo.BackColor = System.Drawing.Color.Black;
            this.btnLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogo.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnLogo.FlatAppearance.BorderSize = 5;
            this.btnLogo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnLogo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogo.Font = new System.Drawing.Font("Kootenay", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogo.ForeColor = System.Drawing.Color.Gray;
            this.btnLogo.Location = new System.Drawing.Point(0, 0);
            this.btnLogo.Name = "btnLogo";
            this.btnLogo.Size = new System.Drawing.Size(615, 154);
            this.btnLogo.TabIndex = 0;
            this.btnLogo.Text = "MUD Designer HUB \r\nBeta 1.0\r\n";
            this.btnLogo.UseVisualStyleBackColor = false;
            // 
            // tabEditors
            // 
            this.tabEditors.Controls.Add(this.tabPage1);
            this.tabEditors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEditors.Location = new System.Drawing.Point(0, 0);
            this.tabEditors.Name = "tabEditors";
            this.tabEditors.SelectedIndex = 0;
            this.tabEditors.Size = new System.Drawing.Size(615, 225);
            this.tabEditors.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(607, 199);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Available Editors";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnProjectManager);
            this.flowLayoutPanel1.Controls.Add(this.btnCurrencyEditor);
            this.flowLayoutPanel1.Controls.Add(this.btnRoomDesigner);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(601, 193);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnProjectManager
            // 
            this.btnProjectManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProjectManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProjectManager.Location = new System.Drawing.Point(3, 3);
            this.btnProjectManager.Name = "btnProjectManager";
            this.btnProjectManager.Size = new System.Drawing.Size(147, 55);
            this.btnProjectManager.TabIndex = 0;
            this.btnProjectManager.Text = "Project Manager";
            this.btnProjectManager.UseVisualStyleBackColor = true;
            this.btnProjectManager.Click += new System.EventHandler(this.btnProjectManager_Click);
            // 
            // btnCurrencyEditor
            // 
            this.btnCurrencyEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCurrencyEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCurrencyEditor.Location = new System.Drawing.Point(156, 3);
            this.btnCurrencyEditor.Name = "btnCurrencyEditor";
            this.btnCurrencyEditor.Size = new System.Drawing.Size(147, 55);
            this.btnCurrencyEditor.TabIndex = 1;
            this.btnCurrencyEditor.Text = "Currency Editor";
            this.btnCurrencyEditor.UseVisualStyleBackColor = true;
            this.btnCurrencyEditor.Click +=new System.EventHandler(btnCurrencyEditor_Click);
            // 
            // btnRoomDesigner
            // 
            this.btnRoomDesigner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomDesigner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomDesigner.Location = new System.Drawing.Point(309, 3);
            this.btnRoomDesigner.Name = "btnRoomDesigner";
            this.btnRoomDesigner.Size = new System.Drawing.Size(147, 55);
            this.btnRoomDesigner.TabIndex = 2;
            this.btnRoomDesigner.Text = "Room Designer";
            this.btnRoomDesigner.UseVisualStyleBackColor = true;
            this.btnRoomDesigner.Click += new System.EventHandler(btnRoomDesigner_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 383);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer Beta";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabEditors.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnLogo;
        private System.Windows.Forms.TabControl tabEditors;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnProjectManager;
        private System.Windows.Forms.Button btnCurrencyEditor;
        private System.Windows.Forms.Button btnRoomDesigner;
    }
}

