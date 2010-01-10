namespace MudDesigner.UIWidgets
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
            this.flowContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowContainer
            // 
            this.flowContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowContainer.Location = new System.Drawing.Point(0, 0);
            this.flowContainer.Name = "flowContainer";
            this.flowContainer.Padding = new System.Windows.Forms.Padding(10);
            this.flowContainer.Size = new System.Drawing.Size(537, 502);
            this.flowContainer.TabIndex = 0;
            // 
            // RealmExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.flowContainer);
            this.Name = "RealmExplorer";
            this.Size = new System.Drawing.Size(537, 502);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowContainer;

    }
}
