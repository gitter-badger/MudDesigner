namespace MudDesigner.Editor
{
    partial class frmLoginRoom
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
            this.comRealms = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comZones = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstRooms = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comRealms);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Realms";
            // 
            // comRealms
            // 
            this.comRealms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.comRealms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comRealms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comRealms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comRealms.ForeColor = System.Drawing.Color.White;
            this.comRealms.FormattingEnabled = true;
            this.comRealms.Location = new System.Drawing.Point(3, 16);
            this.comRealms.Name = "comRealms";
            this.comRealms.Size = new System.Drawing.Size(278, 21);
            this.comRealms.Sorted = true;
            this.comRealms.TabIndex = 0;
            this.comRealms.SelectedIndexChanged += new System.EventHandler(this.comRealms_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comZones);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(0, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 49);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zones";
            // 
            // comZones
            // 
            this.comZones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.comZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comZones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comZones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comZones.ForeColor = System.Drawing.Color.White;
            this.comZones.FormattingEnabled = true;
            this.comZones.Location = new System.Drawing.Point(3, 16);
            this.comZones.Name = "comZones";
            this.comZones.Size = new System.Drawing.Size(278, 21);
            this.comZones.Sorted = true;
            this.comZones.TabIndex = 0;
            this.comZones.SelectedIndexChanged += new System.EventHandler(this.comZones_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstRooms);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(0, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 164);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rooms";
            // 
            // lstRooms
            // 
            this.lstRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lstRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRooms.ForeColor = System.Drawing.Color.White;
            this.lstRooms.FormattingEnabled = true;
            this.lstRooms.Location = new System.Drawing.Point(3, 16);
            this.lstRooms.Name = "lstRooms";
            this.lstRooms.Size = new System.Drawing.Size(278, 145);
            this.lstRooms.Sorted = true;
            this.lstRooms.TabIndex = 0;
            this.lstRooms.SelectedIndexChanged += new System.EventHandler(this.lstRooms_SelectedIndexChanged);
            // 
            // frmLoginRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoginRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mud Designer Editor : Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLoginRoom_FormClosing);
            this.Load += new System.EventHandler(this.frmLoginRoom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comRealms;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comZones;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstRooms;
    }
}