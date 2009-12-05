namespace MudDesigner.Editors
{
    partial class ExistingRealms
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
            this.btnTransfer = new System.Windows.Forms.Button();
            this.lstRealms = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnTransfer
            // 
            this.btnTransfer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnTransfer.Location = new System.Drawing.Point(0, 286);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(242, 23);
            this.btnTransfer.TabIndex = 1;
            this.btnTransfer.Text = "Transfer Zone to Selected Realm";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // lstRealms
            // 
            this.lstRealms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRealms.FormattingEnabled = true;
            this.lstRealms.Location = new System.Drawing.Point(0, 0);
            this.lstRealms.Name = "lstRealms";
            this.lstRealms.Size = new System.Drawing.Size(242, 277);
            this.lstRealms.TabIndex = 2;
            // 
            // ExistingRealms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 309);
            this.Controls.Add(this.lstRealms);
            this.Controls.Add(this.btnTransfer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExistingRealms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Existing Realms";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTransfer;
        internal System.Windows.Forms.ListBox lstRealms;



    }
}