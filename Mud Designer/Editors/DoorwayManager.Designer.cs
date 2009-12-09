namespace MudDesigner.Editors
{
    partial class DoorwayManager
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
            this.lstRealms = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstZones = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstRooms = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.propertyRoom = new System.Windows.Forms.PropertyGrid();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSelectDoorway = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEast = new System.Windows.Forms.Button();
            this.btnWest = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnSouth = new System.Windows.Forms.Button();
            this.btnNorth = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstRealms);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Realms";
            // 
            // lstRealms
            // 
            this.lstRealms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRealms.FormattingEnabled = true;
            this.lstRealms.Location = new System.Drawing.Point(3, 16);
            this.lstRealms.Name = "lstRealms";
            this.lstRealms.Size = new System.Drawing.Size(164, 134);
            this.lstRealms.TabIndex = 0;
            this.lstRealms.SelectedIndexChanged += new System.EventHandler(this.lstRealms_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstZones);
            this.groupBox2.Location = new System.Drawing.Point(0, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 286);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zones contained within Realm";
            // 
            // lstZones
            // 
            this.lstZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstZones.FormattingEnabled = true;
            this.lstZones.Location = new System.Drawing.Point(3, 16);
            this.lstZones.Name = "lstZones";
            this.lstZones.Size = new System.Drawing.Size(164, 264);
            this.lstZones.TabIndex = 0;
            this.lstZones.SelectedIndexChanged += new System.EventHandler(this.lstZones_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstRooms);
            this.groupBox3.Location = new System.Drawing.Point(173, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 221);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Available Rooms";
            // 
            // lstRooms
            // 
            this.lstRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRooms.FormattingEnabled = true;
            this.lstRooms.Location = new System.Drawing.Point(3, 16);
            this.lstRooms.Name = "lstRooms";
            this.lstRooms.Size = new System.Drawing.Size(176, 199);
            this.lstRooms.TabIndex = 0;
            this.lstRooms.SelectedIndexChanged += new System.EventHandler(this.lstRooms_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.propertyRoom);
            this.groupBox4.Location = new System.Drawing.Point(176, 221);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(182, 226);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Room Property Preview";
            // 
            // propertyRoom
            // 
            this.propertyRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyRoom.Enabled = false;
            this.propertyRoom.HelpVisible = false;
            this.propertyRoom.Location = new System.Drawing.Point(3, 16);
            this.propertyRoom.Name = "propertyRoom";
            this.propertyRoom.Size = new System.Drawing.Size(176, 207);
            this.propertyRoom.TabIndex = 0;
            this.propertyRoom.ToolbarVisible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnSelectDoorway);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.btnEast);
            this.groupBox5.Controls.Add(this.btnWest);
            this.groupBox5.Controls.Add(this.btnDown);
            this.groupBox5.Controls.Add(this.btnSouth);
            this.groupBox5.Controls.Add(this.btnNorth);
            this.groupBox5.Controls.Add(this.btnUp);
            this.groupBox5.Location = new System.Drawing.Point(358, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(383, 447);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Doorway Designer";
            // 
            // btnSelectDoorway
            // 
            this.btnSelectDoorway.Location = new System.Drawing.Point(256, 421);
            this.btnSelectDoorway.Name = "btnSelectDoorway";
            this.btnSelectDoorway.Size = new System.Drawing.Size(118, 23);
            this.btnSelectDoorway.TabIndex = 15;
            this.btnSelectDoorway.Text = "Select Doorway";
            this.btnSelectDoorway.UseVisualStyleBackColor = true;
            this.btnSelectDoorway.Click += new System.EventHandler(this.btnSelectDoorway_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 428);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Selected Doorway:";
            // 
            // btnEast
            // 
            this.btnEast.BackColor = System.Drawing.Color.Gray;
            this.btnEast.FlatAppearance.BorderSize = 2;
            this.btnEast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEast.Location = new System.Drawing.Point(256, 161);
            this.btnEast.Name = "btnEast";
            this.btnEast.Size = new System.Drawing.Size(118, 93);
            this.btnEast.TabIndex = 13;
            this.btnEast.Text = "East";
            this.btnEast.UseVisualStyleBackColor = false;
            this.btnEast.Click += new System.EventHandler(this.button_Click);
            // 
            // btnWest
            // 
            this.btnWest.BackColor = System.Drawing.Color.Gray;
            this.btnWest.FlatAppearance.BorderSize = 2;
            this.btnWest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnWest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWest.Location = new System.Drawing.Point(6, 161);
            this.btnWest.Name = "btnWest";
            this.btnWest.Size = new System.Drawing.Size(118, 93);
            this.btnWest.TabIndex = 12;
            this.btnWest.Text = "West";
            this.btnWest.UseVisualStyleBackColor = false;
            this.btnWest.Click += new System.EventHandler(this.button_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Gray;
            this.btnDown.FlatAppearance.BorderSize = 2;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Location = new System.Drawing.Point(132, 353);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(118, 51);
            this.btnDown.TabIndex = 11;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.button_Click);
            // 
            // btnSouth
            // 
            this.btnSouth.BackColor = System.Drawing.Color.Gray;
            this.btnSouth.FlatAppearance.BorderSize = 2;
            this.btnSouth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSouth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSouth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSouth.Location = new System.Drawing.Point(132, 261);
            this.btnSouth.Name = "btnSouth";
            this.btnSouth.Size = new System.Drawing.Size(118, 86);
            this.btnSouth.TabIndex = 10;
            this.btnSouth.Text = "South";
            this.btnSouth.UseVisualStyleBackColor = false;
            this.btnSouth.Click += new System.EventHandler(this.button_Click);
            // 
            // btnNorth
            // 
            this.btnNorth.BackColor = System.Drawing.Color.Gray;
            this.btnNorth.FlatAppearance.BorderSize = 2;
            this.btnNorth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNorth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNorth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNorth.Location = new System.Drawing.Point(132, 69);
            this.btnNorth.Name = "btnNorth";
            this.btnNorth.Size = new System.Drawing.Size(118, 86);
            this.btnNorth.TabIndex = 9;
            this.btnNorth.Text = "North";
            this.btnNorth.UseVisualStyleBackColor = false;
            this.btnNorth.Click += new System.EventHandler(this.button_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Gray;
            this.btnUp.FlatAppearance.BorderSize = 2;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(132, 12);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(118, 51);
            this.btnUp.TabIndex = 8;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.button_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 426);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(744, 23);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // DoorwayManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 449);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DoorwayManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doorway Manager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstRealms;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstZones;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstRooms;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PropertyGrid propertyRoom;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnEast;
        private System.Windows.Forms.Button btnWest;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnSouth;
        private System.Windows.Forms.Button btnNorth;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectDoorway;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}