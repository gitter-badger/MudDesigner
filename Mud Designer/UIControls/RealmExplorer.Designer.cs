namespace MudDesigner.UIControls
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSplash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSplash
            // 
            this.btnSplash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSplash.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSplash.FlatAppearance.BorderSize = 2;
            this.btnSplash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSplash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSplash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSplash.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplash.Location = new System.Drawing.Point(0, 0);
            this.btnSplash.Name = "btnSplash";
            this.btnSplash.Size = new System.Drawing.Size(537, 502);
            this.btnSplash.TabIndex = 0;
            this.btnSplash.Text = "No Realms Available";
            this.btnSplash.UseVisualStyleBackColor = true;
            // 
            // RealmExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.btnSplash);
            this.Name = "RealmExplorer";
            this.Size = new System.Drawing.Size(537, 502);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSplash;
    }
}
