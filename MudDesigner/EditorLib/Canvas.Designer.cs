namespace EditorLib
{
    partial class Canvas
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
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Canvas";
            this.Size = new System.Drawing.Size(425, 362);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
