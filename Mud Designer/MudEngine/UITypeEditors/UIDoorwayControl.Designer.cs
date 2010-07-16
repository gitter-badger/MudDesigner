namespace MudDesigner.Engine.UITypeEditors
{
    partial class UIDoorwayControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstInstalledDoors = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddDoorway = new System.Windows.Forms.Button();
            this.btnRemoveDoorway = new System.Windows.Forms.Button();
            this.propertyDoorway = new System.Windows.Forms.PropertyGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstInstalledDoors);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Gray;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 220);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Installed Doorways";
            // 
            // lstInstalledDoors
            // 
            this.lstInstalledDoors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInstalledDoors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstInstalledDoors.FormattingEnabled = true;
            this.lstInstalledDoors.Location = new System.Drawing.Point(3, 16);
            this.lstInstalledDoors.Name = "lstInstalledDoors";
            this.lstInstalledDoors.Size = new System.Drawing.Size(250, 199);
            this.lstInstalledDoors.TabIndex = 0;
            this.lstInstalledDoors.SelectedIndexChanged += new System.EventHandler(this.lstInstalledDoors_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.propertyDoorway);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Gray;
            this.groupBox2.Location = new System.Drawing.Point(306, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 249);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Installed Doorways";
            // 
            // btnAddDoorway
            // 
            this.btnAddDoorway.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDoorway.Location = new System.Drawing.Point(15, 238);
            this.btnAddDoorway.Name = "btnAddDoorway";
            this.btnAddDoorway.Size = new System.Drawing.Size(106, 23);
            this.btnAddDoorway.TabIndex = 2;
            this.btnAddDoorway.Text = "Add Doorway";
            this.btnAddDoorway.UseVisualStyleBackColor = true;
            this.btnAddDoorway.Click += new System.EventHandler(this.btnAddDoorway_Click);
            // 
            // btnRemoveDoorway
            // 
            this.btnRemoveDoorway.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveDoorway.Location = new System.Drawing.Point(159, 238);
            this.btnRemoveDoorway.Name = "btnRemoveDoorway";
            this.btnRemoveDoorway.Size = new System.Drawing.Size(106, 23);
            this.btnRemoveDoorway.TabIndex = 3;
            this.btnRemoveDoorway.Text = "Remove Doorway";
            this.btnRemoveDoorway.UseVisualStyleBackColor = true;
            // 
            // propertyDoorway
            // 
            this.propertyDoorway.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyDoorway.Location = new System.Drawing.Point(3, 16);
            this.propertyDoorway.Name = "propertyDoorway";
            this.propertyDoorway.Size = new System.Drawing.Size(264, 230);
            this.propertyDoorway.TabIndex = 0;
            this.propertyDoorway.ToolbarVisible = false;
            this.propertyDoorway.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyDoorway_PropertyValueChanged);
            // 
            // UIDoorwayManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(591, 270);
            this.Controls.Add(this.btnRemoveDoorway);
            this.Controls.Add(this.btnAddDoorway);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIDoorwayManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIDoorwayManager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstInstalledDoors;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddDoorway;
        private System.Windows.Forms.Button btnRemoveDoorway;
        private System.Windows.Forms.PropertyGrid propertyDoorway;
    }
}