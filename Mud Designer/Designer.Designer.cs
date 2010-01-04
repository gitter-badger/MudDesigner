namespace MudDesigner
{
    partial class Designer
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
            this.ControlContainer = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.ControlContainer.Panel2.SuspendLayout();
            this.ControlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlContainer
            // 
            this.ControlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlContainer.Location = new System.Drawing.Point(0, 0);
            this.ControlContainer.Name = "ControlContainer";
            // 
            // ControlContainer.Panel2
            // 
            this.ControlContainer.Panel2.Controls.Add(this.propertyGrid1);
            this.ControlContainer.Panel2.Controls.Add(this.button1);
            this.ControlContainer.Size = new System.Drawing.Size(797, 625);
            this.ControlContainer.SplitterDistance = 538;
            this.ControlContainer.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(255, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 23);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(255, 602);
            this.propertyGrid1.TabIndex = 2;
            // 
            // Designer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(797, 625);
            this.Controls.Add(this.ControlContainer);
            this.Name = "Designer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer Replacement Test";
            this.ControlContainer.Panel2.ResumeLayout(false);
            this.ControlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.SplitContainer ControlContainer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;


    }
}